using System;
using System.Collections.Generic;

namespace TechFlurry.Utils.MetronicComponents.Models
{
    public class MenuItemModel
    {
        public MenuItemModel()
        {
            SubMenu = new List<MenuItemModel>();
        }

        public string ActionLink { get; set; }
        public bool IsActive { get; set; }
        public int NavigationPage { get; set; }
        public MenuItemModel Parent { get; set; }
        public List<MenuItemModel> SubMenu { get; private set; }
        public string Section { get; set; }
        public string Text { get; set; }
        public string Icon { get; set; }
        public Action OnClick { get; set; }
        public void AddSubmenuItem(MenuItemModel item)
        {
            item.Parent = this;
            SubMenu.Add(item);
        }
    }
}