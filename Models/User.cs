namespace Models;
public class User{

    public User(){}

    public User(DataRow row){
        ID = (int)row["ID"];
        Username = row["Username"].ToString();
        Password = row["Password"].ToString();
    }

    [System.Text.Json.Serialization.JsonIgnore]
    public int? ID { get; set; }
    [JsonProperty("ID")]
    public int? userID
    {
        get { return ID; }
    }
    public string? Username { get; set; }

    public string? Password { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public List<ProductOrder>? ShoppingCart { get; set; }
    [JsonProperty("ShoppingCart")]
    public List<ProductOrder>? Cart
    {
        get { return ShoppingCart; }
    }
    [System.Text.Json.Serialization.JsonIgnore]
    public List<StoreOrder>? FinishedOrders { get; set; }
    [JsonProperty("FinishedOrders")]
    public List<StoreOrder>? Orders
    {
        get { return FinishedOrders; }
    }
}
