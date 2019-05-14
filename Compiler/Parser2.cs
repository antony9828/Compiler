using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiler.Keywords;

namespace Compiler
{
    class Parser2
    {
        private Lexer lexer;

        public Parser2(string path)
        {
            Lexer.filePath = path;
            lexer = new Lexer();
        }

        private void Error(string message)
        {
            Console.WriteLine("Parser error: " + message);
        }

        private Node Term()
        {
            if(Lexer.symb != null && Lexer.symb == SymbolsAndStatements.Words.ID.ToString())
            {
                Node n = new Node(ParserEnums.Words.VAR, Lexer.value, null, null, null);
                lexer.NextToken();
                return n;
            }
            else if (Lexer.symb != null && Lexer.symb == SymbolsAndStatements.Words.NUM.ToString())
            {
                Node n = new Node(ParserEnums.Words.CONST, Lexer.value, null, null, null);
                lexer.NextToken();
                return n;
            }
            else
            {
                return ParenExpr();
            }
        }

        private Node Summa()
        {
            Node n = Term();
            ParserEnums.Words kind;
            while(Lexer.symb == SymbolsAndStatements.Words.PLUS.ToString() ||
                Lexer.symb == SymbolsAndStatements.Words.MINUS.ToString())
            {
                if(Lexer.symb == SymbolsAndStatements.Words.PLUS.ToString())
                {
                    kind = ParserEnums.Words.ADD;
                }
                else
                {
                    kind = ParserEnums.Words.SUB;
                }
                lexer.NextToken();
                n = new Node(kind, null, n, Term(), null);
            }
            return n;
        }

        private Node Test()
        {
            Node n = Summa();
            if(Lexer.symb == SymbolsAndStatements.Words.LESS.ToString())
            {
                lexer.NextToken();
                n = new Node(ParserEnums.Words.LT, null, n, Summa(), null);
            }
            return n;
        }

        private Node Expr()
        {
            if(Lexer.symb != SymbolsAndStatements.Words.ID.ToString())
            {
                return Test();
            }
            Node n = Test();
            if (n != null && n.kind == ParserEnums.Words.VAR &&
                Lexer.symb == SymbolsAndStatements.Words.EQUAL.ToString())
            {
                lexer.NextToken();
                n = new Node(ParserEnums.Words.SET, null, n, Expr(), null);
            }
            return n;
        }

        private Node ParenExpr()
        {
            if(Lexer.symb != SymbolsAndStatements.Words.LPAR.ToString())
            {
                Error("\"(\" expected");
            }
            lexer.NextToken();
            Node n = Expr();
            if (Lexer.symb != SymbolsAndStatements.Words.RPAR.ToString())
            {
                Error("\")\" expected");
            }
            lexer.NextToken();
            return n;
        }

        Node n = null;

        private Node Statement()
        {
            if(Lexer.symb == SymbolsAndStatements.Words.IF.ToString())
            {
                n = new Node(ParserEnums.Words.IF1, null, null, null, null);
                lexer.NextToken();
                n.op1 = ParenExpr();
                n.op2 = Statement();
                if(Lexer.symb == SymbolsAndStatements.Words.ELSE.ToString())
                {
                    n.kind = ParserEnums.Words.IF2;
                    lexer.NextToken();
                    n.op3 = Statement();
                }
            }
            else if (Lexer.symb == SymbolsAndStatements.Words.WHILE.ToString())
            {
                n = new Node(ParserEnums.Words.WHILE, null, null, null, null);
                lexer.NextToken();
                n.op1 = ParenExpr();
                n.op2 = Statement();
            }
            else if (Lexer.symb == SymbolsAndStatements.Words.SEMICOLON.ToString())
            {
                n = new Node(ParserEnums.Words.EMPTY, null, null, null, null);
                lexer.NextToken();
            }
            else if (Lexer.symb == SymbolsAndStatements.Words.PRINT.ToString())
            {
                n = new Node(ParserEnums.Words.EMPTY, null, null, null, null);
                lexer.NextToken();
                n.op1 = Expr();
            }
            else if (Lexer.symb == SymbolsAndStatements.Words.LBRA.ToString())
            {
                n = new Node(ParserEnums.Words.EMPTY, null, null, null, null);
                lexer.NextToken();
                while(Lexer.symb != SymbolsAndStatements.Words.RBRA.ToString())
                {
                    n = new Node(ParserEnums.Words.SEQ, null, n, Statement(), null);
                }
                lexer.NextToken();
            }
            else
            {
                n = new Node(ParserEnums.Words.EXPR, null, Expr(), null, null);
                if(Lexer.symb != SymbolsAndStatements.Words.SEMICOLON.ToString())
                {
                    Error("\";\" expected");
                }
                lexer.NextToken();
            }
            return n;
        }

        public Node Parse()
        {
            lexer.NextToken();
            Node node = new Node(ParserEnums.Words.PROG, null, Statement(), null, null);
            if(Lexer.symb != SymbolsAndStatements.Words.EOF.ToString())
            {
                Error("Invalid statement syntax");
            }
            return n;
        }
    }
}
