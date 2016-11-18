using System;
using GlobalERP.GUI.Model;

namespace GlobalERP.GUI.Instance
{
    public class EntityFactory:IEntity 
    {
        
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

        public ModelViewModel _viewModel
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

        public static IEntity CreateEntity<T>(bool isVisible,int position) where T : ModelViewModel
        {
            IEntity entity = new EntityFactory();
            entity._viewModel= (T)Activator.CreateInstance(typeof(T));
            entity._isVisible = isVisible;
            entity._position = position;
            return entity;
        }
    }
}
