using GraphsAlgo.Common;
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

            var bfs = new BreadthFirstSearch.BreadthFirstSearch();
            var foundWords = SearchPath(Graph, index, words);
            Assert.IsTrue(foundWords.Count == 0, "Se esperaban 0 palabras encontradas");
            PrintOutput(foundWords);
        }


        private static List<string> SearchPath(Dictionary<Tuple<int, int>, Vertex<Tuple<int, int>, char>> Graph, Dictionary<char, List<Tuple<int, int>>> index, string[] words)
        {
            var foundWords = new List<string>();
            var bfs = new BreadthFirstSearch.BreadthFirstSearch();

            // only search in nodes with the start letter 

       
            foreach (string currentWord in words)
            {
                char firstLetter = currentWord[0];

                if (index.ContainsKey(firstLetter))
                {
                    foreach (Tuple<int, int> vertexKey in index[firstLetter])
                    {
                        bool found = bfs.SearchPath(Graph, vertexKey, currentWord.ToCharArray());
                       
                        if (found)
                        {
                            foundWords.Add(currentWord);
                           
                           
                        }

                    }
                }
            }

          

            return foundWords;
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
