using CIPlatform.Entities.ViewModels;
using Microsoft.AspNetCore.Http;

namespace CIPlatform.Services.Service;
public class StoreMediaService
{
    public static List<MediaVM> storeMediaToWWWRoot(string wwwRootPath, string path, IList<IFormFile> files)
    { 
        List<MediaVM> filePath = new();

        var uploadDir = Path.Combine(wwwRootPath, path);
        if( files.Any() )
        {
            foreach(var file in files)
            {
                string fileName = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(file.FileName);

                using (var fileStream = new FileStream(Path.Combine(uploadDir, fileName + extension), FileMode.Create) )
                {
                    file.CopyTo(fileStream);
                }

                MediaVM media = new()
                {
                    Type = extension,
                    Name = fileName,
                    Path = $@"\{path}\",
                };

                filePath.Add(media);
            }
        }
        return filePath;
    }
}
