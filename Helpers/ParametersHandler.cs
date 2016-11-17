using StandardTools.ServiceLocator;
using System;
using System.Collections.Generic;
using System.Linq;
namespace GlobalERP.Helpers
{
    public class ParametersHandler:IService
    {
        const string PARAM_FILE= "paramterers.ini";
        private Dictionary<ParameterKey, string> _params;
        public ParametersHandler()
        {
            _params = new Dictionary<ParameterKey, string>();
            foreach(string line in System.IO.File.ReadAllLines(PARAM_FILE))
            {
                if (!line.Contains("="))
                    //ignore chapters, for now
                    continue;

                //load parameter
                string[] paramData = line.Split('=');
                _params.Add((ParameterKey)Enum.Parse(typeof(ParameterKey), paramData[0], true),paramData[0]);
            }
        }

        public string get(ParameterKey key)
        {
            return _params[key];
        }
    }

    public enum ParameterKey
    {
        mainXml,
        tagStandardRegex,
        tagRefRegex,
    }
}
