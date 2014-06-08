using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using Versionit.Core;
using Versionit.Data;

namespace Versionit.Core
{
    /// <summary>
    /// Simple wrapper for unity resolution.
    /// http://msdn.microsoft.com/en-us/library/vstudio/hh323691(v=vs.100).aspx
    /// </summary>
    internal class PromptDependencyFactory
    {
        private static IUnityContainer _container;

        public static IUnityContainer Container
        {
            get
            {
                return _container;
            }
            private set
            {
                _container = value;
            }
        }

        static PromptDependencyFactory()
        {
            var container = new UnityContainer();

            container.RegisterType<IVersionRepository, VersionRepository>();
            
            _container = container;
        }

        public static T Resolve<T>(string name = null,
                                   params ResolverOverride[] overrides)
        {
            return Container.Resolve<T>(name, overrides);
        }
    }
}