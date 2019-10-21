using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Translator.DataAccess;
using Translator.DataMapper.Interfaces;
using Translator.DataMapper.Mappers;
using Translator.Services;
using Translator.Services.Interfaces;
using Translator.Views;
using Translator.Views.Interfaces;

namespace Translator.Dependencies
{
    public class ServiceLocator : IServiceLocator
    {
        private readonly IDictionary<Type, Type> _servicesType;
        private readonly IDictionary<Type, object> _instantiatedServices;

        private static readonly object Lock = new object();
        private static IServiceLocator _instance;

        internal ServiceLocator()
        {
            _servicesType = new Dictionary<Type, Type>();
            _instantiatedServices = new Dictionary<Type, object>();

            BuildServiceTypesMap();
        }

        public static IServiceLocator Instance
        {
            get
            {
                lock (Lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ServiceLocator();
                    }
                }
                return _instance;
            }
        }

        public T GetService<T>()
        {
            if (_instantiatedServices.ContainsKey(typeof(T)))
            {
                return (T) _instantiatedServices[typeof(T)];
            }

            var constructor = _servicesType[typeof(T)].GetConstructor(new Type[0]);
            if (constructor != null)
            {
                var service = (T) constructor.Invoke(null);
                _instantiatedServices.Add(typeof(T), service);
                return service;
            }

            throw new ApplicationException("The requested service is not registered");
        }

        public void AddInstantiatedService(Type serviceType, object service)
        {
            _instantiatedServices.Add(serviceType, service);
        }

        private void BuildServiceTypesMap()
        {
            _servicesType.Add(typeof(IDbManager), typeof(DbManager));
            _servicesType.Add(typeof(ILanguageMapper), typeof(LanguageMapper));
            _servicesType.Add(typeof(IWordMapper), typeof(WordMapper));
            _servicesType.Add(typeof(IRoleMapper), typeof(RoleMapper));
            _servicesType.Add(typeof(IUserMapper), typeof(UserMapper));

            // Views
            _servicesType.Add(typeof(IAuthorizationView), typeof(AuthorizationForm));
            _servicesType.Add(typeof(ITranslatorView), typeof(TranslatorForm));

            // Services
            _servicesType.Add(typeof(ICredentialsService), typeof(CredentialsService));

            // App context
            _servicesType.Add(typeof(ApplicationContext), typeof(ApplicationContext));
        }
    }
}
