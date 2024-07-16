using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bits.Api.Tests.Support
{
    public static class DataHelper
    {
        private static Random random = new Random();
        private const string chars = "ABCDEFGHIJKLMNOPQRSTUWXYZabcdefghijklmnopqustuvwxyz1234567890";

        public static string GetRandomString(int length)
        {
            return new string(Enumerable.Repeat(chars, length)
                .Select(c => c[random.Next(c.Length)])
                .ToArray());
        }

        public static dynamic GetUserDetailsWithout(string fieldName)
        {
            dynamic payload = new ExpandoObject();
            if (!fieldName.Equals("title")) payload.title = "Mr";
            if (!fieldName.Equals("firstName")) payload.firstName = "John";
            if (!fieldName.Equals("lastName")) payload.lastName = "Doe";
            if (!fieldName.Equals("dateOfBirth")) payload.dateOfBirth = "1987-06-04";
            if (!fieldName.Equals("email")) payload.email = "test@email.com";
            if (!fieldName.Equals("password")) payload.password = "password123";
            if (!fieldName.Equals("rating")) payload.rating = 7;
            return payload;
        }
    }
}