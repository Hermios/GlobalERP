using GlobalERP.GUI.Model;
using GlobalERP.GUI.Model.Main;
using StandardTools.ViewHandler;

namespace GlobalERP
{
    public class LaunchingERP
    {
        public static void Bootstrap(ModelViewModel mainEntity)
        {
            
            //Open main Window
            WindowHandler.OpenWindow<MainView>(mainEntity, true);
        }

        
    }
}
