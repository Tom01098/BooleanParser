using System;

namespace BooleanParser
{
    public partial class Parser
    {
        private readonly TokenEnumerator tokens;

        public Parser(string input) => tokens = new TokenEnumerator(input);

        public bool Parse()
        {
            var expression = Expression();

            if (expression is null || !(tokens.Current is null))
            {
                throw tokens.UnexpectedToken();
            }

            return expression.Value;
        }

        // Expression := Term {BinaryOperator Term}
        public bool? Expression()
        {
            tokens.SetBacktrackPoint();

            var result = Term();

            if (result is null)
            {
                tokens.Backtrack();
                return null;
            }

            while (true)
            {
                tokens.SetBacktrackPoint();
                var opAndTerm = GetOpAndTerm();

                if (opAndTerm is null)
                {
                    tokens.Backtrack();
                    return result;
                }

                (string op, bool term) = opAndTerm.Value;

                result = BinaryOperation(result.Value, op, term);

                if (result is null)
                {
                    throw tokens.UnexpectedToken();
                }
            }

            (string op, bool term)? GetOpAndTerm()
            {
                tokens.SetBacktrackPoint();

                var op = tokens.Current;

                if (op is null)
                {
                    tokens.Backtrack();
                    return null;
                }

                tokens.MoveNext();

                var term = Term();

                if (term is null)
                {
                    tokens.Backtrack();
                    return null;
                }

                return (op, term.Value);
            }
        }

        // Term := [UnaryOperator] Factor
        public bool? Term()
        {
            tokens.SetBacktrackPoint();

            bool isNot = false;

            if (tokens.Current == "NOT")
            {
                isNot = true;
                tokens.MoveNext();
            }

            var factor = Factor();

            if (factor is null)
            {
                tokens.Backtrack();
            }

            return isNot ? Not(factor) : factor;
        }

        // Factor := Boolean | ParenthesisedExpression
        public bool? Factor()
        {
            tokens.SetBacktrackPoint();

            var boolean = Boolean();

            if (!(boolean is null))
            {
                return boolean;
            }

            var parenthesisedExpression = ParenthesisedExpression();

            if (parenthesisedExpression is null)
            {
                tokens.Backtrack();
            }

            return parenthesisedExpression;
        }

        // ParenthesisedExpression = '(' Expression ')'
        public bool? ParenthesisedExpression()
        {
            tokens.SetBacktrackPoint();

            if (tokens.Current != "(")
            {
                tokens.Backtrack();
                return null;
            }

            tokens.MoveNext();

            var expression = Expression();

            if (tokens.Current != ")")
            {
                tokens.Backtrack();
                return null;
            }

            tokens.MoveNext();

            return expression;
        }

        // Boolean := 'TRUE' | 'FALSE'
        public bool? Boolean()
        {
            if (tokens.Current == "TRUE")
            {
                tokens.MoveNext();
                return true;
            }
            else if (tokens.Current == "FALSE")
            {
                tokens.MoveNext();
                return false;
            }
            else
            {
                return null;
            }
        }
    }
}
