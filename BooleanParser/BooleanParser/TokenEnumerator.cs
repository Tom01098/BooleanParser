using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BooleanParser
{
    /// <summary>
    /// Supports enumerating over tokens (words and parenthesis) while also
    /// supporting stack-based backtracking.
    /// </summary>
    public class TokenEnumerator
    {
        private readonly Stack<int> indexes = new Stack<int>();
        private readonly string[] tokens;

        public TokenEnumerator(string str)
        {
            // Get all the tokens from the string
            // Mmmmmm what a lovely Regex
            tokens = Regex.Split(str, @"([ \(\)])")
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();

            indexes.Push(0);
        }

        /// <summary>
        /// The current token
        /// </summary>
        public string Current => tokens[indexes.Peek()];

        /// <summary>
        /// Push the current point onto the stack as a point that can be 
        /// backtracked to using <see cref="Backtrack"/>.
        /// </summary>
        public void SetBacktrackPoint() => indexes.Push(indexes.Peek());

        /// <summary>
        /// Go back to the appropriate point set using 
        /// <see cref="SetBacktrackPoint"/>
        /// </summary>
        public void Backtrack() => indexes.Pop();

        /// <summary>
        /// Move to the next token
        /// </summary>
        /// 
        /// <returns>
        /// Can <see cref="Current"/> be used?
        /// </returns>
        public bool MoveNext()
        {
            indexes.Push(indexes.Pop() + 1);
            return tokens.Length > indexes.Peek();
        }

        /// <summary>
        /// Create a <see cref="UnexpectedTokenException"/> for the current
        /// token.
        /// </summary>
        /// 
        /// <returns>
        /// An <see cref="UnexpectedTokenException"/>.
        /// </returns>
        public UnexpectedTokenException UnexpectedToken() => 
            new UnexpectedTokenException(tokens[indexes.Peek()]);
    }
}
