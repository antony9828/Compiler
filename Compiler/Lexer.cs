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
        public string charArray = null;
        public int charPosition = -1;
        public string filePath = @"C:\Users\Anton Pushkin\Desktop\Compiler\Compiler\Compiler\Code.txt";
        public string symb;
        public string value;
        public char ch;
        public char EOFchar = '$';
        public char charValue;

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
            NextChar();
        }

        public void NextChar()
        {
            charPosition++;

            if (charPosition < charArray.Length)
            {
                charValue = charArray[charPosition];
            }
            else
            {
                charValue = EOFchar;
            }
        }





        public void NextToken()
        {
            string tmpSymb = null;
            string tmpValue = null;

            while (tmpSymb == null)
            {
                if (charValue == EOFchar)
                {
                    tmpSymb = Enum.GetName(typeof(SymbolsAndStatements.Words),
                                SymbolsAndStatements.Words.EOF);

                }
                else if (Char.IsDigit(charValue))
                {
                    int intValue = 0;
                    while (charValue != EOFchar && Char.IsDigit(charValue))
                    {
                        intValue = intValue * 10 + (int)charValue - 48;
                        NextChar();
                    }
                    tmpValue = intValue.ToString();
                    tmpSymb = SymbolsAndStatements.Words.NUM.ToString();


                }
                else if (Char.IsLetter(charValue))
                {
                    string ident = "";
                    while (charValue != EOFchar && Char.IsLetter(charValue))
                    {
                        ident += Char.ToLower(charValue);
                        NextChar();
                    }
                    ident = ident.ToUpper();
                    if (Enum.IsDefined(typeof(SymbolsAndStatements.Words), ident))
                    {
                        tmpSymb = ident;
                        tmpValue = ident.ToLower();
                    } else if (ident.Length == 1)
                    {
                        tmpSymb = SymbolsAndStatements.Words.ID.ToString();
                        tmpValue = ident.ToLower();
                    }
                    else
                    {
                        Error("Unexpected symbol: " + ident);
                    }
                }
                else if(Symbols.dic.ContainsValue(charValue))
                {
                    foreach (KeyValuePair<string, char> item in Symbols.dic)
                    {
                        if (charValue == item.Value)
                        {
                            tmpSymb = Symbols.dic.FirstOrDefault(x => x.Value == charValue).Key;
                            tmpValue = charValue.ToString();
                        }
                    }
                    NextChar();
                }
                else
                {
                    Error("Lexer error: unknown symbol - " + charValue);
                    NextChar();
                }
            }
            symb = tmpSymb;
            value = tmpValue;
            
        }
    }
}
