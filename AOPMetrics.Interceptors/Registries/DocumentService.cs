using System;
using System.Diagnostics;
using System.Globalization;

namespace AOPMetrics.Interceptors.Registries
{
    public class DocumentService : IDocumentService
    {
        /// <remarks>
        /// The method needs to be set as virtual so it can be intercepted, if we are linking the interceptor to the concrete implementation instead of the interface.
        /// </remarks>
        public string Start(string firstArgument, string secondArgument)
        {
            var startDateTime = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);
            Debug.Write("DocumentService.Start @ {0}", startDateTime);


            return startDateTime;
        }
    }
}