using Microsoft.EntityFrameworkCore; 

namespace CASS_Portal.Models
{ 
    public class ShipperShipmentDetail
    {
        
        public int shipment_id { get; set; }
        public string shipper_name { get; set; }
        public string carrier_name { get; set; }
        public string shipment_description { get; set; }
        public Decimal shipment_weight { get; set; }
        public string shipment_rate_description { get; set; }

    }
}
