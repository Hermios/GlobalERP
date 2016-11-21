using GlobalERP.GUI.Instance;
using StandardTools.ViewHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using GlobalERP.Properties;

namespace GlobalERP.GUI.BusinessObject.Abstract
{
    public abstract class AbstractStandardViewModel : ViewModelBase
    {
        public abstract void InitBusinessObject(XmlNode node);
        //public abstract string displaiedName { get; }
    }
}
