﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace UPpril.Models;

public partial class Consumer_Product
{
    public int Consumer_Product_ID { get; set; }

    public int Consumer_ID { get; set; }

    public int Product_ID { get; set; }

    public virtual Consumer Consumer { get; set; }

    public virtual Product Product { get; set; }
}