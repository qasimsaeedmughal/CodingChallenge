 Create Proc [dbo].[Shipper_Shipment_Details] (
@shipper_id int
) AS
begin
	  select shipper_name,carrier.carrier_name, shipment.shipment_id, shipment.shipment_description,shipment.shipment_weight, shipment_rate.shipment_rate_description
from shipper
inner join shipment on shipment.shipper_id = shipper.shipper_id and shipper.shipper_id = @shipper_id
inner join shipment_rate on shipment_rate.shipment_rate_id = shipment.shipment_rate_id
inner join carrier on carrier.carrier_id = shipment.carrier_id 

order by shipper.shipper_name  

end;