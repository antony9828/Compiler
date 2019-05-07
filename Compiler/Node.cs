using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiler.Keywords;

namespace Compiler
{
    public class Node
    {
        public ParserEnums.Words kind;
        public string value = null;
        public Node op1 = null;
        public Node op2 = null;
        public Node op3 = null;

        public Node(ParserEnums.Words kind_, string value_, Node op1_, Node op2_, Node op3_)
        {
            kind = kind_;
            value = value_;
            op1 = op1_;
            op2 = op2_;
            op3 = op3_;
        }
    }
}
