using GlobalERP.Main;
using StandardTools.ViewHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
