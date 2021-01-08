using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020D4P1
{

    class IssueYear : PassportField
    {
        public IssueYear(string value) : base("Birth Year", "byr", value)
        {
        }

        public override bool IsValid()
        {
            try
            {
                int issueYear = Int32.Parse(this.value);

                if (2010 <= issueYear && issueYear <= 2020)
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