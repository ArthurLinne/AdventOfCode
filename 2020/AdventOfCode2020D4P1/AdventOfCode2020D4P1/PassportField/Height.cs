using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020D4P1
{


    class Height : PassportField
    {
        public Height(string value) : base("Height", "hgt", value)
        {
        }

        public override bool IsValid()
        {
            try
            {
                int height = Int32.Parse(this.value.Substring(0, this.value.Length - 2));
                string units = this.value.Substring(this.value.Length - 2, 2);

                int minHeight = 0;
                int maxHeight = 0;

                switch (units)
                {
                    case "cm":
                        minHeight = 150;
                        maxHeight = 193;
                        break;
                    case "in":
                        minHeight = 59;
                        maxHeight = 76;
                        break;
                    default:
                        break;
                }

                if (minHeight <= height && height <= maxHeight)
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