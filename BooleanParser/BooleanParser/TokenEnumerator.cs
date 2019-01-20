using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BooleanParser
{
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

        public string Current => tokens[indexes.Peek()];

        public void SetBacktrackPoint() => indexes.Push(indexes.Peek());

        public void Backtrack() => indexes.Pop();

        public bool MoveNext()
        {
            indexes.Push(indexes.Pop() + 1);
            return tokens.Length > indexes.Peek();
        }
    }
}
