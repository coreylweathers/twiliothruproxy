using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Proxy
{
    class Program
    {
        static void Main(string[] args)
        {
            var toPhoneNumber = "REPLACE_TO_PHONE_NUMBER_HERE";
            
            var proxyClient = GetProxyClient();
            var twilioClient = new Twilio.Clients.TwilioRestClient(Helper.ACCOUNT_SID, Helper.AUTH_TOKEN, httpClient:new Twilio.Http.SystemNetHttpClient(proxyClient));
            TwilioClient.SetRestClient(twilioClient);
            
            try
            {
                var callResult = CallResource.Create(
                    to: new PhoneNumber(toPhoneNumber),
                    from: new PhoneNumber(Helper.TWILIO_NUMBER),
                    url: new Uri("http://demo.twilio.com/docs/voice.xml")
                );
                Console.WriteLine(callResult.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred: {ex.Message}");
            }
        }

         static HttpClient GetProxyClient()
        {
            var proxyUri = "127.0.0.1";
            var creds = CredentialCache.DefaultCredentials;
            var proxy = new WebProxy(proxyUri,8888)
            {
                UseDefaultCredentials = false,
                Credentials = creds
            };
            var handler = new HttpClientHandler
            {
                Proxy = proxy,
                PreAuthenticate = true,
                UseDefaultCredentials = false
            };
            return new HttpClient(handler);    
        }

    }
}
