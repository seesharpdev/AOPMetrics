using System.Diagnostics;

namespace AOPMetrics.Interceptors.Registries
{
    public class DocumentService : IDocumentService
    {
        public virtual void Start(string firstArgument, string secondArgument)
        {
            Debug.Write("DocumentService.Start");
        }
    }
}