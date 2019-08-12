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

            //var colors = new Hashtable() {
            //    {Color666.White,"White"},
            //    {Color666.Silver,"Silver"},
            //    {Color666.Gray,"Gray"},
            //    {Color666.Black,"Black"},
            //    {Color666.Red,"Red"},
            //    {Color666.Maroon,"Maroon"},
            //    {Color666.Yellow,"Yellow"},
            //    {Color666.Olive,"Olive"},
            //    {Color666.Lime,"Lime"},
            //    {Color666.Green,"Green"},
            //    {Color666.Aqua,"Aqua"},
            //    {Color666.Teal,"Teal"},
            //    {Color666.Blue,"Blue"},
            //    {Color666.Navy,"Navy"},
            //    {Color666.Fuchsia,"Fuchsia"},
            //    {Color666.Purple,"Purple"}
            // };

            var colors = new Hashtable()
            {
                {Color666.LightSalmon,"LightSalmon"},
                {Color666.Salmon,"Salmon"},
                {Color666.DarkSalmon,"DarkSalmon"},
                {Color666.LightCoral,"LightCoral"},
                {Color666.IndianRed,"IndianRed"},
                {Color666.Crimson,"Crimson"},
                {Color666.FireBrick,"FireBrick"},
                {Color666.Red,"Red"},
                {Color666.DarkRed,"DarkRed"},
                {Color666.Coral,"Coral"},
                {Color666.Tomato,"Tomato"},
                {Color666.OrangeRed,"OrangeRed"},
                {Color666.Gold,"Gold"},
                {Color666.Orange,"Orange"},
                {Color666.DarkOrange,"DarkOrange"},
                {Color666.LightYellow,"LightYellow"},
                {Color666.LemonChiffon,"LemonChiffon"},
                {Color666.LightGoldenRodYellow,"LightGoldenRodYellow"},
                {Color666.PapayaWhip,"PapayaWhip"},
                {Color666.Moccasin,"Moccasin"},
                {Color666.Peachpuff,"Peachpuff"},
                {Color666.PaleGoldenRod,"PaleGoldenRod"},
                {Color666.Khaki,"Khaki"},
                {Color666.DarkKhaki,"DarkKhaki"},
                {Color666.Yellow,"Yellow"},
                {Color666.LawnGreen,"LawnGreen"},
                {Color666.LimeGreen,"LimeGreen"},
                {Color666.Lime,"Lime"},
                {Color666.ForestGreen,"ForestGreen"},
                {Color666.Green,"Green"},
                {Color666.DarkGreen,"DarkGreen"},
                {Color666.GreenYellow,"GreenYellow"},
                {Color666.YellowGreen,"YellowGreen"},
                {Color666.SpringGreen,"SpringGreen"},
                {Color666.MediumSpringGreen,"MediumSpringGreen"},
                {Color666.LightGreen,"LightGreen"},
                {Color666.PaleGreen,"PaleGreen"},
                {Color666.DarkSeaGreen,"DarkSeaGreen"},
                {Color666.MediumSeaGreen,"MediumSeaGreen"},
                {Color666.SeaGreen,"SeaGreen"},
                {Color666.Olive,"Olive"},
                {Color666.DarkOliveGreen,"DarkOliveGreen"},
                {Color666.OliveDrab,"OliveDrab"},
                {Color666.LightCyan,"LightCyan"},
                {Color666.Cyan,"Cyan"},
                {Color666.AquaMarine,"AquaMarine"},
                {Color666.MediumAquaMarine,"MediumAquaMarine"},
                {Color666.PaleTurquoise,"PaleTurquoise"},
                {Color666.Turquoise,"Turquoise"},
                {Color666.MediumTurquoise,"MediumTurquoise"},
                {Color666.DarkTurquoise,"DarkTurquoise"},
                {Color666.LightSeaGreen,"LightSeaGreen"},
                {Color666.CadetBlue,"CadetBlue"},
                {Color666.DarkCyan,"DarkCyan"},
                {Color666.Teal,"Teal"},
                {Color666.PowderBlue,"PowderBlue"},
                {Color666.LightBlue,"LightBlue"},
                {Color666.LightSkyBlue,"LightSkyBlue"},
                {Color666.SkyBlue,"SkyBlue"},
                {Color666.DeepSkyBlue,"DeepSkyBlue"},
                {Color666.LightSteelBlue,"LightSteelBlue"},
                {Color666.DodgerBlue,"DodgerBlue"},
                {Color666.CornFlowerBlue,"CornFlowerBlue"},
                {Color666.SteelBlue,"SteelBlue"},
                {Color666.RoyalBlue,"RoyalBlue"},
                {Color666.Blue,"Blue"},
                {Color666.MediumBlue,"MediumBlue"},
                {Color666.DarkBlue,"DarkBlue"},
                {Color666.Navy,"Navy"},
                {Color666.MidnightBlue,"MidnightBlue"},
                {Color666.MediumSlateBlue,"MediumSlateBlue"},
                {Color666.SlateBlue,"SlateBlue"},
                {Color666.DarkSlateBlue,"DarkSlateBlue"},
                {Color666.Pink,"Pink"},
                {Color666.LightPink,"LightPink"},
                {Color666.HotPink,"HotPink"},
                {Color666.DeepPink,"DeepPink"},
                {Color666.PaleVioletRed,"PaleVioletRed"},
                {Color666.MediumVioletRed,"MediumVioletRed"},
                {Color666.White,"White"},
                {Color666.Snow,"Snow"},
                {Color666.HoneyDew,"HoneyDew"},
                {Color666.MintCream,"MintCream"},
                {Color666.Azure,"Azure"},
                {Color666.AliceBlue,"AliceBlue"},
                {Color666.GhostWhite,"GhostWhite"},
                {Color666.WhiteSmoke,"WhiteSmoke"},
                {Color666.SeaShell,"SeaShell"},
                {Color666.Beige,"Beige"},
                {Color666.OldLace,"OldLace"},
                {Color666.FloralWhite,"FloralWhite"},
                {Color666.Ivory,"Ivory"},
                {Color666.AntiqueWhite,"AntiqueWhite"},
                {Color666.Linen,"Linen"},
                {Color666.LavenderBlush,"LavenderBlush"},
                {Color666.MistyRose,"MistyRose"},
                {Color666.Gainsboro,"Gainsboro"},
                {Color666.LightGray,"LightGray"},
                {Color666.Silver,"Silver"},
                {Color666.DarkGray,"DarkGray"},
                {Color666.Gray,"Gray"},
                {Color666.DimGray,"DimGray"},
                {Color666.LightSlateGray,"LightSlateGray"},
                {Color666.SlateGray,"SlateGray"},
                {Color666.DarksLateGray,"DarksLateGray"},
                {Color666.Black,"Black"},
                {Color666.CornSilk,"CornSilk"},
                {Color666.BlanchedAlmond,"BlanchedAlmond"},
                {Color666.Bisque,"Bisque"},
                {Color666.NavajoWhite,"NavajoWhite"},
                {Color666.Wheat,"Wheat"},
                {Color666.BurlyWood,"BurlyWood"},
                {Color666.Tan,"Tan"},
                {Color666.RosyBrown,"RosyBrown"},
                {Color666.SandyBrown,"SandyBrown"},
                {Color666.GoldenRod,"GoldenRod"},
                {Color666.Peru,"Peru"},
                {Color666.Chocolate,"Chocolate"},
                {Color666.SaddleBrown,"SaddleBrown"},
                {Color666.Sienna,"Sienna"},
                {Color666.Brown,"Brown"},
                {Color666.Maroon,"Maroon"}
            };

            foreach (var color in colors.Keys)
            {
                DrawRect(10, 310, 10, 100, (Color666)color);
                DrawString(10, 120, colors[color].ToString(), font, (Color666)color, Color666.Black);
                Thread.Sleep(500);
                DrawRect(10, 310, 120, 130, Color666.Black);                
            }
        }

    }
}
