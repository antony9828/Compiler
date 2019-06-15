using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiler.Keywords;
//asasdasdassdas
namespace Compiler
{
    public class Parser
    {
        private void Error(string message)
        {
            Console.WriteLine("Parser error: " + message);
        }

        int tokenPosition = 0;
        List<Token> tokens;
        Lexer lexer = new Lexer();

        

        private Node Term()
        {
            if(tokens[tokenPosition].key == SymbolsAndStatements.Words.ID.ToString())
            {
                var n = new Node(ParserEnums.Words.VAR, tokens[tokenPosition].value);
                tokenPosition++;

                if ((n.value.Length == 2 || n.value.Length == 3) && tokens[tokenPosition].key == SymbolsAndStatements.Words.DOT.ToString())
                {
                    tokenPosition++;
                }
                else if(n.value.Length == 2)
                {
                    Error(" \".\" expected");
                }
                return n;
            }
            else if (tokens[tokenPosition].key != null && tokens[tokenPosition].key == SymbolsAndStatements.Words.NUM.ToString())
            {
                Node n = new Node(ParserEnums.Words.CONST, tokens[tokenPosition].value);
                tokenPosition++;
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
            ParserEnums.Words kind__;

            while (tokens[tokenPosition].key == SymbolsAndStatements.Words.PLUS.ToString() ||
                tokens[tokenPosition].key == SymbolsAndStatements.Words.MINUS.ToString())
            {
                if (Lexer.symb == SymbolsAndStatements.Words.PLUS.ToString())
                {
                    kind__ = ParserEnums.Words.ADD;  
                } else
                {
                    kind__ = ParserEnums.Words.SUB;
                }
                tokenPosition++;
                n = new Node(kind__, null, n, Term(), null);
            }
            return n;
        }

        private Node Test()
        {
            Node n = Summa();
            if (tokens[tokenPosition].key == SymbolsAndStatements.Words.LESS.ToString())
            {
                tokenPosition++;
                n = new Node(ParserEnums.Words.LT, null, n, Summa(), null);
            }
            return n;
        }

        private Node Expr(bool isPrint = false)
        {
            Node n = null;
            if (tokens[tokenPosition].key != SymbolsAndStatements.Words.ID.ToString())
            {
                return Test();
            }
            if (isPrint)
            {
                var value = tokens[tokenPosition].value;
                n = new Node(ParserEnums.Words.VAR, value, null, null, null);
                tokenPosition++;
            }
            else
            {
                n = Test();
            }

            if (n != null && n.kind == ParserEnums.Words.VAR && 
                (tokens[tokenPosition].key == SymbolsAndStatements.Words.EQUAL.ToString() ||
                 tokens[tokenPosition].key == SymbolsAndStatements.Words.GETLL.ToString() ||
                 tokens[tokenPosition].key == SymbolsAndStatements.Words.CONTAINSHS.ToString() ))
            {
                if(tokens[tokenPosition].key == SymbolsAndStatements.Words.GETLL.ToString())
                {
                    tokenPosition++;
                    n = new Node(ParserEnums.Words.GETFROMLL, null, null, n, ParenExpr());
                } else if (tokens[tokenPosition].key == SymbolsAndStatements.Words.CONTAINSHS.ToString())
                {
                    tokenPosition++;
                    n = new Node(ParserEnums.Words.CONTAINSINHS, null, null, n, ParenExpr());
                }
                else
                {
                    tokenPosition++;
                    Node n1 = Expr();
                    if(n1.op2 != null && n1.op3 != null)
                    {
                        n = new Node(ParserEnums.Words.SET, null, n, n1.op2, n1.op3);
                    }
                    else
                    {
                        n = new Node(ParserEnums.Words.SET, null, n, n1, null);
                    }
                }
            }
            else if (n != null && n.kind == ParserEnums.Words.VAR &&
                tokens[tokenPosition].key == SymbolsAndStatements.Words.ADDLL.ToString())
            {
                tokenPosition++;
                n = new Node(ParserEnums.Words.ADDTOLL, null, n, ParenExpr(), null);
            }
            else if (n != null && n.kind == ParserEnums.Words.VAR &&
                tokens[tokenPosition].key == SymbolsAndStatements.Words.REMOVELL.ToString())
            {
                tokenPosition++;
                n = new Node(ParserEnums.Words.REMOVEFROMLL, null, n, ParenExpr(), null);
            }
            else if (n != null && n.kind == ParserEnums.Words.VAR &&
                tokens[tokenPosition].key == SymbolsAndStatements.Words.ADDHS.ToString())
            {
                tokenPosition++;
                n = new Node(ParserEnums.Words.ADDTOHS, null, n, ParenExpr(), null);
            }
            else if (n != null && n.kind == ParserEnums.Words.VAR &&
                tokens[tokenPosition].key == SymbolsAndStatements.Words.REMOVEHS.ToString())
            {
                tokenPosition++;
                n = new Node(ParserEnums.Words.REMOVEFROMHS, null, n, ParenExpr(), null);
            }
            return n?? throw new Exception("Node is null");
        }

        private Node ParenExpr(bool isPrint = false)
        {

            if (tokens[tokenPosition].key != SymbolsAndStatements.Words.LPAR.ToString())
            {
                Error("\"(\" expected");
            }
            tokenPosition++;

            //Console.WriteLine("" "\"{0}\" = \"{1}\"", Lexer.symb, Lexer.value);

            Node n = Expr(isPrint);

            if(tokens[tokenPosition].key != SymbolsAndStatements.Words.RPAR.ToString())
            {
                Error("\")\" expected");
            }

            tokenPosition++;
            return n;
        }

        private Node IfStatement()
        {
           
            var n = new Node(ParserEnums.Words.IF1, null, null, null, null);
            tokenPosition++;
            n.op1 = ParenExpr();
            n.op2 = Statement();
            if ((tokens[tokenPosition].key == SymbolsAndStatements.Words.ELSE.ToString())){
                n.kind = ParserEnums.Words.IF2;
                tokenPosition++;
                n.op3 = Statement();
            }
            return n;
        }

        private Node WhileStatement()
        {
            var n = new Node(ParserEnums.Words.WHILE, null, null, null, null);
            tokenPosition++;
            n.op1 = ParenExpr();
            n.op2 = Statement();
            return n;
        }

        private Node SemicolonSymb()
        {
            var n = new Node(ParserEnums.Words.EMPTY, null, null, null, null);
            tokenPosition++;
            return n;
        }

        private Node PrintStatment()
        {
            var n = new Node(ParserEnums.Words.PRINT, null, null, null, null);
            tokenPosition++;
            n.op1 = ParenExpr(true);
            return n;
        }

        private Node LbraSymb()
        {
            var n = new Node(ParserEnums.Words.EMPTY, null, null, null, null);
            tokenPosition++;
            while (tokens[tokenPosition].key != SymbolsAndStatements.Words.RBRA.ToString())
            {
                n = new Node(ParserEnums.Words.SEQ, null, n, Statement(), null);
            }
            tokenPosition++;
            return n;
        }

        private Node OtherStatements()
        {
            var n = new Node(ParserEnums.Words.EXPR, null, Expr(), null, null);
            if (tokens[tokenPosition].key != SymbolsAndStatements.Words.SEMICOLON.ToString())
            {
                Error("\";\" expected");
            }
            tokenPosition++;
            return n;
        }

        private Node Statement()
        {
            if (tokens[tokenPosition].key == SymbolsAndStatements.Words.IF.ToString())
            {
                return IfStatement();
            }
            else if (tokens[tokenPosition].key == SymbolsAndStatements.Words.WHILE.ToString())
            {
                return WhileStatement();
            }
            else if (tokens[tokenPosition].key == SymbolsAndStatements.Words.SEMICOLON.ToString())
            {
                return SemicolonSymb();
            }
            else if (tokens[tokenPosition].key == SymbolsAndStatements.Words.PRINT.ToString())
            {
                return PrintStatment();
            }
            else if (tokens[tokenPosition].key == SymbolsAndStatements.Words.LBRA.ToString())
            {
                return LbraSymb();
            }
            else
            {
                return OtherStatements();
            }
        }
        

        public Node Parse(List<Token> tokens)
        {
            this.tokens = tokens;
           
            Node node = new Node(ParserEnums.Words.PROG, null, Statement(), null, null);
            if (tokens[tokenPosition].key != SymbolsAndStatements.Words.EOF.ToString())
            {
                Error("Invalid statement syntax");
            }
            return node;
        }
    }
}
