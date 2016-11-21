using System;
using GlobalERP.GUI.BusinessObject;
using GlobalERP.GUI.BusinessObject.Abstract;

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

        public AbstractStandardViewModel _viewBusinessObject
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

        public static IEntity CreateEntity(AbstractStandardViewModel vm, string isVisible,string position)
        {
            return CreateEntity(vm,bool.Parse(isVisible), int.Parse(position));
        }

        public static IEntity CreateEntity(AbstractStandardViewModel vm,bool isVisible, int position)
        {
            IEntity entity = new EntityFactory();
            entity._viewBusinessObject = vm;
            entity._isVisible = isVisible;
            entity._position = position;
            return entity;
        }
    }
}
