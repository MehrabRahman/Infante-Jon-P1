
namespace DL;
public interface ISRepo{
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

    void EditProduct( int prodID, string name, string description, decimal price, int quantity);
    
    void AddStoreOrder(int storeID, StoreOrder storeOrderToAdd);


}