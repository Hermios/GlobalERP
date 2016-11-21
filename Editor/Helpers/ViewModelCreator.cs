using GlobalERP.Editor.CreateModelMessageBox;
using GlobalERP.Helpers;
using StandardTools.ServiceLocator;
using StandardTools.ViewHandler;
using StandardTools.ViewHandler.MessageBox;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GlobalERP.Editor.Helpers
{
    public class ViewModelCreator:IService
    {
            
        private string _directoryPath;
        private string _attributeTypeName;
        public void CreateModel(string modelName)
        {
            var yesNoCancelButtons = new Dictionary<string, object>();
            yesNoCancelButtons.Add("Yes", true);
            yesNoCancelButtons.Add("No", false);
            var createMBVM = new CreateModelMessageBoxViewModel(modelName);
            WindowHandler.OpenWindow<CreateModelMessageBoxView>(createMBVM, false);
            if (createMBVM.ModelViewModelResult == null)
                return;
            string filePath = Path.Combine(_directoryPath, modelName);
            if (!modelName.EndsWith(".xml"))
                modelName = modelName + ".xml";            
                XmlTextWriter writer = new XmlTextWriter(filePath, Encoding.UTF8);
                writer.WriteStartDocument(true);
                writer.WriteStartElement(modelName.Replace(".xml", ""));
                writer.WriteAttributeString(_attributeTypeName, createMBVM.ModelViewModelResult.Name);
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();
            
        }

        public void initService(IServiceLocator serviceLocator, params object[] args)
        {
            var configParameters = serviceLocator.get<ParametersHandler<ConfigParameterKey>>();
            _directoryPath = configParameters.getString(ConfigParameterKey.xmlDirectoryPath);
            var systemParameters= serviceLocator.get<ParametersHandler<SystemParameterKey>>();
            _attributeTypeName = systemParameters.getString(SystemParameterKey.attributeType);
        }
    }
}
