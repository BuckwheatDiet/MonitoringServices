using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleServer.Configuration;

namespace SimpleServer.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestConfig()
        {
            IConfig config = (new ConfigReader()).GetConfig();
            Console.WriteLine(config.Slack.ApiToken);

        }

        [TestMethod]
        public void GenerateCryptedString()
        {
            string key = "string_to_crypt";
            Console.WriteLine(key);
            var result = Crypto.Encrypt(key, false);
            Console.WriteLine(result);
        }

        [TestMethod]
        public void GenerateBase64Key()
        {
            System.Security.Cryptography.TripleDESCryptoServiceProvider desc = new System.Security.Cryptography.TripleDESCryptoServiceProvider();
            desc.GenerateKey();
            string key = Convert.ToBase64String(desc.Key);
            Console.WriteLine(key);
        }
    }
}
