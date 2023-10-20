namespace Blazor_Mongo_Dev.Server.Helper;
public  class AppStateManager{
    
    public  Task DirectoryControl(string Root){
        var directoryPath = Path.Combine(Root);
        if(!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
        return Task.CompletedTask;
    }
    
    public  Task<string> PrepareUniqueImageName(string fileName){
        var randomName = Path.GetRandomFileName();
        var extension = Path.GetExtension(fileName);
        var newFileName = Path.ChangeExtension(randomName, extension);
        return Task.FromResult(newFileName);
    }
}
