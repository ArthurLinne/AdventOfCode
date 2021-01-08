using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020D4P1
{


    class PassportId : PassportField
    {
        public PassportId(string value) : base("Passport Id", "pid", value)
        {
        }

        public override bool IsValid()
        {
            try
            {
                if (this.value.Length != 9)
                {
                    return false;
                }

                int PassportId = Int32.Parse(this.value);
            }
            catch (FormatException)
            {
                return false;
            }
            
            return true;
        }

    }
}