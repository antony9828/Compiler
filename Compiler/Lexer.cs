using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Compiler.Keywords;

namespace Compiler
{
    class Lexer
    {
        public static string charArray = null;
        public static int charPosition = -1;
        public static string filePath = @"C:\Users\Anton Pushkin\Desktop\Compiler\Compiler\Compiler\Code.txt";
        public static SymbolsAndStatements symb;
        public static string value;
        public static char ch;

        void Error(string message)
        {
            Console.WriteLine("Lexer error: " + message);
            Environment.Exit(0);
        }

        public void GetCharsFromFile()
        {
            char ch;
            int Tchar = 0;
            StreamReader reader;
            reader = new StreamReader(filePath);
            do
            {
                ch = (char)reader.Read();
                charArray += ch;
                Tchar++;
            } while (!reader.EndOfStream);
            reader.Close();
            reader.Dispose();
            charArray = Regex.Replace(charArray, @"\t|\n|\r| ", "");
        }

        public char NextChar()
        {
            if(charArray == null || filePath != null)
            {
                GetCharsFromFile();
            }
            else
            {
                Error("File path is incorrect");
            }

            charPosition++;

            if(charPosition < charArray.Length)
            {
                return charArray[charPosition];
            }
            else
            {
                return '$';
            }
        }

        char charValue = ' ';

        public void NextToken()
        {
            SymbolsAndStatements.Words tmpSymb = 0;
            string tmpValue = null;

            while (tmpSymb == 0)
            {
                if (charValue == '$')
                {
                    tmpSymb = SymbolsAndStatements.Words.EOF;
                }
                else
                {
                    foreach(Char ss in Enum.GetValues(typeof(Symbols.Words)))
                    {
                        if (charValue == ss)
                        {
                            //tmpSymb == Symbols[charValue];

                        }
                    }
                }
            }
        }
    }
}
