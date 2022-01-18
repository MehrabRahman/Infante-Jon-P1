
namespace DL;

public class DBStoreRepo : ISRepo {

    private string _connectionString;
    public DBStoreRepo(string connectionString){
        _connectionString = connectionString;
    }

    /// <summary>
    /// Adds a store to the database with the given store object
    /// </summary>
    /// <param name="storeToAdd"></param>
    public void AddStore(Store storeToAdd){
        //Establishing new connection
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        //Our insert command to add a store
        string sqlCmd = "INSERT INTO Store (ID, Address, Name, City, State) VALUES (@ID, @address, @name, @city, @state)"; 
        using SqlCommand cmdAddStore = new SqlCommand(sqlCmd, connection);
        //Adding paramaters
        //Making new random number for id
        Random rnd = new Random();
        int id = rnd.Next(1000000);
        cmdAddStore.Parameters.AddWithValue("@ID", id);
        cmdAddStore.Parameters.AddWithValue("@address", storeToAdd.Address);
        cmdAddStore.Parameters.AddWithValue("@name", storeToAdd.Name);
        cmdAddStore.Parameters.AddWithValue("@city", storeToAdd.City);
        cmdAddStore.Parameters.AddWithValue("@state", storeToAdd.State);
        //Executing command
        cmdAddStore.ExecuteNonQuery();
        connection.Close();
        Log.Information("A new store has been added with Name: {name}, City: {city}, State: {state}, Address: {address} and ID of {ID}",storeToAdd.Name, storeToAdd.City, storeToAdd.State, storeToAdd.Address, storeToAdd.ID);
    }


    /// <summary>
    /// Gets a list of all the stores from the database. Fills in the nested lists within each store for
    /// products, store orders, and product orders inside each store order.
    /// </summary>
    /// <returns>List of Store Objects</returns>
    public List<Store> GetAllStores(){
        List<Store> allStores = new List<Store>();

        using SqlConnection connection = new SqlConnection(_connectionString);
        string storeSelect = "Select * From Store";
        string productSelect = "Select * From Product";
        string storeOrderSelect = "Select * From StoreOrder";
        string productOrderSelect = "Select * From ProductOrder";
        
        //A single dataSet to hold all our data
        DataSet StSet = new DataSet();

        //Three different adapters for different tables
        using SqlDataAdapter storeAdapter = new SqlDataAdapter(storeSelect, connection);
        using SqlDataAdapter productAdapter = new SqlDataAdapter(productSelect, connection);
        using SqlDataAdapter storeOrderAdapter = new SqlDataAdapter(storeOrderSelect, connection);
        using SqlDataAdapter productOrderAdapter = new SqlDataAdapter(productOrderSelect, connection);

        //Filling the Dataset with each table
        storeAdapter.Fill(StSet, "Store");
        productAdapter.Fill(StSet, "Product");
        storeOrderAdapter.Fill(StSet, "StoreOrder");
        productOrderAdapter.Fill(StSet, "ProductOrder");

        //Declaring each data table from the dataset
        DataTable? storeTable = StSet.Tables["Store"];
        DataTable? productTable = StSet.Tables["Product"];
        DataTable? storeOrderTable = StSet.Tables["StoreOrder"];
        DataTable? productOrderTable = StSet.Tables["ProductOrder"];

        if(storeTable != null){   
            foreach(DataRow row in storeTable.Rows){
                //Use store constructor with DataRow object to quickly create store with parameters
                Store store = new Store(row);

                //Assigns each product corresponding to the current store
                if (productTable != null){
                    store.Products = productTable.AsEnumerable().Where(r => (int) r["storeID"] == store.ID).Select(
                        r => new Product(r)
                    ).ToList();
                }
                //Assigns each store order corresponding to the current store
                if (storeOrderTable != null){
                    store.AllOrders = storeOrderTable.AsEnumerable().Where(r => (int) r["storeID"] == store.ID).Select(
                        r => new StoreOrder(r)
                    ).ToList();
                }
                //Adds each product order to each store order in the list of stores
                if (productOrderTable != null){
                    foreach(StoreOrder storeOrder in store.AllOrders!){
                        storeOrder.Orders = productOrderTable!.AsEnumerable().Where(r => (int) r["storeOrderID"] == storeOrder.ID).Select(
                            r => new ProductOrder(r)
                        ).ToList();
                        }
                    }  
                //Add each store to the list of stores
                allStores.Add(store);
            }
        }
        return allStores;
    }
    /// <summary>
    /// Deletes an entire store from the database
    /// </summary>
    /// <param name="storeID">current store ID selected</param>
    public void DeleteStore(int storeID){
        string storeName = GetStoreByID(storeID).Name!;
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        //Deletes all the products of the current store
        string sqlCascadeDelPOrder = $"DELETE FROM ProductOrder WHERE storeOrderID = @0 AND storeID = @stID";
        string sqlCascadeDelProd = $"DELETE FROM Product WHERE storeID = @st2ID";
        string sqlCascadeDelSOrder = $"DELETE FROM StoreOrder WHERE storeID = @st3ID";
        string sqlDelCmd = $"DELETE FROM Store WHERE ID = @st4ID";
        //Delete product order sql
        using SqlCommand cmdPOrder = new SqlCommand(sqlCascadeDelPOrder, connection);
        cmdPOrder.Parameters.AddWithValue("@0", 0);
        cmdPOrder.Parameters.AddWithValue("@stID",storeID);
        //Delete product sql
        using SqlCommand cmdProd = new SqlCommand(sqlCascadeDelProd, connection);
        cmdProd.Parameters.AddWithValue("@st2ID", storeID);        
        //Delete store order sql
        using SqlCommand cmdSOrder= new SqlCommand(sqlCascadeDelSOrder, connection);
        cmdSOrder.Parameters.AddWithValue("@st3ID", storeID);
        //Delete store sql
        using SqlCommand cmdDelStore = new SqlCommand(sqlDelCmd, connection);
        cmdDelStore.Parameters.AddWithValue("@st4ID", storeID);
        //Deletes all the product orders in the shopping cart with the storeID selected
        cmdPOrder.ExecuteNonQuery();
        //Deletes all the products with the storeID selected
        cmdProd.ExecuteNonQuery();
        //Deletes all the store orders with the storeID selected
        cmdSOrder.ExecuteNonQuery();
        //Deletes the current store after all products with the store id are removed
        cmdDelStore.ExecuteNonQuery();
        connection.Close();
        Log.Information("The store {storename} with an ID of {storeID} has been deleted as well as all of its corresponding products in its inventory", storeName, storeID);

    }

    /// <summary>
    /// Gets the current store details from the list of stores
    /// </summary>
    /// <param name="storeID">selected storeID</param>
    /// <returns>Store object</returns>
    public Store GetStoreByID(int storeID){
        List<Store> allStores = GetAllStores();
        foreach (Store store in allStores)
        {
            if (store.ID == storeID)
            {
                return store;
            }
        }
        //Cant find any stores with that id
        return new Store();
    }

    /// <summary>
    /// Adds a product to the selected store inside the database
    /// </summary>
    /// <param name="storeID">current storeID selected</param>
    /// <param name="productToAdd">Product object to add to t he database</param>
    public void AddProduct(int storeID, Product productToAdd){
        //Establishing new connection
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        //Our insert command to add a product
        string sqlCmd = "INSERT INTO Product (ID, storeID, Name, Description, Price, Quantity) VALUES (@ID, @storeID, @name, @desc, @price, @qty)"; 
        using SqlCommand cmdAddProduct = new SqlCommand(sqlCmd, connection);
        //Adding paramaters
        //Making new random number for id
        Random rnd = new Random();
        int id = rnd.Next(1000000);
        cmdAddProduct.Parameters.AddWithValue("@ID", id);
        cmdAddProduct.Parameters.AddWithValue("@storeID", storeID);
        cmdAddProduct.Parameters.AddWithValue("@name", productToAdd.Name);
        cmdAddProduct.Parameters.AddWithValue("@desc", productToAdd.Description);
        cmdAddProduct.Parameters.AddWithValue("@price", productToAdd.Price);
        cmdAddProduct.Parameters.AddWithValue("@qty", productToAdd.Quantity);
        //Executing command
        cmdAddProduct.ExecuteNonQuery();
        connection.Close();
        Log.Information("The product {productname} with a price of {price} and a quantity of {quantity}, has been added to the store {storename}", productToAdd.Name, productToAdd.Price, productToAdd.Quantity,  GetStoreByID(storeID).Name);
    }
    /// <summary>
    /// Gets all the current products of a specified store in the database
    /// </summary>
    /// <param name="storeID"></param>
    /// <returns>A list of products</returns>
    public List<Product> GetAllProducts(int storeID)
    {
        string selectProdCmd = "SELECT * FROM Product WHERE StoreID = @storeID";
        SqlConnection connection = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand(selectProdCmd, connection);
        cmd.Parameters.AddWithValue("@storeID", storeID);

        DataSet productSet = new DataSet();

        SqlDataAdapter adapter = new SqlDataAdapter(cmd);

        adapter.Fill(productSet, "product");

        DataTable productTable = productSet.Tables["product"]!;

        List<Product> allProducts = new List<Product>();
        foreach (DataRow row in productTable.Rows)
        {
            allProducts.Add(new Product
            {
                ID = (int)row["ID"],
                storeID = (int)row["StoreID"],
                Name = (string)row["Name"] ?? "",
                Description = (string)row["Description"] ?? "",
                Price = (decimal)row["Price"],
                Quantity = (int)row["Quantity"]
            });
        }
        return allProducts;
    }

    /// <summary>
    /// Returns a product by the ID given
    /// </summary>
    /// <param name="prodID">product object ID selected</param>
    /// <returns>Product Object</returns>
    public Product GetProductByID(int prodID){
        string query = "SELECT * From Product WHERE ID = @ID";
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@ID", prodID);

        using SqlDataReader reader = cmd.ExecuteReader();
        Product selectedProduct = new Product();
        if (reader.Read())
        {
            selectedProduct.ID = reader.GetInt32(0);
            selectedProduct.storeID= reader.GetInt32(1);
            selectedProduct.Name = reader.GetString(2);
            selectedProduct.Description = reader.GetString(3);
            selectedProduct.Price = reader.GetDecimal(4);
            selectedProduct.Quantity = reader.GetInt32(5);
        }
        connection.Close();
        return selectedProduct;
    }

    /// <summary>
    /// Delets a product from the database
    /// </summary>
    /// <param name="prodID">selected product ID</param>
    public void DeleteProduct(int prodID){
        //Getting the store name by product id before we delete the product
        string storeName = GetStoreByID((int)GetProductByID(prodID).StoreID!).Name!;
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        //Deletes all the products orders of the current store
        string sqlCascadeDelPOrder = $"DELETE FROM ProductOrder WHERE storeOrderID = @0 AND productID = @prodID1";
        //Delets a single product by id
        string sqlDelCmd = $"DELETE FROM Product WHERE ID = @prodID2";
        //Delete product order sql
        using SqlCommand cmdDelPOrder = new SqlCommand(sqlCascadeDelPOrder, connection);
        cmdDelPOrder.Parameters.AddWithValue("@0", 0);
        cmdDelPOrder.Parameters.AddWithValue("@prodID1", prodID);
        //delets the product
        using SqlCommand cmdDelProd = new SqlCommand(sqlDelCmd, connection);
        cmdDelProd.Parameters.AddWithValue("@prodID2", prodID);
         //Deletes the currentorders in any user's shopping cart selected
        cmdDelPOrder.ExecuteNonQuery();
        //Deletes the current product selected
        cmdDelProd.ExecuteNonQuery();
        connection.Close();
        Log.Information("The product with an ID of {productID} has been deleted from the store {storename}}", prodID, storeName);
    }

    /// <summary>
    /// Edits a product and saves it back to the database
    /// </summary>
    /// <param name="prodID">selected product ID</param>
    /// <param name="prodToEdit">New product parameters to edit</param>
   
    public void EditProduct(int prodID, Product prodToEdit){
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        //Updates a single product by id, and passed in requirements
        string sqlEditCmd = $"UPDATE Product SET Description = @desc, Price = @prc, Quantity = @qty, Name = @name WHERE ID = @prodID";
        using SqlCommand cmdEditProd = new SqlCommand(sqlEditCmd, connection);
        //Adds the paramaters to the sql command
        cmdEditProd.Parameters.AddWithValue("@desc", prodToEdit.Description);
        cmdEditProd.Parameters.AddWithValue("@prc", prodToEdit.Price);
        cmdEditProd.Parameters.AddWithValue("@qty", prodToEdit.Quantity);
        cmdEditProd.Parameters.AddWithValue("@name", prodToEdit.Name);
        cmdEditProd.Parameters.AddWithValue("@prodID", prodID);
        //Edits the current product selected
        cmdEditProd.ExecuteNonQuery();
        connection.Close();
        Log.Information("The product {productname} has been updated with a description of {description}, price of {price}, and a quantity of {quantity}", prodToEdit.Name, prodToEdit.Description, prodToEdit.Price, prodToEdit.Quantity);

    }

    /// <summary>
    /// Adds a store order to the database with the correct information to be retrieved later
    /// </summary>
    /// <param name="storeID">current store ID</param>
    /// <param name="storeOrderToAdd">Store object to add to the database</param>
    public void AddStoreOrder(int storeID, StoreOrder storeOrderToAdd){
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        string sqlInsertCmd = "INSERT INTO StoreOrder (ID, userID, userName, referenceID, storeID, currDate, DateSeconds, TotalAmount) VALUES (@ID, @uID, @username, @refID, @stID, @date, @dateS, @tAmount)";
        //Creates the new sql command
        using SqlCommand cmd = new SqlCommand(sqlInsertCmd, connection);
        //Adds the paramaters to the insert command
        cmd.Parameters.AddWithValue("@ID", storeOrderToAdd.ID);
        cmd.Parameters.AddWithValue("@uID", storeOrderToAdd.userID);
        cmd.Parameters.AddWithValue("@username", storeOrderToAdd.userName);
        cmd.Parameters.AddWithValue("@refID", storeOrderToAdd.referenceID);
        cmd.Parameters.AddWithValue("@stID", storeOrderToAdd.storeID);
        cmd.Parameters.AddWithValue("@date", storeOrderToAdd.currDate);
        cmd.Parameters.AddWithValue("@dateS", storeOrderToAdd.DateSeconds);
        cmd.Parameters.AddWithValue("@tAmount", storeOrderToAdd.TotalAmount);
        //Executes the insert command
        cmd.ExecuteNonQuery();
        connection.Close();
        Log.Information("An order has been added to the store {storename} from the user {username} with a total amount of ${totAmount}", GetStoreByID(storeID).Name, storeOrderToAdd.userName, storeOrderToAdd.TotalAmount);

    }
        
    //Unused with database implementation
    public int GetProductIndexByID(int storeID, int prodID){
        return 0;
    }
    //Unused with database imnlementation
    public int GetStoreIndexByID(int storeID){
        return 0;
    }

}