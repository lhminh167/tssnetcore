


using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Tss.FunctionalTests.Core.Models;

namespace Tss.FunctionalTests.Core.Helpers
{
    public class RandomGeneratorHelper
    {
        public string RandomString(int length)
        {
            return Randomize("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", length);
        }

        public string RandomNumber(int length)
        {
            return Randomize("0123456789", length);
        }

        private Random random = new Random();

        private string Randomize(string input, int length)
        {
            char[] chars = Enumerable.Repeat(input, length)
              .Select(s => s[random.Next(s.Length)]).ToArray();

            return new String(chars);
        }
    }
}
