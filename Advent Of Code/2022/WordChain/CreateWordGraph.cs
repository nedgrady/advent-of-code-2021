using QuikGraph;

namespace WordChain
{
    internal class CreateWordGraph
    {
        private readonly IEnumerable<string> _sourceWords;

        public CreateWordGraph(IEnumerable<string> sourceWords)
        {
            this._sourceWords = sourceWords;
        }

        internal UndirectedGraph<string, Edge<string>> CreateGraph()
        {
            var graph = new UndirectedGraph<string, Edge<string>>();

            graph.AddVertexRange(_sourceWords);

            foreach (var currentWord in _sourceWords)
            {
                foreach(var potentialEdgeTargetWord in _sourceWords)
                {
                    if (currentWord.IsCloseTo(potentialEdgeTargetWord))
                    {
                        graph.AddEdge(new Edge<string>(currentWord, potentialEdgeTargetWord));
                    }
                }
            }

            return graph;
        }

    }
}