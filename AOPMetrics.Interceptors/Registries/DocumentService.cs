using System.Diagnostics;

namespace AOPMetrics.Interceptors.Registries
{
    public class DocumentService : IDocumentService
    {
        public virtual void Start()
        {
            Debug.Write("DocumentService.Start");
        }
    }
}