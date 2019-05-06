using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Keywords
{
    class SymbolsAndStatements
    {
        public static string[] words = {
            "NUM",
            "ID",
            "IF",
            "ELSE",
            "WHILE",
            "LBRA",
            "RBRA",
            "LPAR",
            "RPAR",
            "PLUS",
            "MINUS",
            "LESS",
            "EQUAL",
            "SEMICOLON",
            "EOF",
            "PRINT" };
        public enum Words
        {
            NUM,
            ID,
            IF,
            ELSE,
            WHILE,
            LBRA,
            RBRA,
            LPAR,
            RPAR,
            PLUS,
            MINUS,
            LESS,
            EQUAL,
            SEMICOLON,
            EOF,
            PRINT
        }
    }
}
