using GlobalERP.Helpers;
using StandardTools.ServiceLocator;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace GlobalERP
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //Start Service Location
            var serviceLocator = initServiceLocator();
            //Load configuration data
            var parametersHandler = serviceLocator.get<ParametersHandler<ParameterKey>>();
            //Connect to database

            //Load xml Descriptions
            var viewModelsLoader = serviceLocator.get<ViewModelsLoader>();

            //Load apps
            if (e.Args[0] == "Config") ;
            else
                LaunchingERP.Bootstrap(viewModelsLoader.MainEntity);
        }

        private IServiceLocator initServiceLocator()
        {
            var serviceLocator = ServiceLocator.getServiceLocator();

            // Add services
            serviceLocator.add<ParametersHandler<ParameterKey>>();
            serviceLocator.add<ViewModelsLoader>(serviceLocator, "");

            return serviceLocator;
        }
    }
}
