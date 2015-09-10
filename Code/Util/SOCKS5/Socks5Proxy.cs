using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
    
/*
* zahmed
* Date 23 Jan 2004
* Socks 5 RFC is available at http://www.faqs.org/rfcs/rfc1928.html.
*/
namespace LMKR
{
    public class ConnectionException : ApplicationException
    {
        public ConnectionException(string message)
            : base(message)
        {
        }
    }

    /// <summary>
    /// Provides sock5 functionality to clients (Connect only).
    /// </summary>
    public class SocksProxy
    {
        private SocksProxy()
        {
        }

        private static string[] errorMsgs =	{
										"Operation completed successfully.",
										"General SOCKS server failure.",
										"Connection not allowed by ruleset.",
										"Network unreachable.",
										"Host unreachable.",
										"Connection refused.",
										"TTL expired.",
										"Command not supported.",
										"Address type not supported.",
										"Unknown error."
									};

        public static Socket ConnectToSocks5Proxy(string proxyAdress, ushort proxyPort, string destAddress, ushort destPort, string userName, string password)
        {
            IPAddress destIP = IPAddress.Parse(destAddress);
            IPAddress proxyIP = IPAddress.Parse(proxyAdress);
            byte[] request;
            byte[] response;
            byte[] rawBytes;
            ushort nIndex;

            IPEndPoint proxyEndPoint = new IPEndPoint(proxyIP, proxyPort);

            // open a TCP connection to SOCKS server...
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            s.Connect(proxyEndPoint);

            request = new byte[256];
            nIndex = 0;
            request[nIndex++] = 0x05; // Version 5.
            request[nIndex++] = 0x02; // 2 Authentication methods are in packet...
            request[nIndex++] = 0x00; // NO AUTHENTICATION REQUIRED
            request[nIndex++] = 0x02; // USERNAME/PASSWORD
            // Send the authentication negotiation request...
            s.Send(request, 0, nIndex, SocketFlags.None);

            // Receive 2 byte response...
            response = new byte[256];
            int nGot = s.Receive(response, 0, 2, SocketFlags.None);
            if (nGot != 2)
                throw new ConnectionException("Bad response received from proxy server.");

            switch(response[1])
            {
                case 0x00://no authentication
                    break;
                case 0x02://username/password
                    request = new byte[256];
                    nIndex = 0;
                    request[nIndex++] = 0x01; // Version 5.

                    // add user name
                    request[nIndex++] = (byte)userName.Length;
                    rawBytes = Encoding.Default.GetBytes(userName);
                    rawBytes.CopyTo(request, nIndex);
                    nIndex += (ushort)rawBytes.Length;

                    // add password
                    request[nIndex++] = (byte)password.Length;
                    rawBytes = Encoding.Default.GetBytes(password);
                    rawBytes.CopyTo(request, nIndex);
                    nIndex += (ushort)rawBytes.Length;

                    // Send the Username/Password request
                    s.Send(request, 0, nIndex, SocketFlags.None);
                    // Receive 2 byte response...
                    response = new byte[256];
                    nGot = s.Receive(response, 0, 2, SocketFlags.None);
                    if (nGot != 2)
                        throw new ConnectionException("Bad response received from proxy server.");
                    if (response[1] != 0x00)
                        throw new ConnectionException("Bad Usernaem/Password.");
                    break;
                case 0xFF:// No authentication method was accepted close the socket.
                    s.Close();
                    throw new ConnectionException("None of the authentication method was accepted by proxy server.");
            }

            // This version only supports connect command. 
            // UDP and Bind are not supported.

            // Send connect request now...
            nIndex = 0;
            request = new byte[256];
            request[nIndex++] = 0x05;	// version 5.
            request[nIndex++] = 0x01;	// command = connect.
            request[nIndex++] = 0x00;	// Reserve = must be 0x00

            request[nIndex++] = 0x01;// Address is IPV4 format
            rawBytes = destIP.GetAddressBytes();
            rawBytes.CopyTo(request, nIndex);
            nIndex += (ushort)rawBytes.Length;

            // using big-edian byte order
            byte[] portBytes = BitConverter.GetBytes(destPort);
            for (int i = portBytes.Length - 1; i >= 0; i--)
                request[nIndex++] = portBytes[i];

            // send connect request.
            s.Send(request, nIndex, SocketFlags.None);

            // Get variable length response...
            response = new byte[256];
            s.Receive(response);
            if (response[1] != 0x00)
                throw new ConnectionException(errorMsgs[response[1]]);

            // Success Connected...
            return s;
        }
    }
}




