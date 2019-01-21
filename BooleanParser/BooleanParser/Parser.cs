using System;

namespace BooleanParser
{
    public class Parser
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
            var result = Term();
            // Loop
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

            if (factor.HasValue)
            {
                return isNot
                    ? !factor.Value
                    : factor.Value;
            }

            tokens.Backtrack();
            return null;
        }

        // Factor := Boolean | ParenthesisedExpression
        public bool? Factor()
        {
            tokens.SetBacktrackPoint();

            var result = Boolean() ?? ParenthesisedExpression();

            if (!result.HasValue)
            {
                tokens.Backtrack();
            }

            return result;
        }

        // ParenthesisedExpression = '(' Expression ')'
        public bool? ParenthesisedExpression()
        {
            throw new NotImplementedException();
        }

        // Boolean := 'TRUE' | 'FALSE'
        public bool? Boolean()
        {
            if (tokens.Current == "TRUE")
            {
                return true;
            }
            else if (tokens.Current == "FALSE")
            {
                return false;
            }
            else
            {
                return null;
            }
        }
    }
}
