namespace DoubleLinkedListAssigment.Graphs
{
    internal class Record
    {
        private bool isChecked;
        public bool IsChecked { get => isChecked; set => isChecked = value; }

        private float weight;
        public float Weight { get => weight; set => weight = value; }

        private int previousVertix;
        public int PreviousVertix { get => previousVertix; set => previousVertix = value; }

        public Record()
        {
            isChecked = false;
            weight = int.MaxValue;
            previousVertix = -1;
        }

        public override string ToString() => $"\tDistance {weight}\t Previous Vertix {previousVertix}";
    }
}
