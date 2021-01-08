using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020D4P1
{


    class EyeColor : PassportField
    {
        public EyeColor(string value) : base("Eye Color", "ecl", value)
        {
        }

        public override bool IsValid()
        {
            if (new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }.Contains(value))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}