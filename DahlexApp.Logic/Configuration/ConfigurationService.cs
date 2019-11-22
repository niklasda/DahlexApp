using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using DahlexApp.Logic.Interfaces;

namespace DahlexApp.Logic.Configuration
{
    public class ConfigurationService : IConfigurationService
    {
        public ConfigurationService()
        {
            var ip = GetLocalIp();

            ApiBaseUrl = $"https://{ip}:5000/api/v1/";
        }

        public string ApiBaseUrl { get; private set; }// = @"https://10.11.11.114:5000/api/v1/";


        private string GetLocalIp()
        {
            return GetLocalIPs().FirstOrDefault();
        }

        private IList<string> GetLocalIPs()
        {
            //Try to find our internal IP address...
            string myHost = Dns.GetHostName();
            IPAddress[] addresses = Dns.GetHostEntry(myHost).AddressList;
            IList<string> myIPs = new List<string>();
            string fallbackIp = "";

            for (int i = 0; i < addresses.Length; i++)
            {
                //Is this a valid IPv4 address?
                if (addresses[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    string thisAddress = addresses[i].ToString();
                    //Loopback is not our preference...
                    if (thisAddress == "127.0.0.1")
                    {
                        continue;
                    }

                    //169.x.x.x addresses are self-assigned "private network" IP by Windows
                    if (thisAddress.StartsWith("169"))
                    {
                        fallbackIp = thisAddress;
                        continue;
                    }

                    myIPs.Add(thisAddress);
                }
            }

            if (myIPs.Count == 0 && !string.IsNullOrEmpty(fallbackIp))
            {
                myIPs.Add(fallbackIp);
            }

            return myIPs;
        }
    }
}