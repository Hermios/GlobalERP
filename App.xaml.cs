using GlobalERP.Editor.Helpers;
using GlobalERP.Helpers;
using StandardTools.ServiceLocator;
using StandardTools.ViewHandler;
using StandardTools.ViewHandler.MessageBox;
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
            var serviceLocator = initServiceLocator(e.Args);
            //Load configuration data
            var parametersHandler = serviceLocator.get<ParametersHandler<SystemParameterKey>>();
                        
            //Connect to database

            //Load xml Descriptions
            var viewBusinessObjectsLoader = serviceLocator.get<BusinessObjectsLoader>();

            //Load apps
            LaunchingERP.Bootstrap(viewBusinessObjectsLoader.LoadBusinessObjects());
        }

        private IServiceLocator initServiceLocator(object[] args)
        {
            var serviceLocator = ServiceLocator.getServiceLocator();

            // Add services
            serviceLocator.add<ParametersHandler<SystemParameterKey>>();
            serviceLocator.add<ParametersHandler<ConfigParameterKey>>();
            serviceLocator.add<GlobalData>(args);
            serviceLocator.add<BusinessObjectTypesEnumerator>();
            serviceLocator.add<BusinessObjectsLoader>();
            if (serviceLocator.get<GlobalData>().IsEditMode)
            serviceLocator.add<BusinessObjectsCreator>();

            return serviceLocator;
        }
    }
}
