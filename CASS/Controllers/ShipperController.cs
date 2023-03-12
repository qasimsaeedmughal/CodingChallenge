using CASS.RepoModel.Common;
using CASS.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CASS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipperController : ControllerBase
    {
        private readonly IShipperAndShipment _IShipperAndShipment;
        public ShipperController(IShipperAndShipment shipperAndShipment)
        {
            #region Initialization  
            _IShipperAndShipment = shipperAndShipment; 
            #endregion
        }

        [HttpGet]
        [Route("/[action]")]
        public async Task<DefaultResponse> GetAllShipers()
        {
            return await _IShipperAndShipment.GetAllShipers();
        }

        [HttpGet]
        [Route("/[action]/{id}")]
        public async Task<DefaultResponse> GetShipmentsbyShipperID(int id)
        {
            return await _IShipperAndShipment.GetShipmentsByShipperID(id);
        }
    }
}
