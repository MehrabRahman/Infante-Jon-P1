namespace BL;
public interface IUBL{
    List<User> GetAllUsers();
    
    void AddUser(User userToAdd);
    
    User GetCurrentUserByUsername(string username);
    
    int GetCurrentUserIndexByID(int userID);
    bool LoginUser(string username, string password);


    void AddProductOrder(User currUser, ProductOrder currProdOrder);       
    
    void EditProductOrder(User currUser, int prodOrderID, int quantity, decimal TotalPrice, int storeOrderID, int userOrderID);
    
    void DeleteProductOrder(User currUser, int prodOrderID);
    
    void AddUserStoreOrder(User currUser, StoreOrder currStoreOrder);
    
    void ClearShoppingCart(User currUser);
}