using GraphsAlgo.Common;

namespace GraphsAlgo.DeepFirstStack
{
    internal class DeepFirstStackTraversal
    {
        internal void Traverse<K, V>(Dictionary<K, Vertex<K, V>> vertices, K initialKey) where K : notnull
        {
            foreach (var vertex in vertices)
            {
                vertex.Value.Traversed = false;
                vertex.Value.Predesor = null;

            }

            Stack<K> stack = new Stack<K>();
            
            stack.Push(initialKey); 

            var root = vertices[initialKey];
            root.Traversed = true;
            root.Predesor = null;

            while (stack.Count > 0)
            {
                K key = stack.Pop();
                Vertex<K, V> vertex = vertices[key];
                Console.WriteLine(vertex.Value);
                

                foreach (K currentKey in vertex.AdjacentList)
                {
                    var refVertex = vertices[currentKey];
                    if (!refVertex.Traversed)
                    {
                        refVertex.Traversed = true;
                        refVertex.Predesor = vertex;
                        stack.Push(currentKey);
                    }
                }
            }
            
        }
    }
}
