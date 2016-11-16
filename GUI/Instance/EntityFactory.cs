using System;
using StandardTools.ViewHandler;

namespace GlobalERP.GUI.Instance
{
    public class EntityFactory:IEntity 
    {
        public ViewModelBase _viewModel
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool _isVisible
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int _position
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public static IEntity CreateEntity<T>(bool isVisible,int position) where T : ViewModelBase
        {
            IEntity entity = new EntityFactory();
            entity._viewModel= (T)Activator.CreateInstance(typeof(T));
            entity._isVisible = isVisible;
            entity._position = position;
            return entity;
        }
    }
}
