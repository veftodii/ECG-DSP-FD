#include <avr/io.h>
#include <avr/interrupt.h>

// General transmit package format
// [START | PACKID | CMDID | DATA0 | ... | DATA16 | CRC | END]
// General receive package format
// [START | CMDID | DATA0 | DATA1 | DATA2 | CRC | END]

// Arrays indexes
#define pk_Length		22 // bytes
#define cmd_Length		7 // bytes
#define pk_FirstDataID	3
#define pk_LastDataID	pk_Length-3
#define cmd_FirstDataID	2
#define cmd_LastDataID	cmd_Length-3
#define adc_Length		pk_LastDataID-pk_FirstDataID+1-1 // bytes, first byte DATA0 is channel mask
#define pk_pkID			1 // Index of Tx PACKID
#define pk_cmdID		2 // Index of Tx CMDID
#define cmd_cmdID		1 // Index of Rx CMDID
#define pk_CRC			pk_Length-2
#define cmd_CRC			cmd_Length-2

// Packet values
#define STARTBYTE		0x02
#define ENDBYTE			0x03

// CMDID values
#define ADDCHANNEL		0x0A
#define REMOVECHANNEL	0x0D
#define CMDCOMPLETE		0x10
#define STARTCONVERSION	0x02
#define STOPCONVERSION	0x03
#define ERROR			0x07

// Errors types (DATA0 values)
#define CRC_ERROR			0x04
#define MAXCHANNELS_ERROR	0x06
#define UNKNOWNCMD_ERROR	0x18

// Add / Remove Channel values
#define ECG				0x1A	// Channels
#define EMG				0x1B
#define ERG				0x1C
#define HPF50mHz		0x2A	// High Pass Filters
#define LPF40Hz			0x3A	// Low Pass Filters
#define LPF100Hz		0x3B
#define LPF10kHz		0x3C


// General PORTB mask format	[ CHANNEL | HPF | LPF ]  (MSB -> LSB)

/* WARNING:	Masks depends only of wire's hardware configuration */

// Channel masks for PORTB
#define ECG_mask		0x00	// 1F4052_Y0 ch = 0b 00######;  # = 0
#define EMG_mask		0x80	// 1F4052_Y2 ch = 0b 10######;  # = 0
#define ERG_mask		0xC0	// 1F4052_Y3 ch = 0b 11######;  # = 0

// High Pass Filters masks for PORTB
// 2F4052 and 3F4052 share the same selection code on S1S0 inputs
#define HPF50mHz_mask	0x00	// 2&3F4052_X0 ch = 0b ##0000##;  # = 0
//#define HPF100mHz	0x14	// 2&3F4052_X1 ch = 0b ##0101##;  # = 0

// Low Pass Filters masks for PORTB
#define LPF40Hz_mask	0x03	// 4F4052_X3 ch = 0b ######11;  # = 0
#define LPF100Hz_mask	0x01	// 4F4052_X1 ch = 0b ######01;  # = 0
#define LPF10kHz_mask	0x02	// 4F4052_X2 ch = 0b ######10;  # = 0

// Other constants
#define DefaultChannel	(ECG_mask | HPF50mHz_mask | LPF100Hz_mask)
#define maxChannels		8		// ATmega16 has max 8 ACD channels
#define ADC0			LPF100Hz_mask
#define ADC1			LPF10kHz_mask
#define ADC2			LPF40Hz_mask
#define ADC3			0x00	// Replace on need with it conected LPF filter

// Macros
//#define Low(x)  ((x) & 0xFF)
//#define High(x) (((x)>>8) & 0xFF) 

// Global variables
unsigned char Packet[pk_Length]; // For Tx
unsigned char Command[cmd_Length]; // For Rx
unsigned char ADCStack[adc_Length];
unsigned char Channels[maxChannels]; 
unsigned char i;
unsigned char crc = 0;
unsigned char pkCounter = 0;
unsigned char adcCounter = 0;
unsigned char sendCounter = 0;
unsigned char currentChannelID = 0;
unsigned char totalChannels = 0;
unsigned char SENDCOMPLETEFLAG = 1;
unsigned char ERRORFLAG = 0;

void SelectADCChannel()
{
	// Last 2 bits (LSB) represents ADC channel selector
	switch(Channels[currentChannelID]&0x03)
	{
		case ADC0:	ADMUX &= 0xE0;	break;
		case ADC1:	ADMUX &= 0xE1;	break;
		case ADC2:	ADMUX &= 0xE2;	break;
		case ADC3:	ADMUX &= 0xE3;	//break;
	}
	PORTB = Channels[currentChannelID];
}

void init_ADC()
{
	// Voltage reference: AVCC with external capacitor at AREF pin
	// Set 8-bit ADC
	ADMUX |= (1<<REFS0) | (1<<ADLAR);
	SelectADCChannel();
	// Enable ADC free-running mode, interrupt, prescaler = 128;
	// For Fosc = 11.0592 MHz => Fadc = 86.4 khz
	ADCSRA |= (1<<ADEN) | (1<<ADATE) | (1<<ADIE) | (1<<ADPS2) | (1<<ADPS1) | (1<<ADPS0);
	adcCounter = 0;
}

void init_USART(void)
{	// Format 8 - N - 1
	UCSRB |= (1<<RXCIE) | (1<<RXEN) | (1<<TXEN);
	UCSRC &= ~(1<<URSEL);
	UBRRH = 0x00; // Baud = 115200
	UBRRL = 0x05;
	//UBRRL = 0x0B; //for baud = 57600 in test mode
	pkCounter = 0;
}

void FormatPacket(unsigned char cmdID)
{
	Packet[0] = STARTBYTE;
	Packet[pk_pkID] = ++pkCounter;
	Packet[pk_cmdID] = cmdID;
	Packet[pk_Length-1] = ENDBYTE;
	crc = 0;
	for (i=pk_pkID;i<=pk_LastDataID; i++) crc += Packet[i];
	Packet[pk_CRC] = crc;
}

void SendPacket(void)
{
	if (sendCounter<pk_Length)
	{
		SENDCOMPLETEFLAG = 0;
		UDR = Packet[sendCounter];
		UCSRB |= (1<<UDRIE);
		sendCounter++;
	}
	else
	{
		UCSRB &= ~(1<<UDRIE);
		sendCounter = 0;
		SENDCOMPLETEFLAG = 1;
	}
}

ISR(ADC_vect)
{
	for (i=1; i<adc_Length; i++) ADCStack[i-1] = ADCStack[i];
	ADCStack[adc_Length-1] = ADCH;
	adcCounter++;
	// ADC stack was fulfilled and there are channels to record
	if ((adcCounter == adc_Length) && SENDCOMPLETEFLAG && (totalChannels>0))
	{
	 	// Modify packet if only was sended
		Packet[pk_FirstDataID] = Channels[currentChannelID]; // Send back the mask
		for (i=pk_FirstDataID+1; i<=pk_LastDataID; i++) Packet[i] = ADCStack[i-pk_FirstDataID+1];
		// First 2 bits (MSB) represents channel type/name selector
		switch(Channels[currentChannelID]&0xC0)
		{
			case ECG_mask:	FormatPacket(ECG);	break;
			case EMG_mask:	FormatPacket(EMG);	break;
			case ERG_mask:	FormatPacket(ERG);	//break;
		}
		sendCounter = 0;
		SendPacket();
		adcCounter = 0;
		currentChannelID++;
		if (currentChannelID>=totalChannels) currentChannelID = 0;
		SelectADCChannel();
	}
}

void SendError(unsigned char error)
{
	// Don't wait to send current packet, reset it
	for (i=pk_FirstDataID; i<=pk_LastDataID; i++) Packet[i] = 0;
	switch(error)
	{
		case CRC_ERROR:
		case UNKNOWNCMD_ERROR:
		case MAXCHANNELS_ERROR:	Packet[pk_FirstDataID] = error;
	}	
	FormatPacket(ERROR);
	sendCounter = 0;
	SendPacket();
	ERRORFLAG = 0;
}

void Response(void)
{
	// Don't wait to send current packet, reset it
	for (i=pk_FirstDataID; i<=pk_LastDataID; i++) Packet[i] = 0;
	FormatPacket(CMDCOMPLETE);
	sendCounter = 0;
	SendPacket();
}

unsigned char GetMask()
{
	ERRORFLAG = 0;
	unsigned char mask = 0;
	switch(Command[cmd_FirstDataID])
	{
		case ECG:	mask |= ECG_mask;	break;
		case EMG:	mask |= EMG_mask;	break;
		case ERG:	mask |= ERG_mask;	break;
		default:	ERRORFLAG = 1;		return 0;
	}
	switch(Command[cmd_FirstDataID+1])
	{
		case HPF50mHz:	mask |= HPF50mHz_mask;	break;
		default:		ERRORFLAG = 1;			return 0;
	}
	switch(Command[cmd_FirstDataID+2])
	{
		case LPF40Hz:	mask |= LPF40Hz_mask;	break;
		case LPF100Hz:	mask |= LPF100Hz_mask;	break;
		case LPF10kHz:	mask |= LPF10kHz_mask;	break;
		default:		ERRORFLAG = 1;			return 0;
	}
	return mask;
}

unsigned char AddChannel()
{
	unsigned char mask;
	mask = GetMask();
	if (ERRORFLAG) return UNKNOWNCMD_ERROR;
	for (i=0; i<totalChannels; i++)
		if (Channels[i] == mask) break;
	if (i<totalChannels) return CMDCOMPLETE;
	if (totalChannels == maxChannels) return MAXCHANNELS_ERROR;
	// Channel not found. Add new one
	Channels[totalChannels] = mask;
	if (currentChannelID==totalChannels)
	{
		// Clear ADC stack and select new active channel
		for (i=0; i<adc_Length; i++) ADCStack[i] = 0;
		adcCounter = 0;
		SelectADCChannel();
	}
	totalChannels++;
	return CMDCOMPLETE;
}

unsigned char RemoveChannel()
{
	unsigned char mask, k;
	mask = GetMask();
	if (ERRORFLAG) return UNKNOWNCMD_ERROR;
	for (i=0; i<totalChannels; i++)
		if (Channels[i] == mask) break;
	if (i==totalChannels) return CMDCOMPLETE;
	// Channel found. Remove it
	for (k=i+1; k<totalChannels; k++) Channels[k-1] = Channels[k];
	Channels[totalChannels-1] = 0;
	totalChannels--;
	if (currentChannelID==i)
	{
		// Clear ADC stack and select new active channel
		for (i=0; i<adc_Length; i++) ADCStack[i] = 0;
		adcCounter = 0;
		if (currentChannelID==totalChannels) currentChannelID = 0;
		SelectADCChannel();
	}
	return CMDCOMPLETE;
}

void TranslateCommand(void)
{
    crc = 0;
	for (i=cmd_cmdID; i<=cmd_LastDataID; i++) crc += Command[i];
	if (crc != Command[cmd_CRC]) SendError(CRC_ERROR);
	else
	{
		switch(Command[cmd_cmdID])
		{
			case STARTCONVERSION:	ADCSRA |= (1<<ADSC);
									for (i=0; i<adc_Length; i++) ADCStack[i] = 0;
									adcCounter = 0;
									currentChannelID = 0;
									SelectADCChannel();
									Response();
									break;
			case STOPCONVERSION:	ADCSRA &= ~(1<<ADSC);
									for (i=0; i<adc_Length; i++) ADCStack[i] = 0;
									adcCounter = 0;
									currentChannelID = 0;
									Response();
									break;
			case ADDCHANNEL:		crc=AddChannel();
									if (crc==CMDCOMPLETE) Response();
									else SendError(crc);
									break;
			case REMOVECHANNEL:		crc=RemoveChannel();
									if (crc==CMDCOMPLETE) Response();
									else SendError(crc);
									break;
			case ERROR:				sendCounter = 0;
									SENDCOMPLETEFLAG = 0;
									SendPacket();
									break;
			default:				SendError(UNKNOWNCMD_ERROR);
		}
	}
}

ISR(USART_RXC_vect)
{
	for (i=1; i<cmd_Length; i++) Command[i-1] = Command[i];
	Command[cmd_Length-1] = UDR;
	if (Command[cmd_Length-1] == ENDBYTE)
		if (Command[0] == STARTBYTE) TranslateCommand();
}

ISR(USART_UDRE_vect)
{
	SendPacket();
}

void init_DataArrays(void)
{
	for (i=0; i<pk_Length; i++) Packet[i] = 0;
	for (i=0; i<cmd_Length; i++) Command[i] = 0;
	for (i=0; i<adc_Length; i++) ADCStack[i] = 0;
	for (i=0; i<maxChannels; i++) Channels[i] = 0;
	SENDCOMPLETEFLAG = 1;
	ERRORFLAG = 0;
	totalChannels = 0;
	currentChannelID = 0;
	DDRB = 0xFF; // PORTB for output
	PORTB = Channels[0];
}

int main(void)
{
	init_DataArrays();
	init_ADC();
	init_USART();
	sei();
	while(1)
	{
	}
	return 0;
}
