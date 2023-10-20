using Blazor_Mongo_Dev.Client.Services;
using Blazor_Mongo_Dev.Shared.Entities;
using Blazored.Modal;
using Blazored.Modal.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
namespace Blazor_Mongo_Dev.Client.Pages.Modals;
public partial class BlogAddModal{
    [Inject] protected BlogService _blogService{get;set;}
    [Inject] protected SweetAlertService _alertMessage{get;set;}
    
    [CascadingParameter] BlazoredModalInstance _modal{get;set;}
    
    
    [Parameter] public EventCallback<bool> ModalCloseCallBack{get;set;}


    [Parameter] public Blog blog{get;set;}
    

    
    //true gelirse guncelle false gelirse yeni ekle
    [Parameter] public bool BlogStatus{get;set;}
   
    
    private async Task ImageUpload(InputFileChangeEventArgs e){
        try{
            var files = e.GetMultipleFiles(1);
            blog.ImageUrl = e.File.Name;
            var buf = new byte[e.File.Size];
            await using(var stream = e.File.OpenReadStream(10000000)){
                var readAsync = await stream.ReadAsync(buf);
                blog.ImageBase64 = Convert.ToBase64String(buf);
            }
            StateHasChanged();
        } catch(Exception ex){
            Console.WriteLine(ex.Message);
        }
    }
    private async Task BlogAdd(){

        try{
            if(BlogStatus==true){
                await _blogService.UpdateBlog(blog);
            } else{
                await _blogService.AddBlog(blog);
            }


            await _alertMessage.FireAsync("Blog Kaydedildi");
            blog = new();
        } catch(Exception e){
            Console.WriteLine(e.Message);
        } finally{
           await ModalCloseCallBack.InvokeAsync(true);
            await _modal.CloseAsync();
        }
    }
}
