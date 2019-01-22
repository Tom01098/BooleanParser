using System;

namespace BooleanParser
{
    public partial class Parser
    {
        private readonly TokenEnumerator tokens;

        public Parser(string input) => tokens = new TokenEnumerator(input);

        public bool Parse()
        {
            return Expression()
                ?? throw tokens.UnexpectedToken();
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

            // TODO I'm pretty sure this loop has something minor wrong about it, definitely rewrite.
            while (!(tokens.Current is null))
            {
                tokens.SetBacktrackPoint();

                var op = tokens.Current;
                tokens.MoveNext();

                if (op is null)
                {
                    tokens.Backtrack();
                    return result;
                }

                var term = Term();
                tokens.MoveNext();

                if (term is null)
                {
                    tokens.Backtrack();
                    return result;
                }

                result = BinaryOperation(result.Value, op, term.Value);
            }

            return result;
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
