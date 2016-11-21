using GlobalERP.GUI.BusinessObject;
using GlobalERP.GUI.BusinessObject.Abstract;
using GlobalERP.GUI.BusinessObject.Main;
using StandardTools.ViewHandler;

namespace GlobalERP
{
    public class LaunchingERP
    {
        public static void Bootstrap(AbstractContainerViewModel  mainEntity)
        {
            
            //Open main Window
            WindowHandler.OpenWindow<MainView>(mainEntity, true);
        }

        
    }
}
