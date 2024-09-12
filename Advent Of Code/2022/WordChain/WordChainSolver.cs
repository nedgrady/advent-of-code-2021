using QuikGraph;
using QuikGraph.Algorithms;
using System.Reflection.Metadata.Ecma335;

namespace WordChain
{
    internal class WordChainSolver
    {
        private readonly UndirectedGraph<string, Edge<string>> _graph;

        public WordChainSolver(List<string> sourceWords)
        {
            this._graph = new CreateWordGraph(sourceWords).CreateGraph();
        }

        internal IEnumerable<IEnumerable<string>> ShortestPathBetween(string startWord, string endWord)
        {
            var tryGetPaths = _graph.ShortestPathsDijkstra(_ => 1, startWord);

            tryGetPaths(endWord, out var shortestPath);



            var firstResult =
                shortestPath.Select(edge => edge.Source).Union(new[] { endWord });

            return new[] { firstResult };


        }
    }
}