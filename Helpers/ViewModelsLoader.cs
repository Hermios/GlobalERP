using StandardTools.ServiceLocator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using GlobalERP.GUI.Model;
using GlobalERP.GUI.Instance;
using GlobalERP.Editor.Helpers;

namespace GlobalERP.Helpers
{
    public class ViewModelLoader:IService
    {
        public ModelViewModel MainEntity { get; private set; }

        private Dictionary<string, ModelViewModel> _vms;
        private ParametersHandler<SystemParameterKey> _sysparameters;
        private IServiceLocator _serviceLocator;

        private bool _isEditMode;
        private string _tagRefRegex;
        private string _directoryPath;
        
        public ViewModelLoader()
        {
            _vms = new Dictionary<string, ModelViewModel>();            
        }

        public ModelViewModel LoadModels()
        {
            return getFileModel(_sysparameters.getString(SystemParameterKey.mainXmlFile));
        }

        private ModelViewModel getFileModel(string fileName)
        {
            if (!fileName.EndsWith(".xml"))
                fileName = fileName + ".xml";
            ModelViewModel result = null;
            string file = Path.Combine(_directoryPath,fileName);
            if (!File.Exists(file) && _isEditMode)
                _serviceLocator.get<ViewModelCreator>().CreateModel(fileName);
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
                                Type vmType = Type.GetType(reader.GetAttribute(reader.Name));
                                result = (ModelViewModel)Activator.CreateInstance(vmType);
                            }
                            else
                            {
                                ModelViewModel vm = null;
                                var isVisible = reader.GetAttribute(_sysparameters.getString(SystemParameterKey.attributeIsVisible));
                                var position = reader.GetAttribute(_sysparameters.getString(SystemParameterKey.attributePosition));
                                    
                                //if customized entity
                                if (reader.Name.StartsWith(_tagRefRegex))
                                {
                                    if (!_vms.ContainsKey(reader.Name))
                                        _vms.Add(reader.Name, getFileModel(reader.Name));
                                    vm = _vms[reader.Name];
                                }
                                else
                                    vm = (ModelViewModel)Activator.CreateInstance(Type.GetType(reader.Name));

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
        }
    }
}
