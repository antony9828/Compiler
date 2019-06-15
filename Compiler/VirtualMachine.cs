using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiler.Keywords;
using Compiler.LinkedList;
using Compiler.HashSet;

namespace Compiler
{
    class VirtualMachine
    {
        public void Run(List<Object> program)
        {
            var ar = Enumerable.Repeat(0, 26).ToArray();
            var llAr = Enumerable.Range(0, 26).Select(_ => new LList<int>()).ToArray();
            var hsAr = Enumerable.Range(0, 26).Select(_ => new HSet<int>()).ToArray();
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
                        int index = arg.ToString()[0] - 'a';
                        stack.Push(ar[index]);
                    }
                    pc += 2;
                }
                else if (op.Equals(Instructions.Words.STORE))
                {
                    //ar[arg.GetHashCode() + 842352731] = stack.Pop();
                    if(arg.ToString().Length == 2)
                    {
                        var position = arg.ToString()[0] - 'a';
                        var list = llAr[position];
                        if(list == null)
                        {
                            list = new LList<int>();
                        }
                        list.Add(stack.Pop());
                        llAr[position] = list;
                    }
                    else if(arg.ToString().Length == 3)
                    {
                        var position = arg.ToString()[0] - 'a';

                        var list = hsAr[position];
                        if(list == null)
                        {
                            list = new HSet<int>();
                        }
                        list.Add(stack.Pop());
                        hsAr[position] = list;
                    }
                    else
                    {
                        int index = arg.ToString()[0] - 'a';
                        ar[index] = stack.Pop();
                    }
                    pc += 2;
                }
                else if (op.Equals(Instructions.Words.PUSH))
                {
                    if (arg is string)
                    {
                        //if (Int32.TryParse((string)arg, out int num))
                        //{
                        //    stack.Push(num);
                        //}
                        var args = arg.ToString().Split(',');
                        if (args.Length == 2)
                        {
                            var position = args[0][0] - 'a';
                            if(args[0].Length == 2)
                            {
                                if (Int32.TryParse((string)args[1], out int num))
                                {
                                    var value = llAr[position].Get(num);
                                    stack.Push((int)value);
                                }
                            }
                            else if ((args[0].Length == 3))
                            {
                                if (Int32.TryParse((string)args[1], out int num))
                                {
                                    if (hsAr[position].Contains(num))
                                    {
                                        var value = 1;
                                        stack.Push(value);
                                    } else
                                    {
                                        var value = 0;
                                        stack.Push(value);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (Int32.TryParse((string)arg, out int num))
                            {
                                stack.Push(num);
                            }
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
                    string result;
                    if(arg.ToString().Length == 2)
                    {
                        var position = arg.ToString()[0] - 'a';
                        var list = llAr[position];
                        result = $"({string.Join(", ", list.GetElements())})";
                    }
                    else if (arg.ToString().Length == 3)
                    {
                        var position = arg.ToString()[0] - 'a';
                        var set = hsAr[position];
                        result = $"{{{string.Join(", ", set.GetElements())}}}";
                    }
                    else
                    {
                        var position = arg.ToString()[0] - 'a';
                        result = ar[position].ToString();
                    }
                    Console.WriteLine(result);
                    pc += 3;
                }
                else if (op.Equals(Instructions.Words.DELFROMLL))
                {
                    var position = arg.ToString()[0] - 'a';
                    var list = llAr[position];
                    list.Remove(stack.Pop());
                    llAr[position] = list;
                    pc += 2;
                }
                else if (op.Equals(Instructions.Words.DELFROMHS))
                {
                    var position = arg.ToString()[0] - 'a';
                    var set = hsAr[position];
                    set.Remove(stack.Pop());
                    hsAr[position] = set;
                    pc += 2;
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
