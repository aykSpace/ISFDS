using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntegratedFlghtDynamicSystem.Models.Grid
{
    /// <summary>
    /// Defines the jqGrid settings.
    /// </summary>
    /// <remarks>
    /// These settings are used for paging and sorting of the grid data.
    /// </remarks>
    [ModelBinder(typeof(GridModelBinder))]
    [Serializable]
    public class GridSettings
    {
        /// <summary>
        /// Gets or sets the page size.
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// Gets or sets the page index.
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// Gets or sets the sort column.
        /// </summary>
        public string SortColumn { get; set; }
        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        public string SortOrder { get; set; }

        /// <summary>
        /// Initializes a new instance of the GridSettings class.
        /// </summary>
        public GridSettings()
        {
            SortColumn = "ID_NU";
            SortOrder = "DESC";
            PageIndex = 1;
            PageSize = 20;
        }

        /// <summary>
        /// Initializes a new instance of the GridSettings class.
        /// </summary>
        /// <param name="sortColumn">The sort column.</param>
        /// <param name="ascending">Sort ascending flag.</param>
        public GridSettings(string sortColumn, bool ascending)
        {
            SortColumn = sortColumn;
            SortOrder = (ascending ? "ASC" : "DESC");
            PageIndex = 1;
            PageSize = 20;
        }

        /// <summary>
        /// Initializes a new instance of the GridSettings class.
        /// </summary>
        /// <param name="stringData">The string that represents the a GridSettings object.</param>
        public GridSettings(string stringData)
        {
            string[] tempArray = stringData.Split(new string[] { "#;" }, StringSplitOptions.None);
            PageSize = int.Parse(tempArray[0]);
            PageIndex = int.Parse(tempArray[1]);
            SortColumn = tempArray[2];
            SortOrder = tempArray[3];
        }

        /// <summary>
        /// Build a string used to cache the current GridSettings object.
        /// </summary>
        /// <returns>A string that represents the current GridSettings object.</returns>
        public override string ToString()
        {
            return string.Format("{0}#;{1}#;{2}#;{3}",
                PageSize,
                PageIndex,
                SortColumn,
                SortOrder);
        }

        /// <summary>
        /// Load the data from the data source for the current grid settings.
        /// </summary>
        /// <typeparam name="T">The entity type used as data model.</typeparam>
        /// <param name="dataSource">The data source.</param>
        /// <param name="count">Returns the number of elements of the returned results.</param>
        /// <returns>The grid data that match the current grid settings.</returns>
        public IQueryable<T> LoadGridData<T>(IQueryable<T> dataSource, out int count)
        {
            var query = dataSource;
            //
            // Sorting and Paging by using the current grid settings.
            //
            query = query.OrderBy<T>(SortColumn, SortOrder);
            count = query.Count();
            //
            if (PageIndex < 1)
                PageIndex = 1;
            //
            var data = query.Skip((PageIndex - 1) * this.PageSize).Take(this.PageSize);
            //
            return data;
        }

    }
}