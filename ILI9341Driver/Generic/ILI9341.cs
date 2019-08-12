using System;
using System.Text;
using Windows.Devices.Gpio;
using Windows.Devices.Spi;

namespace ILI9341Driver.Generic
{
    public abstract partial class ILI9341
    {
        #region Private Variables
        protected readonly GpioPin _dataCommandPin;
        protected readonly GpioPin _resetPin;
        protected readonly GpioPin _backlightPin;
        protected readonly GpioPin _chipSelectPin;

        protected readonly SpiDevice _spi;

        protected LCDSettings _lcdSettings;
        #endregion

        #region Properties
        private bool _backlightOn;
        public bool BacklightOn
        {
            get
            {
                return _backlightOn;
            }
            set
            {
                if (_backlightPin != null)
                {
                    var pinValue = value ? GpioPinValue.High : GpioPinValue.Low;
                    _backlightPin.Write(pinValue);
                    _backlightOn = value;
                }
            }
        }

        private int _width;
        public int Width
        {
            get
            {
                return _width;
            }
            private set
            {
                _width = value;
            }
        }

        private int _height;

        public int Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        }
        #endregion

        #region Construtor
        public ILI9341(LCDSettings lcdSettings,
          GpioPin chipSelectPin = null,
          GpioPin dataCommandPin = null,
          GpioPin resetPin = null,
          GpioPin backlightPin = null,
          int spiClockFrequency = 18 * 1000 * 1000,
          SpiMode spiMode = SpiMode.Mode0,
          string spiBus = "SPI1")
        {
            if (chipSelectPin != null)
            {
                _chipSelectPin = chipSelectPin;
                _chipSelectPin.SetDriveMode(GpioPinDriveMode.Output);
            }
            else
            {
                throw new ArgumentNullException("chipSelectPin");
            }

            if (dataCommandPin != null)
            {
                _dataCommandPin = dataCommandPin;
                _dataCommandPin.SetDriveMode(GpioPinDriveMode.Output);
            }
            else
            {
                throw new ArgumentNullException("dataCommandPin");
            }

            if (resetPin != null)
            {
                _resetPin = resetPin;
                _resetPin.SetDriveMode(GpioPinDriveMode.Output);
            }

            if (backlightPin != null)
            {
                _backlightPin = backlightPin;
                _backlightPin.SetDriveMode(GpioPinDriveMode.Output);
            }

            var connectionSettings = new SpiConnectionSettings(chipSelectPin.PinNumber)
            {
                DataBitLength = 8,
                ClockFrequency = spiClockFrequency,
                Mode = spiMode
            };

            _spi = SpiDevice.FromId(spiBus, connectionSettings);
            InitializeScreen();
            _lcdSettings = lcdSettings;
            SetOrientation(lcdSettings);
        }
        #endregion

        #region Init Method
        public abstract void InitializeScreen();
        #endregion

        #region Public Control Methods

        public void SetOrientation(LCDSettings lcdSettings)
        {
            lock (this)
            {

                Width = lcdSettings.Width;
                Height = lcdSettings.Height;

                SendCommand(Commands.MemoryAccessControl);
                SendData(lcdSettings.Orientation);

                SetWindow(0, Width - 1, 0, Height - 1);
            }
        }
        #endregion

        #region Communication Methods

        protected virtual void Write(byte[] data)
        {
            _spi.Write(data);
        }

        protected virtual void Write(ushort[] data)
        {
            _spi.Write(data);
        }

        protected virtual void SendCommand(Commands command)
        {
            _dataCommandPin.Write(GpioPinValue.Low);
            Write(new[] { (byte)command });
        }

        protected virtual void SendData(params byte[] data)
        {
            _dataCommandPin.Write(GpioPinValue.High);
            Write(data);
        }

        protected virtual void SendData(params ushort[] data)
        {
            _dataCommandPin.Write(GpioPinValue.High);
            Write(data);
        }

        protected virtual void WriteReset(GpioPinValue value)
        {
            lock (this)
            {
                if (_resetPin != null)
                {

                    _resetPin.Write(value);
                }
            }
        }

        #endregion Communication Methods

        #region Public Display Methods        
        public void ScrollUp(int pixels)
        {
            lock (this)
            {
                SendCommand(Commands.VerticalScrollingStartAddress);
                SendData((ushort)pixels);

                SendCommand(Commands.MemoryWrite);
            }
        }

        public void SetWindow(int left, int right, int top, int bottom)
        {
            lock (this)
            {
                SendCommand(Commands.ColumnAddressSet);
                SendData((byte)((left >> 8) & 0xFF),
                         (byte)(left & 0xFF),
                         (byte)((right >> 8) & 0xFF),
                         (byte)(right & 0xFF));
                SendCommand(Commands.PageAddressSet);
                SendData((byte)((top >> 8) & 0xFF),
                         (byte)(top & 0xFF),
                         (byte)((bottom >> 8) & 0xFF),
                         (byte)(bottom & 0xFF));
                SendCommand(Commands.MemoryWrite);
            }
        }
        #endregion
    }
}
