using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Console.WriteLine(Lexer.charArray);

            Console.WriteLine(Lexer.ch);

            Console.ReadKey();

        }
    }
}
