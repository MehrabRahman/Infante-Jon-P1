namespace UI;

public class AllShoppingStoresMenu : IMenuWithID {

    private UserBL _iubl;
    private StoreBL _sbl;

    public AllShoppingStoresMenu(UserBL iubl, StoreBL sbl){
        _iubl = iubl;
        _sbl = sbl;
    }  
    public void Start(int userID){  
        
        List<Store> allStores = _sbl.GetAllStores();
            bool exit = false;
            while (!exit){
                if(allStores.Count == 0){
                    Console.WriteLine("\nNo stores found!");
                    exit = true;
                }
                else{
                int i = 0;
                ColorWrite.wc("\n==================[All Stores]=================", ConsoleColor.DarkCyan);
                foreach(Store store in allStores){
                    Console.WriteLine($"[{i}] {store.ToString()}");
                    i++;
                }
                ColorWrite.wc("\n      Select the store's ID to browse.\n   Or enter [r] to [Return] to the User Menu.", ConsoleColor.DarkYellow);
                Console.WriteLine("=============================================");
                string? select = Console.ReadLine();
                int storeIndex;
                //Returns to menu
                if (select == "r"){
                    exit = true;
                    }
                else {
                    //Checks for valid integer
                    if(!int.TryParse(select, out storeIndex)){
                        Console.WriteLine("\nPlease select a valid input!");
                    }
                    else{
                        if (storeIndex >= 0 && storeIndex < allStores.Count){
                            int storeID = (int)allStores[storeIndex].ID!;
                            //Initialize shopping store's product menu (no menu factory)
                            ShoppingStoreMenu ssMenu = new ShoppingStoreMenu(_iubl, _sbl);
                            ssMenu.Start(storeID, userID);
                        }  
                        //Index out of range
                        else{
                            Console.WriteLine("\nPlease select an index within range!");
                        }
                }
                }
            }
        }
    }
}