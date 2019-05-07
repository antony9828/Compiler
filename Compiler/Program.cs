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
            Lexer.filePath = @"C:\Users\Anton Pushkin\Desktop\Compiler\Compiler\Compiler\Code.txt";
            var lexer = new Lexer();
            lexer.GetCharsFromFile();
            Console.WriteLine(Lexer.charArray);
            var parser = new Parser(Lexer.filePath);
            var compiler = new Compiler();


            var node = parser.Parse();
            var node1 = parser.Parse();
            //var node1 = parser.Parse();
            var program = compiler.Compile(node);
            Console.WriteLine(program.Count());
            foreach(Object obj in program)
            {
                Console.WriteLine(obj);
            }
            
            var virtualMachine = new VirtualMachine();
            virtualMachine.Run(program);





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
    }
}
