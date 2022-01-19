namespace BL;
using CustomExceptions;

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
    /// Deletes the current selected store
    /// </summary>
    /// <param name="storeID">Current store ID</param>
    public void DeleteStore(int storeID)
    {
        _dl.DeleteStore(storeID);
    }
    /// <summary>
    /// Adds a current store to the list of stores
    /// </summary>
    /// <param name="storeToAdd">Store object to add to the list</param>
    public void AddStore(Store storeToAdd)
    {
        _dl.AddStore(storeToAdd);
    }
    /// <summary>
    /// Gets all the products of the current store selected
    /// </summary>
    /// <param name="storeID">The selected store's ID</param>
    /// <returns>A list of products</returns>
    public List<Product> GetAllProducts(int storeID)
    {
        return _dl.GetAllProducts(storeID);
    }

    /// <summary>
    /// Gets the current product by the product ID
    /// </summary>
    /// <param name="prodID">current Product ID</param>
    /// <returns>Selected Product Object</returns>
    public Product GetProductByID(int prodID){
        return _dl.GetProductByID(prodID);
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
    /// <param name="prodID">Product's current D</param>
    public void DeleteProduct(int prodID){
        _dl.DeleteProduct(prodID);

    }
    /// <summary>
    /// Edits and updates the product selected in the current store
    /// </summary>
    /// <param name="prodID">ID of the current product</param>
    /// <param name="name">Product with new paramters to edit</param>

    public void EditProduct(int prodID, Product prodToEdit){
        _dl.EditProduct(prodID, prodToEdit);
    }
    /// <summary>
    /// Takes the current lists of product orders, packages them in a store order and adds to list
    /// </summary>
    /// <param name="storeID">ID of the current store</param>
    /// <param name="storeOrderToAdd">Store order packaged and ready to add</param>
    public void AddStoreOrder(int storeID, StoreOrder storeOrderToAdd){
        _dl.AddStoreOrder(storeID, storeOrderToAdd);
    }
    /// <summary>
    /// Gets a list of all store orders from a particular store. Sorts by default or high/low, old/new
    /// </summary>
    /// <param name="storeID">Selected store id</param>
    /// <param name="selection">The selection we want to order the store orders by, by time ordered or by total price</param>
    /// <returns>A List of store orders</returns>
    public List<StoreOrder> GetStoreOrders(int storeID, string selection)
    {
        return _dl.GetStoreOrders(storeID, selection);
    }

}
