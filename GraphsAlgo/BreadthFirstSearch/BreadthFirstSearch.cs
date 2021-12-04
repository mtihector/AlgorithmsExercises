using GraphsAlgo.Common;
using GraphsAlgo.Tries;

namespace GraphsAlgo.BreadthFirstSearch
{
    public class BreadthFirstSearch
    {
        public void Traverse<K, V>(Dictionary<K, Vertex<K, V>> vertices, K initialKey) where K : notnull
        {

            foreach (var vertex in vertices)
            {
                vertex.Value.Traversed = false;
                vertex.Value.Predesor = null;
            }

            Queue<K> queue = new Queue<K>();

            queue.Enqueue(initialKey);

            Vertex<K, V> root = vertices[initialKey];

            root.Traversed = true;
            root.Predesor = null;

            while (queue.Count > 0)
            {
                K key = queue.Dequeue();

                Vertex<K, V> vertex = vertices[key];
                Console.WriteLine(vertex.Value.ToString());


                foreach (K currentRefKey in vertex.AdjacentList)
                {
                    Vertex<K, V> refVertex = vertices[currentRefKey];
                    if (!refVertex.Traversed)
                    {
                        refVertex.Traversed = true;
                        refVertex.Predesor = vertex;
                        queue.Enqueue(currentRefKey);
                    }
                }


            }
        }



        internal bool SearchPath<K, V>(Dictionary<K, Vertex<K, V>> vertices, K initialKey, V[] pathToFind) where K : notnull
        {

            bool found = false;

            foreach (var vertex in vertices)
            {
                vertex.Value.Traversed = false;
                vertex.Value.Predesor = null;
            }

            Queue<Tuple<K, int>> queue = new Queue<Tuple<K, int>>();

            queue.Enqueue(new Tuple<K, int>(initialKey, 0));

            Vertex<K, V> root = vertices[initialKey];


            root.Traversed = true;
            root.Predesor = null;



            while (queue.Count > 0)
            {

                Tuple<K, int> key = queue.Dequeue();

                Vertex<K, V> vertex = vertices[key.Item1];

                int cdeep = key.Item2;






                if (vertex.Value.Equals(pathToFind[cdeep]))
                {

                    if (cdeep == pathToFind.Length - 1)
                    {
                        return true;
                    }


                    foreach (K currentRefKey in vertex.AdjacentList)
                    {
                        Vertex<K, V> refVertex = vertices[currentRefKey];
                        if (!refVertex.Traversed && (refVertex.Value.Equals(pathToFind[cdeep + 1])))
                        {
                            refVertex.Traversed = true;
                            refVertex.Predesor = vertex;
                            queue.Enqueue(new Tuple<K, int>(currentRefKey, cdeep + 1));
                        }
                    }

                }

            }


            return false;
        }


        internal void SearchPath(Dictionary<Tuple<int, int>, Vertex<Tuple<int, int>, char>> vertices, Tuple<int, int> initialKey, Trie trie, Dictionary<string, bool> ixWords)
        {
          

            foreach (var vertex in vertices)
            {
                vertex.Value.Traversed = false;
                vertex.Value.Predesor = null;
            }


             SearchPath(vertices, vertices[initialKey], trie, ixWords, "");
        }



        private void SearchPath(Dictionary<Tuple<int, int>, Vertex<Tuple<int, int>, char>> vertices, Vertex<Tuple<int, int>, char> currentVertex, Trie trie, Dictionary<string, bool> ixWords, string currentWord)
        {

            currentVertex.Traversed = true;
            currentVertex.Predesor = null;
            currentWord += trie.Value?.ToString() ?? "" ;
            if (trie.IsWord)
            {
                ixWords[currentWord] = true;
            }

            if (trie.Children.ContainsKey(currentVertex.Value))
            {
                foreach (var currentRefKey in currentVertex.AdjacentList)
                {
                    var refVertex = vertices[currentRefKey];

                    if (!refVertex.Traversed)
                    {
                        SearchPath(vertices, refVertex, trie.Children[currentVertex.Value], ixWords, currentWord);                        
                    }
                }
            }
            
        }
    }
}
