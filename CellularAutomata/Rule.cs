using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ElementaryCellularAutomaton
{
    public class Rule
    {
        private readonly bool[] _creationSchema;

        public Rule(byte ruleNumber)
        {
            _creationSchema = new bool[8];

            for (byte i = 0; i < 8; i++)
            {
                _creationSchema[i] = (ruleNumber & (1 << i)) != 0;
            }
        }

        public bool GetNextStatus(bool left, bool self, bool right)
        {
            byte code = 0;
            if (left)
                code |= 1 << 2;
            if (self)
                code |= 1 << 1;
            if (right)
                code |= 1;

            return _creationSchema[code];
        }
    }
}
