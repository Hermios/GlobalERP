using StandardTools.ViewHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalERP.Misc
{
    public interface IViewModelEntity<T> where T:ViewModelBase
    {
        T _viewModel { get; set; }
        bool _isVisible { get; set; }
        object _position { get; set; }
    }
}