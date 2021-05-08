using GeoMed.Base;
using GeoMed.SqlServer;

namespace GeoMed.Main.Data.Repositories
{
    public  class SettingsRepository : BaseRepository
    {
        public SettingsRepository(GMContext context) :
            base(context)
        {

        }
    }
}
