using GlobalERP.GUI.Model.Main;
using StandardTools.ViewHandler;

namespace GlobalERP
{
    public class LaunchingApp
    {
        public static void Bootstrap()
        {
            //Start Service Location

            //Load configuration data

            //Connect to database

            //Load xml Descriptions

            //Open main Window
            WindowHandler.OpenWindow<MainView>(new MainViewModel(), true);
        }
    }
}
