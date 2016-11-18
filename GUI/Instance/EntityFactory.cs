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

        public static IEntity CreateEntity(ModelViewModel vm, string isVisible,string position)
        {
            return CreateEntity(vm,bool.Parse(isVisible), int.Parse(position));
        }

        public static IEntity CreateEntity(ModelViewModel vm,bool isVisible, int position)
        {
            IEntity entity = new EntityFactory();
            entity._viewModel = vm;
            entity._isVisible = isVisible;
            entity._position = position;
            return entity;
        }
    }
}
