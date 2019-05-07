using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiler.Keywords;

namespace Compiler
{
    public class Compiler
    {
        List<Object> program = new List<Object>();
        int pc = 0;

        public void Generation(Object command)
        {
            program.Add(command);
            pc++;
        }

        public List<Object> Compile(Node node)
        {
            switch (node.kind)
            {
                case ParserEnums.Words.VAR:
                    Generation(Instructions.Words.FETCH);
                    break;
            }

            return program;
        }
    }
}
