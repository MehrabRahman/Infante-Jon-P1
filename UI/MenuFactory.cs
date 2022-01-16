namespace UI;

//Design pattern useful for making a similarly shaped object
public static class MenuFactory{
    public static IMenu GetMenu(string menuString){
        string connectionString = File.ReadAllText("connectionString.txt");
        StoreBL sbl = new StoreBL(new DBStoreRepo(connectionString));
        UserBL iubl = new UserBL(new DBUserRepo(connectionString));
        // StoreBL sbl = new StoreBL(new StoreRepo());
        // UserBL iubl = new UserBL(new UserRepo());

        switch(menuString){
            //root
            case "login":
                return new LoginMenu(iubl);
            //under login
            case "admin":
                return new AdminMenu(sbl);
            //under admin
            case "allStores":
                return new AllStoresMenu(sbl);
            //invalid menu selected
            default:
                Console.WriteLine("Invalid Menu selected");
                return new LoginMenu(iubl);
        }

    }
}