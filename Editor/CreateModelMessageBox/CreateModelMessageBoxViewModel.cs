using StandardTools.ViewHandler;
using StandardTools.ViewHandler.MessageBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalERP.Editor.CreateModelMessageBox
{
    public class CreateModelMessageBoxViewModel:ViewModelBase
    {
        private Tuple<string, object> _selectedItem;
        private string _modelName;
        public Type ModelViewModelResult { get; private set; }
        public CreateModelMessageBoxViewModel(string modelName)
        {
            _modelName = modelName;
            var buttonsDictionary = new Dictionary<string, object>();
                buttonsDictionary.Add(Properties.Texts.Ok,true);
            buttonsDictionary.Add(Properties.Texts.Cancel, false);
            UCButtonsVM = new UserControlButtonsViewModel(buttonsDictionary, OnValidationExecute);
        }

        private void OnValidationExecute(object obj)
        {
            ModelViewModelResult = (bool)obj ? (Type)SelectedItem.Item2 : null;
        }

        public string Title
        {
            get
            {
                return string.Format(Properties.Texts.CreateModelTypeTitle, _modelName);
            }
        }
        public string Text {
            get
            {
                return string.Format(Properties.Texts.CreateModelTypeQuestion, _modelName);
            }
        }
        public UserControlButtonsViewModel UCButtonsVM { get; private set;}

        public Tuple<string, object> SelectedItem
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
