using CustomerOrderViewer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

// En esta clare estamos cogiendo los datos del SQL
namespace CustomerOrderViewer.Repository
{
    internal class CustomerOrderDetailCommand
    {
        //Declaramos una variable privada por lo que se va a usar solo aquí
        private string _connectionString;

        //Generamos una función pública en la que asignamos un string de entrada a la variable privada que hemos creado.
        //Esto lo usamos cuando declaramos el objeto
        public CustomerOrderDetailCommand(string connectionString) 
        {
            _connectionString = connectionString;
        }

        //Creamos esta función que esta asociada al objeto y le decimos que la salida va a ser un ILIST
        public IList<CustomerOrderDetailModel> GetList()
        {
            //Creamos una lista
            List<CustomerOrderDetailModel> customerOrderDetailModels = new List<CustomerOrderDetailModel>();
            //Definimos la conexión con el conection String que ha sido la entrada al generar el objeto
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                //Lanzamos la query con SQLCommand
                using (SqlCommand command = new SqlCommand("SELECT CustomerOrderId, CustomerId, ItemId, FirstName, Lastname, [Description], Price FROM [CustomerOrderViewer].[dbo].[CustomerOrderDetail]", connection))
                {
                    //Una vez que tenemos la respuesta de SqlConmmand usamos SqlReader para leer
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                //Miramos si tiene contenido la rspuesta y usamos .Read() para leer línea a línea 
                                //Creamos una lista por cada una de las líneas
                                CustomerOrderDetailModel customerOrderDetailModel = new CustomerOrderDetailModel()
                                {
                                    CustomerOrderId = Convert.ToInt32(reader["CustomerOrderId"]),
                                    CustomerId = Convert.ToInt32(reader["CustomerId"]),
                                    ItemId = Convert.ToInt32(reader["ItemId"]),
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    Price = Convert.ToDecimal(reader["Price"]),
                                };
                                //Añadimos cada lista de una línea a las Lista general de líneas
                                customerOrderDetailModels.Add(customerOrderDetailModel);
                            }
                        }
                    }
                }
            }

                return customerOrderDetailModels;
        }


    }
}
