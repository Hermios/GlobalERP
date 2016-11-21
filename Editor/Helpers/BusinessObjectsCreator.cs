using GlobalERP.Editor.CreateBusinessObjectMessageBox;
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
    public class BusinessObjectsCreator:IService
    {
            
        private string _directoryPath;
        public void CreateBusinessObject(string businessObjectName)
        {
            var createMBVM = new CreateContainerBusinessObjectMessageBoxViewModel(businessObjectName);
            WindowHandler.OpenWindow<CreateBusinessObjectMessageBoxView>(createMBVM, false);
            if (createMBVM.ContainerBusinessObjectResult == null)
                return;
            string filePath = Path.Combine(_directoryPath, businessObjectName);
            if (!businessObjectName.EndsWith(".xml"))
                businessObjectName = businessObjectName + ".xml";            
                XmlTextWriter writer = new XmlTextWriter(filePath, Encoding.UTF8);
                writer.WriteStartDocument(true);
                writer.WriteStartElement(createMBVM.ContainerBusinessObjectResult.Name);
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();
            
        }

        public void initService(IServiceLocator serviceLocator, params object[] args)
        {
            var configParameters = serviceLocator.get<ParametersHandler<ConfigParameterKey>>();
            _directoryPath = configParameters.getString(ConfigParameterKey.xmlDirectoryPath);
            var systemParameters= serviceLocator.get<ParametersHandler<SystemParameterKey>>();
        }
    }
}
