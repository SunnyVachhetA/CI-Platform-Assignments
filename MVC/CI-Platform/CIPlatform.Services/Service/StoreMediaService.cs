using CIPlatform.Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

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

    internal static List<IFormFile> FetchMediaFromRootPath(List<MediaVM> mediaList, string wwwRootPath)
    {
        string path = mediaList[0].Path;
        var directory = path[1..^1];

        var formFiles = new List<IFormFile>();

        foreach (var file in mediaList)
        {
            var mediaPath = Path.Combine(wwwRootPath, directory, $"{file.Name}{file.Type}");
            var stream = new FileStream(mediaPath, FileMode.Open);
            formFiles.Add(new FormFile(stream, 0, stream.Length, null, file.Name));
        }
        return formFiles;
    }
}
