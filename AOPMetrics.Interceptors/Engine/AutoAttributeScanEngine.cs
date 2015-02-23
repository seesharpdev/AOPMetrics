using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using log4net;

using AOPMetrics.Core.Interfaces;

namespace AOPMetrics.Interceptors.Engine
{
    public class AutoAttributeScanEngine : IAttributeScanEngine
    {
        #region Private Members

        private readonly IDirectoryTask _directoryTask;

        private readonly ILog _logger;

        private bool _firstCall = true;

        #endregion

        #region Ctor's

        public AutoAttributeScanEngine()
        {
        }

        public AutoAttributeScanEngine(IDirectoryTask directoryTask, ILog logger)
        {
            _directoryTask = directoryTask;
            _logger = logger;
        }

        #endregion

        public void Run(IInvocation invocation, Type type)
        {
            var concreteObject = (IAttributeScanTask)Activator.CreateInstance(type, _logger);
            concreteObject.Run(invocation, _firstCall);
            _firstCall = false;
        }

        public virtual IEnumerable<Type> AutoFindAttribute(string path = "")
        {
            IList<Type> result = new List<Type>();

            if (string.IsNullOrEmpty(path))
            {
                path = _directoryTask.GetCurrentDirectory();
            }

            var assemblies = _directoryTask.GetFiles(path, "*.dll");
            //if (assemblies.Length > 0)
            if (assemblies.Any())
            {
                AddRange(result, FindAllAssemblies(assemblies));
            }

            return result;
        }

        private static IEnumerable<Type> FindAllAssemblies(IEnumerable<string> paths)
        {
            IList<Type> result = new List<Type>();

            foreach (var assembly in paths)
            {
                var realAssembly = Assembly.LoadFrom(assembly);
                if (realAssembly == null)
                {
                    throw new NullReferenceException();
                }

                var allTypes = realAssembly.GetTypes();
                if (allTypes.Length > 0)
                {
                    AddRange(result, FindAllTypes(allTypes));
                }
            }

            return result;
        }

        private static void AddRange(ICollection<Type> source, IEnumerable<Type> destination)
        {
            foreach (var type in destination)
            {
                source.Add(type);
            }
        }

        private static IEnumerable<Type> FindAllTypes(IEnumerable<Type> types)
        {
            IList<Type> result = new List<Type>();

            foreach (var type in types)
            {
                if (typeof(IAttributeScanTask).IsAssignableFrom(type) &&
                    type.FullName != typeof(IAttributeScanTask).FullName)
                {
                    result.Add(type);
                }
            }

            return result;
        }
    }

    public interface IDirectoryTask
    {
        string GetCurrentDirectory();
        IEnumerable<string> GetFiles(string path, string extension);
    }
}
