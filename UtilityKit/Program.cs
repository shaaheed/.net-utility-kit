﻿using Msi.UtilityKit.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using Msi.UtilityKit.Security;
using Msi.UtilityKit.Sms;
using UtilityKit.Sms;

namespace UtilityKit
{
    class Program
    {
        static void Main(string[] args)
        {

            #region Twilio Sms

            SmsUtilities.AddService("twilio", new TwilioSmsService("+10000000000", "sid", "token"));

            var text = "Test Sms!";
            SmsUtilities.SendSms(text, "+8801815000000");

            #endregion

            #region Encription/Decryption

            SecurityUtilities.ConfigureStaticAesOptions(x =>
            {
                x.Key = "";
                x.Secret = "";
            });

            string str = "Normal String";
            string encryptedString = str.Encrypt();

            #endregion

            #region Dynamic Search

            var users = new List<User>
            {
                new User { Id = 1, Name = "A" },
                new User { Id = 2, Name = "B" },
                new User { Id = 3, Name = "B" },
            }.AsQueryable();

            var searchOptions = new SearchOptions
            {
                Search = new string[] { "name eq B" }
            };

            var result = users.ApplySearch(searchOptions).ToList();

            #endregion

            Console.ReadLine();
        }
    }
}
