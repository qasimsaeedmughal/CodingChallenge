using CASS.Model.Quoteable;
using CASS.RepoModel.Common;

namespace CASS.Repository
{
    public interface IQuotesRepo
    {
        public Task<DefaultResponse> GeRandomtQuote();
        public Task<DefaultResponse> GetQuotesByAuthorName();
    }
}
