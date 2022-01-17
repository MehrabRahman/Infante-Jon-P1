
namespace BL;
using CustomExceptions;
public class UserBL : IUBL {
    private IURepo _dl;

    public UserBL(IURepo repo) {
        _dl = repo;
    }
    /// <summary>
    /// Gets all users
    /// </summary>
    /// <returns>list of all users</returns>
    public List<User> GetAllUsers(){
        return _dl.GetAllUsers();

    }
    /// <summary>
    /// Returns a user by their username
    /// </summary>
    /// <param name="username">Username</param>
    /// <returns>User object</returns>
    public User GetCurrentUserByUsername(string username){
        return _dl.GetCurrentUserByUsername(username);
    }
    /// <summary>
    /// Returns a user's index by their ID
    /// </summary>
    /// <param name="userID">User ID</param>
    /// <returns>index of current user</returns>
    public int GetCurrentUserIndexByID(int userID){
        return _dl.GetCurrentUserIndexByID(userID);
    }
    /// <summary>
    /// Adds a new user to the list
    /// </summary>
    /// <param name="userToAdd">user object to add</param>
    public void AddUser(User userToAdd){
        if (!_dl.IsDuplicate(userToAdd.Username!))
        {
            _dl.AddUser(userToAdd);
        }
        else throw new DuplicateRecordException("A user with that username already exists!");
        
    }
    /// <summary>
    /// Adds a product order to the user's shopping list
    /// </summary>
    /// <param name="currUser">Current user [object]</param>
    /// <param name="currProdOrder">New product order to be added to the user's shopping cart</param>
    public void AddProductOrder(User currUser, ProductOrder currProdOrder){
        _dl.AddProductOrder(currUser, currProdOrder);
    }
    /// <summary>
    /// Edits an existing product's order by quantity
    /// </summary>
    /// <param name="currUser">Current user [object]</param>
    /// <param name="prodOrderID">Product order's ID in the shopping cart</param>
    /// <param name="quantity">New quantity to be update to</param>
    /// <param name="TotalPrice">New total price to be update to</param>
    /// <param name="storeOrderID">Update the store order id when we check out cart</param>
    /// <param name="userOrderID">Update the user's order id when we check out cart</param>
    public void EditProductOrder(User currUser, int prodOrderID, int quantity, decimal TotalPrice, int storeOrderID, int userOrderID){
        _dl.EditProductOrder(currUser, prodOrderID, quantity, TotalPrice, storeOrderID, userOrderID);
    }
    /// <summary>
    /// Deletes a product from your shopping list
    /// </summary>
    /// <param name="currUserIndex">Current user [object]</param>
    /// <param name="prodOrderID">Product order to delete by ID</param>
    public void DeleteProductOrder(User currUser , int prodOrderID){
        _dl.DeleteProductOrder(currUser, prodOrderID);
    }
    /// <summary>
    /// Adds a store order to the user's order list
    /// </summary>
    /// <param name="currUserIndex">Current user [object]</param>
    /// <param name="currStoreOrder">Store order to add</param>
    public void AddUserStoreOrder(User currUser, StoreOrder currStoreOrder){
        _dl.AddUserStoreOrder(currUser, currStoreOrder);
    }
    /// <summary>
    /// Clears the user's shopping cart
    /// </summary>
    /// <param name="currUser">Current user [object]</param>
    public void ClearShoppingCart(User currUser){
        _dl.ClearShoppingCart(currUser);
    }
}
