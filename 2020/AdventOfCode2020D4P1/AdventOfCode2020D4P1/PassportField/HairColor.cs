using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020D4P1
{


    class HairColor : PassportField
    {
        public HairColor(string value) : base("Hair Color", "hcl", value)
        {
        }

        public override bool IsValid()
        {
            try
            {
                char identifier = value[0];

                if (identifier != '#')
                {
                    return false;
                }

                string hairColor = this.value.Substring(1, this.value.Length);

                foreach (char character in hairColor)
                {
                    if (!(new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' }.Contains(character)))
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

    }
}