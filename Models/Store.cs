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

    public int? ID { get; set; }

    public string? Name { get; set;}

    public string? Address{ get; set;}

    public string? City { get; set; }
    
    public string? State { get; set; }

    public List<Product>? Products { get; set; }

    public List<StoreOrder>? AllOrders { get; set; }

    public override string ToString(){
          return ($"Store: {this.Name}\n    City: {this.City}, State: {this.State}\n    Address: {this.Address}");
    }


}