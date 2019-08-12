using ILI9341Driver.Fonts;
using ILI9341Driver.Generic;
using nanoFramework.Runtime.Native;
using System;
using System.Collections;
using System.Threading;
using Windows.Devices.Gpio;

namespace ILI9341Driver._18Bit
{
    public partial class ILI9341Bit18
    {
        
        public void Mosaic(int SquareSize, int Repeats)
        {
            lock (this)
            {
                // Needed vars
                int rndC;
                int rndX = 0;
                int rndY = 0;

                // Temp for the 20 * 20 rect
                byte[] block = new byte[SquareSize * SquareSize * 3];
                byte[] line = new byte[SquareSize*3];

                // Get our random color
                Random random = new Random();

                // Do this 100 times for a start
                for (int i = 0; i < Repeats; i++)
                {
                    // Generate the random numbers for color, x and y position
                    rndC = random.Next(16777215);
                    rndX = random.Next(Width - SquareSize + 1);
                    rndY = random.Next(Height - SquareSize + 1);
                    var colorBytes = ColorConverter.ToRgb666Bytes(rndC);
                    // Fill the array with random color
                    for (int j = 0; j < block.Length; j = j + 3)
                    {
                        block[j] = colorBytes[0];
                        block[j+1] = colorBytes[1];
                        block[j+2] = colorBytes[2];
                    }

                    // We first have a fixed 20*20 block of one color to show
                    SetWindow(rndX, rndX + SquareSize - 1, rndY, rndY + SquareSize - 1);

                    SendCommand(Commands.MemoryWrite);
                    _spi.ConnectionSettings.DataBitLength = 23;
                    if (SquareSize % 2 == 0)
                    {
                        SendData(block);
                    }
                    else
                    {
                        // Byte mode we have to transfer per line
                        // Fill line with random color
                        for (int c = 0; c < line.Length; c = c + 3)
                        {
                            line[c] = colorBytes[0];
                            line[c + 1] = colorBytes[1];
                            line[c + 2] = colorBytes[2];
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

        //public void FlipBox(Color666 boxColor, Color666 backgroundColor)
        //{
        //    // Create the auto bitmaps
        //    var nano = new ushort[400];
        //    var black = new ushort[400];
        //    //var nanoBlue = (ushort)Color565.Blue;

        //    // Fill it with nanoFramework blue
        //    for (int i = 0; i < nano.Length; i++)
        //    {
        //        nano[i] = nanoBlue;
        //        black[i] = 0;
        //    }

        //    // Do the flip
        //    for (int varDelay = 250; varDelay > 30; varDelay -= 20)
        //    {
        //        for (int i = 0; i < 2; i++)
        //        {
        //            Flush(120, 180, 20, 20, nano);
        //            Flush(140, 180, 20, 20, black);
        //            Flush(120, 200, 20, 20, black);
        //            Flush(140, 200, 20, 20, nano);
        //            Thread.Sleep(varDelay);
        //            Flush(120, 180, 20, 20, black);
        //            Flush(140, 180, 20, 20, nano);
        //            Flush(120, 200, 20, 20, nano);
        //            Flush(140, 200, 20, 20, black);
        //            Thread.Sleep(varDelay);
        //        }
        //    }
        //}

        public void DisplayBoardInfo()
        {
            var font = new StandardFixedWidthFont();
            // Well, clear the screen
            ClearScreen();
            //FillScreen(Color565.Red);

            //var all = SystemInfo.OEMString;
            //var idx1 = all.IndexOf('@');
            //var idx2 = all.IndexOf("built");
            //var idx3 = all.IndexOf("ChibiOS");

            var who = "So guess Who";
            //var what = SystemInfo.TargetName;
            var with = "Quadrophenia";
            //var ver = SystemInfo.Version.ToString();

            // And let us know who we are
            DrawString(10, 10, "Hello World of nanoFramework", font, Color666.Red, Color666.Black);
            DrawString(10, 40, "System  . " + who, font, Color666.Blue, Color666.Black);
            //DrawString(10, 55, "Board . . " + what, Color565.LightBlue, Color565.Black, font);
            DrawString(10, 70, "RTOS  . . " + with, font, Color666.Blue, Color666.Black);
            //DrawString(10, 85, "HAL . . . " + ver, Color565.White, Color565.Black, font);

            // Get the used memory 
            var _avail = Debug.GC(false);

            // Put it on screen
            DrawString(10, 110, "Memory:", font, Color666.White, Color666.Black);
            DrawString(25, 125, "Maximum . . " + _avail.ToString(), font, Color666.White, Color666.Black);
            //tft.DrawString(25, 140, "Used  . . . " + _used.ToString(), Color565.White, Color565.Black, font);
        }

        public void ColorScreenTest()
        {

            // missing enum.getvalues
            //var values = Enum.GetValues(typeof(Color565));

            // lets use a thirty thing....
            var font = new StandardFixedWidthFont();

            var colors = new Hashtable() {
                {Color666.White,"White"},
                {Color666.Silver,"Silver"},
                {Color666.Gray,"Gray"},
                {Color666.Black,"Black"},
                {Color666.Red,"Red"},
                {Color666.Maroon,"Maroon"},
                {Color666.Yellow,"Yellow"},
                {Color666.Olive,"Olive"},
                {Color666.Lime,"Lime"},
                {Color666.Green,"Green"},
                {Color666.Aqua,"Aqua"},
                {Color666.Teal,"Teal"},
                {Color666.Blue,"Blue"},
                {Color666.Navy,"Navy"},
                {Color666.Fuchsia,"Fuchsia"},
                {Color666.Purple,"Purple"}
             };

            foreach(var color in colors.Keys)
            {
                DrawRect(10, 310, 10, 100, (Color666)color);
                DrawString(10, 120, colors[color].ToString(), font, (Color666)color, Color666.Black);
                Thread.Sleep(500);
                DrawRect(10, 310, 120, 130, Color666.Black);                
            }
        }

    }
}
