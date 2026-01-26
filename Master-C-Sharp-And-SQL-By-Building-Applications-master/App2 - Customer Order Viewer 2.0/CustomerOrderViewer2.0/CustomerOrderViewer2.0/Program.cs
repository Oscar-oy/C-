using CustomerOrderViewer2._0.Models;
using CustomerOrderViewer2._0.Repository;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Reflection.Metadata;

namespace CustomerOrderViewer2._0
{
    internal class Program
    {
        /// <summary>
        /// Learning how to manage SQL databases and creating commands to connect with them.
        /// Empezamos el programa declarando las variables privadas de la clase. 
        /// Definimos la conexión a la base de datos, y después definimos cada command. 
        /// </summary>
        private static string _connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=CustomerOrderViewer;Integrated Security=True";
        private static readonly CustomerOrderCommand _customerOrderCommand = new CustomerOrderCommand(_connectionString);
        private static readonly CustomerCommand _customerCommand = new CustomerCommand(_connectionString);
        private static readonly ItemCommand _itemCommand = new ItemCommand(_connectionString);
        static void Main(string[] args)
        {
            //Definimos el programa dentro de un try catch para que no de problemas con la base de datos.
            try
            {
                var continueManaging = true;
                var userId = string.Empty;
                Console.WriteLine("What is your user name?");
                userId = Console.ReadLine();

                //Vamos a hacer un bucle por pantalla que nos ofrezca opciones y las ejecute hasta que eljamos la opción 4
                do {
                    Console.WriteLine("1 - Show all | 2 - Upsert Customer Order | 3 - Delete Customer Order | 4 - Exit");
                    int option = Convert.ToInt32(Console.ReadLine());

                    if (option == 1)
                    {
                        ShowAll();
                    }
                    else if (option == 2)
                    {
                        UpsertCustomerOrder(userId);
                    }else if (option == 3){
                        DeleteCustomerOrder(userId);
                    }else if (option == 4)
                    {
                        continueManaging = false;
                    }
                    else
                    {
                        Console.WriteLine("Option not found.");
                    }

                } while (continueManaging == true);

            }catch(Exception ex)
            {
                Console.WriteLine("Something went wrong: {0}", ex.Message);
            }
        }

        private static void DeleteCustomerOrder(string userId)
        {
            Console.WriteLine("Enter CustomerOrderId:");
            int customerOrderId = Convert.ToInt32(Console.ReadLine());

            _customerOrderCommand.Delete(customerOrderId, userId);

        }

        private static void UpsertCustomerOrder(string userId)
        {
            Console.WriteLine("Note: for updating insert existing CustomerOrderId, for new entries enter -1.");

            Console.WriteLine("Enter CustomerOrderId:");
            int newCustomerOrderId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter CustomerId");
            int newCustomerId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter ItemId");
            int newItemId = Convert.ToInt32(Console.ReadLine());

            _customerOrderCommand.Upsert(newCustomerOrderId, newCustomerId, newItemId, userId);
        }

        private static void ShowAll()
        {
            Console.WriteLine("{0}All Customer Orders {1}", Environment.NewLine, Environment.NewLine);
            DisplayCustomerOrders();

            Console.WriteLine("{0}All Customers {1}", Environment.NewLine, Environment.NewLine);
            DisplayCustomers();

            Console.WriteLine("{0}All Customer Items {1}", Environment.NewLine, Environment.NewLine);
            DisplayItems();

            Console.WriteLine();
        }

        private static void DisplayItems()
        {
            IList<ItemModel> items = _itemCommand.GetList();

            if (items.Any())
            {
                foreach(ItemModel item in items)
                {
                    Console.WriteLine("{0}: Description: {1}, Price: {2}", item.ItemId, item.description, item.price);
                }
            }
        }

        private static void DisplayCustomers()
        {
            IList<CustomerModel> customers = _customerCommand.GetList();

            if (customers.Any())
            {
                foreach (CustomerModel customer in customers)
                {
                    Console.WriteLine("{0}: First Name: {1}, Middle Name: {2}, Last Name: {3}, Age: {4}", customer.CustomerId, customer.FirstName, customer.MiddleName ?? "N/A", customer.LastName, customer.Age);
                }
            }
        }

        private static void DisplayCustomerOrders()
        {
            IList<CustomerOrderDetailModel> customerOrderDetails = _customerOrderCommand.GetList();

            if (customerOrderDetails.Any())
            {
                foreach (CustomerOrderDetailModel customerOrderDetail in customerOrderDetails)
                {
                    Console.WriteLine(String.Format("{0}: FullName: {1} {2} (Id: {3}) - purchased {4} for {5} (Id: {6})",
                        customerOrderDetail.CustomerOrderId,
                        customerOrderDetail.FirstName,
                        customerOrderDetail.LastName,
                        customerOrderDetail.CustomerId,
                        customerOrderDetail.Description,
                        customerOrderDetail.price,
                        customerOrderDetail.ItemId));

                }
            }
        }
    }
}

