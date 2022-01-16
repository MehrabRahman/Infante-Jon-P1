namespace UI;

public class LoginMenu : IMenu {
    private IUBL _iubl;

    public LoginMenu(IUBL iubl){
        _iubl = iubl;
    }
    public void Start(){
        Console.WriteLine("\nWelcome to Jon's Conglomerate of used hardware stores.");
        bool exit = false;
        while(!exit){
            ColorWrite.wc("\n==================[Login Menu]=================", ConsoleColor.DarkCyan);
            Console.WriteLine("What would you like to do?\n");
            Console.WriteLine("[1] Sign Up");
            Console.WriteLine("[2] Login as User");
            Console.WriteLine("[3] Login as Administrator");
            ColorWrite.wc("\n\t        Enter [x] to [Exit]", ConsoleColor.DarkRed);
            Console.WriteLine("=============================================");

            string? input = Console.ReadLine();

            switch (input){
                case "1":
                    Console.WriteLine("Username: ");
                    string? username = Console.ReadLine();
                    List<User> users = _iubl.GetAllUsers();
                    bool userFound = false;
                    foreach(User user in users){
                        if (user.Username == username){
                            Console.WriteLine("\nUser already registered!");
                            userFound = true;
                            break;
                        }
                    }
                    //get new id for the user
                    bool isEmpty = !users.Any();
                    //get new user id from datetime for incremental user IDS mod 1 bil to get under int limit
                    int id = (int)((DateTime.Now.Subtract(DateTime.MinValue).TotalSeconds)%1000000000);
                    ///If the user isn't found, instantiate a new user
                    if (!userFound){
                        Console.WriteLine("Password: ");
                        string? password = Console.ReadLine();
                        //Generate hashed password
                        string hashedPassword = PasswordHash.GenerateHashedPassword(password!);
                        User newUser = new User{
                            ID = id!,
                            Username = username!,
                            Password = hashedPassword!,
                            };

                        _iubl.AddUser(newUser);
                        Console.WriteLine("\nSuccessfully signed up and logged in!");
                        //User Menu initialization | id is the user's ID
                        MenuFactoryWithID.GetMenu("user").Start(id);
                    }

                    break;
                case "2":
                    Console.WriteLine("\nWhat is your username?");
                    string? getUsername = Console.ReadLine();
                    List<User> currUsers = _iubl.GetAllUsers();
                    bool found = false;
                    User currUser = new User();
                    foreach(User user in currUsers){
                        if (user.Username == getUsername){
                            found = true;
                            currUser = user;
                            }
                        }
                    //If the current username is not found in the database
                    if (found == false){
                        Console.WriteLine("\nUsername not found!");
                    }
                    else{
                        Console.WriteLine("Password");
                        string? getPassword = Console.ReadLine();
                        //Validates for the correct password from store hashed password
                        if(PasswordHash.VeryifyHashedPassword(getPassword!, currUser.Password!)){
                            Console.WriteLine("\nLogin successful!");
                            //User Menu initialization
                            MenuFactoryWithID.GetMenu("user").Start((int)currUser.ID!);     
                        }
                        else{
                            Console.WriteLine("\nIncorrect password.");
                        }
                    }
                    break;
                case "3":
                    Console.WriteLine("\nPlease enter your admin key to continue.");
                    string? inp = Console.ReadLine();
                    if (inp == "emily"){
                        Console.WriteLine("\nLogged in to admin account.");
                        //Initialize Admin Menu
                        MenuFactory.GetMenu("admin").Start();     
                    }
                    else{
                        Console.WriteLine("\nIncorrect key!");
                    }
                    break;
                case "x":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("\nI did not expect that command! Please try again with a valid input.");
                    break;
            }   
        }
    }
}