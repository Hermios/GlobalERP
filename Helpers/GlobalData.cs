using StandardTools.ServiceLocator;

namespace GlobalERP.Helpers
{
    public class GlobalData : IService
    {
        public bool IsEditMode;
        public void initService(IServiceLocator serviceLocator,params object[] args)
        {
            IsEditMode = args.Length>0 && serviceLocator.get<ParametersHandler<SystemParameterKey>>().getString(SystemParameterKey.configModeTag) == (string)args[0];
        }
    }
}
