using GeoMed.Base;
using GeoMed.Model.Main;
using GeoMed.Share.IData.IRepositories;
using GeoMed.SqlServer;
using Microsoft.Extensions.Hosting;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace GeoMed.Share.Data
{
    public class StoreDataRepository : BaseRepository , IStoreDataRepository
    {
        private readonly IHostEnvironment HostingEnvironment;

        public StoreDataRepository( GMContext  context ,  IHostEnvironment _hostingEnvironment)
            : base(context)
        {
            HostingEnvironment = _hostingEnvironment;
        }
        public void ReadExcelData()
        {
            DataTable dtTable = new DataTable();
            List<string> rowList = new List<string>();
            ISheet sheet;
             using (var stream = new FileStream(Path.Combine(HostingEnvironment.ContentRootPath, $"{"Book1"}.xlsx"), FileMode.Open))
            {
                stream.Position = 0;
                XSSFWorkbook xssWorkbook = new XSSFWorkbook(stream);

                sheet = xssWorkbook.GetSheetAt(0);
                IRow headerRow = sheet.GetRow(0);
                int cellCount = headerRow.LastCellNum;
                //for (int j = 0; j < cellCount; j++)
                //{
                //    ICell cell = headerRow.GetCell(j);
                //    if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                //    {
                //        dtTable.Columns.Add(cell.ToString());
                //    }
                //}
                for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null) continue;
                    if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                        {
                            if (!string.IsNullOrEmpty(row.GetCell(j).ToString()) &&
                                !string.IsNullOrWhiteSpace(row.GetCell(j).ToString()))
                            {
                                //rowList.Add(row.GetCell(j).ToString());
                                //Context.Patients.Add( new Patient()
                                //{                        
                                     
                                //});
                            }
                        }

                        if (rowList.Count > 0)
                            dtTable.Rows.Add(rowList.ToArray());
                        rowList.Clear();
                    }
                }
            }
        }
    }
}
