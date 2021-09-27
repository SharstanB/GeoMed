using GeoMed.Base;
using GeoMed.Model.DataSet;
using GeoMed.NN.Base;
using GeoMed.Repository.DataSet.DataTransformObject;
using GeoMed.Repository.DataSet.Interface;
using GeoMed.SqlServer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
            if (!Context.CovidZones.Any())
            {
                var usa = LocallyDataAPI_Test.APIs.COVID19_US_Country.COVID19USCountry.ALLUSA();
                var usainfo = LocallyDataAPI_Test.APIs.COVID19_US_Country.COVID19USCountry.ALLUSAInfo();


               var res= usainfo.GroupJoin(usa, info => info.FipsCode, usa => usa.FipsCode, (info,usa)=> new //SpatialInfo()
                {
                   firstUsa = usa.FirstOrDefault(x => x.Lat != 0 && x.Long != 0),
                   Population = info.Population,
                   MedianAge = info.MedianAge,
                   fib = info.FipsCode,
                   CovidZones = usa.Where(x => x.Lat != 0 && x.Long != 0).Select(item=> new CovidZone() {
                       Deaths = item.Deaths,
                       FipsCode = item.FipsCode,
                       Cases = item.Cases,
                       Date = item.Date,
                       StateCode = item.StateCode,
                   } ).ToList()
               }).Select(info=> new SpatialInfo() {
                   Lat = info.firstUsa.Lat,
                   Long = info.firstUsa.Long,
                   State = info.firstUsa.State,
                   Country = info.firstUsa.Country,
                   Population = info.Population,
                   MedianAge = info.MedianAge,
                   fib = info.fib,
                   CovidZones = info.CovidZones,
               });

                int inter = 0;
                Context.ChangeTracker.AutoDetectChangesEnabled = false;
                //await Context.BulkInsertAsync((IList<SpatialInfo>)res);


                foreach (var item in res)
                {
                    Context.AddRange(item);
                    if (inter++ % 100 == 0)
                        Context.SaveChanges();
                }
                Context.SaveChanges();

                    //    foreach (var item in list)
                    //{

                    //    //Context.Add(new CovidZone()
                    //    //{
                    //    //    Cases = item.Cases,
                    //    //    Date = item.Date,
                    //    //    Deaths = item.Deaths,
                    //    //    FipsCode = item.FipsCode,
                    //    //    SpatialInfo = 
                    //    //    Context.SpatialInfos.Any(s=>s.fib == item.FipsCode) ? Context.SpatialInfos.SingleOrDefault(s=>s.fib == item.FipsCode) : 
                    //    //    new SpatialInfo()
                    //    //    {

                    //    //        Country = item.SpatialInfo.Country,
                    //    //        Lat = item.SpatialInfo.Lat,
                    //    //        Long = item.SpatialInfo.Long,
                    //    //        State = item.SpatialInfo.State,
                    //    //        MedianAge = item.SpatialInfo.MedianAge,
                    //    //        Population = item.SpatialInfo.Population,
                    //    //    },
                    //    //    StateCode = item.StateCode,
                    //    //});
                    //    //Context.Add(new SpatialInfo()
                    //    //{
                    //    //    Lat = data.FirstOrDefault(s => s.FipsCode == item.FipsCode).Lat,
                    //    //    Country = data.FirstOrDefault(s => s.FipsCode == item.FipsCode).Country,
                    //    //    Long = data.FirstOrDefault(s => s.FipsCode == item.FipsCode).Long,
                    //    //    MedianAge = item.MedianAge,
                    //    //    Population = item.Population,
                    //    //    State = data.FirstOrDefault(s => s.FipsCode == item.FipsCode).State,
                    //    //    CovidZones = data.Where(s => s.FipsCode == item.FipsCode).Select(ss => new CovidZone()
                    //    //    {
                    //    //        Deaths = ss.Deaths,
                    //    //        Cases = ss.Cases,
                    //    //        Date = ss.Date,
                    //    //        StateCode = ss.StateCode,
                    //    //        FipsCode = ss.FipsCode,
                    //    //    }).ToList()

                    //    //});

                    //    if (inter++ % 100 == 0)
                    //    {
                    //        Context.SaveChanges();
                    //    }
                    //}

                }

         //   }

            // data = Context.SpatialInfos.Where(s => s.CovidZones.Any());

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

            operation.Result = (await Context.CovidZones.Include(x => x.SpatialInfo).Where(zone => zone.Cases > 0).AsNoTracking().ToListAsync()).
                GroupBy(model => (model.SpatialInfo.State, model.SpatialInfo.Country))//.Select(x => x.OrderByDescending(x => x.Cases))
              .Select(group => new CovidZoneDto()    //.Select(x => (CovidZoneDto)x.First());
              {
                //Cases=x.Aggregate(0d,(all,next)=> all+=next.Cases),
                //Deaths=x.Aggregate(0,(all,next)=> all+=next.Deaths),
                Cases = group.Sum(x => x.Cases),
                Deaths = group.Sum(x => x.Deaths),
                Country = group.Key.Country,
                State = group.Key.State,
                StateCode = group.First().StateCode,
                Lat = group.First().SpatialInfo.Lat,
                Long = group.First().SpatialInfo.Long,
                FipsCode  =group.First().FipsCode,
                Date = group.First().Date,
                DeleteDate = group.First().DeleteDate,
                Id = group.First().Id,
            });

            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            var path = Path.Combine(projectDirectory, @"GeoMed\GeoMed\wwwroot\results\CovidZoneDtoResult.json");
            await System.IO.File.WriteAllTextAsync(path, System.Text.Json.JsonSerializer.Serialize(operation.Result ,  new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, }));


            return operation;
        }


        public async Task<OperationResult<IEnumerable<CovidZoneDto>>> USAOne()
        {
                var usa = LocallyDataAPI_Test.APIs.COVID19_US_Country.COVID19USCountry.OneUSA();

            OperationResult<IEnumerable<CovidZoneDto>> operation = new OperationResult<IEnumerable<CovidZoneDto>>();

            operation.Result = (usa).
                GroupBy(model => (model.State, model.Country))
              .Select(group => new CovidZoneDto()
              {
                  Cases = group.Sum(x => x.Cases),
                  Deaths = group.Sum(x => x.Deaths),
                  Country = group.Key.Country,
                  State = group.Key.State,
                  StateCode = group.First().StateCode,
                  Lat = group.First().Lat,
                  Long = group.First().Long,
                  FipsCode = group.First().FipsCode,
                  Date = group.First().Date,
                  DeleteDate = null,
                  Id = 0,
              });

            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            var path = Path.Combine(projectDirectory, @"GeoMed\GeoMed\wwwroot\results\CovidZoneOneDtoResult.json");
            await System.IO.File.WriteAllTextAsync(path, System.Text.Json.JsonSerializer.Serialize(operation.Result, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, }));


            return operation;
        }


        public async Task<OperationResult<IEnumerable<CovidZoneDto>>> USATen()
        {
            var usa = LocallyDataAPI_Test.APIs.COVID19_US_Country.COVID19USCountry.TenUSA();

            OperationResult<IEnumerable<CovidZoneDto>> operation = new OperationResult<IEnumerable<CovidZoneDto>>();

            operation.Result = (usa).
                GroupBy(model => (model.State, model.Country))
              .Select(group => new CovidZoneDto()
              {
                  Cases = group.Sum(x => x.Cases),
                  Deaths = group.Sum(x => x.Deaths),
                  Country = group.Key.Country,
                  State = group.Key.State,
                  StateCode = group.First().StateCode,
                  Lat = group.First().Lat,
                  Long = group.First().Long,
                  FipsCode = group.First().FipsCode,
                  Date = group.First().Date,
                  DeleteDate = null,
                  Id = 0,
              });

            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            var path = Path.Combine(projectDirectory, @"GeoMed\GeoMed\wwwroot\results\CovidZoneTenDtoResult.json");
            await System.IO.File.WriteAllTextAsync(path, System.Text.Json.JsonSerializer.Serialize(operation.Result, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, }));


            return operation;
        }



        public async Task<OperationResult<IEnumerable<CovidZoneDto>>> GetMapData(MapDataDto mapDataDto)
        {
            var result = new OperationResult<IEnumerable<CovidZoneDto>>();


            result.Result = await Context.CovidZones.Select(x => (CovidZoneDto)x).ToListAsync(); 
            return result;

        }

    }
}
