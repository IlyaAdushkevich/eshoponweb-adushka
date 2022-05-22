using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.ApplicationCore.Entities;
public class OrderDetail
{
    public decimal TotalPrice { get; set; }
    public Address ShippingAddress { get; set; }
    public IList<int> ItemIds { get; set; }
}
