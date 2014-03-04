using System;
using System.Data.Objects;
using System.Linq;
using IntegratedFlghtDynamicSystem.Models.DataTools;

namespace IntegratedFlghtDynamicSystem.Models
{
    public partial class NU
    {
        /// <summary>
        /// Quick search vectors .
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="searchStartDate">The search start date.</param>
        /// <param name="searchEndDate">The search end date.</param>
        /// <param name="idSpcr">Id of spacecraft initial data</param>
        /// <returns>IQueryable NU</returns>
        public static IQueryable<NU> SearchNuByDates(IUnitOfWork unitOfWork, DateTime searchStartDate,
            DateTime searchEndDate, int idSpcr)
        {
            if (searchStartDate.Date == DateTime.MinValue && searchEndDate.Date == DateTime.MinValue)
            {
                return unitOfWork.NuRepository.Context.NUs.Where(x => x.SpacecraftInitialData_ID == idSpcr).AsQueryable();
            }
            IQueryable<NU> searchResults;
            if (searchStartDate.Date == DateTime.MinValue)
            {
                searchResults = unitOfWork.NuRepository.Context.NUs.Where(c =>
                    EntityFunctions.TruncateTime(c.DateNU) <= EntityFunctions.TruncateTime(searchEndDate) && 
                                 c.SpacecraftInitialData_ID == idSpcr);
                return searchResults.AsQueryable();
            }
            if (searchEndDate.Date == DateTime.MinValue)
            {
                var searchResult =
                    unitOfWork.NuRepository.Context.NUs.Where(
                        c => EntityFunctions.TruncateTime(c.DateNU) >= EntityFunctions.TruncateTime(searchStartDate) &&
                                         c.SpacecraftInitialData_ID == idSpcr);
                return searchResult.AsQueryable();
            }
            searchResults = unitOfWork.NuRepository.Context.NUs.Where(c =>
                EntityFunctions.TruncateTime(c.DateNU) >= EntityFunctions.TruncateTime(searchStartDate) &&
                EntityFunctions.TruncateTime(c.DateNU) <= EntityFunctions.TruncateTime(searchEndDate) &&
                c.SpacecraftInitialData_ID == idSpcr);
            //
            return searchResults.AsQueryable();
        }
    }
}