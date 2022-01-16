namespace UI;

public class UserMenu : IMenuWithID {
    private UserBL _iubl;
    private StoreBL _sbl;

    public UserMenu(UserBL iubl, StoreBL sbl){
        _iubl = iubl;
        _sbl = sbl;
    }
    public void Start(int userID){
        bool exit = false;
        User currUser =  _iubl.GetCurrentUserByID(userID);
        while(!exit){
            ColorWrite.wc("\n==================[User Menu]==================", ConsoleColor.DarkCyan);
            Console.WriteLine($"What would you like to do {currUser.Username}?\n");
            Console.WriteLine("[1] Browse Stores");
            Console.WriteLine("[2] View Profile");
            ColorWrite.wc("\n     Enter [r] to [Return] to the Login Menu", ConsoleColor.DarkYellow);
            Console.WriteLine("=============================================");

            string? inputSelection = Console.ReadLine();

            switch (inputSelection){
                case "1":
                    //Initialize all shopping stores menu
                    MenuFactoryWithID.GetMenu("allShoppingStores").Start(userID);
                    break;
                case "2":
                    //Initialize user profile menu
                    MenuFactoryWithID.GetMenu("userProfile").Start(userID);  
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