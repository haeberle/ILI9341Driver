using System;
using System.Text;
using Windows.Devices.Gpio;
using Windows.Devices.Spi;

namespace ILI9341Driver
{
    public partial class ILI9341
    {
        #region Variables
        LCDSettings _lcdSettings;
        #endregion

        #region Constructor
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
    }
}
