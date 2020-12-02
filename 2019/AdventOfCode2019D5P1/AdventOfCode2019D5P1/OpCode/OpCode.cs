using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019D5P1
{
    abstract class OpCode
    {
        private int codeNumber;

        private int numArgs;

        OpCode()
        {

        }

        public abstract int EvaluateOpCode();

    }
}
