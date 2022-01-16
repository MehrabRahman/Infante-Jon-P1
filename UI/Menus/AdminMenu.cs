namespace UI;
public class AdminMenu : IMenu {
    private StoreBL _sbl;

    public AdminMenu(StoreBL sbl){
        _sbl = sbl;
    }
    public void Start(){
        bool exit = false;
        while(!exit){
            ColorWrite.wc("\n=================[Admin Menu]==================", ConsoleColor.DarkCyan);
            Console.WriteLine("What would you like to do?\n");
            Console.WriteLine("[1] Add a new Store");
            Console.WriteLine("[2] View all Stores");            
            ColorWrite.wc("\n     Enter [r] to [Return] to the Login Menu", ConsoleColor.DarkYellow);
            Console.WriteLine("=============================================");
            
            string? input = Console.ReadLine();

            switch (input){
                case "1":
                    Console.WriteLine("Name: ");
                    string? name = Console.ReadLine();
                    Console.WriteLine("City: ");
                    string? city = Console.ReadLine();
                    Console.WriteLine("State: ");
                    string? state = Console.ReadLine();
                    Console.WriteLine("Address: ");
                    string? address = Console.ReadLine();

                    //get new store id from datetime for incremental store IDs mod 1 bil to get under int limity
                    int id = (int)((DateTime.Now.Subtract(DateTime.MinValue).TotalSeconds)%1000000000);

                    Store newStore= new Store{
                        ID = id!,
                        Name = name!,
                        City = city!,
                        State = state!,
                        Address = address!
                    };
                    //Adds a store to the list of stores
                    _sbl.AddStore(newStore);

                    Console.WriteLine("\nYour store had been added!");
                
                    break;
                case "2":
                    //Initializes all stores menu
                    MenuFactory.GetMenu("allStores").Start();          
                    break;
                case "r":
                    exit = true;
                    break;                
                default:
                    Console.WriteLine("\nI did not expect that command! Please try again with a valid input.");
                    break;
            }
        }        
    }
        

    }