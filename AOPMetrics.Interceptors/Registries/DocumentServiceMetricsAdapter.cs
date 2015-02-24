using System;
using System.Diagnostics;

namespace AOPMetrics.Interceptors.Registries
{
    public class DocumentServiceMetricsAdapter : IDocumentService
    {
        private readonly IDocumentService _documentService;

        public DocumentServiceMetricsAdapter(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public void Start(string firstArgument, string secondArgument)
        {
            //Debug.Write("DocumentServiceMetricsAdapter.Start");

            try
            {
                _documentService.Start(firstArgument, secondArgument);
            }
            catch (Exception)
            {
                //Console.WriteLine(e);
                // Increment the exception count
            }
        }
    }
}