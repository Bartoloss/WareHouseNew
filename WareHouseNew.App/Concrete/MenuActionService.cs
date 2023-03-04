using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouseNew.App.Common;
using WareHouseNew.Domain.Entity;
using WareHouseNew.App.Managers;

namespace WareHouseNew.App.Concrete
{
    public class MenuActionService : BaseService<MenuAction>
    {
        public MenuActionService()
        {
            Initialize();
        }

        public List<MenuAction> GetMenuActionsByMenuName(string menuName) //metoda która wyświeli wszystkie opcje w zależności od nazwy menuName
        {
            List<MenuAction> result = new List<MenuAction>(); //utworzenie nowej listy "result"
            foreach (var menuAction in ObjList) //pętla wszystkich opcji w menu
            {
                if (menuAction.MenuName == menuName) //jeśli nazwa opcji będzie taka sama jak nazwa menu
                {
                    result.Add(menuAction); //to dodanie opcji do utworzonej wyżej listy "result"
                }
            }
            return result; //zwrócenie gotowej listy
        }


        private void Initialize()
        {
            AddItem(new MenuAction(1, "Add product", "MainMenu"));
            AddItem(new MenuAction(2, "Remove product", "MainMenu"));
            AddItem(new MenuAction(3, "List of products", "MainMenu"));
            AddItem(new MenuAction(4, "Show details of product", "MainMenu"));

            AddItem(new MenuAction(1, "Tshirts", "AddNewItemMenu"));
            AddItem(new MenuAction(2, "Hoddies", "AddNewItemMenu"));
            AddItem(new MenuAction(3, "Gadgets", "AddNewItemMenu"));
            AddItem(new MenuAction(4, "Trousers", "AddNewItemMenu"));

            AddItem(new MenuAction(0, "List of all products", "ListOfProductsMenu"));
            AddItem(new MenuAction(1, "List of Tshirts products", "ListOfProductsMenu"));
            AddItem(new MenuAction(2, "List of Hoddies products", "ListOfProductsMenu"));
            AddItem(new MenuAction(3, "List of Gadgets products", "ListOfProductsMenu"));
            AddItem(new MenuAction(4, "List of Trousers products", "ListOfProductsMenu"));

        }

    }      
}
