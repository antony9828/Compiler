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
            ["LESS"] = '<',
            ["DOT"] = '.'
        };
    }
}
