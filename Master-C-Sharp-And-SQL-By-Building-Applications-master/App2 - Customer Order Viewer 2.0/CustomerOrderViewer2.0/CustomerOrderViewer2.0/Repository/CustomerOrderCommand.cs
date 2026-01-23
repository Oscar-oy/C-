using CustomerOrderViewer2._0.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Markup;

namespace CustomerOrderViewer2._0.Repository
{
    internal class CustomerOrderCommand
    {
        /// <summary>
        /// Esta aplicación funciona en consola y lo que hace es que el usuario tenga acceso a la base de datos y pueda ejecutar todas las acciones
        /// CRUD crear customers, read la base de datos, Update la base de datos y Delete customers (soft delete en nuestro caso)
        /// </summary>
        
        private string _connectionString;
        /// <summary>
        /// Definimos la clase y una variable _connectionString se pone _ por que es una variable privada
        /// </summary>
        public CustomerOrderCommand(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Upsert(int customerOrderId,int customerId, int itemId, string userId)
        {
            //Aqui declaramos la función Upsert  que lo que hace es llamar a un procedimiento de SQL
            var upsertStatement = "CustomerOrderDetail_Upsert";
            
            //Definimos una tabla
            var dataTable = new DataTable();
            dataTable.Columns.Add("CustomerOrderId", typeof(int));
            dataTable.Columns.Add("CustomerId", typeof(int));
            dataTable.Columns.Add("Item", typeof(int));
            dataTable.Rows.Add(customerOrderId, customerId,itemId);

            //Hacemos la conexión y ejecujtamos el procedimiento guardado en SQL metiendo la tabla como parametro de entrada
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(upsertStatement, new { @CustomerOrdeType = dataTable.AsTableValuedParameter("CustomerOrdeType"), @UserId = userId }, commandType: CommandType.StoredProcedure);
            }

        }

        public void Delete(int customerOrderId,string userId)
        {
            var upsertStatement = "CustomerOrderDetail_Delete";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(upsertStatement, new { @CustomerOrderId = customerOrderId, @UserId = userId }, commandType: CommandType.StoredProcedure);
            }
        }

        public IList<CustomerOrderDetailModel> GetList()
        {
            List<CustomerOrderDetailModel> customerOrderDetails = new List<CustomerOrderDetailModel>();

            var sql = "CustomerOrderDetail_GetList";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                customerOrderDetails = connection.Query<CustomerOrderDetailModel>(sql).ToList();

            }

            return customerOrderDetails;
        }


    }
}
