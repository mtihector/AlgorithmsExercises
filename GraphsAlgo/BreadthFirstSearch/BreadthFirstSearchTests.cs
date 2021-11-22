using GraphsAlgo.Common;
using NUnit.Framework;

namespace GraphsAlgo.BreadthFirstSearch
{

    
    [TestFixture]
    internal class BreadthFirstSearchTests
    {
        [Test]
        public void BreadthFirstSearchTestsTraversalTest()
        {
            Dictionary<int, Vertex<int, string>> vertices = new Dictionary<int, Vertex<int, string>>();

            vertices.Add(1, new Vertex<int, string>(1, "a", new List<int>() { 2,3 }));
            vertices.Add(2, new Vertex<int, string>(2, "b", new List<int>() { 4,1,3 }));
            vertices.Add(3, new Vertex<int, string>(3, "c", new List<int>() { 5 }));
            vertices.Add(4, new Vertex<int, string>(4, "d", new List<int>() { 6 }));
            vertices.Add(5, new Vertex<int, string>(5, "e"));
            vertices.Add(6, new Vertex<int, string>(6, "f"));


            new BreadthFirstSearch().Traverse(vertices, 1);



        }
    }
}
