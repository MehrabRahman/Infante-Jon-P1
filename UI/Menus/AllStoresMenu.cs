namespace UI;

public class AllStoresMenu : IMenu {
    private StoreBL _sbl;

    public AllStoresMenu(StoreBL sbl){
        _sbl = sbl;
    }  
    public void Start(){
        bool valid = false;
        while (!valid){
            List<Store> allStores = _sbl.GetAllStores();
            //No stores exist
            if(allStores.Count == 0){
                Console.WriteLine("\nNo stores found!");
                valid = true;
                }
            //Found stores
            else{
            int i = 0;
            ColorWrite.wc("\n=================[All Stores]==================", ConsoleColor.DarkCyan);
            foreach(Store store in allStores){
                Console.WriteLine($"[{i}] {store.ToString()}");
                i++;
            }
            ColorWrite.wc("\n Select the store's ID to view its details.\n   Or enter [r] to [Return] to the Admin Menu.", ConsoleColor.DarkYellow);
            Console.WriteLine("=============================================");
            string? select = Console.ReadLine();
            int storeIndex;
            //Returns to menu
            if (select == "r"){
                valid = true;
                }
            else {
                //Checks for valid integer
                if(!int.TryParse(select, out storeIndex)){
                    Console.WriteLine("\nPlease select a valid input!");
                }
                else{
                    if (storeIndex >= 0 && storeIndex < allStores.Count){
                        int storeID = (int)allStores[storeIndex].ID!;
                        valid = true;
                        //Initializes store menu                        
                        MenuFactoryWithID.GetMenu("store").Start(storeID);
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