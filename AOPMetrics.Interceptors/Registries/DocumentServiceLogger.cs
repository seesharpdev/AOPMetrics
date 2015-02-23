using System.Diagnostics;
using AOPMetrics.Core.Interfaces.Logging;
using StructureMap;
using StructureMap.Interceptors;

namespace AOPMetrics.Interceptors.Registries
{
    public class DocumentServiceLogger : IDocumentService
    {
        private readonly IDocumentService _target;
        private readonly ILog _logger;

        public DocumentServiceLogger(IDocumentService target)
        {
            _target = target;
        }

        //public DocumentServiceLogger(IDocumentService target, ILog logger)
        //{
        //    _target = target;
        //    _logger = logger;
        //}

        public void Start()
        {
            //_logger.Info("DocumentServiceLogger.Start");
            Debug.Write("DocumentServiceLogger.Start");
        }

        //public object Process(object target, IContext context)
        //{
        //    return (IDocumentService)target;
        //}
    }
}