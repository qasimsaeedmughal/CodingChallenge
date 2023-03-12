using CASS.Models;
using CASS.RepoModel.Common;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CASS.Repository
{
    public class ShipperAndShipment: IShipperAndShipment
    {
        public IConfiguration _IConfiguration;
        public HttpClient _Client;
        public DefaultResponse _DefaultResponse;
        private readonly CodingCContext _CodingCContext;
        private ILogger <ShipperAndShipment> _ILogger;
        public ShipperAndShipment(IConfiguration configuration, HttpClient client, CodingCContext codingCContext, ILogger<ShipperAndShipment> logger)
        {
            #region Initialization
            _IConfiguration = configuration;
            _CodingCContext = codingCContext;
            _ILogger = logger;
            #endregion

            #region HttpClient 
            _Client = client;
            _Client.BaseAddress = new Uri(_IConfiguration.GetValue<string>("QuoteAbleURL") ?? "https://api.quotable.io/");
            _Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            
            #endregion


        }

        public Task<DefaultResponse> GetAllShipers()
        {
            _DefaultResponse = new DefaultResponse() { IsSuccess = false, Msg = string.Empty };

            try
            {
                var Data = _CodingCContext.Shippers.ToList();
                if (Data.Count > 0)
                {
                    _DefaultResponse.IsSuccess = true;
                    _DefaultResponse.Msg = "Success";
                    _DefaultResponse.Data = JsonConvert.SerializeObject(Data);
                }

            }
            catch (Exception ex)
            {
                _ILogger.LogError(ex, ex.Message);
                _DefaultResponse.IsSuccess = false;
                _DefaultResponse.Msg = ex.Message;
            }
            return Task.FromResult(_DefaultResponse);

        }

        public Task<DefaultResponse> GetShipmentsByShipperID(int id)
        {
            _DefaultResponse = new DefaultResponse() { IsSuccess = false, Msg = string.Empty };

            try
            {
                var Data = _CodingCContext.ShipperShipmentDetail.
                                                FromSqlInterpolated($"exec Shipper_Shipment_Details {id}").ToList();

                if (Data.Count > 0)
                {
                    _DefaultResponse.IsSuccess = true;
                    _DefaultResponse.Msg = "Success";
                    _DefaultResponse.Data = JsonConvert.SerializeObject(Data);
                }

            }
            catch (Exception ex)
            {
                _ILogger.LogError(ex, ex.Message);
                _DefaultResponse.IsSuccess = false;
                _DefaultResponse.Msg = ex.Message;
            }
            return Task.FromResult(_DefaultResponse);


        }
    }
}
