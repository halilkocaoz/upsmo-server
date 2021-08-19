using System;

namespace UpsMo.Common.DTO.Response
{
    public class Token
    {
        public Token(string value, DateTime expiryDate)
        {
            Value = value;
            ExpiryDate = expiryDate;
        }

        public string Value { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}