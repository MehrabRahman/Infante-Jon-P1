namespace UI;

//Design pattern useful for making a similarly shaped object
public static class MenuFactoryWithID{
    public static IMenuWithID GetMenu(string menuString){
        string connectionString = File.ReadAllText("connectionString.txt");
        StoreBL sbl = new StoreBL(new DBStoreRepo(connectionString));
        UserBL iubl = new UserBL(new DBUserRepo(connectionString));
        // StoreBL sbl = new StoreBL(new StoreRepo());
        // UserBL iubl = new UserBL(new UserRepo());

        switch(menuString){
            //under login
            case "user":
                return new UserMenu(iubl, sbl);
            //under user
            case "allShoppingStores":
                return new AllShoppingStoresMenu(iubl, sbl);
            //under user
            case "userProfile":
                return new UserProfileMenu();
            //under user profile
            case "shoppingCart":
                return new ShoppingCart(iubl, sbl);
            //under user profile
            case "userOrder":
                return new UserOrderMenu(iubl);
            //under allStores
            case "store":
                return new StoreMenu(sbl);
            //under store
            case "product":
                return new ProductMenu(sbl);
            //under store
            case "storeOrder":
                return new StoreOrderMenu(iubl, sbl);
            //invalid menu selected
            default:
                Console.WriteLine("Invalid Menu selected");
                return new UserMenu(iubl, sbl);
        }

    }
}