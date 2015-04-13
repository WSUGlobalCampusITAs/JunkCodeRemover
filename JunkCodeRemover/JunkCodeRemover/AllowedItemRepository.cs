using JunkCodeRemover.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JunkCodeRemover
{
    public class AllowedItemRepository
    {
        private List<AllowedItemModel> _tagsList;
        private List<AllowedItemModel> _stylesList;
        private List<AllowedItemModel> _attributesList;

        public AllowedItemRepository()
        {
            _tagsList = new List<AllowedItemModel>();
            _stylesList = new List<AllowedItemModel>();
            _attributesList = new List<AllowedItemModel>();

            //using a string splitter variable to hold the tags after they 
            //are seperated from the Allowed Tags or Styles settings. 
            var splitstring = Settings.Default.AllowedTags.Split(',');

            foreach (string tag in splitstring)
            {
                _tagsList.Add(new AllowedItemModel(tag.Trim()));
            }

            splitstring = Settings.Default.AllowedStyles.Split(',');

            foreach (string tag in splitstring)
            {
                _stylesList.Add(new AllowedItemModel(tag.Trim()));
            }

            splitstring = Settings.Default.AllowedAttributes.Split(',');

            foreach (string tag in splitstring)
            {
                _attributesList.Add(new AllowedItemModel(tag.Trim()));
            }
        }

        public List<AllowedItemModel> Tags { get { return _tagsList; } }
        public List<AllowedItemModel> Styles { get { return _stylesList; } }
        public List<AllowedItemModel> Attributes { get { return _attributesList; } }
    }
}
