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
            BR9600   = 9600,
            BR14400  = 14400,
            BR19200  = 19200,
            BR38400  = 38400,
            BR57600  = 57600,
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

        public string sErrorMessage;  
        private System.IO.Ports.SerialPort _serialPort;



        #endregion

        #region Property

        public int Baud
        {
            set
            {
                _serialPort.BaudRate = value;
            }
            get
            {
                return _serialPort.BaudRate;
            }
        }

        public bool DtrEnable
        {
            set
            {
                _serialPort.DtrEnable = value;
            }
            get
            {
                return _serialPort.DtrEnable;
            }
        }

        public bool RtsEnable
        {
            set
            {
                _serialPort.RtsEnable = value;
            }
            get
            {
                return _serialPort.RtsEnable;
            }
        }

        public Handshake Handshake
        {
            set
            {
                _serialPort.Handshake = value;
            }
            get
            {
                return _serialPort.Handshake;
            }
        }

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
                return sErrorMessage;
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

            _serialPort.DtrEnable = true;   // DTR
            _serialPort.RtsEnable = true;   // RTS
            _serialPort.Handshake = Handshake.None; // Handshake
        }

        public SerialPortInterface(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
        {
            _serialPort = new SerialPort();
            _serialPort.PortName = portName;
            _serialPort.BaudRate = baudRate;
            _serialPort.Parity   = parity;
            _serialPort.DataBits = dataBits;
            _serialPort.StopBits = stopBits;

            _serialPort.DtrEnable = true;   // DTR
            _serialPort.RtsEnable = true;   // RTS
            _serialPort.Handshake = Handshake.None; // Handshake
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
                sErrorMessage = "Exception:" + ex.Message;
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
                sErrorMessage = "Exception:" + ex.Message;
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
