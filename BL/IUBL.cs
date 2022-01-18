namespace BL;
public interface IUBL{
    List<User> GetAllUsers();
    
    void AddUser(User userToAdd);
    
    User GetCurrentUserByUsername(string username);
    
    int GetCurrentUserIndexByID(int userID);

    bool LoginUser(string username, string password);

    Product GetProductByID(int prodID);

    List<ProductOrder> GetAllProductOrders(string username);

    ProductOrder GetProductOrder(int prodOrderID);

    void AddProductOrder(string username, int prodID, int quantity);

    void EditProductOrder(int prodOrderID, int quantity, int storeOrderID, int userOrderID);

    void DeleteProductOrder(int prodOrderID);
    
    void AddUserStoreOrder(User currUser, StoreOrder currStoreOrder);
    
    void ClearShoppingCart(User currUser);
}