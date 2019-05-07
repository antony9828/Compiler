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
            Compiler compiler = new Compiler();
            Lexer lexer = new Lexer();
            VirtualMachine virtualMachine = new VirtualMachine();

           
            lexer.GetCharsFromFile();
            Console.WriteLine(Lexer.charArray);
            Console.WriteLine(SymbolsAndStatements.Words.ID.ToString());
            for (int i = 0; i < 30; i++)
            {
                lexer.NextToken();
                Console.WriteLine("\"{0}\" = \"{1}\"", Lexer.symb, Lexer.value);
            }
            

            

            Console.ReadKey();











        }
    }
}
