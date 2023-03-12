using CASS.Model.Quoteable;
using CASS.Models;
using CASS.RepoModel.Common;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CASS.Repository
{
    public class QuotesRepo : IQuotesRepo
    {

        public IConfiguration _IConfiguration;
        public HttpClient _Client;
        public DefaultResponse _DefaultResponse;
        private ILogger<QuotesRepo> _ILogger;
        private readonly CodingCContext _CodingCContext;
        public QuotesRepo(IConfiguration configuration, HttpClient client, CodingCContext codingCContext, ILogger<QuotesRepo> iLogger)
        {
            #region Initialization
            _IConfiguration = configuration;
            _CodingCContext = codingCContext;
            _ILogger = iLogger;
            #endregion

            #region HttpClient 
            _Client = client;
            _Client.BaseAddress = new Uri(_IConfiguration.GetValue<string>("QuoteAbleURL") ?? "https://api.quotable.io/");
            _Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
           
            #endregion


        }

        public Task<DefaultResponse> GeRandomtQuote()
        {
            _DefaultResponse = new DefaultResponse() { IsSuccess = false, Msg = string.Empty };

            try
            {
                var response = _Client.GetAsync("random").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(responseContent))
                    {
                        // _Quoteable = JsonConvert.DeserializeObject<Quoteable>(responseContent);
                        _DefaultResponse.IsSuccess = true;
                        _DefaultResponse.Msg = "Success";
                        _DefaultResponse.Data = responseContent;
                    }

                }
                else
                {
                    _DefaultResponse.Msg = $"Error: Response returned with status Code: {response.StatusCode.ToString()}";
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

        public Task<DefaultResponse> GetQuotesByAuthorName()
        {
            _DefaultResponse = new DefaultResponse() { IsSuccess = false, Msg = string.Empty };

            BulkQuotes bulkQuotes = new BulkQuotes();
            bulkQuotes.results = new List<Quoteable>();
            try
            {
                var response = _Client.GetAsync("quotes?limit=30&author=albert-einstein").Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(responseContent))
                    {
                        bulkQuotes = JsonConvert.DeserializeObject<BulkQuotes>(responseContent);

                        if (bulkQuotes.count > 0)
                        {
                            bulkQuotes.Short = new List<Quoteable>();
                            bulkQuotes.Medium = new List<Quoteable>();
                            bulkQuotes.Long = new List<Quoteable>();

                            foreach (var item in bulkQuotes.results)
                            {
                                var totalWords = item.content.Split(" ").Length;
                                if (totalWords < 10)
                                {
                                    bulkQuotes.Short.Add(item);
                                }
                                else if (totalWords >= 11 && totalWords <= 20)
                                {
                                    bulkQuotes.Medium.Add(item);
                                }
                                else
                                {
                                    bulkQuotes.Long.Add(item);
                                }
                            }
                        }
                        _DefaultResponse.IsSuccess = true;
                        _DefaultResponse.Msg = "Success";
                        _DefaultResponse.Data = JsonConvert.SerializeObject(bulkQuotes);

                    }
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

