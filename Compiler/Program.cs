using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiler.Keywords;

namespace Compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            string path;
            if (args.Count() == 0)  // Warning : Index was out of the bounds of the array
            {
                path = "Code.txt";
            }
            else
            {
                path = args[0];
            }
            Lexer.filePath = path;
            var lexer = new Lexer();
            lexer.GetCharsFromFile();
            Console.WriteLine(Lexer.charArray);
            var parser = new Parser(Lexer.filePath);
            var compiler = new Compiler();


            var node = parser.Parse();
            var program = compiler.Compile(node);
            foreach(Object obj in program)
            {
                Console.WriteLine(obj);
            }
            G(node);
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
            Console.WriteLine(new string('-', deep)+node.kind);
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
    }
}
