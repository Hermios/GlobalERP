using GlobalERP.Helpers;
using StandardTools.ServiceLocator;
using StandardTools.ViewHandler;
using StandardTools.ViewHandler.MessageBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace GlobalERP.Editor.CreateBusinessObjectMessageBox
{
    public class CreateContainerBusinessObjectMessageBoxViewModel:ViewModelBase
    {
        private Tuple<string, Type> _selectedItem;
        private ResourceManager _textsRM;
        private string _businessObjectName;
        public Type ContainerBusinessObjectResult { get; private set; }
        public CreateContainerBusinessObjectMessageBoxViewModel(string BusinessObjectName)
        {
            _businessObjectName = BusinessObjectName;
            _textsRM = Properties.Texts.ResourceManager;
            var buttonsDictionary = new Dictionary<string, object>();
                buttonsDictionary.Add(Properties.Texts.Ok,true);
            buttonsDictionary.Add(Properties.Texts.Cancel, false);
            UCButtonsVM = new UserControlButtonsViewModel(buttonsDictionary, OnValidationExecute);
        }

        public List<Tuple<string,Type>> ListContainerBusinessObjects
        {
            get
            {              
                return ServiceLocator.getServiceLocator().get<BusinessObjectTypesEnumerator>().
                    GetListContainerBusinessObjects().Select(x=>new Tuple<string, Type>(_textsRM.GetObject( x.Name).ToString(),x)).ToList();
            }
        }

        private void OnValidationExecute(object obj)
        {
            ContainerBusinessObjectResult = (bool)obj ? (Type)SelectedItem.Item2 : null;
        }

        public string Title
        {
            get
            {
                return string.Format(Properties.Texts.CreateBusinessObjectTypeTitle, _businessObjectName);
            }
        }
        public string Text {
            get
            {
                return string.Format(Properties.Texts.CreateBusinessObjectTypeQuestion, _businessObjectName);
            }
        }
        public UserControlButtonsViewModel UCButtonsVM { get; private set;}

        public Tuple<string, Type> SelectedItem
        {
            get
            {
                return _selectedItem;
            }

            set
            {
                _selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }
    }
}
