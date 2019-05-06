using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Keywords
{
    class Symbols
    {
        public static Dictionary<string, char> dic = new Dictionary<string, char>
        {
            ["LBRA"] = '{',
            ["RBRA"] = '}',
            ["EQUAL"] = '=',
            ["SEMICOLON"] = ';',
            ["LPAR"] = '(',
            ["RPAR"] = ')',
            ["PLUS"] = '+',
            ["MINUS"] = '-',
            ["LESS"] = '<'
        };
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

        public static string CharSymbol(char ch)
        {
            switch (ch)
            {
                case (char)Words.EQUAL:
                    return (Enum.GetName(typeof(Words),Words.EQUAL));
                case (char)Words.LBRA:
                    return (Enum.GetName(typeof(Words), Words.LBRA));
                case (char)Words.LESS:
                    return (Enum.GetName(typeof(Words), Words.LESS));
                case (char)Words.LPAR:
                    return (Enum.GetName(typeof(Words), Words.LPAR));
                case (char)Words.MINUS:
                    return (Enum.GetName(typeof(Words), Words.MINUS));
                case (char)Words.PLUS:
                    return (Enum.GetName(typeof(Words), Words.PLUS));
                case (char)Words.RBRA:
                    return (Enum.GetName(typeof(Words), Words.RBRA));
                case (char)Words.RPAR:
                    return (Enum.GetName(typeof(Words), Words.RPAR));
                case (char)Words.SEMICOLON:
                    return (Enum.GetName(typeof(Words), Words.SEMICOLON));
                default:
                    return "ERROR";
            }
        }
    }
}
