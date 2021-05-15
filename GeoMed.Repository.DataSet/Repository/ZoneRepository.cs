using GeoMed.Base;
using GeoMed.Model.DataSet;
using GeoMed.Repository.DataSet.DataTransformObject;
using GeoMed.Repository.DataSet.Interface;
using GeoMed.SqlServer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.Repository.DataSet.Repository
{
    public class ZoneRepository : BaseRepository, IZoneRepository
    {
        public ZoneRepository(GMContext context) : base(context)
        {
        }

        public async Task<OperationResult<IEnumerable<CovidZoneDto>>> USAAggregate()
        {
            //if( !Context.CovidZones.Any())
            //{
            //   var data =  GeoMed.LocallyDataAPI_Test.APIs.COVID19_US_Country.COVID19USCountry.ALLUSA();
            //    Context.ChangeTracker.AutoDetectChangesEnabled = false;
            //    int inter = 100;
            //    foreach (var item in data)
            //    {

            //        Context.Add(new CovidZone() {
            //            Cases=item.Cases,
            //            Country=item.Country,
            //            Date=item.Date,
            //            Deaths=item.Deaths,
            //            FipsCode=item.FipsCode,
            //            Lat=item.Lat,
            //            Long=item.Long,
            //            State=item.State,
            //            StateCode=item.StateCode,
            //        });
            //        if(inter++%100==0)
            //        {
            //            Context.SaveChanges();
            //        }
            //    }
            //}

           

            OperationResult<IEnumerable<CovidZoneDto>> operation = new OperationResult<IEnumerable<CovidZoneDto>>();


            // operation.Result = await Context.CovidZones.Select(x => (CovidZoneDto)x).ToListAsync();

            //operation.Result = (await Context.CovidZones.AsNoTracking().Where(zone=>zone.Cases>0).ToListAsync()).
            //    GroupBy(model => (model.State, model.Country)).Select(group => new CovidZoneDto()
            //{
            //    //Cases=x.Aggregate(0d,(all,next)=> all+=next.Cases),
            //    //Deaths=x.Aggregate(0,(all,next)=> all+=next.Deaths),
            //    Cases = group.Sum(x => x.Cases),
            //    Deaths = group.Sum(x => x.Deaths),
            //    Country = group.Key.Country,
            //    State = group.Key.State,
            //    StateCode = group.First().StateCode,
            //    Lat = group.First().Lat,
            //    Long = group.First().Long,
            //    // FipsCode
            //});

            operation.Result = (await Context.CovidZones.AsNoTracking().ToListAsync()).
                GroupBy(model => (model.State, model.Country)).Select(x => x.OrderByDescending(x => x.Cases))
                .Select(x => (CovidZoneDto)x.First());

            
            return operation;
        }


    }
}
