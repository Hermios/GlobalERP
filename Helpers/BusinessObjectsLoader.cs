using StandardTools.ServiceLocator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using GlobalERP.GUI.Instance;
using GlobalERP.Editor.Helpers;
using GlobalERP.GUI.BusinessObject.Abstract;

namespace GlobalERP.Helpers
{
    public class BusinessObjectsLoader:IService
    {
        public AbstractContainerViewModel  MainEntity { get; private set; }

        private Dictionary<string, AbstractContainerViewModel > _vms;
        private ParametersHandler<SystemParameterKey> _sysparameters;
        private IServiceLocator _serviceLocator;

        private bool _isEditMode;
        private string _tagRefRegex;
        private string _directoryPath;
        private BusinessObjectTypesEnumerator _boTypeEnum;
        
        public BusinessObjectsLoader()
        {
            _vms = new Dictionary<string, AbstractContainerViewModel >();            
        }

        public AbstractContainerViewModel  LoadBusinessObjects()
        {
            return getFileBusinessObject(_sysparameters.getString(SystemParameterKey.mainXmlFile));
        }

        private AbstractContainerViewModel  getFileBusinessObject(string fileName)
        {
            if (!fileName.EndsWith(".xml"))
                fileName = fileName + ".xml";
            AbstractContainerViewModel  result = null;
            string file = Path.Combine(_directoryPath,fileName);
            if (!File.Exists(file) && _isEditMode)
                _serviceLocator.get<BusinessObjectsCreator>().CreateBusinessObject(fileName);
            using (var reader=XmlReader.Create(file))
            {
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            //if mainRoot
                            if (result == null)
                            {
                                Type vmType = _boTypeEnum.GetListContainerBusinessObjects().Find(t=>t.Name==reader.Name);
                                result = (AbstractContainerViewModel )Activator.CreateInstance(vmType);
                            }
                            else
                            {
                                AbstractStandardViewModel vm = null;
                                var isVisible = reader.GetAttribute(_sysparameters.getString(SystemParameterKey.attributeIsVisible));
                                var position = reader.GetAttribute(_sysparameters.getString(SystemParameterKey.attributePosition));
                                    
                                //if customized entity
                                if (reader.Name.StartsWith(_tagRefRegex))
                                {
                                    if (!_vms.ContainsKey(reader.Name))
                                        _vms.Add(reader.Name, getFileBusinessObject(reader.Name));
                                    vm = _vms[reader.Name];
                                }
                                else
                                    vm = (AbstractStandardViewModel)Activator.CreateInstance(_boTypeEnum.GetListContainerBusinessObjects().Find(t => t.Name == reader.Name));

                                result.addEntity(EntityFactory.CreateEntity(vm,isVisible, position));

                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            return result;
                
        }

        public void initService(IServiceLocator serviceLocator, params object[] args)
        {
            _serviceLocator = serviceLocator;
            var configParameters = _serviceLocator.get<ParametersHandler<ConfigParameterKey>>();
            var directoryPath = configParameters.getString(ConfigParameterKey.xmlDirectoryPath);
            if (!directoryPath.EndsWith("/"))
                directoryPath = directoryPath + "/";
            _sysparameters = _serviceLocator.get<ParametersHandler<SystemParameterKey>>();
            _isEditMode = _serviceLocator.get<GlobalData>().IsEditMode;
            _directoryPath = directoryPath;
            _tagRefRegex =_sysparameters.getString(SystemParameterKey.tagRefRegex);
            _boTypeEnum = _serviceLocator.get<BusinessObjectTypesEnumerator>();
        }
    }
}
