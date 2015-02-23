using System;

namespace AOPMetrics.Core.Interfaces.Logging
{
    public class Logger : ILog
    {
        public void Debug(string message)
        {
            throw new NotImplementedException();
        }

        public void Error(string message)
        {
            throw new NotImplementedException();
        }

        public void ErrorException(string message, Exception ex)
        {
            throw new NotImplementedException();
        }

        public void Fatal(string message)
        {
            throw new NotImplementedException();
        }

        public void Info(string message)
        {
            System.Diagnostics.Debug.Write(message);
        }

        public void Trace(string message)
        {
            throw new NotImplementedException();
        }

        public void Warn(string message)
        {
            throw new NotImplementedException();
        }
    }
}