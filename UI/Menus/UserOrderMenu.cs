namespace UI;

public class UserOrderMenu : IMenuWithID {
    private UserBL _iubl;

    public UserOrderMenu(UserBL iubl){
        _iubl = iubl;
    }
    public void Start(int userID){
        bool exit = false;
        User currUser = _iubl.GetCurrentUserByID(userID);
        List<StoreOrder> finishedOrders = currUser.FinishedOrders!;
        bool timeSort = false;
        bool costSort = false;
        while(!exit){
            if(finishedOrders == null || finishedOrders.Count == 0){
                Console.WriteLine("\nNo Orders found!");
                exit = true;
                }
            else{
            ColorWrite.wc("\n====================[Orders]===================", ConsoleColor.DarkCyan);
            foreach(StoreOrder storeorder in finishedOrders){
                Console.WriteLine($"\n{storeorder.currDate}");
                Console.WriteLine("|-------------------------------------------|");
                foreach(ProductOrder pOrder in storeorder.Orders!){
                    Console.WriteLine($"| {pOrder.ItemName} | Qty: {pOrder.Quantity} || ${pOrder.TotalPrice}");
                }
                Console.WriteLine("|-------------------------------------------|");
                Console.WriteLine($"| Total Price: ${storeorder.TotalAmount}");
            }
            if(!timeSort){
                ColorWrite.wc("\nEnter [s] to to [Sort] your orders by most recent", ConsoleColor.Magenta);
            }
            else{
                ColorWrite.wc("\nEnter [s] to to [Sort] your orders by first ordered", ConsoleColor.Magenta);
            }
            if(costSort){
                ColorWrite.wc(" Enter [c] to [Sort] orders by most expensive", ConsoleColor.Green);
            }
            else{
                ColorWrite.wc(" Enter [c] to [Sort] orders by least expensive", ConsoleColor.Green);
            }
            ColorWrite.wc("    Enter [r] to [Return] to the Profile Menu", ConsoleColor.DarkYellow);
            Console.WriteLine("=============================================");

            string? input = Console.ReadLine();

            switch (input){
                case "r":
                    exit = true;
                    break;
                case "s":
                    //Sorts the orders in most recent first
                    if (!timeSort){
                        timeSort = true;
                        finishedOrders.Sort((x, y) => y.DateSeconds.CompareTo(x.DateSeconds));
                    }
                    //Sorts the orders by last ordered first
                    else{
                        timeSort = false;
                        finishedOrders.Sort((x, y) => x.DateSeconds.CompareTo(y.DateSeconds));
                    }
                    break;
                case "c":
                    //Sorts the orders in most expensive first
                    if (!costSort){
                        costSort = true;
                        finishedOrders.Sort((x, y) => x.TotalAmount.CompareTo(y.TotalAmount));
                    }
                    //Sorts the orders by least expensive first
                    else{
                        costSort = false;
                        finishedOrders.Sort((x, y) => y.TotalAmount.CompareTo(x.TotalAmount));
                    }
                    break;
                default:
                    Console.WriteLine("\nI did not expect that command! Please try again with a valid input.");
                    break;       
            }
        }
        }
    }
 }