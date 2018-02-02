using System;
using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Tony.Util.Ioc
{
   public class UnityIocHelper:IServiceProvider
   {
       private readonly IUnityContainer _container;
        private static readonly UnityIocHelper dbInstance = new UnityIocHelper("DBcontainer");
       private UnityIocHelper(string contaninerName)
       {
           var section =(UnityConfigurationSection)ConfigurationManager.GetSection("unity");
           _container = new UnityContainer();
           section.Configure(_container, contaninerName);
       }
       public static string GetmapToByName(string containerName, string itype, string name = "")
       {
           try
           {
               var section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
               var containers = section.Containers;
               foreach (var container in containers)
               {
                   if (container.Name == containerName)
                   {
                       var registraions = container.Registrations;
                       foreach (var registraion in registraions)
                       {
                           if (name == "" && string.IsNullOrEmpty(registraion.Name) && registraion.TypeName == itype)
                           {
                               return registraion.MapToName;
                           }
                       }
                       break;
                   }
               }
                return "";
           }
           catch
           {
               throw;
           }
       }
        public static UnityIocHelper DBInstance { get { return dbInstance; } }
        public object GetService(Type serviceType)
        {
            return _container.Resolve(serviceType);
        }
       public T GetService<T>()
       {
           return _container.Resolve<T>();
       }
       public T GetService<T>(params ParameterOverride[] obj)
       {
           return _container.Resolve<T>(obj);
       }
       public T GetService<T>(string name, params ParameterOverride[] obj)
       {
           return _container.Resolve<T>(name, obj);
        }
    }
}
