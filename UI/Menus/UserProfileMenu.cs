namespace UI;

public class UserProfileMenu : IMenuWithID {

    public UserProfileMenu(){
    }
    public void Start(int userID){
        bool exit = false;
        while(!exit){
            ColorWrite.wc("\n================[Profile Menu]=================", ConsoleColor.DarkCyan);
            Console.WriteLine("What would you like to do?\n");
            Console.WriteLine("[1] Shopping Cart");
            Console.WriteLine("[2] Previous Orders");
            ColorWrite.wc("\n      Enter [r] to [Return] to the User Menu", ConsoleColor.DarkYellow);
            Console.WriteLine("=============================================");

            string? input = Console.ReadLine();

            switch (input){
                case "1":
                    //Initializes the current user's shopping cart menu
                    MenuFactoryWithID.GetMenu("shoppingCart").Start(userID);  
                    break;
                case "2":
                    //Initialize the current user's previous order menu
                    MenuFactoryWithID.GetMenu("userOrder").Start(userID);
                    break;
                //Return to user menu
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