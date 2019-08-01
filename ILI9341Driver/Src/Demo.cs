using System;
using System.Text;
using System.Threading;
using Windows.Devices.Gpio;

namespace ILI9341Driver
{
    public partial class ILI9341
    {
        public void Mosaic(int SquareSize, int Repeats)
        {
            lock (this)
            {
                // Needed vars
                ushort rndC;
                int rndX = 0;
                int rndY = 0;

                // Temp for the 20 * 20 rect
                ushort[] block = new ushort[SquareSize * SquareSize];
                ushort[] line = new ushort[SquareSize];

                // Get our random color
                Random random = new Random();

                // Do this 100 times for a start
                for (int i = 0; i < Repeats; i++)
                {
                    // Generate the random numbers for color, x and y position
                    rndC = (ushort)random.Next(UInt16.MaxValue);
                    rndX = random.Next(Width - SquareSize + 1);
                    rndY = random.Next(Height - SquareSize + 1);

                    // Fill the array with random color
                    for (int j = 0; j < block.Length; j++)
                    {
                        block[j] = rndC;
                    }

                    // We first have a fixed 20*20 block of one color to show
                    SetWindow(rndX, rndX + SquareSize - 1, rndY, rndY + SquareSize - 1);

                    SendCommand(Commands.MemoryWrite);
                    _spi.ConnectionSettings.DataBitLength = 16;
                    if (SquareSize % 2 == 0)
                    {
                        SendData(block);
                    }
                    else
                    {
                        // Byte mode we have to transfer per line
                        // Fill line with random color
                        for (int c = 0; c < line.Length; c++)
                        {
                            line[c] = rndC;
                        }

                        // Set data mode                
                        _dataCommandPin.Write(GpioPinValue.High);

                        for (int r = 0; r < SquareSize; r++)
                        {
                            Write(line);
                        }
                    }
                }
                _spi.ConnectionSettings.DataBitLength = 8;
            }
        }

        public void FlipBox()
        {
            // Create the auto bitmaps
            var nano = new ushort[400];
            var black = new ushort[400];
            var nanoBlue = (ushort)ColorConversion.ToRgb565(Color888.NanoBlue);

            // Fill it with nanoFramework blue
            for (int i = 0; i < nano.Length; i++)
            {
                nano[i] = nanoBlue;
                black[i] = 0;
            }

            // Do the flip
            for (int varDelay = 250; varDelay > 30; varDelay -= 20)
            {
                for (int i = 0; i < 2; i++)
                {
                    Flush(120, 180, 20, 20, nano);
                    Flush(140, 180, 20, 20, black);
                    Flush(120, 200, 20, 20, black);
                    Flush(140, 200, 20, 20, nano);
                    Thread.Sleep(varDelay);
                    Flush(120, 180, 20, 20, black);
                    Flush(140, 180, 20, 20, nano);
                    Flush(120, 200, 20, 20, nano);
                    Flush(140, 200, 20, 20, black);
                    Thread.Sleep(varDelay);
                }
            }
        }
    }
}
