using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Windows.Forms;

namespace GraphDisplayLib
{
    namespace Processing
    {
        public class ECG_Processing
        {
            public double[] Detect_RPeaks(double[] ecg, double samplingrate, int nFilter = 1) //samplerate in Hz
            {
                Complex[] fresult = new Complex[ecg.Length];
                FastFourierTransform ft = new FastFourierTransform();
                
                // Remove lower frequencies
                bool a = ft.FFT(ecg, fresult, (uint)ecg.Length);
                if (!a)
                {
                    MessageBox.Show("Can't execute FFT !", "FFT function Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    double[] err = {-1.0};
                    return err;
                }
                int v = (int)Math.Round(((double)fresult.Length * 5) / samplingrate);
                for (int i = 0; i < v; i++) fresult[i] = 0.0;
                for (int i = fresult.Length - v; i < fresult.Length; i++) fresult[i] = 0.0;
                double[] corrected = new double[fresult.Length];
                Complex[] corrected_C = new Complex[fresult.Length];
                a = ft.IFFT(fresult, corrected_C, (uint)fresult.Length);
                if (!a)
                {
                    MessageBox.Show("Can't execute IFFT !", "IFFT function Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    double[] err = { -1.0 };
                    return err;
                }
                for (int i = 0; i < fresult.Length; i++) corrected[i] = corrected_C[i].Real;
                
                // Filter - first pass
                double WinSize = Math.Floor(samplingrate * 571.0 / 1000.0);
                if (Math.IEEERemainder(WinSize, 2.0) == 0.0) WinSize += 1;
                double[] filtered1 = ECGWinMax(corrected, WinSize);
                // Scale ecg
                double[] peaks1 = new double[filtered1.Length];
                double v1 = filtered1.Max() / 7.0;
                for (int i = 0; i < peaks1.Length; i++) peaks1[i] = filtered1[i] / v1;
                //Filter by threshold filter
                for (int data=0; data<peaks1.Length; data++)
                    if (peaks1[data] < 4) peaks1[data] = 0;
                    else peaks1[data] = 1;
                int[] positions = find(peaks1);
                int distance = positions[1] - positions[0];
                for (int data=0; data<positions.Length-1; data++)
                    if (positions[data+1]-positions[data]<distance) distance=positions[data+1]-positions[data];
                // Optimize filter window size
                double QRdistance = Math.Floor(0.04 * samplingrate);
                if (Math.IEEERemainder(QRdistance, 2.0) == 0.0) QRdistance += 1;
                WinSize = 2.0 * (double)distance - QRdistance;
                // Filter - second pass
                double[] filtered2 = ECGWinMax(corrected, WinSize);
                double[] peaks2 = new double[filtered2.Length];
                for (int i = 0; i < peaks2.Length; i++) peaks2[i] = filtered2[i];
                for (int data=0; data<peaks2.Length; data++)
                    if (peaks2[data] < 4) peaks2[data] = 0;
                    else peaks2[data] = 1;
                if (nFilter == 1) return peaks1;
                else if (nFilter == 2) return peaks2;
                else
                {
                    MessageBox.Show("nFilter must be \"1\" or \"2\" !", "R-Peaks function Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    double[] err = { -1.0 };
                    return err;
                }
            }

            private double[] ECGWinMax(double[] Original, double WinSize)
            {
                double WinHalfSize = Math.Floor(WinSize / 2.0);
                double WinHalfSizePlus = WinHalfSize + 1.0;
                double WinSizeSpec = WinSize - 1.0;
                int FrontIterator = 0;
                double WinPos = WinHalfSize;
                double WinMaxPos = WinHalfSize;
                double WinMax = Original[0];
                int OutputIterator = -1;
                double[] Filtered = new double[Original.Length];
                for (int LengthCounter = 0; LengthCounter < WinHalfSize; LengthCounter++)
                {
                    if (Original[FrontIterator + 1] > WinMax)
                    {
                        WinMax = Original[FrontIterator + 1];
                        WinMaxPos = WinHalfSizePlus + (double)LengthCounter;
                    }
                    FrontIterator++;
                }
                if (WinMaxPos == WinHalfSize) Filtered[OutputIterator+1]=WinMax;
                else Filtered[OutputIterator+1]=0.0;
                OutputIterator++;
                for (int LengthCounter = 0; LengthCounter < WinHalfSize; LengthCounter++)
                {
                    if (Original[FrontIterator + 1] > WinMax)
                    {
                        WinMax = Original[FrontIterator + 1];
                        WinMaxPos = WinSizeSpec;
                    }
                    else WinMaxPos -= 1;
                    if (WinMaxPos == WinHalfSize) Filtered[OutputIterator+1]=WinMax;
                    else Filtered[OutputIterator+1]=0.0;
                    FrontIterator++;
                    OutputIterator++;
                }
                int WinIterator = 0;
                for (FrontIterator = FrontIterator; FrontIterator < Original.Length - 1; FrontIterator++)
                {
                    if (Original[FrontIterator+1]>WinMax)
                    {
                        WinMax=Original[FrontIterator+1];
                        WinMaxPos=WinSizeSpec;
                    }
                    else
                    {
                        WinMaxPos -= 1;
                        if (WinMaxPos < 0)
                        {
                            WinIterator = FrontIterator-(int)WinSizeSpec;
                            WinMax = Original[WinIterator+1];
                            WinMaxPos = 0;
                            WinPos=0;
                            for (WinIterator=WinIterator;WinIterator<=FrontIterator;WinIterator++)
                            {
                                if (Original[WinIterator+1]>WinMax)
                                {
                                    WinMax = Original[WinIterator+1];
                                    WinMaxPos = WinPos;
                                }
                                WinPos +=1;
                            }
                        }
                    }
                    if (WinMaxPos==WinHalfSize) Filtered[OutputIterator+1]=WinMax;
                    else Filtered[OutputIterator+1]=0;
                    OutputIterator++;
                }
                WinIterator--;
                WinMaxPos -= 1;
                for (int LengthCounter = 0; LengthCounter < WinHalfSize-1; LengthCounter++)
                {
                    if (WinMaxPos<0)
                    {
                        WinIterator=Original.Length-(int)WinSize+LengthCounter;
                        WinMax=Original[WinIterator+1];
                        WinMaxPos=0;
                        WinPos=1;
                        for (WinIterator=WinIterator+1;WinIterator< Original.Length - 1;WinIterator++)
                        {
                            if (Original[WinIterator+1]>WinMax)
                            {
                                WinMax=Original[WinIterator+1];
                                WinMaxPos=WinPos;
                            }
                            WinPos +=1;
                        }
                    }
                    if (WinMaxPos==WinHalfSize) Filtered[OutputIterator+1]=WinMax;
                    else Filtered[OutputIterator+1]=0;
                    FrontIterator--;
                    WinMaxPos -= 1;
                    OutputIterator++;
                }
                return Filtered;
            }

            // Returns the indices of the vector X that are non-zero
            private int[] find(double[] source)
            {
                List<int> s = new List<int>();
                for (int i = 0; i < source.Length; i++)
                    if (source[i] != 0.0) s.Add(i);
                // Check what happens when s.count == 0;
                int[] s1 = s.ToArray();
                return s1;
            }
        }
    }
}
