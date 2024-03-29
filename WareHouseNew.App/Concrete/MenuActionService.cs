﻿using System;
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
            AddItem(new MenuAction(1, "Add product.", "MainMenu"));
            AddItem(new MenuAction(2, "Remove product.", "MainMenu"));
            AddItem(new MenuAction(3, "List of products.", "MainMenu"));
            AddItem(new MenuAction(4, "Show details of product.", "MainMenu"));
            AddItem(new MenuAction(5, "Add categories of products.", "MainMenu"));
            AddItem(new MenuAction(6, "Save progress to the file.", "MainMenu"));


            AddItem(new MenuAction(1, "List of all products.", "ListOfProductsViewMenu")); //można dodać z tym samym id, ponieważ oba elementy z id=1 są elementami dwóch niezależnych menu
            AddItem(new MenuAction(2, "List of products with low stack.", "ListOfProductsViewMenu"));
            AddItem(new MenuAction(3, "List of products by chosen category.", "ListOfProductsViewMenu"));

        }

    }      
}
