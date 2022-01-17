namespace DL;

public interface IURepo {
    //No access modifiers. Interface members are implicetly public
    //Also lack method body
    List<User> GetAllUsers();

    void AddUser(User userToAdd);

    bool IsDuplicate(string username);

    User GetCurrentUserByUsername(string username);
    bool LoginUser(string username, string password);

    int GetCurrentUserIndexByID(int userID);

    void AddProductOrder(User currUser, ProductOrder currProdOrder);

    void EditProductOrder(User currUser, int prodOrderID, int quantity,decimal TotalPrice, int StoreOrderID, int userOrderID);

    void DeleteProductOrder(User currUser, int prodOrderID);

    void AddUserStoreOrder(User currUser, StoreOrder currStoreOrder);
    
    void ClearShoppingCart(User currUser);
}