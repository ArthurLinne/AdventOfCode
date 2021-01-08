using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020D4P1
{
    public abstract class PassportField
    {
        protected string fieldName;

        protected string fieldIdentifier;

        protected readonly string value;

        public PassportField(string fieldName, string fieldIdentifier, string value)
        {
            this.fieldName = fieldName;
            this.fieldIdentifier = fieldIdentifier;
            this.value = value;
        }

        public abstract bool IsValid();

    }
}
