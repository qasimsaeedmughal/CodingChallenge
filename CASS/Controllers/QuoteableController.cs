using CASS.Model.Quoteable;
using CASS.RepoModel.Common;
using CASS.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CASS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteableController : ControllerBase
    {
        private readonly IQuotesRepo _IQuotesRepo; 
        public QuoteableController(IQuotesRepo quotesRepo)
        {
            #region Initialization 

            _IQuotesRepo = quotesRepo; 

            #endregion
        } 


        [HttpGet]
        [Route("/[action]")]
        public async Task<DefaultResponse> GetRandomQuote()
        { 
            return (await _IQuotesRepo.GeRandomtQuote()); 
        }

        [HttpGet]
        [Route("/[action]")]
        public async Task<DefaultResponse> GetQuotesByAuthorName()
        {
            return (await _IQuotesRepo.GetQuotesByAuthorName()); 
        } 
    }
}
