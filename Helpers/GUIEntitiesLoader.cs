using GlobalERP.GUI.Instance;
using StandardTools.ServiceLocator;
using StandardTools.ViewHandler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Text.RegularExpressions;

namespace GlobalERP.Helpers
{
    public class GUIEntitiesLoader
    {
        private Dictionary<string, IEntity> _entities;
        private ParametersHandler _paramHandler;

        private Regex _tagStandardRegex;
        private Regex _tagRefRegex;
        private string _directoryPath;
        public GUIEntitiesLoader(string directoryPath)
        {
            if (!directoryPath.EndsWith("/"))
                directoryPath = directoryPath + "/";
            _paramHandler =ServiceLocator.getServiceLocator().get<ParametersHandler>();
            _directoryPath = directoryPath;
            _tagStandardRegex = new Regex(_paramHandler.get(ParameterKey.tagStandardRegex));
            _tagRefRegex = new Regex(_paramHandler.get(ParameterKey.tagRefRegex));
            var listFiles = Directory.GetFiles(directoryPath).Select(x=>x.Replace(directoryPath+"\\","")).ToList();
            var mainXmlName = _paramHandler.get(ParameterKey.mainXml);
            if (!listFiles.Contains(mainXmlName))
                throw new MissingFieldException();
        }

        private Dictionary<string, IEntity> getFileEntities(string fileName)
        {
            string file = _directoryPath + "/" + fileName;
            using (var reader=XmlReader.Create(file))
            {
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        //Create entity
                        case XmlNodeType.Element:
                            //Create instance from default entity
                            if(_tagStandardRegex.Match(reader.Name).Success)
                                ;
                            //Create instance from customized entity
                            else if (_tagStandardRegex.Match(reader.Name).Success)
                                if(!_entities.ContainsKey(reader.Name))
                                    getFileEntities(Entit;                       
                            break;
                        default:
                            break;
                    }
                }
            }
                
        }
    }
}
