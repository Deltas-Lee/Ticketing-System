using AutoMapper;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TicketSystem.Models.AssetModels;
using TicketSystem.Models.ClientModels;
using TicketSystem.Services;

namespace TicketSystem.Pages.User.GenericClasses
{
    public class EditGenericBase<T,T1> : ComponentBase 
        where T : class, new()
        where T1 : class , new()
    {
        public Type myClass = typeof(T);
        public Type myClass1 = typeof(T1);

        [Inject]
        public IGenericService<T> genericService { get; set; }

        [Parameter]
        public EventCallback<Guid> OnAssetDeleted { get; set; }

        public T Entity { get; set; } = new T();
        public T1 editModel { get; set; } = new T1();
       
        
        [Parameter]
        public string Id { get; set; }

        public string PageHeader { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }
        protected HttpResponseMessage result;

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public Guid myID { get; set; }
        protected async override Task OnInitializedAsync()
        {
            //myID = new(Id);
            if (Id != null)
            {
                Guid myId = new(Id);
                PageHeader = $"Edit {myClass.Name}";
                Entity = await genericService.GetEntity(myId);
            }
            else
            {
                PageHeader = $"Create {myClass.Name}";
                Entity = new T();
            }
            Mapper.Map(Entity, editModel);
        }

        
        protected async Task HandleValidSubmit()
        {
            Mapper.Map(editModel, Entity);

            //HttpResponseMessage result;
            //Guid myId = new(Id);
            //myID = new(Id);
            
            if ( Id != null)
            {
                result = await genericService.UpdateEntity(Entity);
            }
            else
            {
                result = await genericService.CreateEntity(Entity);
            }
            if (result != null)
            {
                NavigationManager.NavigateTo($"{myClass.Name}slist");
            }
        }


        protected void Delete_Click()
        {
            DeleteConfirmation.Show();
            //await AssetService.DeleteAsset(Asset.Id);
            //NavigationManager.NavigateTo("Assetlist");
        }

        protected TicketSystem.Pages.ConfirmBase DeleteConfirmation { get; set; }



        protected async Task ConfirmDelete_Click(bool deleteConfirmed)
        {
            if (deleteConfirmed)
            {
                await genericService.DeleteEntity((Guid)myClass.GetProperty("Id").GetValue(Entity));
                await OnAssetDeleted.InvokeAsync((Guid)myClass.GetProperty("Id").GetValue(Entity));
                NavigationManager.NavigateTo($"{myClass.Name}slist");
            }
        }
    }
}
