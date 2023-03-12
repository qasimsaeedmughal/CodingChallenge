using CASS.RepoModel.Common;

namespace CASS.Repository
{
    public interface IShipperAndShipment
    { 
        public Task<DefaultResponse> GetAllShipers();
        public Task<DefaultResponse> GetShipmentsByShipperID(int id);
    }
}
