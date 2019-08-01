using System;
using System.Text;
using Windows.Devices.Gpio;

namespace ILI9341Driver
{
    public partial class ILI9341
    {
        public void Flush(int x, int y, int width, int height, ushort[] bitmap)
        {
            lock (this)
            {
                if (x < 0 || y < 0 || width <= 0 || height <= 0) { return; }
                if ((x + width) > Width) width = Width;
                if ((y + height) > Height) height = Height;

                // We first have a fixed 20*20 list of ushorts called bmp2
                // So let us try that first;

                SetWindow(x, x + width - 1, y, y + height - 1);

                SendCommand(Commands.MemoryWrite);
                _spi.ConnectionSettings.DataBitLength = 16;
                //if (width % 2 == 0)
                //{
                //    SendData(bitmap);
                //}
                //else
                {
                    // Byte mode we have to transfer per line
                    ushort[] line = new ushort[width];
                    int offset = 0;

                    // Set data mode                
                    _dataCommandPin.Write(GpioPinValue.High);

                    for (int i = 0; i < height; i++)
                    {
                        // Fill buffer with line of pixel color bytes 
                        for (var j = 0; j < width; j++)
                        {
                            line[j] = bitmap[offset++];
                        }
                        Write(line);
                    }
                }
                _spi.ConnectionSettings.DataBitLength = 8;
            }
        }
        public void LoadBitmap(int x, int y, int width, int height, byte[] bmp)
        {
            if (x < 0 || y < 0 || width <= 0 || height <= 0) { return; }
            if ((x + width) > Width) width = Width;
            if ((y + height) > Height) height = Height;

            // We first have a fixed 20*20 list of ushorts called bmp2
            // So let us try that first;

            SetWindow(x, x + width - 1, y, y + height - 1);

            SendCommand(Commands.MemoryWrite);
            _spi.ConnectionSettings.DataBitLength = 16;
            //if (width % 2 == 0)
            //{
            //    SendData(bmp);
            //}
            //else
            {
                // Byte mode we have to transfer per line
                ushort[] line = new ushort[width];
                int offset = 0;

                // Set data mode                
                _dataCommandPin.Write(GpioPinValue.High);

                for (int i = 0; i < height; i++)
                {
                    // Fill buffer with line of pixel color bytes 
                    for (var j = 0; j < width; j++)
                    {
                        line[j] = bmp[offset++];
                    }
                    Write(line);
                }
            }
            _spi.ConnectionSettings.DataBitLength = 8;
        }

        //public void LoadBitmap(int left, int right, int top, int bottom, byte[] bmp)
        //{
        //    // For the first proof of concept we have a static little bitmap
        //    lock (this)
        //    {
        //        // Why can't we set the window and send the complete bitmap buffer ???????
        //        // Rather as done now per line ?

        //        int len = bmp.Length;
        //        int offset = 0;

        //        // we ignore the setting coords, since we are fixed
        //        SetWindow(left, right, top, bottom);
        //        //SetWindow(101, 120, 101, 120);

        //        var buffer = new ushort[20];

        //        for (int y = 0; y < 20; y++)
        //        {
        //            // Fill buffer with line of pixel color bytes 
        //            for (var i = 0; i < 20; i++)
        //            {
        //                buffer[i] = (ushort)(bmp[offset] << 8 | bmp[offset + 1]);
        //                offset += 2;
        //            }
        //            SendData(buffer);
        //        }
        //    }
        //}
    }
}
