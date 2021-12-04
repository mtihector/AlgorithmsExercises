using GraphsAlgo.Common;
using GraphsAlgo.Tries;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsAlgo.Problems
{
    /**
     * Given an m x n board of characters and a list of strings words, return all words on the board.
     * Each word must be constructed from letters of sequentially adjacent cells, where adjacent cells 
     * are horizontally or vertically neighboring. The same letter cell may not be used more than once in a word.
     * 
     * Input: board = [["o","a","a","n"],["e","t","a","e"],["i","h","k","r"],["i","f","l","v"]], words = ["oath","pea","eat","rain"]
     * Output: ["eat","oath"]
     */
    [TestFixture]
    internal class WordSearchII
    {

        /**
         * Input: board = [["o","a","a","n"],["e","t","a","e"],["i","h","k","r"],["i","f","l","v"]], words = ["oath","pea","eat","rain"]
         *  Output: ["eat","oath"]
         */
        [Test]
        public void Test1()
        {
            Dictionary<Tuple<int, int>, Vertex<Tuple<int, int>, char>> Graph = GetGraphForTest1(out Dictionary<char, List<Tuple<int, int>>> index);


            String[] words = new string[] { "oath", "pea", "eat", "rain" };

            var foundWords = SearchPath(Graph, index, words);

            Assert.IsTrue(foundWords.Count == 2, "Se esperaban 2 palabras encontradas");
            Assert.IsTrue(foundWords.Contains("oath"), "Se esperaba la palabra oath");
            Assert.IsTrue(foundWords.Contains("eat"), "Se esperaba la palabra pea");

            PrintOutput(foundWords);
        }



        [Test]
        public void Test2()
        {
            Dictionary<Tuple<int, int>, Vertex<Tuple<int, int>, char>> Graph = GetGraphForTest2(out Dictionary<char, List<Tuple<int, int>>> index);

            //["abcb"]
            string[] words = new string[] { "abcb" };

          //  var bfs = new BreadthFirstSearch.BreadthFirstSearch();
            var foundWords = SearchPath(Graph, index, words);
            Assert.IsTrue(foundWords.Count == 0, "Se esperaban 0 palabras encontradas");
            PrintOutput(foundWords);
        }


        [Test]
        public void Test3()
        {
            Dictionary<Tuple<int, int>, Vertex<Tuple<int, int>, char>> Graph = GetGraphForTest3(out Dictionary<char, List<Tuple<int, int>>> index);

            var words = new[]
           {
                "ababababaa", "ababababab", "ababababac", "ababababad", "ababababae", "ababababaf", "ababababag",
                "ababababah", "ababababai", "ababababaj", "ababababak", "ababababal", "ababababam", "ababababan",
                "ababababao", "ababababap", "ababababaq", "ababababar", "ababababas", "ababababat", "ababababau",
                "ababababav", "ababababaw", "ababababax", "ababababay", "ababababaz", "ababababba", "ababababbb",
                "ababababbc", "ababababbd", "ababababbe", "ababababbf", "ababababbg", "ababababbh", "ababababbi",
                "ababababbj", "ababababbk", "ababababbl", "ababababbm", "ababababbn", "ababababbo", "ababababbp",
                "ababababbq", "ababababbr", "ababababbs", "ababababbt", "ababababbu", "ababababbv", "ababababbw",
                "ababababbx", "ababababby", "ababababbz", "ababababca", "ababababcb", "ababababcc", "ababababcd",
                "ababababce", "ababababcf", "ababababcg", "ababababch", "ababababci", "ababababcj", "ababababck",
                "ababababcl", "ababababcm", "ababababcn", "ababababco", "ababababcp", "ababababcq", "ababababcr",
                "ababababcs", "ababababct", "ababababcu", "ababababcv", "ababababcw", "ababababcx", "ababababcy",
                "ababababcz", "ababababda", "ababababdb", "ababababdc", "ababababdd", "ababababde", "ababababdf",
                "ababababdg", "ababababdh", "ababababdi", "ababababdj", "ababababdk", "ababababdl", "ababababdm",
                "ababababdn", "ababababdo", "ababababdp", "ababababdq", "ababababdr", "ababababds", "ababababdt",
                "ababababdu", "ababababdv", "ababababdw", "ababababdx", "ababababdy", "ababababdz", "ababababea",
                "ababababeb", "ababababec", "ababababed", "ababababee", "ababababef", "ababababeg", "ababababeh",
                "ababababei", "ababababej", "ababababek", "ababababel", "ababababem", "ababababen", "ababababeo",
                "ababababep", "ababababeq", "ababababer", "ababababes", "ababababet", "ababababeu", "ababababev",
                "ababababew", "ababababex", "ababababey", "ababababez", "ababababfa", "ababababfb", "ababababfc",
                "ababababfd", "ababababfe", "ababababff", "ababababfg", "ababababfh", "ababababfi", "ababababfj",
                "ababababfk", "ababababfl", "ababababfm", "ababababfn", "ababababfo", "ababababfp", "ababababfq",
                "ababababfr", "ababababfs", "ababababft", "ababababfu", "ababababfv", "ababababfw", "ababababfx",
                "ababababfy", "ababababfz", "ababababga", "ababababgb", "ababababgc", "ababababgd", "ababababge",
                "ababababgf", "ababababgg", "ababababgh", "ababababgi", "ababababgj", "ababababgk", "ababababgl",
                "ababababgm", "ababababgn", "ababababgo", "ababababgp", "ababababgq", "ababababgr", "ababababgs",
                "ababababgt", "ababababgu", "ababababgv", "ababababgw", "ababababgx", "ababababgy", "ababababgz",
                "ababababha", "ababababhb", "ababababhc", "ababababhd", "ababababhe", "ababababhf", "ababababhg",
                "ababababhh", "ababababhi", "ababababhj", "ababababhk", "ababababhl", "ababababhm", "ababababhn",
                "ababababho", "ababababhp", "ababababhq", "ababababhr", "ababababhs", "ababababht", "ababababhu",
                "ababababhv", "ababababhw", "ababababhx", "ababababhy", "ababababhz", "ababababia", "ababababib",
                "ababababic", "ababababid", "ababababie", "ababababif", "ababababig", "ababababih", "ababababii",
                "ababababij", "ababababik", "ababababil", "ababababim", "ababababin", "ababababio", "ababababip",
                "ababababiq", "ababababir", "ababababis", "ababababit", "ababababiu", "ababababiv", "ababababiw",
                "ababababix", "ababababiy", "ababababiz", "ababababja", "ababababjb", "ababababjc", "ababababjd",
                "ababababje", "ababababjf", "ababababjg", "ababababjh", "ababababji", "ababababjj", "ababababjk",
                "ababababjl", "ababababjm", "ababababjn", "ababababjo", "ababababjp", "ababababjq", "ababababjr",
                "ababababjs", "ababababjt", "ababababju", "ababababjv", "ababababjw", "ababababjx", "ababababjy",
                "ababababjz", "ababababka", "ababababkb", "ababababkc", "ababababkd", "ababababke", "ababababkf",
                "ababababkg", "ababababkh", "ababababki", "ababababkj", "ababababkk", "ababababkl", "ababababkm",
                "ababababkn", "ababababko", "ababababkp", "ababababkq", "ababababkr", "ababababks", "ababababkt",
                "ababababku", "ababababkv", "ababababkw", "ababababkx", "ababababky", "ababababkz", "ababababla",
                "abababablb", "abababablc", "ababababld", "abababable", "abababablf", "abababablg", "abababablh",
                "ababababli", "abababablj", "abababablk", "ababababll", "abababablm", "ababababln", "abababablo",
                "abababablp", "abababablq", "abababablr", "ababababls", "abababablt", "abababablu", "abababablv",
                "abababablw", "abababablx", "abababably", "abababablz", "ababababma", "ababababmb", "ababababmc",
                "ababababmd", "ababababme", "ababababmf", "ababababmg", "ababababmh", "ababababmi", "ababababmj",
                "ababababmk", "ababababml", "ababababmm", "ababababmn", "ababababmo", "ababababmp", "ababababmq",
                "ababababmr", "ababababms", "ababababmt", "ababababmu", "ababababmv", "ababababmw", "ababababmx",
                "ababababmy", "ababababmz", "ababababna", "ababababnb", "ababababnc", "ababababnd", "ababababne",
                "ababababnf", "ababababng", "ababababnh", "ababababni", "ababababnj", "ababababnk", "ababababnl",
                "ababababnm", "ababababnn", "ababababno", "ababababnp", "ababababnq", "ababababnr", "ababababns",
                "ababababnt", "ababababnu", "ababababnv", "ababababnw", "ababababnx", "ababababny", "ababababnz",
                "ababababoa", "ababababob", "ababababoc", "ababababod", "ababababoe", "ababababof", "ababababog",
                "ababababoh", "ababababoi", "ababababoj", "ababababok", "ababababol", "ababababom", "ababababon",
                "ababababoo", "ababababop", "ababababoq", "ababababor", "ababababos", "ababababot", "ababababou",
                "ababababov", "ababababow", "ababababox", "ababababoy", "ababababoz", "ababababpa", "ababababpb",
                "ababababpc", "ababababpd", "ababababpe", "ababababpf", "ababababpg", "ababababph", "ababababpi",
                "ababababpj", "ababababpk", "ababababpl", "ababababpm", "ababababpn", "ababababpo", "ababababpp",
                "ababababpq", "ababababpr", "ababababps", "ababababpt", "ababababpu", "ababababpv", "ababababpw",
                "ababababpx", "ababababpy", "ababababpz", "ababababqa", "ababababqb", "ababababqc", "ababababqd",
                "ababababqe", "ababababqf", "ababababqg", "ababababqh", "ababababqi", "ababababqj", "ababababqk",
                "ababababql", "ababababqm", "ababababqn", "ababababqo", "ababababqp", "ababababqq", "ababababqr",
                "ababababqs", "ababababqt", "ababababqu", "ababababqv", "ababababqw", "ababababqx", "ababababqy",
                "ababababqz", "ababababra", "ababababrb", "ababababrc", "ababababrd", "ababababre", "ababababrf",
                "ababababrg", "ababababrh", "ababababri", "ababababrj", "ababababrk", "ababababrl", "ababababrm",
                "ababababrn", "ababababro", "ababababrp", "ababababrq", "ababababrr", "ababababrs", "ababababrt",
                "ababababru", "ababababrv", "ababababrw", "ababababrx", "ababababry", "ababababrz", "ababababsa",
                "ababababsb", "ababababsc", "ababababsd", "ababababse", "ababababsf", "ababababsg", "ababababsh",
                "ababababsi", "ababababsj", "ababababsk", "ababababsl", "ababababsm", "ababababsn", "ababababso",
                "ababababsp", "ababababsq", "ababababsr", "ababababss", "ababababst", "ababababsu", "ababababsv",
                "ababababsw", "ababababsx", "ababababsy", "ababababsz", "ababababta", "ababababtb", "ababababtc",
                "ababababtd", "ababababte", "ababababtf", "ababababtg", "ababababth", "ababababti", "ababababtj",
                "ababababtk", "ababababtl", "ababababtm", "ababababtn", "ababababto", "ababababtp", "ababababtq",
                "ababababtr", "ababababts", "ababababtt", "ababababtu", "ababababtv", "ababababtw", "ababababtx",
                "ababababty", "ababababtz", "ababababua", "ababababub", "ababababuc", "ababababud", "ababababue",
                "ababababuf", "ababababug", "ababababuh", "ababababui", "ababababuj", "ababababuk", "ababababul",
                "ababababum", "ababababun", "ababababuo", "ababababup", "ababababuq", "ababababur", "ababababus",
                "ababababut", "ababababuu", "ababababuv", "ababababuw", "ababababux", "ababababuy", "ababababuz",
                "ababababva", "ababababvb", "ababababvc", "ababababvd", "ababababve", "ababababvf", "ababababvg",
                "ababababvh", "ababababvi", "ababababvj", "ababababvk", "ababababvl", "ababababvm", "ababababvn",
                "ababababvo", "ababababvp", "ababababvq", "ababababvr", "ababababvs", "ababababvt", "ababababvu",
                "ababababvv", "ababababvw", "ababababvx", "ababababvy", "ababababvz", "ababababwa", "ababababwb",
                "ababababwc", "ababababwd", "ababababwe", "ababababwf", "ababababwg", "ababababwh", "ababababwi",
                "ababababwj", "ababababwk", "ababababwl", "ababababwm", "ababababwn", "ababababwo", "ababababwp",
                "ababababwq", "ababababwr", "ababababws", "ababababwt", "ababababwu", "ababababwv", "ababababww",
                "ababababwx", "ababababwy", "ababababwz", "ababababxa", "ababababxb", "ababababxc", "ababababxd",
                "ababababxe", "ababababxf", "ababababxg", "ababababxh", "ababababxi", "ababababxj", "ababababxk",
                "ababababxl", "ababababxm", "ababababxn", "ababababxo", "ababababxp", "ababababxq", "ababababxr",
                "ababababxs", "ababababxt", "ababababxu", "ababababxv", "ababababxw", "ababababxx", "ababababxy",
                "ababababxz", "ababababya", "ababababyb", "ababababyc", "ababababyd", "ababababye", "ababababyf",
                "ababababyg", "ababababyh", "ababababyi", "ababababyj", "ababababyk", "ababababyl", "ababababym",
                "ababababyn", "ababababyo", "ababababyp", "ababababyq", "ababababyr", "ababababys", "ababababyt",
                "ababababyu", "ababababyv", "ababababyw", "ababababyx", "ababababyy", "ababababyz", "ababababza",
                "ababababzb", "ababababzc", "ababababzd", "ababababze", "ababababzf", "ababababzg", "ababababzh",
                "ababababzi", "ababababzj", "ababababzk", "ababababzl", "ababababzm", "ababababzn", "ababababzo",
                "ababababzp", "ababababzq", "ababababzr", "ababababzs", "ababababzt", "ababababzu", "ababababzv",
                "ababababzw", "ababababzx", "ababababzy", "ababababzz"
            };

            //  var bfs = new BreadthFirstSearch.BreadthFirstSearch();
            var foundWords = SearchPath(Graph, index, words);
            Assert.IsTrue(foundWords.Count == 1, "Se esperaban 1 palabras encontradas");
            Assert.IsTrue(foundWords.First().Equals("ababababab") , "Se esperaba la palabra ababababab se recibipo  "+ foundWords.First());
            PrintOutput(foundWords);
        }


        private static List<string> SearchPath(Dictionary<Tuple<int, int>, Vertex<Tuple<int, int>, char>> Graph, Dictionary<char, List<Tuple<int, int>>> index, string[] words)
        {
            var foundWords = new Dictionary<string, bool>();

            foreach (var word in words)
            {
                foundWords[word] = false;    
            }

            var bfs = new BreadthFirstSearch.BreadthFirstSearch();
            var trieRoot = new Trie();

            foreach (var word in words)
            {
                trieRoot.AddWord(word);
            }


            foreach (Trie trie in trieRoot.Children.Values)
            {
                char firstLetter = trie.Value!.Value;
                if (index.ContainsKey(firstLetter))
                {
                    foreach (Tuple<int, int> vertexKey in index[firstLetter])
                    {
                        bfs.SearchPath(Graph, vertexKey, trieRoot, foundWords);
                    }
                }
            }



            return foundWords.Where(c=> c.Value).Select(c => c.Key).ToList();
        }

        private static Dictionary<Tuple<int, int>, Vertex<Tuple<int, int>, char>> GetGraphForTest1(out Dictionary<char, List<Tuple<int, int>>> index)
        {

            index = new Dictionary<char, List<Tuple<int, int>>>();

            // jagged arrays https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/arrays/jagged-arrays
            char[][] board = new char[][]
            {
               new char[] { 'o', 'a', 'a', 'n' },
               new char[] { 'e', 't', 'a', 'e' },
               new char[] { 'i', 'h', 'k', 'r' },
               new char[] { 'i', 'f', 'l', 'v' }
            };

            Dictionary<Tuple<int, int>, Vertex<Tuple<int, int>, char>> vertices = new Dictionary<Tuple<int, int>, Vertex<Tuple<int, int>, char>>();

            int edgeCounter = 0;
            for (int y = 0; y < board.Length; y++)
            {
                for (int x = 0; x < board[y].Length; x++)
                {
                    Tuple<int, int> cp = Tuple.Create(x, y);

                    List<Tuple<int, int>> adjList = new List<Tuple<int, int>>();

                    if (x < (board[y].Length - 1))
                    {
                        adjList.Add(Tuple.Create(x + 1, y));

                    }

                    if (x > 0)
                    {
                        adjList.Add(Tuple.Create(x - 1, y));
                    }

                    if (y < (board.Length - 1))
                    {
                        adjList.Add(Tuple.Create(x, y + 1));
                    }
                    if (y > 0)
                    {
                        adjList.Add(Tuple.Create(x, y - 1));
                    }

                    edgeCounter += adjList.Count;
                    char letter = board[y][x];

                    if (!index.ContainsKey(letter))
                    {
                        index.Add(letter, new List<Tuple<int, int>>());
                    }

                    index[letter].Add(cp);

                    vertices.Add(cp, new Vertex<Tuple<int, int>, char>(cp, letter, adjList));
                }


            }
            // Console.WriteLine(edgeCounter);
            return vertices;
        }




        private static Dictionary<Tuple<int, int>, Vertex<Tuple<int, int>, char>> GetGraphForTest2(out Dictionary<char, List<Tuple<int, int>>> index)
        {

            index = new Dictionary<char, List<Tuple<int, int>>>();

            // jagged arrays https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/arrays/jagged-arrays
            char[][] board = new char[][]
            {
               new char[] { 'a', 'b'},
               new char[] { 'c', 'd'},

            };

            Dictionary<Tuple<int, int>, Vertex<Tuple<int, int>, char>> vertices = new Dictionary<Tuple<int, int>, Vertex<Tuple<int, int>, char>>();

            int edgeCounter = 0;
            for (int y = 0; y < board.Length; y++)
            {
                for (int x = 0; x < board[y].Length; x++)
                {
                    Tuple<int, int> cp = Tuple.Create(x, y);

                    List<Tuple<int, int>> adjList = new List<Tuple<int, int>>();

                    if (x < (board[y].Length - 1))
                    {
                        adjList.Add(Tuple.Create(x + 1, y));

                    }

                    if (x > 0)
                    {
                        adjList.Add(Tuple.Create(x - 1, y));
                    }

                    if (y < (board.Length - 1))
                    {
                        adjList.Add(Tuple.Create(x, y + 1));
                    }
                    if (y > 0)
                    {
                        adjList.Add(Tuple.Create(x, y - 1));
                    }

                    edgeCounter += adjList.Count;
                    char letter = board[y][x];

                    if (!index.ContainsKey(letter))
                    {
                        index.Add(letter, new List<Tuple<int, int>>());
                    }

                    index[letter].Add(cp);

                    vertices.Add(cp, new Vertex<Tuple<int, int>, char>(cp, letter, adjList));
                }


            }
            // Console.WriteLine(edgeCounter);
            return vertices;
        }

        private static Dictionary<Tuple<int, int>, Vertex<Tuple<int, int>, char>> GetGraphForTest3(out Dictionary<char, List<Tuple<int, int>>> index)
        {

            index = new Dictionary<char, List<Tuple<int, int>>>();

            // jagged arrays https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/arrays/jagged-arrays
            char[][] board = new[]
            {
                new[] { 'b', 'a', 'b', 'a', 'b', 'a', 'b', 'a', 'b', 'a' },
                new[] { 'a', 'b', 'a', 'b', 'a', 'b', 'a', 'b', 'a', 'b' },
                new[] { 'b', 'a', 'b', 'a', 'b', 'a', 'b', 'a', 'b', 'a' },
                new[] { 'a', 'b', 'a', 'b', 'a', 'b', 'a', 'b', 'a', 'b' },
                new[] { 'b', 'a', 'b', 'a', 'b', 'a', 'b', 'a', 'b', 'a' },
                new[] { 'a', 'b', 'a', 'b', 'a', 'b', 'a', 'b', 'a', 'b' },
                new[] { 'b', 'a', 'b', 'a', 'b', 'a', 'b', 'a', 'b', 'a' },
                new[] { 'a', 'b', 'a', 'b', 'a', 'b', 'a', 'b', 'a', 'b' },
                new[] { 'b', 'a', 'b', 'a', 'b', 'a', 'b', 'a', 'b', 'a' },
                new[] { 'a', 'b', 'a', 'b', 'a', 'b', 'a', 'b', 'a', 'b' }
            };

            Dictionary<Tuple<int, int>, Vertex<Tuple<int, int>, char>> vertices = new Dictionary<Tuple<int, int>, Vertex<Tuple<int, int>, char>>();

            int edgeCounter = 0;
            for (int y = 0; y < board.Length; y++)
            {
                for (int x = 0; x < board[y].Length; x++)
                {
                    Tuple<int, int> cp = Tuple.Create(x, y);

                    List<Tuple<int, int>> adjList = new List<Tuple<int, int>>();

                    if (x < (board[y].Length - 1))
                    {
                        adjList.Add(Tuple.Create(x + 1, y));

                    }

                    if (x > 0)
                    {
                        adjList.Add(Tuple.Create(x - 1, y));
                    }

                    if (y < (board.Length - 1))
                    {
                        adjList.Add(Tuple.Create(x, y + 1));
                    }
                    if (y > 0)
                    {
                        adjList.Add(Tuple.Create(x, y - 1));
                    }

                    edgeCounter += adjList.Count;
                    char letter = board[y][x];

                    if (!index.ContainsKey(letter))
                    {
                        index.Add(letter, new List<Tuple<int, int>>());
                    }

                    index[letter].Add(cp);

                    vertices.Add(cp, new Vertex<Tuple<int, int>, char>(cp, letter, adjList));
                }


            }
            // Console.WriteLine(edgeCounter);
            return vertices;
        }




        private void PrintOutput(List<string> foundWords)
        {
            bool isFirst = true;
            Console.Write("[");
            foreach (string word in foundWords)
            {
                if (isFirst)
                {

                    isFirst = false;
                }
                else
                {
                    Console.Write(",");
                }
                Console.Write($"\"{word}\"");
            }
            Console.Write("]");
        }
    }
}
