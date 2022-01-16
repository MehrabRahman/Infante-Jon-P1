using System.Globalization;

namespace UI;
public class ShoppingStoreMenu {
    private StoreBL _sbl;
    private  UserBL _iubl;

    public ShoppingStoreMenu(UserBL iubl, StoreBL sbl){
        _sbl = sbl;
        _iubl = iubl;
    }
    public void Start(int storeID, int userID){
        bool valid = false;
        while (!valid){
            //Find our current products list
            Store currStore = _sbl.GetStoreByID(storeID);
            List<Product> allProducts = currStore.Products!;
            User currUser = _iubl.GetCurrentUserByID(userID);

            //If the products list hasn't been initialzied or is empty
            if(allProducts == null || allProducts.Count == 0){
                Console.WriteLine("\nNo products found!");
                valid = true;
                }
            else{
            int i = 0;
            ColorWrite.wc("\n================[All Products]=================", ConsoleColor.DarkCyan);
            Console.WriteLine($"{currStore.Name}\n");

            //Iterate over each product
            foreach(Product prod in allProducts){
                Console.WriteLine($"[{i}]  {prod.Name} | ${prod.Price} || Quantity: {prod.Quantity}\n     {prod.Description}");
                i++;
            }
            Console.WriteLine("\nSelect the product's index to make a purchase.");
            ColorWrite.wc("Or enter [r] to [Return] to the the list of Stores", ConsoleColor.DarkYellow);
            Console.WriteLine("=============================================");
            string? inputSelect = Console.ReadLine();
            int prodIndex = 0;
            //Return to the Product Menu
            if (inputSelect == "r"){
                valid = true;
                }
            else {
                if(!int.TryParse(inputSelect, out prodIndex)){
                    Console.WriteLine("\nPlease select a valid input!");
                }
                //Valid index found to edit a product
                else{
                    //Check if index is in range
                    if (prodIndex >= 0 && prodIndex < allProducts.Count){
                        int prodIDSelected = (int)allProducts[prodIndex!].ID!;
                        //Get product to make a purchase
                        Product selectedProduct = _sbl.GetProductByID(storeID, prodIDSelected);

                        Console.WriteLine($"How many {selectedProduct.Name}s would you like to order?");
                        enterAmount:
                        string? userInput = Console.ReadLine();
                        int selectedQuantity;
                        if(!int.TryParse(userInput, out selectedQuantity!)){
                            Console.WriteLine("Please enter a valid input:");
                            goto enterAmount;
                        }
                        else{
                            int prodQuantity = (int)selectedProduct.Quantity!;
                            if(prodQuantity == 0){
                                Console.WriteLine("\nSorry, we are out of stock of this item!");
                            }
                            else if(selectedQuantity > prodQuantity){
                                Console.WriteLine($"You may only purchase up to {prodQuantity} {selectedProduct.Name}s\nPlease enter a valid amount:");
                                goto enterAmount;
                            }
                            else if (selectedQuantity <= 0){
                                Console.WriteLine("Please enter an amount greater than 0:");
                                goto enterAmount;
                            }
                            else{
                                //Get total quantity and price of current product
                                decimal prodPrice = (decimal)selectedProduct.Price!;
                                int newQuantity = prodQuantity - selectedQuantity;
                                //Updates quantity remaining of the product
                                _sbl.EditProduct(storeID, prodIDSelected, selectedProduct.Description!, selectedProduct.Price!, newQuantity);
                                //get new product order id from datetime for incremental product order IDs mod 1 bil to get under int limity
                                int id = (int)((DateTime.Now.Subtract(DateTime.MinValue).TotalSeconds)%1000000000);
                                ProductOrder currOrder = new ProductOrder{
                                        ID = id!,
                                        userID = userID,
                                        storeID = storeID,
                                        storeOrderID = 0,
                                        userOrderID = 0,
                                        productID = selectedProduct.ID,
                                        ItemName = selectedProduct.Name!,
                                        TotalPrice = (selectedQuantity * prodPrice),
                                        Quantity = selectedQuantity!,
                                    };
                                //Add product order to user's shopping cart
                                _iubl.AddProductOrder(currUser, currOrder);

                                Console.WriteLine("\nYour order has been added to your shopping cart!");
                            }
                        }
                    }
                    //Integer out of range of the product list's index
                    else{
                        Console.WriteLine("\nPlease select an index within range!");
                    }
                }
            }
        }
    }
}
}   
