namespace DL;

public class DBUserRepo : IURepo {

    private string _connectionString;
    public DBUserRepo(string connectionString){
        _connectionString = connectionString;
    }

    /// <summary>
    /// Adds a user object to the database
    /// </summary>
    /// <param name="userToAdd"></param>
    public void AddUser(User userToAdd){
        //Establishing new connection
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        //Our insert command to add a user
        string sqlCmd = "INSERT INTO Customer (ID, Username, Password) VALUES (@ID, @username, @pass)"; 
        using SqlCommand cmdAddUser= new SqlCommand(sqlCmd, connection);
        //Adding paramaters
        Random rnd = new Random();
        int id = rnd.Next(1000000);
        cmdAddUser.Parameters.AddWithValue("@ID", id);
        cmdAddUser.Parameters.AddWithValue("@username", userToAdd.Username);
        string hashedPassword = PasswordHash.GenerateHashedPassword(userToAdd.Password!);
        cmdAddUser.Parameters.AddWithValue("@pass", hashedPassword);
        //Executing command
        cmdAddUser.ExecuteNonQuery();
        connection.Close();
        //Log user has been added
        Log.Information("The user {user} has been added to the database with the ID {ID}", userToAdd.Username, userToAdd.ID);
    }
    /// <summary>
    /// Search for the user for matching username
    /// </summary>
    /// <param name="username">username we are searching for a duplicate</param>
    /// <returns>bool: true if there is duplicate, false if not</returns>
    public bool IsDuplicate(string username)
    {
        string searchQuery = $"SELECT * FROM Customer WHERE Username= @username";

        using SqlConnection connection = new SqlConnection(_connectionString);
        using SqlCommand cmd = new SqlCommand(searchQuery, connection);
        cmd.Parameters.AddWithValue("@username", username);


        connection.Open();

        using SqlDataReader reader = cmd.ExecuteReader();

        if (reader.HasRows)
        {
            //Query returned something, there exists a record that shares the same username 
            return true;
        }
        //no record was returned. No duplicate record in the db
        return false;
    }

    /// <summary>
    /// Gets the entire list of users from the database[named customer inside the database due to special naming by SQL]
    /// </summary>
    /// <returns>Returns a list of customer(user) objects</returns>
    public List<User> GetAllUsers(){
        List<User> allUsers = new List<User>();

        using SqlConnection connection = new SqlConnection(_connectionString);
        string customerSelect = "Select * From Customer";
        string productOrderSelect = "Select * From ProductOrder";
        string storeOrderSelect = "Select * From StoreOrder";
        
        //A single dataSet to hold all our data
        DataSet usSet = new DataSet();

        //Three different adapters for different tables
        using SqlDataAdapter customerAdapter = new SqlDataAdapter(customerSelect, connection);
        using SqlDataAdapter productOrderAdapter = new SqlDataAdapter(productOrderSelect, connection);
        using SqlDataAdapter storeOrderAdapter = new SqlDataAdapter(storeOrderSelect, connection);

        //Filling the Dataset with each table
        customerAdapter.Fill(usSet, "Customer");
        productOrderAdapter.Fill(usSet, "ProductOrder");
        storeOrderAdapter.Fill(usSet, "StoreOrder");

        //Declaring each data table from the dataset
        DataTable? customerTable = usSet.Tables["Customer"];
        DataTable? productOrderTable = usSet.Tables["ProductOrder"];
        DataTable? storeOrderTable = usSet.Tables["StoreOrder"];

        if(customerTable != null){   
            foreach(DataRow row in customerTable.Rows){
                //Use customer constructor with DataRow object to quickly create user with parameters
                User user = new User(row);

                //Assigns each product order corresponding to the current user
                if (productOrderTable != null){
                    user.ShoppingCart = productOrderTable.AsEnumerable().Where(r => (int) r["userID"] == user.ID && (int)r["storeOrderID"] == 0).Select(
                        r => new ProductOrder(r)
                    ).ToList();
                }
                //Assigns each store order corresponding to the current user [uses reference id to match user id]
                if (storeOrderTable != null){
                    user.FinishedOrders = storeOrderTable.AsEnumerable().Where(r => (int) r["referenceID"] == user.ID).Select(
                        r => new StoreOrder(r)
                    ).ToList();
                }
                //Adds each product order to each store order in the list of users
                if(productOrderTable != null){
                    foreach(StoreOrder storeOrder in user.FinishedOrders!){
                        storeOrder.Orders = productOrderTable!.AsEnumerable().Where(r => (int) r["userOrderID"] == storeOrder.ID).Select(
                            r => new ProductOrder(r)
                        ).ToList();
                        }
                    }  
                //Add each store to the list of users
                allUsers.Add(user);
            }
        }
        return allUsers;
    }

    /// <summary>
    /// Gets the current user from the list of users by name
    /// </summary>
    /// <param name="username">current usename selected</param>
    /// <returns>User Object</returns>
    public User GetCurrentUserByUsername(string username){
        List<User> allUsers = GetAllUsers();
        foreach(User user in allUsers){
            if(user.Username == username){
                return user;
            }
        }
        //User not found
        return new User();
    }
    /// <summary>
    /// Checks if a user has entered a valid username and password
    /// </summary>
    /// <param name="username">username entered</param>
    /// <param name="password">password enetered</param>
    /// <returns>A boolean value true if logged in, false if invalid password</returns>
    public bool LoginUser(string username, string password)
    {
        User currUser = GetCurrentUserByUsername(username);
        //Hashes the entered password and checks if it matches the user's hashed password
        if (PasswordHash.VeryifyHashedPassword(password, currUser.Password!))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Adds a product order to the user's shopping cart inside the database
    /// </summary>
    /// <param name="currUser">current user object inputted</param>
    /// <param name="currProdOrder">product order object to add to the database</param>
    public void AddProductOrder(User currUser, ProductOrder currProdOrder){
        //Establishing new connection
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        //Our insert command to add a product order
        string sqlCmd = "INSERT INTO ProductOrder (ID, userID, storeID, storeOrderID, userOrderID, productID, ItemName, TotalPrice, Quantity) VALUES (@ID, @userID, @storeID, @storeOrderID, @userOrderID, @productID, @itemName, @totPrice, @qty)"; 
        using SqlCommand cmdAddProductOrder= new SqlCommand(sqlCmd, connection);
        //Adding paramaters
        cmdAddProductOrder.Parameters.AddWithValue("@ID", currProdOrder.ID);
        cmdAddProductOrder.Parameters.AddWithValue("@userID", currProdOrder.userID);
        cmdAddProductOrder.Parameters.AddWithValue("@storeID", currProdOrder.storeID);
        cmdAddProductOrder.Parameters.AddWithValue("@storeOrderID", currProdOrder.storeOrderID);
        cmdAddProductOrder.Parameters.AddWithValue("@userOrderID", currProdOrder.userOrderID);
        cmdAddProductOrder.Parameters.AddWithValue("@productID", currProdOrder.productID);
        cmdAddProductOrder.Parameters.AddWithValue("@itemName", currProdOrder.ItemName);
        cmdAddProductOrder.Parameters.AddWithValue("@totPrice", currProdOrder.TotalPrice);
        cmdAddProductOrder.Parameters.AddWithValue("@qty", currProdOrder.Quantity);
        //Executing command
        cmdAddProductOrder.ExecuteNonQuery();
        connection.Close();
        Log.Information("A product order of {quantity} {itemName}s has been placed with the ID of {ID} to the user {currUsername}'s shopping cart",currProdOrder.Quantity, currProdOrder.ItemName, currProdOrder.ID, currUser.Username);
        }

    /// <summary>
    /// Edits a selected product order in the user's shopping cart, and saves it back to the database
    /// </summary>
    /// <param name="currUser">current user selected</param>
    /// <param name="prodOrderID">ID of the product order selected to edit</param>
    /// <param name="quantity">New quantity to update</param>
    /// <param name="TotalPrice">New total price to update</param>
    /// <param name="storeOrderID">Edit's the store order id when we checkout the cart</param>
    /// <param name="userOrderID">Edit's the user's order id when we checkout the cart</param>
    public void EditProductOrder(User currUser, int prodOrderID, int quantity, decimal TotalPrice, int storeOrderID, int userOrderID){
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        //Updates a single product by id, and passed in requirements
        string sqlEditCmd = $"UPDATE ProductOrder SET Quantity = @qty, TotalPrice = @tPrice, storeOrderID = @sOrderID, userOrderID = @uOrderID WHERE ID = @prodOrderID";
        using SqlCommand cmdEditProdOrder = new SqlCommand(sqlEditCmd, connection);
        //Adds the paramaters to the sql command
        cmdEditProdOrder.Parameters.AddWithValue("@qty", quantity);
        cmdEditProdOrder.Parameters.AddWithValue("@tPrice", TotalPrice);
        cmdEditProdOrder.Parameters.AddWithValue("@prodOrderID", prodOrderID);
        cmdEditProdOrder.Parameters.AddWithValue("@sOrderID", storeOrderID);
        cmdEditProdOrder.Parameters.AddWithValue("@uOrderID", userOrderID);
        //Edits the current product selected
        cmdEditProdOrder.ExecuteNonQuery();
        connection.Close();
        //If the user order id is 0, that means the product order is still in the cart.
        //When the user checks out, the user order id is changed to match for the store orders and we don't want to call the edit product logger
        if (userOrderID == 0){
            Log.Information("The product order with ID {ID} has been edited with a new quantity of {quantity} for user {currUsername}",prodOrderID, quantity, currUser.Username);
        }
    }

    /// <summary>
    /// Remove a Product Order from the user's shopping cart and database
    /// </summary>
    /// <param name="currUser">current user object inputted</param>
    /// <param name="prodOrderID">the product order ID of the product order we have selected</param>
    public void DeleteProductOrder(User currUser, int prodOrderID){
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        //Deletes a single product by id
        string sqlDelCmd = $"DELETE FROM ProductOrder WHERE ID = @prodOrderID";
        using SqlCommand cmdDelProdOrder = new SqlCommand(sqlDelCmd, connection);
        cmdDelProdOrder.Parameters.AddWithValue("@prodOrderID", prodOrderID);
        //Deletes the current product selected
        cmdDelProdOrder.ExecuteNonQuery();
        connection.Close();
        Log.Information("The product order with ID {ID} has been deleted from the user {user}'s shopping cart", prodOrderID, currUser.Username);

    }

    /// <summary>
    /// Adds a store order to the user's list of previous orders and to the database
    /// </summary>
    /// <param name="currUser">current user object selected</param>
    /// <param name="currStoreOrder">storeOrder object to add to previous orders</param>
    public void AddUserStoreOrder(User currUser, StoreOrder currStoreOrder){
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        string sqlInsertCmd = "INSERT INTO StoreOrder (ID, userID, referenceID, storeID, currDate, DateSeconds, TotalAmount) VALUES (@ID, @uID, @refID, @stID, @date, @dateS, @tAmount)";
        //Creates the new sql command
        using SqlCommand cmd = new SqlCommand(sqlInsertCmd, connection);
        //Adds the paramaters to the insert command
        cmd.Parameters.AddWithValue("@ID", currStoreOrder.ID);
        cmd.Parameters.AddWithValue("@uID", currStoreOrder.userID);
        cmd.Parameters.AddWithValue("@refID", currStoreOrder.referenceID);
        cmd.Parameters.AddWithValue("@stID", currStoreOrder.storeID);
        cmd.Parameters.AddWithValue("@date", currStoreOrder.currDate);
        cmd.Parameters.AddWithValue("@dateS", currStoreOrder.DateSeconds);
        cmd.Parameters.AddWithValue("@tAmount", currStoreOrder.TotalAmount);
        //Executes the insert command
        cmd.ExecuteNonQuery();
        connection.Close();
        Log.Information("The user {currUsername} has checked out and created a new store order with ID {ID} with a total amount of ${totAmount}",currUser.Username, currStoreOrder.ID, currStoreOrder.TotalAmount);
    }

    //Unused with database implementation
    public void ClearShoppingCart(User currUser){}

    //Unused with database implementation
    public int GetCurrentUserIndexByID(int userID){
        return 0;
    }
}