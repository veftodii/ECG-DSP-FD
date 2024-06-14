using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace GraphDisplayLib
{
    namespace Processing
    {
        //Cooley–Tukey FFT algorithm
       public class FastFourierTransform
        {
            //public FastFourierTransform()
            //{
            //}
            //   FORWARD FOURIER TRANSFORM, INPLACE VERSION
            //     Input  - input data
            //     Output - transform result
            //     N      - length of both input data and result
            public bool FFT(Complex[] Input, Complex[] Output, uint N)
            {
                //   Check input parameters
                if (Input == null || Output == null || N < 1 || (N & (N - 1)) != 0) return false;
                //   Initialize data
                Rearrange(Input, Output, N);
                //   Call FFT implementation
                Perform(Output, N);
                //   Succeeded
                return true;
            }

            public bool FFT(double[] Input, Complex[] Output, uint N)
            {
                //   Check input parameters
                if (Input == null || Output == null || N < 1 || (N & (N - 1)) != 0) return false;
                //   Initialize data
                Rearrange(Input, Output, N);
                //   Call FFT implementation
                Perform(Output, N);
                //   Succeeded
                return true;
            }

            //   FORWARD FOURIER TRANSFORM, INPLACE VERSION
            //     Data - both input data and output
            //     N    - length of input data
            public bool FFT(Complex[] Data, uint N)
            {
                //   Check input parameters
                if (Data == null || N < 1 || (N & (N - 1)) != 0) return false;
                //   Rearrange
                Rearrange(Data, N);
                //   Call FFT implementation
                Perform(Data, N);
                //   Succeeded
                return true;
            }

            //   INVERSE FOURIER TRANSFORM
            //     Input  - input data
            //     Output - transform result
            //     N      - length of both input data and result
            //     Scale  - if to scale result
            public bool IFFT(Complex[] Input, Complex[] Output, uint N, bool scale = true)
            {
                //   Check input parameters
                if (Input == null || Output == null || N < 1 || (N & (N - 1)) != 0) return false;
                //   Initialize data
                Rearrange(Input, Output, N);
                //   Call FFT implementation
                Perform(Output, N, true);
                //   Scale if necessary
                if (scale) Scale(Output, N);
                //   Succeeded
                return true;
            }

            public bool IFFT(double[] Input, Complex[] Output, uint N, bool scale = true)
            {
                //   Check input parameters
                if (Input == null || Output == null || N < 1 || (N & (N - 1)) != 0) return false;
                //   Initialize data
                Rearrange(Input, Output, N);
                //   Call FFT implementation
                Perform(Output, N, true);
                //   Scale if necessary
                if (scale) Scale(Output, N);
                //   Succeeded
                return true;
            }

            //   INVERSE FOURIER TRANSFORM, INPLACE VERSION
            //     Data  - both input data and output
            //     N     - length of both input data and result
            //     Scale - if to scale result
            public bool IFFT(Complex[] Data, uint N, bool scale = true)
            {
                //   Check input parameters
                if (Data == null || N < 1 || (N & (N - 1)) != 0) return false;
                //   Rearrange
                Rearrange(Data, N);
                //   Call FFT implementation
                Perform(Data, N, true);
                //   Scale if necessary
                if (scale) Scale(Data, N);
                //   Succeeded
                return true;
            }

            //   Rearrange function and its inplace version
            private void Rearrange(Complex[] Input, Complex[] Output, uint N)
            {
                //   Data entry position
                uint Target = 0;
                //   Process all positions of input signal
                for (uint Position = 0; Position < N; ++Position)
                {
                    //  Set data entry
                    Output[Target] = Input[Position];
                    //   Bit mask
                    uint Mask = N;
                    //   While bit is set
                    while ((Target & (Mask >>= 1)) != 0)
                        //   Drop bit
                        Target &= ~Mask;
                    //   The current bit is 0 - set it
                    Target |= Mask;
                }
            }

            private void Rearrange(double[] Input, Complex[] Output, uint N)
            {
                //   Data entry position
                uint Target = 0;
                Complex[] inp = new Complex[Input.Length];
                for (uint i = 0; i < Input.Length; i++) inp[i] = new Complex(Input[i], 0.0);
                    //   Process all positions of input signal
                    for (uint Position = 0; Position < N; ++Position)
                    {
                        //  Set data entry
                        Output[Target] = inp[Position];
                        //   Bit mask
                        uint Mask = N;
                        //   While bit is set
                        while ((Target & (Mask >>= 1)) != 0)
                            //   Drop bit
                            Target &= ~Mask;
                        //   The current bit is 0 - set it
                        Target |= Mask;
                    }
            }

            private void Rearrange(Complex[] Data, uint N)
            {
                //   Swap position
                uint Target = 0;
                //   Process all positions of input signal
                for (uint Position = 0; Position < N; ++Position)
                {
                    //   Only for not yet swapped entries
                    if (Target > Position)
                    {
                        //   Swap entries
                        Complex Temp = new Complex(Data[Target].Real, Data[Target].Imaginary);
                        Data[Target] = Data[Position];
                        Data[Position] = Temp;
                    }
                    //   Bit mask
                    uint Mask = N;
                    //   While bit is set
                    while ((Target & (Mask >>= 1)) != 0)
                        //   Drop bit
                        Target &= ~Mask;
                    //   The current bit is 0 - set it
                    Target |= Mask;
                }
            }

            //   FFT implementation
            private void Perform(Complex[] Data, uint N, bool Inverse = false)
            {
                double pi = (Inverse ? 3.14159265358979323846 : -3.14159265358979323846);
                //   Iteration through dyads, quadruples, octads and so on...
                for (uint Step = 1; Step < N; Step <<= 1)
                {
                    //   Jump to the next entry of the same transform factor
                    uint Jump = Step << 1;
                    //   Angle increment
                    double delta = pi / (double)Step;
                    //   Auxiliary sin(delta / 2)
                    double Sine = Math.Sin(delta * 0.5);
                    //   Multiplier for trigonometric recurrence
                    Complex Multiplier = new Complex(-2.0 * Sine * Sine, Math.Sin(delta));
                    //   Start value for transform factor, fi = 0
                    Complex Factor = new Complex(1.0, 0.0);
                    //   Iteration through groups of different transform factor
                    for (uint Group = 0; Group < Step; ++Group)
                    {
                        //   Iteration within group 
                        for (uint Pair = Group; Pair < N; Pair += Jump)
                        {
                            //   Match position
                            uint Match = Pair + Step;
                            //   Second term of two-point transform
                            Complex Product = new Complex((Factor * Data[Match]).Real, (Factor * Data[Match]).Imaginary);
                            //   Transform for fi + pi
                            Data[Match] = Data[Pair] - Product;
                            //   Transform for fi
                            Data[Pair] += Product;
                        }
                        //   Successive transform factor via trigonometric recurrence
                        Factor = Multiplier * Factor + Factor;
                    }
                }
            }

            //   Scaling of inverse FFT result
            private void Scale(Complex[] Data, uint N)
            {
                double Factor = 1.0 / (double)N;
                //   Scale all data entries
                for (uint Position = 0; Position < N; ++Position) Data[Position] *= Factor;
            }

            public bool FFTShift(Complex[] Input, Complex[] Output, uint N)
            {
                //   Check input parameters
                if (Input == null || Output == null || N < 1 || (N & (N - 1)) != 0) return false;
                int half = (int)(Input.Length / 2);
                for (int i = 0; i < half; i++)
                {
                    Output[i + half] = Input[i];
                    Output[i] = Input[i + half];
                }
                return true;
            }
        }
    }
}
