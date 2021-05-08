using GeoMed.Base;
using GeoMed.SqlServer;

namespace GeoMed.Security.Data
{
    public class AccountRepository : BaseRepository
    {
        public AccountRepository(GMContext context) :
            base(context)
        {

        }


    }
}
