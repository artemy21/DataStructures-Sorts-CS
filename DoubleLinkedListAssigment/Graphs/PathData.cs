namespace DoubleLinkedListAssigment.Graphs
{
    internal class PathData
    {
        public float TotalWeight { get; set; }
        public StackArray<int> VerticesCollection { get; }

        public PathData(int capacity) => VerticesCollection = new StackArray<int>(capacity);

        public override string ToString()
        {
            string str = VerticesCollection.ToString().Replace(",", " =>");
            return str.Substring(0, str.Length - 3);
        }
    }
}
