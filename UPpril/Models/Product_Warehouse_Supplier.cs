﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace UPpril.Models;

public partial class Product_Warehouse_Supplier
{
    public int Product_Warehouse_Supplier_ID { get; set; }

    public int Product_ID { get; set; }

    public int Warehouse_ID { get; set; }

    public int Supplier_ID { get; set; }

    public virtual Product Product { get; set; }

    public virtual Supplier Supplier { get; set; }

    public virtual Warehouse Warehouse { get; set; }
}