using nanoFramework.Runtime.Native;
using System;
using System.Threading;
using Windows.Devices.Gpio;

namespace ILI9341Driver
{
    public partial class ILI9341
    {
        Color565 myEnum;
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
            var nanoBlue = (ushort)Color565.LightBlue;

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

        public void DisplayBoardInfo()
        {
            var font = new StandardFixedWidthFont();
            // Well, clear the screen
            //tft.ClearScreen();
            FillScreen(Color565.Red);

            //var all = SystemInfo.OEMString;
            //var idx1 = all.IndexOf('@');
            //var idx2 = all.IndexOf("built");
            //var idx3 = all.IndexOf("ChibiOS");

            var who = "So guess Who";
            //var what = SystemInfo.TargetName;
            var with = "Quadrophenia";
            //var ver = SystemInfo.Version.ToString();

            // And let us know who we are
            DrawString(10, 10, "Hello World of nanoFramework", Color565.Red, Color565.Black, font);
            DrawString(10, 40, "System  . " + who, Color565.Blue, Color565.Black, font);
            //DrawString(10, 55, "Board . . " + what, Color565.LightBlue, Color565.Black, font);
            DrawString(10, 70, "RTOS  . . " + with, Color565.DarkBlue, Color565.Black, font);
            //DrawString(10, 85, "HAL . . . " + ver, Color565.White, Color565.Black, font);

            // Get the used memory 
            var _avail = Debug.GC(false);

            // Put it on screen
            DrawString(10, 110, "Memory:", Color565.White, Color565.Black, font);
            DrawString(25, 125, "Maximum . . " + _avail.ToString(), Color565.White, Color565.Black, font);
            //tft.DrawString(25, 140, "Used  . . . " + _used.ToString(), Color565.White, Color565.Black, font);
        }

        public void ColorScreenTest()
        {

            // missing enum.getvalues
            //var values = Enum.GetValues(typeof(Color565));

            // lets use a thirty thing....
            var font = new StandardFixedWidthFont();

            var colors = new Color565[] { Color565.AliceBlue,
                Color565.AntiqueWhite,
                Color565.Aqua,
                Color565.Aquamarine,
                Color565.Azure,
                Color565.Beige,
                Color565.Bisque,
                Color565.Black,
                Color565.BlanchedAlmond,
                Color565.Blue,
                Color565.BlueViolet,
                Color565.Brown,
                Color565.Burlywood,
                Color565.CadetBlue,
                Color565.Chartreuse,
                Color565.Chocolate,
                Color565.Coral,
                Color565.CornflowerBlue,
                Color565.Cornsilk,
                Color565.Crimson,
                Color565.Cyan,
                Color565.DarkBlue,
                Color565.DarkCyan,
                Color565.DarkGoldenrod,
                Color565.DarkGray,
                Color565.DarkGreen,
                Color565.DarkKhaki,
                Color565.DarkMagenta,
                Color565.DarkOliveGreen,
                Color565.DarkOrange,
                Color565.DarkOrchid,
                Color565.DarkRed,
                Color565.DarkSalmon,
                Color565.DarkSeaGreen,
                Color565.DarkSlateBlue,
                Color565.DarkSlateGray,
                Color565.DarkTurquoise,
                Color565.DarkViolet,
                Color565.DeepPink,
                Color565.DeepSkyBlue,
                Color565.DimGray,
                Color565.DodgerBlue,
                Color565.FireBrick,
                Color565.FloralWhite,
                Color565.ForestGreen,
                Color565.Fuchsia,
                Color565.Gainsboro,
                Color565.GhostWhite,
                Color565.Gold,
                Color565.Goldenrod,
                Color565.Gray,
                Color565.Green,
                Color565.GreenYellow,
                Color565.HoneyDew,
                Color565.HotPink,
                Color565.IndianRed,
                Color565.Indigo,
                Color565.Ivory,
                Color565.Khaki,
                Color565.Lavender,
                Color565.LavenderBlush,
                Color565.LawnGreen,
                Color565.LemonChiffon,
                Color565.LightBlue,
                Color565.LightCoral,
                Color565.LightCyan,
                Color565.LightGoldenRodYellow,
                Color565.LightGray,
                Color565.LightGreen,
                Color565.LightPink,
                Color565.LightSalmon,
                Color565.LightSeaGreen,
                Color565.LightSkyBlue,
                Color565.LightSlateGray,
                Color565.LightSteelBlue,
                Color565.LightYellow,
                Color565.Lime,
                Color565.LimeGreen,
                Color565.Linen,
                Color565.Magenta,
                Color565.Maroon,
                Color565.MediumAquamarine,
                Color565.MediumBlue,
                Color565.MediumOrchid,
                Color565.MediumPurple,
                Color565.MediumSeaGreen,
                Color565.MediumSlateBlue,
                Color565.MediumSpringGreen,
                Color565.MediumTurquoise,
                Color565.MediumVioletRed,
                Color565.MidnightBlue,
                Color565.MintCream,
                Color565.Mistyrose,
                Color565.Moccasin,
                Color565.NavajoWhite,
                Color565.Navy,
                Color565.Oldlace,
                Color565.Olive,
                Color565.OliveDrab,
                Color565.Orange,
                Color565.OrangeRed,
                Color565.Orchid,
                Color565.PaleGoldenRod,
                Color565.PaleGreen,
                Color565.PaleTurquoise,
                Color565.PaleVioletRed,
                Color565.PapayaWhip,
                Color565.PeachPuff,
                Color565.Peru,
                Color565.Pink,
                Color565.Plum,
                Color565.PowderBlue,
                Color565.Purple,
                Color565.Red,
                Color565.RosyBrown,
                Color565.RoyalBlue,
                Color565.SaddleBrown,
                Color565.Salmon,
                Color565.SandyBrown,
                Color565.SeaGreen,
                Color565.SeaShell,
                Color565.Sienna,
                Color565.Silver,
                Color565.SkyBlue,
                Color565.SlateBlue,
                Color565.SlateGray,
                Color565.Snow,
                Color565.SpringGreen,
                Color565.SteelBlue,
                Color565.Tan,
                Color565.Teal,
                Color565.Thistle,
                Color565.Tomato,
                Color565.Turquoise,
                Color565.Violet,
                Color565.Wheat,
                Color565.White,
                Color565.WhiteSmoke,
                Color565.Yellow,
                Color565.YellowGreen };

            foreach(var color in colors)
            {
                DrawRect(10, 310, 10, 100, color);
                DrawString(10, 120, color.ToString(), color, Color565.Black, font);
                Thread.Sleep(500);
            }
        }

    }
}
