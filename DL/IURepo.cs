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

    Product GetProductByID(int prodID);

    List<ProductOrder> GetAllProductOrders(string username);

    ProductOrder GetProductOrder(int prodOrderID);

    void AddProductOrder(string username, int prodID, int quantity);

    void EditProductOrder(int prodOrderID, int quantity, int storeOrderID, int userOrderID);

    void DeleteProductOrder(int prodOrderID);

    void AddStoreOrder(string username, StoreOrder currStoreOrder);
    void Checkout(string username);

    void ClearShoppingCart(User currUser);
}