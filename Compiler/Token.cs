using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiler.Keywords;

namespace Compiler
{
    public class Token
    {
        public string key;
        public string value;

        public Token(string key, string value)
        {
            this.key = key;
            this.value = value;
        }
        public override string ToString()
        {
            return $"{key}: {value}";
        }
    }
}
