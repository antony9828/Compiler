using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiler.Keywords;

namespace Compiler
{
    class VirtualMachine
    {
        public void Run(List<Object> program)
        {
            List<int> ar = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Stack<int> stack = new Stack<int>();
            int pc = 0;
            Object arg = null;
            while (true)
            {
                var op = program[pc];
                if (pc < program.Count - 1)
                {
                    arg = program[pc + 1];
                }
                if (op.Equals(Instructions.Words.FETCH))
                {
                    if (arg is char || arg is string)
                    {
                        
                        stack.Push(ar[arg.GetHashCode() + 842352731]);
                        pc += 2;
                    }
                }
                else if (op.Equals(Instructions.Words.STORE))
                {
                    ar[arg.GetHashCode() + 842352731] = stack.Pop();
                    pc += 2;
                }
                else if (op.Equals(Instructions.Words.PUSH))
                {
                    if (arg is string)
                    {
                        if (Int32.TryParse((string)arg, out int num))
                        {
                            stack.Push(num);
                        }
                    }
                    pc += 2;
                }
                else if (op.Equals(Instructions.Words.POP))
                {
                    stack.Push(arg.GetHashCode() + 842352731);
                    stack.Pop();
                    pc += 1;
                }
                else if (op.Equals(Instructions.Words.ADD))
                {
                    int last = stack.Pop();
                    int last2 = stack.Pop();
                    stack.Push(last2 + last);
                    pc += 1;
                }
                else if (op.Equals(Instructions.Words.SUB))
                {
                    int last = stack.Pop();
                    int last2 = stack.Pop();
                    stack.Push(last2 - last);
                    pc += 1;
                }
                else if (op.Equals(Instructions.Words.OUT))
                {
                    arg = program[pc + 2];
                    Console.WriteLine(ar[arg.GetHashCode() + 842352731]);
                    pc += 3;
                }
                else if (op.Equals(Instructions.Words.LT))
                {
                    int last = stack.Pop();
                    int last2 = stack.Pop();
                    if (last2 < last)
                    {
                        stack.Push(1);
                    }
                    else
                    {
                        stack.Push(0);
                    }
                    pc += 1;
                }
                else if (op.Equals(Instructions.Words.JZ))
                {
                    if(stack.Pop() == 0 && arg is int)
                    {
                        pc = (int)arg;
                    }
                    else
                    {
                        pc += 2;
                    }   
                }
                else if (op.Equals(Instructions.Words.JNZ))
                {
                    if (stack.Pop() != 0 && arg is int)
                    {
                        pc = Int32.Parse((string)arg);
                    }
                    else
                    {
                        pc += 2;
                    }
                }
                else if (op.Equals(Instructions.Words.JMP) && arg is int)
                {
                   pc = (int)arg;
                }
                else if (op.Equals(Instructions.Words.HALT))
                {
                    break;
                }
            }
            
           
        }
    }
}
