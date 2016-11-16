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

namespace GlobalERP.Helpers
{
    public class GUIEntitiesLoader
    {
        private Dictionary<string, IEntity> _entities;
        public GUIEntitiesLoader(string directoryPath)
        {
            var paramHandler =ServiceLocator.getServiceLocator().get<ParametersHandler>();
            var listFiles = Directory.GetFiles(directoryPath).ToList();
            if (!listFiles.Contains(paramHandler.get(ParameterKey.mainXml)))
                throw new MissingFieldException();
        }

        private static Dictionary<string, IEntity> getFileEntities(string file)
        {
            using (var reader=XmlReader.Create(file))
            {
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            writer.WriteStartElement(reader.Name);
                            break;
                        default:
                            break;
                    }
                }
            }
                
        }
    }
}
