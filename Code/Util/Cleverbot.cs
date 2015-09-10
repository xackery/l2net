using System;
using System.Web;
using System.Web.Security;
using System.Xml.XPath;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

/*
    ChatterBotAPI
    Copyright (C) 2011 pierredavidbelanger@gmail.com
 
    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Lesser General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Lesser General Public License for more details.

    You should have received a copy of the GNU Lesser General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
namespace L2_login
{
    public enum ChatterBotType
    {
        CLEVERBOT,
        JABBERWACKY,
        PANDORABOTS
    }

    public interface ChatterBot
    {

        ChatterBotSession CreateSession();
    }

    public interface ChatterBotSession
    {

        ChatterBotThought Think(ChatterBotThought thought);

        string Think(string text);
    }
    static class Utils
    {

        public static string ParametersToWWWFormURLEncoded(IDictionary<string, string> parameters)
        {
            string wwwFormUrlEncoded = null;
            foreach (string parameterKey in parameters.Keys)
            {
                string parameterValue = parameters[parameterKey];
                string parameter = string.Format("{0}={1}", HttpUtility.UrlEncode(parameterKey), HttpUtility.UrlEncode(parameterValue));
                if (wwwFormUrlEncoded == null)
                {
                    wwwFormUrlEncoded = parameter;
                }
                else
                {
                    wwwFormUrlEncoded = string.Format("{0}&{1}", wwwFormUrlEncoded, parameter);
                }
            }
            return wwwFormUrlEncoded;
        }

        public static string MD5(string input)
        {
            //return FormsAuthentication.HashPasswordForStoringInConfigFile(input, "MD5");

            System.Security.Cryptography.HashAlgorithm algorithm;
            algorithm = System.Security.Cryptography.MD5.Create();
            
            return ByteArrayToHexString(algorithm.ComputeHash(Encoding.UTF8.GetBytes(input)));

        }
        public static String ByteArrayToHexString(byte[] bytes)
        {
            int length = bytes.Length;

            char[] chars = new char[length << 1];

            for (int i = 0, j = 0; i < length; i++, j++)
            {
                byte b = (byte)(bytes[i] >> 4);
                chars[j] = (char)(b > 9 ? b + 0x37 : b + 0x30);

                j++;

                b = (byte)(bytes[i] & 0x0F);
                chars[j] = (char)(b > 9 ? b + 0x37 : b + 0x30);
            }

            return new String(chars);
        }
        public static string Post(string url, IDictionary<string, string> parameters)
        {
            string postData = ParametersToWWWFormURLEncoded(parameters);
            byte[] postDataBytes = Encoding.ASCII.GetBytes(postData);

            WebRequest webRequest = WebRequest.Create(url);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentLength = postDataBytes.Length;

            Stream outputStream = webRequest.GetRequestStream();
            outputStream.Write(postDataBytes, 0, postDataBytes.Length);
            outputStream.Close();

            WebResponse webResponse = webRequest.GetResponse();
            StreamReader responseStreamReader = new StreamReader(webResponse.GetResponseStream());
            return responseStreamReader.ReadToEnd().Trim();
        }

        public static string XPathSearch(string input, string expression)
        {
            XPathDocument document = new XPathDocument(new MemoryStream(Encoding.ASCII.GetBytes(input)));
            XPathNavigator navigator = document.CreateNavigator();
            return navigator.SelectSingleNode(expression).Value;
        }

        public static string StringAtIndex(string[] strings, int index)
        {
            if (index >= strings.Length) return "";
            return strings[index];
        }
    }

    public class ChatterBotFactory
    {

        public ChatterBot Create(ChatterBotType type)
        {
            return Create(type, null);
        }

        public ChatterBot Create(ChatterBotType type, object arg)
        {
            switch (type)
            {
                case ChatterBotType.CLEVERBOT:
                    return new Cleverbot("http://cleverbot.com/webservicemin");
                case ChatterBotType.JABBERWACKY:
                    return new Cleverbot("http://jabberwacky.com/webservicemin");
            }
            return null;
        }
    }
    public class ChatterBotThought
    {

        public string[] Emotions { get; set; }

        public string Text { get; set; }
    }

	class Cleverbot: ChatterBot {
		private readonly string url;
		
		public Cleverbot(string url) {
			this.url = url;
		}
		
		public ChatterBotSession CreateSession() {
			return new CleverbotSession(url);
		}
	}
	
	class CleverbotSession: ChatterBotSession {
		private readonly string url;
		private readonly IDictionary<string, string> vars;
		
		public CleverbotSession(string url) {
			this.url = url;
			vars = new Dictionary<string, string>();
			vars["start"] = "y";
			vars["icognoid"] = "wsf";
			vars["fno"] = "0";
			vars["sub"] = "Say";
			vars["islearning"] = "1";
			vars["cleanslate"] = "false";
		}
		
		public ChatterBotThought Think(ChatterBotThought thought) {
			vars["stimulus"] = thought.Text;
			
			string formData = Utils.ParametersToWWWFormURLEncoded(vars);
			string formDataToDigest = formData.Substring(9, 20);
			string formDataDigest = Utils.MD5(formDataToDigest);
			vars["icognocheck"] = formDataDigest;
			
			string response = Utils.Post(url, vars);
			
			string[] responseValues = response.Split('\r');
			
			//vars[""] = Utils.StringAtIndex(responseValues, 0); ??
			vars["sessionid"] = Utils.StringAtIndex(responseValues, 1);
			vars["logurl"] = Utils.StringAtIndex(responseValues, 2);
			vars["vText8"] = Utils.StringAtIndex(responseValues, 3);
			vars["vText7"] = Utils.StringAtIndex(responseValues, 4);
			vars["vText6"] = Utils.StringAtIndex(responseValues, 5);
			vars["vText5"] = Utils.StringAtIndex(responseValues, 6);
			vars["vText4"] = Utils.StringAtIndex(responseValues, 7);
			vars["vText3"] = Utils.StringAtIndex(responseValues, 8);
			vars["vText2"] = Utils.StringAtIndex(responseValues, 9);
			vars["prevref"] = Utils.StringAtIndex(responseValues, 10);
			//vars[""] = Utils.StringAtIndex(responseValues, 11); ??
			vars["emotionalhistory"] = Utils.StringAtIndex(responseValues, 12);
			vars["ttsLocMP3"] = Utils.StringAtIndex(responseValues, 13);
			vars["ttsLocTXT"] = Utils.StringAtIndex(responseValues, 14);
			vars["ttsLocTXT3"] = Utils.StringAtIndex(responseValues, 15);
			vars["ttsText"] = Utils.StringAtIndex(responseValues, 16);
			vars["lineRef"] = Utils.StringAtIndex(responseValues, 17);
			vars["lineURL"] = Utils.StringAtIndex(responseValues, 18);
			vars["linePOST"] = Utils.StringAtIndex(responseValues, 19);
			vars["lineChoices"] = Utils.StringAtIndex(responseValues, 20);
			vars["lineChoicesAbbrev"] = Utils.StringAtIndex(responseValues, 21);
			vars["typingData"] = Utils.StringAtIndex(responseValues, 22);
			vars["divert"] = Utils.StringAtIndex(responseValues, 23);
			
			ChatterBotThought responseThought = new ChatterBotThought();
			
			responseThought.Text = Utils.StringAtIndex(responseValues, 16);
			
			return responseThought;
		}
		
		public string Think(string text) {
			return Think(new ChatterBotThought() { Text = text }).Text;
		}
	}
}