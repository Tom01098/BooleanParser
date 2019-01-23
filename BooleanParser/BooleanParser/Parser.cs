namespace BooleanParser
{
    public partial class Parser
    {
        private readonly TokenEnumerator tokens;

        public Parser(string input) => tokens = new TokenEnumerator(input);

        public bool Parse()
        {
            var expression = TryParseMethod(Expression);

            if (expression is null || !(tokens.Current is null))
            {
                throw tokens.UnexpectedToken();
            }

            return expression.Value;
        }

        // Expression := Term {BinaryOperator Term}
        public bool? Expression()
        {
            var result = TryParseMethod(Term);

            if (result is null)
            {
                return null;
            }

            while (true)
            {
                var opAndTerm = TryParseMethod(GetOpAndTerm);

                if (opAndTerm is null)
                {
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
                var op = tokens.Current;

                if (op is null)
                {
                    return null;
                }

                tokens.MoveNext();

                var term = TryParseMethod(Term);

                if (term is null)
                {
                    return null;
                }

                return (op, term.Value);
            }
        }

        // Term := [UnaryOperator] Factor
        public bool? Term()
        {
            bool isNot = false;

            if (tokens.Current == "NOT")
            {
                isNot = true;
                tokens.MoveNext();
            }

            var factor = TryParseMethod(Factor);

            return isNot ? Not(factor) : factor;
        }

        // Factor := Boolean | ParenthesisedExpression
        public bool? Factor()
        {
            return TryParseMethods(Boolean, ParenthesisedExpression);
        }

        // ParenthesisedExpression = '(' Expression ')'
        public bool? ParenthesisedExpression()
        {
            if (tokens.Current != "(")
            {
                return null;
            }

            tokens.MoveNext();

            var expression = TryParseMethods(Expression);

            if (tokens.Current != ")")
            {
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
