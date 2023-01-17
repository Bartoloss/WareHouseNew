using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseNew
{
    public class MenuActionService
    {
        private List<MenuAction> menuActions; //lista w której są wszystkie opcje menu

        public void AddNewAction(int id, string name, string menuName) //metoda odpowiedzialna za dodawanie nowej opcji menu
        {
            MenuAction menuAction = new MenuAction() { Id = id, Name = name, MenuName = menuName }; //utworzenie nowej opcji
            menuActions.Add(menuAction); //dodanie nowej opcji do głównego menu
        }
    
        public List<MenuAction> GetMenuActionsByMenuName(string menuName) //metoda która wyświeli wszystkie opcje w zależności od nazwy menuName
        {
            List<MenuAction> result = new List<MenuAction>(); //utworzenie nowej listy "result"
            foreach (var menuAction in menuActions) //pętla wszystkich opcji w menu
            {
                if (menuAction.MenuName == menuName) //jeśli nazwa opcji będzie taka sama jak nazwa menu
                {
                    result.Add(menuAction); //to dodanie opcji do utworzonej wyżej listy "result"
                }
            }
            return result; //zwrócenie gotowej listy
        }

    }
}
