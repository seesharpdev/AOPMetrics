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

        public void Start()
        {
            //Debug.Write("DocumentServiceMetricsAdapter.Start");

            try
            {
                _documentService.Start();
            }
            catch (Exception)
            {
                //Console.WriteLine(e);
                // Increment the exception count
            }
        }
    }
}