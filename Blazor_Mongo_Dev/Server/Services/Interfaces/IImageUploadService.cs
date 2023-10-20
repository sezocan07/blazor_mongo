namespace Blazor_Mongo_Dev.Server.Services.Interfaces;
public interface IImageUploadService{
    Task<string> Upload(string Id, string ImageUrl);
}
