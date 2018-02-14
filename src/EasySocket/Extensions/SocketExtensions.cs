using System;
using System.Net.Sockets;

namespace EasySocket
{
    public static class SocketExtensions
    {
        #region KEEP_ALIVE
        // http://snipplr.com/view/54476/
		private const int BytesPerLong = 8; // 32 / 8 / 4
		private const int BitsPerByte = 8;
 
		/// <summary>
		/// Sets the keep-alive interval for the socket.
		/// </summary>
		/// <param name="socket">The socket.</param>
		/// <param name="time">Time between two keep alive "pings".</param>
		/// <param name="interval">Time between two keep alive "pings" when first one fails.</param>
		public static void SetKeepAlive(this Socket socket, ulong time, ulong interval)
		{
			// Array to hold input values.
			var input = new[]
			{
				(time == 0 || interval == 0) ? 0UL : 1UL, // on or off
				time,
				interval
			};

			// Pack input into byte struct.
			byte[] inValue = new byte[3 * BytesPerLong];
			for (int i = 0; i < input.Length; i++)
			{
				inValue[i * BytesPerLong + 3] = (byte)(input[i] >> ((BytesPerLong - 1) * BitsPerByte) & 0xff);
				inValue[i * BytesPerLong + 2] = (byte)(input[i] >> ((BytesPerLong - 2) * BitsPerByte) & 0xff);
				inValue[i * BytesPerLong + 1] = (byte)(input[i] >> ((BytesPerLong - 3) * BitsPerByte) & 0xff);
				inValue[i * BytesPerLong + 0] = (byte)(input[i] >> ((BytesPerLong - 4) * BitsPerByte) & 0xff);
			}

			// Create bytestruct for result (bytes pending on server socket).
			byte[] outValue = BitConverter.GetBytes(0);

			// Write SIO_VALS to Socket IOControl.
			socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.KeepAlive, true);
			socket.IOControl(IOControlCode.KeepAliveValues, inValue, outValue);
		}
        #endregion // KEEP_ALIVE

        public static bool IsConnected(this Socket socket)
        {
            try
            {
                return !(socket.Poll(1, SelectMode.SelectRead) && socket.Available == 0 || !socket.Connected);
            }
            catch
            {
                return false;
            }
        }
    }
}