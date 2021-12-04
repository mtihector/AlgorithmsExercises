using GraphsAlgo.Tries;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsAlgo.TriesTests
{
    [TestFixture]
    public class TrieTest
    {
        [Test]
        public void AddWordTest()
        {
            Trie root = new Trie();

            root.AddWord("Gato");
            root.AddWord("Gatito");
            root.AddWord("Gata");
            root.AddWord("Gatita");


            root.PrintWords();

            var trie = root.GetTrie('G');
            Assert.IsTrue(trie.Value.Equals('g'));


             trie = root.GetTrie('a');
        }

    }
}
