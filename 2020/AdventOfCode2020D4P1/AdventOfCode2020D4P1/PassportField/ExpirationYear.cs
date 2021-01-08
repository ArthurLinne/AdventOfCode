using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020D4P1
{


    class ExpirationYear : PassportField
    {
        public ExpirationYear(string value) : base("Expiration Year", "eyr", value)
        {
        }

        public override bool IsValid()
        {
            try
            {
                int expYear = Int32.Parse(this.value);

                if (2020 <= expYear && expYear <= 2030)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (FormatException)
            {
                return false;
            }
        }

    }
}