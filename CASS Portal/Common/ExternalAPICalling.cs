
using Azure;
using CASS_Portal.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CASS_Portal.Common
{
    public class ExternalAPICalling  : IExternalAPICalling
    {


        private HttpClient _Client; 
        private IConfiguration _IConfiguration;
        private DefaultResponse _DefaultResponse;
        private ILogger<ExternalAPICalling> _ILogger;
        public ExternalAPICalling(IConfiguration configuration, ILogger<ExternalAPICalling> iLogger)
        {
            #region Initialization
            _IConfiguration = configuration;
            _ILogger= iLogger;
            #endregion

            #region HttpClient 

            #endregion
        }

        public DefaultResponse GeRandomtQuote(string APIName)
        {
            _DefaultResponse = new DefaultResponse() { IsSuccess = false,Msg=string.Empty };


            try
            {
                _Client = new HttpClient();
                _Client.BaseAddress = new Uri(_IConfiguration.GetValue<string>("APIBASEURL") ?? "https://localhost:7296");
                _Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var response = _Client.GetAsync(APIName).Result;
                if (response.IsSuccessStatusCode)
                {
                    string? responseContent = response.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(responseContent))
                    {
                        var res =  JsonConvert.DeserializeObject<DefaultResponse>( responseContent);
                        _DefaultResponse.IsSuccess = res.IsSuccess;
                        _DefaultResponse.Msg = res.Msg;
                        _DefaultResponse.Data = res.Data; 
                    }
                }
                else
                {
                    
                    _DefaultResponse.Msg = $"Error: Response returned with status Code: {response.IsSuccessStatusCode}";
                }
            }
            catch (Exception ex)
            {
                _ILogger.LogError(ex, ex.Message);
                _DefaultResponse.IsSuccess = false;
                _DefaultResponse.Msg = ex.Message;
            }
            return _DefaultResponse ;
        }

    }

}
