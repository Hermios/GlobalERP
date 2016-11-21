using GlobalERP.GUI.Instance;
using StandardTools.ViewHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GlobalERP.GUI.BusinessObject.Abstract
{
    public abstract class AbstractContainerViewModel : AbstractStandardViewModel
    {
        
        public List<IEntity> Entities { get; private set; }

        public void addEntity(IEntity entity)
        {
            Entities.Add(entity);
        }
    }
}
