namespace Flexmonster.Accelerator.Utils
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;

    using Flexmonster.Accelerator.Models;

    internal class RequestValidator
    {
        private const string ApiSecret = "UuJdIm2mKuT5XcOVTYsoezKW6DkXvtPT";

        public static bool IsValidRequest(BaseArgs value)
        {
            return RequestValidator.GetSignature(
                    Regex.Replace(value.GetHash(), "[^a-zA-Z0-9]", string.Empty))
                .Equals(value.signature);
        }

        private static string GetSignature(string data)
        {
            return Convert.ToBase64String(
                new HMACSHA1(Encoding.UTF8.GetBytes("UuJdIm2mKuT5XcOVTYsoezKW6DkXvtPT"))
                    .ComputeHash(Encoding.UTF8.GetBytes(data)));
        }
    }
}
