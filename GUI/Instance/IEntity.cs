using StandardTools.ViewHandler;

namespace GlobalERP.GUI.Instance
{
    public interface IEntity
    {
        ViewModelBase _viewModel { get; set; }
        bool _isVisible { get; set; }
        int _position { get; set; }
    }
}