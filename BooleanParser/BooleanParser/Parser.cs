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
            throw new NotImplementedException();
        }

        // Term := [UnaryOperator] Factor
        public bool? Term()
        {
            throw new NotImplementedException();
        }

        // Factor := Boolean | ParenthesisedExpression
        public bool? Factor()
        {
            throw new NotImplementedException();
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
