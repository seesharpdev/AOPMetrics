using System;

namespace AOPMetrics.Core.Interfaces.Logging
{
    public interface ILog
    {
        void Debug(string message);

        void Error(string message);

        void ErrorException(string message, Exception ex);

        void Fatal(string message);

        void Info(string message);

        void Trace(string message);

        void Warn(string message);
    }
}