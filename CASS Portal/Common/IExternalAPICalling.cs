using CASS_Portal.Models;

namespace CASS_Portal.Common
{
    public interface IExternalAPICalling
    {
        public  DefaultResponse  GeRandomtQuote(string APIName);
    }
}
