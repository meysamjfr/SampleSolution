using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Project.DTOs.Datatable.Base;

namespace Project.Extentions
{
    public static class DatatableExtention
    {
        public static void GetDataFromRequest(this HttpRequest Request, out FiltersFromRequestDataTable filtersFromRequest)
        {
            //TODO: Make Strings Safe String
            filtersFromRequest = new FiltersFromRequestDataTable
            {
                Draw = Request.Form["draw"].FirstOrDefault(),
                Start = Convert.ToInt32(Request.Form["start"].FirstOrDefault()),
                Length = Convert.ToInt32(Request.Form["length"].FirstOrDefault()),
                SortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault(),
                SortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault(),
                SearchValue = Request.Form["search[value]"].FirstOrDefault()
            };
            filtersFromRequest.SortColumnIndex = Request.Form["order[0][column]"].FirstOrDefault();

            filtersFromRequest.SearchValue = filtersFromRequest.SearchValue?.ToLower();

        }

        public static async Task<DatatableResponse<T>> ToDataTableAsync<T>(this IQueryable<T> source, int totalRecords, FiltersFromRequestDataTable filtersFromRequest)
        {
            return new DatatableResponse<T>()
            {
                SEcho = filtersFromRequest.Draw,
                RecordsTotal = totalRecords,
                RecordsFiltered = await source.CountAsync(),
                Data = await source.DatatableOrderBy(filtersFromRequest)
                            .DatatablePaginate(filtersFromRequest.Start, filtersFromRequest.Length)
                            .ToListAsync()
            };
        }

        private static IQueryable<T> DatatableOrderBy<T>(this IQueryable<T> source, FiltersFromRequestDataTable filtersFromRequest)
        {
            var props = typeof(T).GetProperties();
            string propertyName = "";
            for (int i = 0; i < props.Length; i++)
            {
                if (i.ToString() == filtersFromRequest.SortColumnIndex)
                    propertyName = props[i].Name;
            }

            System.Reflection.PropertyInfo propByName = typeof(T).GetProperty(propertyName);
            if (propByName != null)
            {
                if (filtersFromRequest.SortColumnDirection == "desc")
                    source = source.OrderByDescending(x => propByName.GetValue(x, null));
                else
                    source = source.OrderBy(x => propByName.GetValue(x, null));
            }

            return source;
        }

        public static IQueryable<T> DatatablePaginate<T>(this IQueryable<T> queryable, int start, int length)
        {
            return queryable.Skip(start).Take(length);
        }
        
    }
}
