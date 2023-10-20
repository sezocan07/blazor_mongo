using Blazor_Mongo_Dev.Server.Services.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
namespace Blazor_Mongo_Dev.Server.Services.Concreates;
public class CloudinaryImageUploadService : IImageUploadService{
    protected readonly IConfiguration _configuration;

    public CloudinaryImageUploadService(IConfiguration configuration){
        _configuration = configuration;
    }
    public async Task<string> Upload(string Id, string ImageUrl){
        Account account = new Account(
            _configuration.GetSection("Cloudinary:CloudName").Value, 
            _configuration.GetSection("Cloudinary:ApiKey").Value,
            _configuration.GetSection("Cloudinary:ApiSecret").Value);

        Cloudinary cloudinary = new Cloudinary(account);
        
        var uploadParams = new ImageUploadParams(){
            File = new FileDescription(ImageUrl),
            PublicId = Id};
        var uploadResult = cloudinary.Upload(uploadParams);

        return uploadResult.SecureUrl.AbsoluteUri;
    }
}
