using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CodeCollection
{
    class ModbusRTU
    {
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

        private string strErrorMessage;
        private SerialPort _serialPort = null;

        #endregion

        #region Property

        public string ErrorMessage
        {
            get
            {
                return strErrorMessage;
            }
        }

        #endregion

        #region Construct

        public ModbusRTU(SerialSetting serialSetting)
        {
            strErrorMessage = "";

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

        public ModbusRTU(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
        {
            strErrorMessage = "";

            _serialPort = new SerialPort();
            _serialPort.PortName = portName;
            _serialPort.BaudRate = baudRate;
            _serialPort.Parity = parity;
            _serialPort.DataBits = dataBits;
            _serialPort.StopBits = stopBits;

            _serialPort.DtrEnable = true;   // DTR
            _serialPort.RtsEnable = true;   // RTS
            _serialPort.Handshake = Handshake.None; // Handshake
        }

        ~ModbusRTU()
        {
            if (_serialPort != null && _serialPort.IsOpen)
            {
                _serialPort.Close();
                _serialPort.Dispose();
            }
        }

        #endregion

        #region Function

        public bool Open()
        {
            try
            {
                // If the serialport is already open, close the port
                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                }

                _serialPort.Open();
                _serialPort.DiscardInBuffer();
                _serialPort.DiscardOutBuffer();

                if (_serialPort.IsOpen == false)
                {
                    strErrorMessage = "Fail to open serial port !!!";
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

        public bool Close()
        {
            try
            {
                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                }

                if (_serialPort.IsOpen)
                {
                    strErrorMessage = "Fail to close serial port !!!";
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






        // 读取保持型寄存器
        public byte[] ReadRegisters(int slaveId, ushort startAddress, ushort count)
        {

            try
            {
                // 拼接报文
                List<byte> SendCmd = new List<byte>();

                // 从站地址 + 功能码 + 起始高位 + 起始低位 + 数量高位 + 数量低位 + CRC校验
                SendCmd.Add((byte)slaveId);
                SendCmd.Add(0x03);
                SendCmd.Add((byte)(startAddress / 256));
                SendCmd.Add((byte)(startAddress % 256));
                SendCmd.Add((byte)(count / 256));
                SendCmd.Add((byte)(count % 256));

                //byte[] crc = CRC16(SendCmd.ToArray(), 6);
                //SendCmd.AddRange(crc);

                // 发送报文
                _serialPort.Write(SendCmd.ToArray(), 0, SendCmd.Count());

                // 接收报文
                Thread.Sleep(100);
                byte[] buffer = new byte[_serialPort.BytesToRead];
                _serialPort.Read(buffer, 0, buffer.Length);

                // 验证报文   
                //if (CheckCRC(buffer))
                //{

                //}

                // 解析报文
                byte[] result = new byte[count * 2];

                Array.Copy(buffer, 3, result, 0, count * 2);

                return result;
            }
            catch (Exception ex)
            {
                strErrorMessage = "Exception:" + ex.Message;
                return null;
            }
        }









        #endregion


    }
}
