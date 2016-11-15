using GlobalERP.Frame;
using GlobalERP.Menu;
using GlobalERP.Tab;
using StandardTools.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalERP.Main
{
    public class MainViewModel : ViewModelBase
    {
        List<VMFrame> _frames;
        List<VMTab> _tabs;
        List<VMMenu> _menus;
    }
}
