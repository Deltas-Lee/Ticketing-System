using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSystem.Services;

namespace TicketSystem.Pages.User.GenericClasses
{
   public class GenericListBase<T> : ComponentBase where T : class
    {
        Type myType = typeof(T);

        [Inject]
        public IGenericService<T> genericService { get; set; }
        public IEnumerable<T> entities { get; set; }

        public string str = "base";

        protected override async Task OnInitializedAsync()
        {
            //LoadTickets();
            //return base.OnInitializedAsync();

            str = "intialized";
            entities = await genericService.GetEntities();
        }        
    }
}
