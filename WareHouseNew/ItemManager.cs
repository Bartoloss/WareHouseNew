using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouseNew
{
    public class ItemManager
    {
        public List<Item> Items { get; set; }
        public ItemManager()
        {
            Items = new List<Item>();
        }

        public int AddNewItemView(MenuActionService actionService) //do metody "AddNewItem" przekazuje "actionService" ponieważ w tej metodzie chcę, aby było menu
        {
            Console.WriteLine("Please select the category of added item");
            var addNewItemMenu = actionService.GetMenuActionsByMenuName("AddNewItemMenu"); //do zmiennej "addNewItemMenu" przypisuje opcje menu z kategorii "AddNewItemMenu"
            for (int i = 0; i < addNewItemMenu.Count; i++) //dla wszystkich znalezionych opcji z kategorii AddNewItemMenu
            {
                Console.WriteLine($"{addNewItemMenu[i].Id}. {addNewItemMenu[i].Name}"); //wyświetl na ekranie Id oraz Name tej opcji.
            }
            string addOperation = Console.ReadLine();
            int addOperationInt;
            {
                if (Int32.TryParse(addOperation, out addOperationInt) == true)
                {
                    if (addOperationInt == 1 || addOperationInt == 2 || addOperationInt == 3 || addOperationInt == 4)
                    {
                        return addOperationInt;
                    }
                    else
                    {
                        Console.WriteLine("Error! Please enter the correct number");
                        return -1;
                    }
                }
                else
                {
                    Console.WriteLine("Error! Please enter the number, not letter or word.");
                    return -1;
                }
            }
        }

        public void AddNewItem(int categoryId) //metoda odpowiedzialna za dodanie nowego produktu
        {
            int retrieveId;
            Item item = new Item();
            item.CategoryId = categoryId;
            GetLastId(categoryId);
            Console.WriteLine($"The id for new product is {lastId}:");
            string userId = Console.ReadLine();

            
                item.Id = addId;
                Console.WriteLine("Please enter name for new product:");
                string userName = Console.ReadLine();
                item.Name = userName;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Product {userName} was added successfully!");
                Console.ForegroundColor = ConsoleColor.White;
                Items.Add(item);
            


            ////foreach (var item in Items)
            ////{
            ////    if (item.Id == categoryId)
            ////    {
            ////        Console.WriteLine("Error! Id you entered exist. Please enter another id:"); ; //nadpisanie znalezionego produktu do wcześniej zadeklarowanego pustego produktu do usunięcia
            ////        return;
            ////    }
            ////    else
            ////    {
            //int addId;
            //Item item = new Item();
            //item.CategoryId = categoryId;
            //Console.WriteLine("Please enter number of id for new product:");
            //string userId = Console.ReadLine();

            //if (Int32.TryParse(userId, out addId) == true)
            //{
            //    item.Id = addId;
            //    Console.WriteLine("Please enter name for new product:");
            //    string userName = Console.ReadLine();
            //    item.Name = userName;
            //    Console.ForegroundColor = ConsoleColor.Green;
            //    Console.WriteLine($"Product {userName} was added successfully!");
            //    Console.ForegroundColor = ConsoleColor.White;
            //    Items.Add(item);
            //}
            //else
            //{
            //    Console.WriteLine("Error! Please enter a number of product id.");
            //    return;
            //}
            //    //}
            ////}
        } 

        public int RemoveItemView()
        {
            Console.WriteLine("Please enter id of product you want to delete:");
            string userId = Console.ReadLine();
            int id;
            if(Int32.TryParse(userId, out id)==true)
            {
                return id;
            }
            else
            {
                return -1;
            }
        }

        public void RemoveItem(int removeId) //metoda odpowiedzialna za usunięcie produktu
        {
            if (removeId < 0)
            {
                RemoveItemView();
            }
            else
            {
                Item productToRemove = new Item(); //zadeklarowanie i stworzenie pustego produktu do usunięcia
                foreach (Item product in Items)
                {
                    if (product.Id == removeId)
                    {
                        productToRemove = product; //nadpisanie znalezionego produktu do wcześniej zadeklarowanego pustego produktu do usunięcia
                        break;
                    }
                }
                Items.Remove(productToRemove); //usunięcie produktu
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Product of id={removeId} was deleted successfully!");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public int ListOfProductsView(MenuActionService actionService)
        {
            Console.WriteLine("Please select category of products you want to see:");
            var listOfProductsMenu = actionService.GetMenuActionsByMenuName("ListOfProductsMenu"); //do zmiennej "addNewItemMenu" przypisuje opcje menu z kategorii "AddNewItemMenu"
            for (int i = 0; i < listOfProductsMenu.Count; i++) //dla wszystkich znalezionych opcji z kategorii AddNewItemMenu
            {
                Console.WriteLine($"{listOfProductsMenu[i].Id}. {listOfProductsMenu[i].Name}"); //wyświetl na ekranie Id oraz Name tej opcji.
            }
            string listOperation = Console.ReadLine();
            int listOperationInt;
            Int32.TryParse(listOperation, out listOperationInt);
            return listOperationInt;
        }

        public void ListOfProducts(int categoryId)
        {
            //UPDATE-(5 tydzień - LINQ) w przypadku braku produktów z danej kategorii wyświetlić komunikat o braku produktów
           
            List<Item> productsToShow = new List<Item>();
            foreach (Item product in Items)
            {
                if (product.CategoryId == categoryId)
                {
                    productsToShow.Add(product);
                }
            }
            if (productsToShow.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                foreach (Item product in productsToShow)
                {
                    Console.WriteLine($"{product.Id}. {product.Name}");
                }
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Sory, there are no products in this category.");
                Console.ForegroundColor = ConsoleColor.White;
            }
            

        }

        public int GetLastId(int categoryId)
        {
            List<Item> CategoryItem = new List<Item>();
            foreach (Item product in Items)
            {
                if (product.CategoryId == categoryId)
                {
                    CategoryItem.Add(product);
                }
            }
            int lastId;
            if (Items.Any())
            {
                lastId = Items.OrderBy(p => p.Id).LastOrDefault().Id;
            }
            else
            {
                lastId = 0;
            }
            return lastId;
        }
    }

    
}
