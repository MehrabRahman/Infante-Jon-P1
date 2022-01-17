namespace Models;

public class Store {
    
    public Store(){}
    public Store(DataRow row)
    {
        this.ID = (int) row["ID"];
        this.Name = row["Name"].ToString() ?? "";
        this.Address = row["Address"].ToString() ?? "";
        this.City = row["City"].ToString() ?? "";
        this.State = row["State"].ToString() ?? "";
    }
    [System.Text.Json.Serialization.JsonIgnore]
    public int? ID { get;set; }
    [JsonProperty("ID")]
    public int? storeID
    {
        get { return ID; }
    }
    [Required]
    public string? Name { get; set;}
    [Required]
    public string? Address{ get; set;}
    [Required]
    public string? City { get; set; }
    [Required]
    public string? State { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public List<Product>? Products { get; set; }
    [JsonProperty("Products")]
    public List<Product>? AllProducts
    {
        get { return Products; }
    }
    [System.Text.Json.Serialization.JsonIgnore]
    public List<StoreOrder>? AllOrders { get; set; }
    [JsonProperty("AllOrders")]
    public List<StoreOrder>? AllStoreOrders
    {
        get { return AllOrders; }
    }
    public override string ToString(){
          return ($"Store: {this.Name}\n    City: {this.City}, State: {this.State}\n    Address: {this.Address}");
    }


}