using System;

namespace Translator.Dependencies
{
    public interface IServiceLocator
    {
        T GetService<T>();
        void AddInstantiatedService(Type serviceType, object service);
    }
}
