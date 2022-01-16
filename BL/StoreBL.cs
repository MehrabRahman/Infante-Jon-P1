namespace BL;

public class StoreBL : ISBL {
    private ISRepo _dl;

    public StoreBL(ISRepo repo) {
        _dl = repo;
    }
    /// <summary>
    /// Gets all stores
    /// </summary>
    /// <returns>list of all stores</returns>
    public List<Store> GetAllStores(){
        return _dl.GetAllStores();
    }
    /// <summary>
    /// Get current store by the store's ID
    /// </summary>
    /// <param name="storeID">storeID to look up</param>
    /// <returns>Store object</returns>
    public Store GetStoreByID(int storeID){
        return _dl.GetStoreByID(storeID);
        }
    /// <summary>
    /// Get current store's index by storeID
    /// </summary>
    /// <param name="storeID">storeID to look up</param>
    /// <returns>store index</returns>
    public int GetStoreIndexByID(int storeID){
        return _dl.GetStoreIndexByID(storeID);
        }
    /// <summary>
    /// Gets the current product by the product ID
    /// </summary>
    /// <param name="storeID">Current storeID</param>
    /// <param name="prodID">current Product ID</param>
    /// <returns>Selected Product Object</returns>
    public Product GetProductByID(int storeID, int prodID){
        return _dl.GetProductByID(storeID, prodID);
    }
    /// <summary>
    /// Gets the current product index by product ID
    /// </summary>
    /// <param name="storeID">Current Store ID</param>
    /// <param name="prodID">The selected Product's ID</param>
    /// <returns>The index of the product in the product list</returns>
    public int GetProductIndexByID(int storeID, int prodID){
        return _dl.GetProductIndexByID(storeID, prodID);
    }
    /// <summary>
    /// Deletes the current selected store
    /// </summary>
    /// <param name="storeID">Current store ID</param>
    public void DeleteStore(int storeID){
        _dl.DeleteStore(storeID);
    }
    /// <summary>
    /// Adds a current store to the list of stores
    /// </summary>
    /// <param name="storeToAdd">Store object to add to the list</param>
    public void AddStore(Store storeToAdd){
        _dl.AddStore(storeToAdd);
    }
    /// <summary>
    /// Adds a product to the current selected store
    /// </summary>
    /// <param name="storeID">ID of the current store</param>
    /// <param name="productToAdd">product we are adding to the store</param>
    public void AddProduct(int storeID, Product productToAdd){
        _dl.AddProduct(storeID, productToAdd);
    }
    /// <summary>
    /// Deletes a product from the current selected store and product index
    /// </summary>
    /// <param name="storeID">ID of the current store</param>
    /// <param name="prodIndex">Product's current index</param>
    public void DeleteProduct(int storeID, int prodIndex){
        _dl.DeleteProduct(storeID, prodIndex);

    }
    /// <summary>
    /// Edits and updates the product selected in the current store
    /// </summary>
    /// <param name="storeID">ID of the current store</param>
    /// <param name="prodID">ID of the current product</param>
    /// <param name="description">Product's new description</param>
    /// <param name="price">Product's new price</param>
    /// <param name="quantity">Product's new quantity</param>
    public void EditProduct(int storeIndex, int prodIndex, string description, decimal price, int quantity){
        _dl.EditProduct(storeIndex, prodIndex, description, price, quantity);
    }
    /// <summary>
    /// Takes the current lists of product orders, packages them in a store order and adds to list
    /// </summary>
    /// <param name="storeID">ID of the current store</param>
    /// <param name="storeOrderToAdd">Store order packaged and ready to add</param>
    public void AddStoreOrder(int storeID, StoreOrder storeOrderToAdd){
        _dl.AddStoreOrder(storeID, storeOrderToAdd);
    }

}
