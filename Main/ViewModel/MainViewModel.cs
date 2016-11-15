using GlobalERP.Frame;
using GlobalERP.Menu;
using GlobalERP.Misc;
using GlobalERP.Tab;
using StandardTools.ViewHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalERP.Main
{
    public class MainViewModel : ViewModelBase
    {
        List<IViewModelEntity<VMFrame>> _frames;
        List<IViewModelEntity<VMTab>> _tabs;
        List<IViewModelEntity<VMMenu>> _menus;
    }
}
