using System;

namespace BooleanParser
{
    public partial class Parser
    {
        private bool? Not(bool? val) => 
            val.HasValue ? !val.Value : val;

        private bool? BinaryOperation(bool lhs, string op, bool rhs)
        {
            switch (op)
            {
                case "AND":
                    return lhs && rhs;
                case "OR":
                    return lhs || rhs;
                case "XOR":
                    return lhs ^ rhs;
                case "NOR":
                    return !(lhs || rhs);
                case "NAND":
                    return !(lhs && rhs);
                default:
                    return null;
            }
        }

        private T? TryParseMethod<T>(Func<T?> parseMethod)
            where T : struct
        {
            tokens.SetBacktrackPoint();

            T? result = parseMethod();

            if (result is null)
            {
                tokens.Backtrack();
            }

            return result;
        }

        private T? TryParseMethods<T>(params Func<T?>[] parseMethods)
            where T : struct
        {
            foreach (var parseMethod in parseMethods)
            {
                T? result = parseMethod();

                if (!(result is null))
                {
                    return result;
                }
            }

            return null;
        }
    }
}
