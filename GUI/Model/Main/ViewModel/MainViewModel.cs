using GlobalERP.GUI.Model.Frame;
using GlobalERP.GUI.Model.Menu;
using GlobalERP.GUI.Model.Tab;
using StandardTools.ViewHandler;
using System.Collections.Generic;

namespace GlobalERP.GUI.Model.Main
{
    public class MainViewModel : ViewModelBase
    {
        List<IEntity<VMFrame>> _frames;
        List<IEntity<VMTab>> _tabs;
        List<IEntity<VMMenu>> _menus;
    }
}
