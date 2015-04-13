using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JunkCodeRemover
{
    public class AllowedItemModel
    {
        private string _itemName;
        private bool _isChecked;

        public AllowedItemModel(string ItemName)
        {
            _itemName = ItemName;
            _isChecked = true;
        }

        public string ItemName { get { return _itemName; } }
        public bool IsChecked 
        { 
            get { return _isChecked; }
            set { _isChecked = value; }
        }
    }
}
