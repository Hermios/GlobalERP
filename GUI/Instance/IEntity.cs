using GlobalERP.GUI.Model;

namespace GlobalERP.GUI.Instance
{
    public interface IEntity
    {
        ModelViewModel _viewModel { get; set; }
        bool _isVisible { get; set; }
        int _position { get; set; }
    }
}