
namespace DL;
public interface ISBL{
    List<Store> GetAllStores();
    void AddStore(Store storeToAdd);

    void DeleteStore(int storeID);

    Store GetStoreByID(int storeID);

    int GetStoreIndexByID(int storeID);

    Product GetProductByID(int storeID, int prodID);

    int GetProductIndexByID(int storeID, int prodID);
    
    void AddProduct(int storeID, Product productToAdd);
    
    void DeleteProduct(int storeID, int prodID);
    
    void EditProduct(int storeID, int prodID, string description, decimal price, int quantity);
    
    void AddStoreOrder(int storeID, StoreOrder storeOrderToAdd);
}