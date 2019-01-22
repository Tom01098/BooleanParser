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
    }
}
