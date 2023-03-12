using System;
using System.Collections.Generic;

namespace CASS.Models;

public partial class Shipment
{
    public int ShipmentId { get; set; }

    public int ShipperId { get; set; }

    public string ShipmentDescription { get; set; } = null!;

    public decimal ShipmentWeight { get; set; }

    public int ShipmentRateId { get; set; }

    public int CarrierId { get; set; }

    public virtual Carrier Carrier { get; set; } = null!;

    public virtual ShipmentRate ShipmentRate { get; set; } = null!;

    public virtual Shipper Shipper { get; set; } = null!;
}
