using System.Text.Json;

namespace DL;
public class StoreRepo : ISRepo{

    public StoreRepo(){
    }
    //make path from UI folder to file location
    private string filePath = "../DL/Stores.json";

    /// <summary>
    /// Gets all stores from a file
    /// </summary>
    /// <returns>List of all stores</returns>
    public List<Store> GetAllStores(){
        //returns all restaurants written in the file
        string jsonString = File.ReadAllText(filePath)!;
        List<Store> jsonDeserialized = JsonSerializer.Deserialize<List<Store>>(jsonString)!;
        return jsonDeserialized;
    }   

    /// <summary>
    /// Adds a store to the file
    /// </summary>
    /// <param name="storeToAdd">Store object</param>
    public void AddStore(Store storeToAdd){
        List<Store> allStores = GetAllStores();
        allStores.Add(storeToAdd);
        string jsonString = JsonSerializer.Serialize(allStores);
        File.WriteAllText(filePath, jsonString);
    }
    /// <summary>
    /// Deletes the current selected store
    /// </summary>
    /// <param name="storeID">Current Store ID</param>
    public void DeleteStore(int storeID){
        List<Store> allStores = GetAllStores();
        int storeIndex = GetStoreIndexByID(storeID);
        allStores.RemoveAt(storeIndex);
        string jsonString = JsonSerializer.Serialize(allStores);
        File.WriteAllText(filePath, jsonString);    
    }
    /// <summary>
    /// Get the current store by store id
    /// </summary>
    /// <param name="storeID">Store's ID</param>
    /// <returns>Current store object</returns>
    public Store GetStoreByID(int storeID){
        List<Store> allStores = GetAllStores();
        Store currStore = new Store();
        foreach(Store store in allStores){
            if(store.ID == storeID){
                currStore = store;
            }
          }
        return currStore;
    }
    /// <summary>
    /// Get the index of the store in the all stores list by store ID
    /// </summary>
    /// <param name="storeID">current Store ID</param>
    /// <returns>index of current Store</returns>
    public int GetStoreIndexByID(int storeID){
        List<Store> allStores = GetAllStores();
        for (int i = 0; i < allStores.Count; i++){
            if (allStores[i].ID == storeID){
                return i;
            }
        }
        return 0;
    }
    /// <summary>
    /// Gets the current product by store and product id
    /// </summary>
    /// <param name="storeID">Store's ID</param>
    /// <param name="prodID">Product's ID</param>
    /// <returns>Current product object</returns>
    public Product GetProductByID(int storeID, int prodID){
        Store currStore = GetStoreByID(storeID);
        Product currProduct = new Product();
        foreach(Product product in currStore.Products!){
            if(product.ID == prodID){
                currProduct = product;
            }
        }
        return currProduct;
    }
        /// <summary>
    /// Get the index of the Product in the all stores list by Product ID
    /// </summary>
    /// <param name="storeID">Store's ID</param>
    /// <param name="prodID">current Product ID</param>
    /// <returns>index of current Product</returns>
    public int GetProductIndexByID(int storeID, int prodID){
        List<Store> allStores = GetAllStores();
        Store currStore = GetStoreByID(storeID);
        List<Product> allProducts = currStore.Products!;
        for (int i = 0; i < allProducts.Count; i++){
            if (allProducts[i].ID == prodID){
                return i;
            }
        }
        return 0;
    }
    /// <summary>
    /// Adds a product to the store's inventory
    /// </summary>
    /// <param name="storeID">Store ID</param>
    /// <param name="productToAdd">Product object to add</param>
    public void AddProduct(int storeID, Product productToAdd){
    List<Store> allStores = GetAllStores();
    Store currStore = GetStoreByID(storeID);
    if(currStore.Products == null){
        currStore.Products = new List<Product>();
        }
    currStore.Products.Add(productToAdd);
    //Update the current store as just selecting the store by store id wasn't saving it to database.
    allStores[GetStoreIndexByID(storeID)] = currStore;
    string jsonString = JsonSerializer.Serialize(allStores);
    File.WriteAllText(filePath, jsonString);
    }
    /// <summary>
    /// Deletes a product from the current store
    /// </summary>
    /// <param name="storeID">Store selected</param>
    /// <param name="prodID">Product selected</param>
    public void DeleteProduct(int storeID, int prodID){
        List<Store> allStores = GetAllStores();
        Store currStore = GetStoreByID(storeID);
        int currProdIndex = GetProductIndexByID(storeID!, prodID!);
        currStore.Products!.RemoveAt(currProdIndex);
        //Update the current store as just selecting the store by store id wasn't saving it to database.
        allStores[GetStoreIndexByID(storeID)] = currStore;
        string jsonString = JsonSerializer.Serialize(allStores);
        File.WriteAllText(filePath, jsonString);
    }       
    /// <summary>
    /// Edits the product with the correct description, price, and quantity
    /// </summary>
    /// <param name="storeID">ID of store to edit the product</param>
    /// <param name="prodIndex">Product selected's ID</param>
    /// <param name="description">New description</param>
    /// <param name="price">New price</param>
    /// <param name="quantity">New quantity</param>
    public void EditProduct(int storeID, int prodID, string description, decimal price, int quantity){
        List<Store> allStores = GetAllStores();
        Store currStore = GetStoreByID(storeID);
        int productIndex = GetProductIndexByID(storeID, prodID);
        Product currProduct = currStore.Products![productIndex];
        currProduct.Description = description;
        currProduct.Price = price;
        currProduct.Quantity = quantity;
        allStores[GetStoreIndexByID(storeID)] = currStore;
        string jsonString = JsonSerializer.Serialize(allStores);
        File.WriteAllText(filePath, jsonString);
    }
    /// <summary>
    /// Adds an order to the store's list of orders corresponding to the correct store
    /// </summary>
    /// <param name="storeID">ID of current store</param>
    /// <param name="storeOrderToAdd">StoreOrder object to add</param>
    public void AddStoreOrder(int storeID, StoreOrder storeOrderToAdd){
        List<Store> allStores = GetAllStores();
        Store currStore = GetStoreByID(storeID);
        //If no store orders exist yet
        if(currStore.AllOrders == null){
            currStore.AllOrders = new List<StoreOrder>();
            }
        currStore.AllOrders.Add(storeOrderToAdd);
        //Update the current store as just selecting the store by store id wasn't saving it to database.
        allStores[GetStoreIndexByID(storeID)] = currStore;
        string jsonString = JsonSerializer.Serialize(allStores);
        File.WriteAllText(filePath, jsonString);
    }

}