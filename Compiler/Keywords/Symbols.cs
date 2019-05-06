using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Keywords
{
    class Symbols
    {
        public enum Words
        {
            LBRA = '{',
            RBRA = '}',
            EQUAL = '=',
            SEMICOLON = ';',
            LPAR = '(',
            RPAR = ')',
            PLUS = '+',
            MINUS = '-',
            LESS = '<'
        }

        public bool IsChar(char ch)
        {
            switch (ch)
            {
                case (char)Words.EQUAL:
                    return true;
                case (char)Words.LBRA:
                    return true;
                case (char)Words.LESS:
                    return true;
                case (char)Words.LPAR:
                    return true;
                case (char)Words.MINUS:
                    return true;
                case (char)Words.PLUS:
                    return true;
                case (char)Words.RBRA:
                    return true;
                case (char)Words.RPAR:
                    return true;
                case (char)Words.SEMICOLON:
                    return true;
                default:
                    return false;
            }
        }
    }
}
