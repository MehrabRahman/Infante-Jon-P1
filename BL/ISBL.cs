
namespace DL;
public interface ISBL{
    List<Store> GetAllStores();
    void AddStore(Store storeToAdd);

    void DeleteStore(int storeID);

    Store GetStoreByID(int storeID);

    int GetStoreIndexByID(int storeID);

    Product GetProductByID(int prodID);

    List<Product> GetAllProducts(int storeID);

    int GetProductIndexByID(int storeID, int prodID);
    
    void AddProduct(int storeID, Product productToAdd);
    
    void DeleteProduct(int prodID);
    
    void EditProduct(int prodID, Product prodToEdit);

    void AddStoreOrder(int storeID, StoreOrder storeOrderToAdd);

    List<StoreOrder> GetStoreOrders(int storeID, string selection);
}