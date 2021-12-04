namespace GraphsAlgo.Tries
{
    public class Trie
    {
        const int LetterCount = 27; //include ñ, //Store words in lowercase
        private Dictionary<char, Trie> _childrend;
        private readonly char? _value;
        private readonly bool _isWord;

        

        public char? Value { get { return _value; } }
        public bool IsWord { get { return _isWord; } }
        public IReadOnlyDictionary<char, Trie> Children { get { return _childrend; } }
        

        public Trie(char? value = null, bool isWord = false)
        {
            _childrend = new Dictionary<char, Trie>(LetterCount);
            this._value = value;
            _isWord = isWord;
            
        }

        public void PrintWords(string currentWord="")
        {
            if (this._value != null)
            {
                currentWord += this._value.ToString();
            }
            if (this.IsWord)
            {
                Console.WriteLine(currentWord);
            }
            foreach (Trie child in _childrend.Values)
            {

                child.PrintWords(currentWord);
            }
        }

        public void AddWord(string word)
        {
            AddWord(word.ToLower(), 0);
        }

        protected void AddWord(string word, int ix)
        {
            if (ix == word.Length)
            {
                return;
            }
            char currentLetter = word[ix++];
            if (!_childrend.ContainsKey(currentLetter))
            {
                _childrend.Add(currentLetter, new Trie(currentLetter, word.Length == ix));
            }

            _childrend[currentLetter].AddWord(word, ix);
        }

        public Trie? GetTrie(char letter)
        {
            letter = Char.ToLower(letter);
            Trie? retTrie = null;
            _childrend.TryGetValue(letter, out retTrie);

            return retTrie;
        }
    }
}
