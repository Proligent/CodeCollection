using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CodeCollection
{
    class SerialPortInterface
    {

        #region Enum

        public enum BaudRate
        {
            BR9600 = 9600,
            BR14400 = 14400,
            BR19200 = 19200,
            BR38400 = 38400,
            BR57600 = 57600,
            BR115200 = 115200,
            BR128000 = 128000
        }

        #endregion

        #region Struct

        public struct SerialSetting
        {
            public string PortName;
            public int BaudRate;
            public Parity Parity;
            public int DataBits;
            public StopBits StopBits;
        }

        #endregion


        #region Variable

        public string strErrorMessage;
        private System.IO.Ports.SerialPort _serialPort;



        #endregion

        #region Property

        public bool IsOpen
        {
            get
            {
                return _serialPort.IsOpen;
            }
        }

        public string ErrorMessage
        {
            get
            {
                return strErrorMessage;
            }
        }

        #endregion

        #region Construct

        public SerialPortInterface()
        {
            _serialPort = new SerialPort();
        }

        public SerialPortInterface(SerialSetting serialSetting)
        {
            _serialPort = new SerialPort();
            _serialPort.PortName = serialSetting.PortName;
            _serialPort.BaudRate = serialSetting.BaudRate;
            _serialPort.Parity = serialSetting.Parity;
            _serialPort.DataBits = serialSetting.DataBits;
            _serialPort.StopBits = serialSetting.StopBits;
        }

        public SerialPortInterface(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
        {
            _serialPort = new SerialPort();
            _serialPort.PortName = portName;
            _serialPort.BaudRate = baudRate;
            _serialPort.Parity = parity;
            _serialPort.DataBits = dataBits;
            _serialPort.StopBits = stopBits;
        }



        ~SerialPortInterface()
        {
            if (_serialPort != null && _serialPort.IsOpen)
            {
                _serialPort.Close();
                _serialPort.Dispose();
            }
        }


        #endregion

        #region Function


        protected bool Open()
        {
            try
            {
                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                }

                _serialPort.Open();
                _serialPort.DiscardInBuffer();
                _serialPort.DiscardOutBuffer();

                if (_serialPort.IsOpen == false)
                {
                    _serialPort.Close();
                    return false;
                }

            }
            catch (Exception ex)
            {
                strErrorMessage = "Exception:" + ex.Message;
                return false;
            }


            return true;
        }

        protected bool Close()
        {
            try
            {
                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                }

                if (_serialPort.IsOpen)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                strErrorMessage = "Exception:" + ex.Message;
                return false;
            }

            return true;
        }




        // Query
        protected bool Query()
        {



            return true;
        }



        // Read
        protected bool Read()
        {

            return true;
        }

        // Write
        protected bool Write()
        {

            return true;
        }


        #endregion




    }


}
