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
            TwilioClient.Init(Helper.ACCOUNT_SID,Helper.AUTH_TOKEN);

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
    }
}
