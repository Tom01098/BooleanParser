namespace BooleanParser
{
    /// <summary>
    /// Parses a given string of input as a boolean expression and produces
    /// the boolean output of the expression
    /// </summary>
    public partial class Parser
    {
        private readonly TokenEnumerator tokens;

        /// <summary>
        /// Construct a parser with a given boolean expression
        /// </summary>
        /// 
        /// <param name="input">
        /// The boolean expression to parse
        /// </param>
        public Parser(string input) => tokens = new TokenEnumerator(input);

        /// <summary>
        /// Parse the boolean expression given on instantiation
        /// </summary>
        /// 
        /// <returns>
        /// The result of parsing the boolean expression
        /// </returns>
        /// 
        /// <exception cref="UnexpectedTokenException">
        /// Thrown when a token was unexpected during parsing
        /// </exception>
        public bool Parse()
        {
            if (tokens.Current is null)
            {
                throw new UnexpectedTokenException("<EOF>");
            }

            var expression = ParseWith(Expression);

            if (expression is null || !(tokens.Current is null))
            {
                throw tokens.UnexpectedToken();
            }

            return expression.Value;
        }

        // Expression := Term {BinaryOperator Term}
        private bool? Expression()
        {
            var result = ParseWith(Term);

            if (result is null)
            {
                return null;
            }

            while (true)
            {
                var opAndTerm = ParseWith(GetOpAndTerm);

                if (opAndTerm is null)
                {
                    return result;
                }

                (string op, bool term) = opAndTerm.Value;

                result = BinaryOperation(result.Value, op, term);

                if (result is null)
                {
                    return null;
                }
            }

            (string op, bool term)? GetOpAndTerm()
            {
                var op = tokens.Current;

                if (op is null)
                {
                    return null;
                }

                tokens.MoveNext();

                var term = ParseWith(Term);

                if (term is null)
                {
                    return null;
                }

                return (op, term.Value);
            }
        }

        // Term := [UnaryOperator] Factor
        private bool? Term()
        {
            bool isNot = false;

            if (tokens.Current == "NOT")
            {
                isNot = true;
                tokens.MoveNext();
            }

            var factor = ParseWith(Factor);

            return isNot ? Not(factor) : factor;
        }

        // Factor := Boolean | ParenthesisedExpression
        private bool? Factor()
        {
            return ParseWith(Boolean, ParenthesisedExpression);
        }

        // ParenthesisedExpression := '(' Expression ')'
        private bool? ParenthesisedExpression()
        {
            if (tokens.Current != "(")
            {
                return null;
            }

            tokens.MoveNext();

            var expression = ParseWith(Expression);

            if (tokens.Current != ")")
            {
                return null;
            }

            tokens.MoveNext();

            return expression;
        }

        // Boolean := 'TRUE' | 'FALSE'
        private bool? Boolean()
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
