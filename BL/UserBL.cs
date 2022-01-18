
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
    /// Validates if the user has entered the correct username and password
    /// </summary>
    /// <param name="username">username entered</param>
    /// <param name="password">password entered</param>
    /// <returns>Returns true if a valid username and password has been found, false if invalid password</returns>
    public bool LoginUser(string username, string password)
    {
        return _dl.LoginUser(username, password);
    }
    /// <summary>
    /// Gets a single product by id
    /// </summary>
    /// <param name="prodID">product id selected</param>
    /// <returns>A product object</returns>
    public Product GetProductByID(int prodID)
    {
        return _dl.GetProductByID(prodID);
    }
    /// <summary>
    /// Returns all product orders in a user's shopping cart
    /// </summary>
    /// <param name="username">Current username selected</param>
    /// <returns>All product orders in a user's cart</returns>
    public List<ProductOrder> GetAllProductOrders(string username)
    {
        return _dl.GetAllProductOrders(username);
    }
    /// <summary>
    /// Gets a specific product order by id
    /// </summary>
    /// <param name="prodOrderID">prod order id selected</param>
    /// <returns>A product order object</returns>
    public ProductOrder GetProductOrder(int prodOrderID)
    {
        return _dl.GetProductOrder(prodOrderID);
    }
    /// <summary>
    /// Adds a product order to the user's shopping list
    /// </summary>
    /// <param name="username">Current username selected</param>
    /// <param name="prodID">The product's id we have selected</param>
    /// <param name="quantity">Quantity of the product we have selected to order</param>
    public void AddProductOrder(string username, int prodID, int quantity){
        _dl.AddProductOrder(username, prodID, quantity);
    }
    /// <summary>
    /// Edits an existing product's order by quantity
    /// </summary>
    /// <param name="prodOrderID">Product order's ID in the shopping cart</param>
    /// <param name="quantity">New quantity to be update to</param>
    /// <param name="storeOrderID">Update the store order id when we check out cart</param>
    /// <param name="userOrderID">Update the user's order id when we check out cart</param>
    public void EditProductOrder(int prodOrderID, int quantity, int storeOrderID, int userOrderID){
        _dl.EditProductOrder(prodOrderID, quantity, storeOrderID, userOrderID);
    }
    /// <summary>
    /// Deletes a product from your shopping list
    /// </summary>
    /// <param name="prodOrderID">Product order to delete by ID</param>
    public void DeleteProductOrder(int prodOrderID){
        _dl.DeleteProductOrder(prodOrderID);
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
