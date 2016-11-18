using StandardTools.ServiceLocator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Text.RegularExpressions;
using GlobalERP.GUI.Model;
using GlobalERP.GUI.Instance;

namespace GlobalERP.Helpers
{
    public class ViewModelsLoader:IService
    {
        public ModelViewModel MainEntity { get; private set; }

        private Dictionary<string, ModelViewModel> _vms;
        private ParametersHandler<ParameterKey> _parameters;

        private Regex _tagStandardRegex;
        private Regex _tagRefRegex;
        private string _directoryPath;
        private string _attributeType;
        public ViewModelsLoader()
        {
            _vms = new Dictionary<string, ModelViewModel>();            
        }

        private ModelViewModel getFileModel(string fileName)
        {
            if (fileName.EndsWith(".xml"))
                fileName = fileName + ".xml";
            ModelViewModel result = null;
            string file = _directoryPath + "/" + fileName;
            using (var reader=XmlReader.Create(file))
            {
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        //Create entity
                        case XmlNodeType.Element:
                            if (result == null && reader.Name + ".xml" == fileName)
                            {
                                Type vmType = Type.GetType(reader.GetAttribute(_attributeType));
                                result = (ModelViewModel)Activator.CreateInstance(vmType);
                            }
                            else
                            {
                                ModelViewModel vm = null;
                                var isVisible = reader.GetAttribute(_parameters.getString(ParameterKey.attributeIsVisible));
                                var position = reader.GetAttribute(_parameters.getString(ParameterKey.attributePosition));
                                //Create instance from default entity
                                if (_tagStandardRegex.Match(reader.Name).Success)                                
                                    vm=(ModelViewModel)Activator.CreateInstance(Type.GetType(reader.Name));
                                
                                //Create instance from customized entity
                                else if (_tagStandardRegex.Match(reader.Name).Success)
                                {
                                    if (!_vms.ContainsKey(reader.Name))
                                        _vms.Add(reader.Name, getFileModel(reader.Name));
                                    vm = _vms[reader.Name];
                                }
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

        public void initService(params object[] args)
        {
            var serviceLocator = (IServiceLocator)args[0];
            var directoryPath = (string)args[1];
            if (!directoryPath.EndsWith("/"))
                directoryPath = directoryPath + "/";
            _parameters = serviceLocator.get<ParametersHandler<ParameterKey>>();
            _directoryPath = directoryPath;
            _tagStandardRegex = new Regex(_parameters.getString(ParameterKey.tagStandardRegex));
            _tagRefRegex = new Regex(_parameters.getString(ParameterKey.tagRefRegex));
            _attributeType = _parameters.getString(ParameterKey.attributeType);
            MainEntity = getFileModel(_parameters.getString(ParameterKey.mainXml));
        }
    }
}
