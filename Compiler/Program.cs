using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiler.Keywords;
using Compiler.LinkedList;

namespace Compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            //LList list = new LList();
            //list.InsertFront(list, 1);
            //list.InsertFront(list, 2);
            //list.InsertFront(list, 3);
            //list.InsertFront(list, 4);
            //list.InsertFront(list, 5);
            //list.InsertFront(list, 6);
            //list.InsertLast(list, 7);
            //list.InsertLast(list, 8);
            //list.InsertFront(list, 9);
            //list.DeleteNodebyKey(list, 3);

            //list.PrintAllNodes();

            string path;
            if (args.Count() == 0)  // Warning : Index was out of the bounds of the array
            {
                path = "Code.txt";
            }
            else
            {
                path = args[0];
                if (!System.IO.File.Exists(args[0]))
                {
                    Console.WriteLine($"Path {path} is incorrect");
                    return;
                }
            }

            var needLog = IsNeedLogs(args);

            Lexer.filePath = path;
            var lexer = new Lexer();
            lexer.GetCharsFromFile();

            

            var parser = new Parser();


            var compiler = new Compiler();


            var tokens = lexer.GetTokens();

            if (needLog)
            {
                foreach (var token in tokens)
                {
                    Console.WriteLine($"'{token.key}':'{token.value}'");
                }
            }

            var node = parser.Parse(tokens);

            if (needLog)
                G(node);

            var program = compiler.Compile(node);

            if (needLog)
            {
                var i = 0;
                foreach (Object obj in program)
                {
                    Console.WriteLine($"{i++}: {obj}");
                }
            }
            var virtualMachine = new VirtualMachine();
            virtualMachine.Run(program);




            //runConsole.WriteLine(program[program.Count() - 1]);







            //Compiler compiler = new Compiler();
            //Lexer lexer = new Lexer();
            //VirtualMachine virtualMachine = new VirtualMachine();


            //lexer.GetCharsFromFile();
            //Console.WriteLine(Lexer.charArray);
            //Console.WriteLine(SymbolsAndStatements.Words.ID.ToString());


            //for (int i = 0; i < 30; i++)
            //{
            //    lexer.NextToken();
            //    Console.WriteLine("\"{0}\" = \"{1}\"", Lexer.symb, Lexer.value);
            //}




            Console.ReadKey();












        }

        static void G(Node node, int deep = 0)
        {
            if (node.kind == ParserEnums.Words.EMPTY) return;
            Console.WriteLine(new string('-', deep) + node.kind);
            if (node.op1 != null)
            {
                G(node.op1, deep + 1);
            }
            if (node.op2 != null)
            {
                G(node.op2, deep + 1);
            }
            if (node.op3 != null)
            {
                G(node.op3, deep + 1);
            }
        }

        static bool IsNeedLogs(string[] args)
        {
            return args.Contains("-log");
        }
    }
}
