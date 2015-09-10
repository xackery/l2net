using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Odbc;
using System.Net;
using System.Net.Sockets;
using System.Net.Mail;
using System.Net.NetworkInformation;

namespace L2_login
{
    public static partial class Util
    {
        public static bool TestWebsite(string addy)
        {
            try
            {
                System.Net.HttpWebRequest myReq = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(addy);

                myReq.Timeout = 10000;//set the timeout at 10 seconds instead of 5min
                myReq.ReadWriteTimeout = 10000;//lets put a 10 seconds instead of 5min delay

                System.Net.HttpWebResponse myRes = (System.Net.HttpWebResponse)myReq.GetResponse();

                System.IO.BinaryReader reader = new System.IO.BinaryReader(myRes.GetResponseStream());

                byte[] buff = reader.ReadBytes((int)myRes.ContentLength);

            }
            catch
            {
                return false;
            }

            return true;
        }

        public static bool TestODBC(string addy)
        {
            try
            {
                OdbcConnection conn = new OdbcConnection(addy);
                conn.Open();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static bool TestDNS(string addy)
        {
            try
            {
                Dns.GetHostAddresses(addy);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static bool TestPing(string addy)
        {
            try
            {
                Ping ping = new Ping();
                PingReply pingreply = ping.Send(addy);

                if (pingreply.Status != IPStatus.Success)
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static void Send_Email(string _from, string _to, string _sub, string _body, string _server, int _port)
        {
            try
            {
                SmtpClient smtp = new SmtpClient(_server, _port);
                MailMessage mmsg = new MailMessage();

                MailAddress fromAdd = new MailAddress(_from);

                mmsg.From = fromAdd;
                mmsg.To.Add(_to);
                mmsg.Subject = _sub;
                mmsg.Body = _body;

                mmsg.IsBodyHtml = true;

                smtp.Send(mmsg);
            }
            catch
            {
                //sending the mail message failed... uh oh
            }
        }
    }
}
