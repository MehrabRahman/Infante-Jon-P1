/*using System.Text.Json;

namespace DL;
public class UserRepo : IURepo {

    public UserRepo(){
    }
    //make path from UI folder to file location
    private string? filePath = "../DL/Users.json";

    /// <summary>
    /// Gets all users from the file
    /// </summary>
    /// <returns>List of all users</returns>
    public List<User> GetAllUsers(){
        //returns all restaurants written in the file
        string jsonString = File.ReadAllText(filePath!);
        List<User> jsonDeserialized = JsonSerializer.Deserialize<List<User>>(jsonString)!;
        return jsonDeserialized!;
    }   

    /// <summary>
    /// Adds a user to the file
    /// </summary>
    /// <param name="userToAdd">User object</param>
    public void AddUser(User userToAdd){
        List<User> allUsers = GetAllUsers();
        allUsers.Add(userToAdd);
        string jsonString = JsonSerializer.Serialize(allUsers)!;
        File.WriteAllText(filePath!, jsonString!);
    }
    /// <summary>
    /// Gets the current user by their ID
    /// </summary>
    /// <param name="ID">integer of the user's ID</param>
    /// <returns>User object</returns>
    public User GetCurrentUserByID(int ID){
        List<User> allUsers = GetAllUsers();
        User currUser = new User();
        foreach(User user in allUsers){
            if (user.ID == ID){
                currUser = user;
            }
        }
        return currUser;
    }
    /// <summary>
    /// Gets the current user's index by their ID
    /// </summary>
    /// <param name="ID">integer of the user's ID</param>
    /// <returns>User's index in list of users'</returns>
    public int GetCurrentUserIndexByID(int userID){
        List<User> allUsers = GetAllUsers();
        int i = 0;
        foreach(User user in allUsers){
            if (user.ID == userID){
                return i;
            }
            i++;
        }
        return 0;
    }
    /// <summary>
    /// Adds a product to the shopping cart
    /// </summary>
    /// <param name="currUser">Current user [object]</param>
    /// <param name="currProdOrder">The current product object</param>
    public void AddProductOrder(User currUser, ProductOrder currProdOrder){
        List<User> allUsers = GetAllUsers();
        if(currUser.ShoppingCart == null)
            {
                currUser.ShoppingCart = new List<ProductOrder>();
            }
        currUser.ShoppingCart!.Add(currProdOrder!);
        //Remapping the current user to update the list of users
        allUsers[GetCurrentUserIndexByID((int)currUser.ID!)] = currUser;
        string jsonString = JsonSerializer.Serialize(allUsers)!;
        File.WriteAllText(filePath!, jsonString!);
    }
    /// <summary>
    /// Edits an existing product order in the shopping cart
    /// </summary>
    /// <param name="currUser">Current user object</param>
    /// <param name="prodOrderID">ID of the product order in the shopping cart</param>
    /// <param name="quantity">New Updates quantity</param>
    /// <param name="TotalPrice">New Updates for total price</param>
    /// <param name="storeOrderID">Edits the store order id when we check out the cart</param>
    /// <param name="suserOrderID">Edits the user's order id when we check out the cart</param>
    public void EditProductOrder(User currUser, int prodOrderID, int quantity,decimal TotalPrice, int storeOrderID, int userOrderID){
        List<User> allUsers = GetAllUsers();
        //Selected the currrent product based off the current user and the product order's iD in the shopping cart
        List<ProductOrder> allProdOrders = currUser.ShoppingCart!;
        int i = 0;
        foreach(ProductOrder prodOrder in allProdOrders){
            if (prodOrderID == prodOrder.ID){
                break;
            }
            else{
                i++;
            }
        }
        ProductOrder currProduct = allProdOrders[i]!;
        int oldQuantity = (int)currProduct.Quantity!;
        //First check to throw exception if quantity is not an integer
        currProduct.Quantity = quantity;
        //Replacing the old quantity back in for calculations
        currProduct.Quantity = oldQuantity;
        //Calculating the new total amount
        int intQuantity = quantity;
        decimal currentTotal = currProduct.TotalPrice!;
        int currentQuantity = (int)currProduct.Quantity!;
        decimal itemPrice = (currentTotal / currentQuantity);
        decimal newTotal = (itemPrice * intQuantity);
        //Declaring new quantity, total
        currProduct.TotalPrice = newTotal;
        currProduct.Quantity = quantity;
        //Remapping the current user to update the list of users
        allUsers[GetCurrentUserIndexByID((int)currUser.ID!)] = currUser;
        string jsonString = JsonSerializer.Serialize(allUsers);
        File.WriteAllText(filePath!, jsonString!);
    }
    /// <summary>
    /// Delete's a product order from the user's shopping cart
    /// </summary>
    /// <param name="currUser">Current user [object]</param>
    /// <param name="prodOrderID">Current product orders ID</param>
    public void DeleteProductOrder(User currUser, int prodOrderID){
        List<User> allUsers = GetAllUsers();
        List<ProductOrder> allProdOrders = currUser.ShoppingCart!;
        int i = 0;
        foreach(ProductOrder prodOrder in allProdOrders){
            if (prodOrderID == prodOrder.ID){
                break;
            }
            else{
                i++;
            }
        }
        allProdOrders!.RemoveAt(i);
        //Remapping the current user to update the list of users
        allUsers[GetCurrentUserIndexByID((int)currUser.ID!)] = currUser;
        string jsonString = JsonSerializer.Serialize(allUsers);
        File.WriteAllText(filePath!, jsonString!);
    }
    /// <summary>
    /// Adds a store order to the user's store order list
    /// </summary>
    /// <param name="currUser">Current user [object]</param>
    /// <param name="currStoreOrder">Store order to add</param>
    public void AddUserStoreOrder(User currUser, StoreOrder currStoreOrder){
        List<User> allUsers = GetAllUsers();
        if(currUser.FinishedOrders == null) {
            currUser.FinishedOrders = new List<StoreOrder>();
        }
        currUser.FinishedOrders.Add(currStoreOrder);

        //Remapping the current user to update the list of users
        allUsers[GetCurrentUserIndexByID((int)currUser.ID!)] = currUser;

        string jsonString = JsonSerializer.Serialize(allUsers);
        File.WriteAllText(filePath!, jsonString!);
    }
    /// <summary>
    /// Clears a user's shopping cart
    /// </summary>
    /// <param name="currUser">Current user [object]</param>
    public void ClearShoppingCart(User currUser){
        List<User> allUsers = GetAllUsers();
        currUser.ShoppingCart!.Clear();
        //Remapping the current user to update the list of users
        allUsers[GetCurrentUserIndexByID((int)currUser.ID!)] = currUser;
        string jsonString = JsonSerializer.Serialize(allUsers);
        File.WriteAllText(filePath!, jsonString!);
    }
}
*/