using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020D4P1
{


    class BirthYear : PassportField
    {
        public BirthYear(string value) : base("Birth Year", "byr", value)
        {
        }

        public override bool IsValid()
        {
            try
            {
                int birthYear = Int32.Parse(this.value);

                if (1920 <= birthYear && birthYear <= 2020)
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