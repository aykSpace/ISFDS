using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace IntegratedFlghtDynamicSystem.Areas.Default.ViewModels
{
    public class AvaliableServersViewModel
    {

        public int SelectedServer { get; set; }
        
        private readonly List<string> _avalibleServerId = new List<string>();

        public List<string> AvalibleServers
        {
            get
            {
                return _avalibleServerId;
            }

        }

        [Display(Name = @"Досупные серверы")]
        public IEnumerable<SelectListItem> AvalibleServersSelectListItems
        {
            get
            {
                var allServers = AvalibleServers.Select((t, i) => new SelectListItem
                {
                    Value = i.ToString(CultureInfo.InvariantCulture),
                    Text = t.ToString(CultureInfo.InvariantCulture),
                    Selected = SelectedServer == i
                });
                return allServers;
            }
        }

        /*public IEnumerable<SelectListItem> DefaultServer
        {
            get
            {
                return Enumerable.Repeat(new SelectListItem {Value = "-1", Text = @"Select a server"}, 1);
            }
        } */
    }
}