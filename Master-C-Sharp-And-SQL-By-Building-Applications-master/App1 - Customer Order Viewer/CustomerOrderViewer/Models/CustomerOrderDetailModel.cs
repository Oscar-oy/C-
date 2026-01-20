using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerOrderViewer.Models
{
    //Aquí estamos generando un modelo que es una clase u objeto que definimos los parametros que tiene.
    internal class CustomerOrderDetailModel
    {
        public int CustomerOrderId { get; set; }
        public int CustomerId { get; set; }
        public int ItemId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public int MyProperty { get; set; }
        public decimal Price { get; set; }
    }
}
