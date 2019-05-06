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
            Node node = new Node();
            Parser parser = new Parser();
            VirtualMachine virtualMachine = new VirtualMachine();

           
            lexer.GetCharsFromFile();
            Console.WriteLine(lexer.charArray);

            for (int i = 0; i < 30; i++)
            {
                lexer.NextToken();
                Console.WriteLine(lexer.symb + " " + lexer.value);
            }
            

            

            Console.ReadKey();











        }
    }
}
