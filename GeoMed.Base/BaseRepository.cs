using GeoMed.SqlServer;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoMed.Base
{
    public class BaseRepository
    {
        protected readonly GMContext Context;
        protected BaseRepository(GMContext context)
        {
            Context = context;
        }
    }

}
