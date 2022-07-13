using DoubleLinkedListAssigment.Graphs;
using System.Collections.Generic;

namespace DoubleLinkedListAssigment
{
    internal class Graph
    {
        private float[,] graphArray;
        private int graphArrayWidth;

        public Graph() => Load();

        private void Load()
        {
            graphArray = new float[8, 8]
            {
                {-1, -1, 3, -1, -1, 6, -1, -1},     //0
                {-1, -1, -1, -1, 11, -1, -1, -1},   //1
                {3, -1, -1, 7, 7, -1, 5, -1},       //2
                {-1, -1, 7, -1, -1, -1, 15, -1},    //3
                {-1, 11, 7, -1, -1, 8, -1, -1},     //4
                {6, -1, -1, -1, 8, -1, -1, 0.5F},   //5
                {-1, -1, 5, 15, -1, -1, -1, -1},    //6
                {-1, -1, -1, -1, -1, 0.5F, -1, -1}, //7
            };

            //graphArray = new float[5, 5]
            //{
            //    {-1, 6, -1, 1, -1},  //A0
            //    {6, -1, 5, 2, 2},    //B1
            //    {-1, 5, -1, -1, 5},  //C2
            //    {1, 2, -1, -1, 1},   //D3
            //    {-1, 2, 5, 1, -1},   //E4
            //};

            graphArrayWidth = graphArray.GetLength(1);
        }

        private bool IsIndexExist(int index) => index >= 0 && index < graphArrayWidth;

        public bool IsPathExists(int source, int dest, int steps)
        {
            if (!IsIndexExist(source) || !IsIndexExist(dest)) return false;
            bool[] visitedVertices = new bool[graphArrayWidth];
            visitedVertices[source] = true;
            return IsPathExists(source, dest, steps, visitedVertices);
        }
        private bool IsPathExists(int source, int dest, int steps, bool[] visitedVertices)
        {
            if (source == dest) return true;
            if (steps <= 0) return false;
            for (int i = 0; i < graphArrayWidth; i++)
            {
                if (graphArray[source, i] > 0 && !visitedVertices[i])
                {
                    visitedVertices[i] = true;
                    if (IsPathExists(i, dest, steps - 1, visitedVertices)) return true;
                    visitedVertices[i] = false;
                }
            }
            return false;
        }

        public List<PathData> FindAllPaths(int source, int dest)
        {
            if (!IsIndexExist(source) || !IsIndexExist(dest)) return null;

            List<PathData> allPathsList = new List<PathData>();
            bool[] visitedVertices = new bool[graphArrayWidth];
            visitedVertices[source] = true;

            FindAllPaths(source, dest, allPathsList, visitedVertices);
            foreach (var path in allPathsList) path.VerticesCollection.Push(source);
            return allPathsList;
        }
        private void FindAllPaths(int source, int dest, List<PathData> allPathsList, bool[] visitedVertices)
        {
            if (source == dest)
            {
                allPathsList.Add(new PathData(graphArrayWidth));
                return;
            }
            for (int i = 0; i < graphArrayWidth; i++)
            {
                if (graphArray[source, i] > 0 && visitedVertices[i] == false)
                {
                    visitedVertices[i] = true;
                    int lenBefore = allPathsList.Count;
                    FindAllPaths(i, dest, allPathsList, visitedVertices);

                    for (int j = lenBefore; j < allPathsList.Count; j++)
                    {
                        allPathsList[j].TotalWeight += graphArray[source, i];
                        allPathsList[j].VerticesCollection.Push(i);
                    }
                    visitedVertices[i] = false;
                }
            }
        }

        public Record[] DijkstraMinPath(int source)
        {
            var recordArray = new Record[graphArrayWidth];
            for (int i = 0; i < recordArray.Length; i++) recordArray[i] = new Record();
            recordArray[source].Weight = 0;
            int currentVertix = source;

            while (!IsAllChecked(recordArray))
            {
                for (int i = 0; i < graphArrayWidth; i++)
                {
                    float distance = graphArray[currentVertix, i];
                    bool vertixIsNotChecked = !recordArray[i].IsChecked;
                    float currentVertixWeight = recordArray[currentVertix].Weight;

                    if (distance > 0 && vertixIsNotChecked && currentVertixWeight + distance < recordArray[i].Weight)
                    {
                        recordArray[i].Weight = currentVertixWeight + distance;
                        recordArray[i].PreviousVertix = currentVertix;
                    }
                }
                recordArray[currentVertix].IsChecked = true;

                float minDistance = int.MaxValue;
                for (int i = 0; i < graphArrayWidth; i++)
                {
                    if (recordArray[i].IsChecked) continue;
                    if (minDistance > recordArray[i].Weight)
                    {
                        minDistance = recordArray[i].Weight;
                        currentVertix = i;
                    }
                }
            }
            return recordArray;
        }
        private bool IsAllChecked(Record[] records)
        {
            foreach (var record in records)
            {
                if (!record.IsChecked) return false;
            }
            return true;
        }
    }
}
