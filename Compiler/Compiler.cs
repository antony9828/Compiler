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
                    Generation(node.value);
                    break;
                case ParserEnums.Words.CONST:
                    Generation(Instructions.Words.PUSH);
                    Generation(node.value);
                    break;
                case ParserEnums.Words.ADD:
                    Compile(node.op1);
                    Compile(node.op2);
                    Generation(Instructions.Words.ADD);
                    break;
                case ParserEnums.Words.SUB:
                    Compile(node.op1);
                    Compile(node.op2);
                    Generation(Instructions.Words.SUB);
                    break;
                case ParserEnums.Words.PRINT:
                    Generation(Instructions.Words.OUT);
                    Compile(node.op1);
                    break;
                case ParserEnums.Words.LT:
                    Compile(node.op1);
                    Compile(node.op2);
                    Generation(Instructions.Words.LT);
                    break;
                case ParserEnums.Words.SET:
                    Compile(node.op1);
                    Generation(Instructions.Words.STORE);
                    Generation(node.op1.value);
                    break;
                case ParserEnums.Words.IF1:
                    Compile(node.op1);
                    Generation(Instructions.Words.JZ);
                    int addr = pc;
                    Generation(0);
                    Compile(node.op2);
                    program[addr] = pc;
                    break;
                case ParserEnums.Words.IF2:
                    Compile(node.op1);
                    Generation(Instructions.Words.JZ);
                    int addr1 = pc;
                    Generation(0);
                    Compile(node.op2);
                    Generation(Instructions.Words.JMP);
                    int addr2 = pc;
                    Generation(0);
                    program[addr1] = pc;
                    Compile(node.op3);
                    program[addr2] = pc;
                    break;
                case ParserEnums.Words.WHILE:
                    int addr3 = pc;
                    Compile(node.op1);
                    Generation(Instructions.Words.JZ);
                    int addr4 = pc;
                    Generation(0);
                    Compile(node.op2);
                    Generation(Instructions.Words.JMP);
                    Generation(addr3);
                    program[addr4] = pc;
                    break;
                case ParserEnums.Words.SEQ:
                    Compile(node.op1);
                    Compile(node.op2);
                    break;
                case ParserEnums.Words.EXPR:
                    Compile(node.op1);
                    Generation(Instructions.Words.POP);
                    break;
                case ParserEnums.Words.PROG:
                    Compile(node.op1);
                    Generation(Instructions.Words.HALT);
                    break;
            }
            return program;
        }
    }
}
