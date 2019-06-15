using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Keywords
{
    class ConditionalStatements
    {
        public static Dictionary<string, string> dic = new Dictionary<string, string>
        {
            ["IF"] = "if",
            ["ELSE"] = "else",
            ["WHILE"] = "while",
            ["PRINT"] = "print",
            ["ADDLL"] = "add",
            ["GETLL"] = "get",
            ["REMOVELL"] = "remove",
            ["CONTAINSHS"] = "contains",
            ["REMOVEHS"] = "removeelement",
            ["ADDHS"] = "addelement"
        };

        //public bool IsKeyWord(string keyWordString)
        //{
        //    var coompareResult = false;
        //    foreach (keyWordValue in Values())
        //}
    }
}
