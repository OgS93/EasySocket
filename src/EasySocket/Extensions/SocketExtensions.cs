using System;
using System.IO;
using System.Net.Sockets;

public static class SocketExtensions
{
    /// <summary>
    /// Checks socket connection state.
    /// </summary>
    /// <param name="socket">The socket.</param>
    /// <returns></returns>
    public static bool IsConnected(this Socket socket)
    {
        try
        {
            return !(socket.Poll(1000, SelectMode.SelectRead) && socket.Available == 0);
        }
        catch (SocketException)
        {
            return false;
        }
    }


    /// <summary>
    /// Sets the keep-alive interval for the socket.
    /// </summary>
    /// <param name="socket">The socket.</param>
    /// <param name="time">Time between two keep alive "pings".</param>
    /// <param name="interval">Time between two keep alive "pings" when first one fails.</param>
    /// <remarks>based on: http://www.codekeep.net/snippets/269152eb-726b-4cd5-a22d-4e7cef27f93f.aspx </remarks>
    public static void SetKeepAlive(this Socket socket, ulong time, ulong interval)
    {
		const int BytesPerLong = 4; // 32 / 8
    	const int BitsPerByte = 8;
        try
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
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
            socket.IOControl(IOControlCode.KeepAliveValues, inValue, outValue);
        }
        catch (Exception)
        {
            throw new IOException("Could not set Socket Keep Alive values");
        }
    }
}