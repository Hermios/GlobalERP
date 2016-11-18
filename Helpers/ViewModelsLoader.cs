using StandardTools.ServiceLocator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Text.RegularExpressions;
using GlobalERP.GUI.Model;

namespace GlobalERP.Helpers
{
    public class ViewModelsLoader:IService
    {
        private Dictionary<string, ModelViewModel> _vms;
        private ParametersHandler<ParameterKey> _paramHandler;

        private Regex _tagStandardRegex;
        private Regex _tagRefRegex;
        private string _directoryPath;
        private string _attributeType;
        public ViewModelsLoader(string directoryPath)
        {
            if (!directoryPath.EndsWith("/"))
                directoryPath = directoryPath + "/";
            _paramHandler =ServiceLocator.getServiceLocator().get<ParametersHandler<ParameterKey>>();
            _directoryPath = directoryPath;
            _tagStandardRegex = new Regex(_paramHandler.getString(ParameterKey.tagStandardRegex));
            _tagRefRegex = new Regex(_paramHandler.getString(ParameterKey.tagRefRegex));
            _attributeType = _paramHandler.getString(ParameterKey.attributeType);
            var listFiles = Directory.GetFiles(directoryPath).Select(x=>x.Replace(directoryPath+"\\","")).ToList();
            var mainXmlName = _paramHandler.getString(ParameterKey.mainXml);
            if (!listFiles.Contains(mainXmlName))
                throw new MissingFieldException();
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
                            //Create instance from default entity
                            else
                            {
                                ModelViewModel vm = null;
                                if (_tagStandardRegex.Match(reader.Name).Success)
                                {
                                    Type vmType = Type.GetType(reader.Name);
                                    vm = (ModelViewModel)Activator.CreateInstance(vmType);
                                    //EntityFactory.CreateEntity<vmType>();
                                }
                                    
                                //Create instance from customized entity
                                else if (_tagStandardRegex.Match(reader.Name).Success)
                                    if (!_vms.ContainsKey(reader.Name))
                                        vm=getFileModel(reader.Name);
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
        }
    }
}
