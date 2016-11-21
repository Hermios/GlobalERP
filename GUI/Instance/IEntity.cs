using GlobalERP.GUI.BusinessObject;
using GlobalERP.GUI.BusinessObject.Abstract;

namespace GlobalERP.GUI.Instance
{
    public interface IEntity
    {
        AbstractStandardViewModel _viewBusinessObject { get; set; }
        bool _isVisible { get; set; }
        int _position { get; set; }
    }
}