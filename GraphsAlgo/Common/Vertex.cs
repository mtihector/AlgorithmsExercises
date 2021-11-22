namespace GraphsAlgo.Common
{
    internal class Vertex<K, V>
    {
        private K _key;

        private V _value;
                
        public K Key { get { return _key; } }

        public V Value { get { return _value; } }

        private List<K> _adjacentList;

        public List<K> AdjacentList { get { return _adjacentList; } }
        
        public Vertex<K, V>? Predesor { get; set; }

        public bool Traversed { get; set; }


        public Vertex(K key, V value, List<K>? adjacentList = null)
        {
            _key = key;
            _value = value;

            _adjacentList = adjacentList == null ? new List<K>() : adjacentList;
                        
           Predesor = null;

            Traversed = false;
        }


    }
}
