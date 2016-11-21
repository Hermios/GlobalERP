using GlobalERP.GUI.BusinessObject.Abstract;
using StandardTools.ServiceLocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GlobalERP.Helpers
{
    public class BusinessObjectTypesEnumerator:IService
    {
        private List<Type> _listStandardBusinessObjects;
        private List<Type> _listContainerBusinessObjects;
        public List<Type> GetListStandardBusinessObjects()
        {
            if(_listStandardBusinessObjects==null)
            _listStandardBusinessObjects= GetListObjects<AbstractStandardViewModel>();
            return _listStandardBusinessObjects;
        }

        public List<Type> GetListContainerBusinessObjects()
        {
            if (_listContainerBusinessObjects == null)
                _listContainerBusinessObjects = GetListObjects<AbstractContainerViewModel>();
            return _listContainerBusinessObjects;
        }

        private static List<Type> GetListObjects<T>()
        {
            return Assembly.GetAssembly(typeof(T)).GetTypes().Where(type => type.IsClass && !type.IsAbstract & type.IsSubclassOf(typeof(T))).ToList();
        }

        public void initService(IServiceLocator serviceLocator, params object[] args)
        {}
    }
}
