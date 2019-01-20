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
            throw new NotImplementedException();
        }

        // Term := [UnaryOperator] Factor
        public bool? Term()
        {
            throw new NotImplementedException();
        }

        // Factor := Boolean | '(' Expression ')'
        public bool? Factor()
        {
            throw new NotImplementedException();
        }

        // Boolean := 'TRUE' | 'FALSE'
        public bool? Boolean()
        {
            throw new NotImplementedException();
        }
    }
}
