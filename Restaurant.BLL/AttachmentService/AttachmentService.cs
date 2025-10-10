using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {
        List<string> AllowedExtensions = new List<string> { ".jpg", ".jpeg", ".png" };
        const int MaxSize= 2_097_152; 
        public string? Upload(IFormFile File, string FolderName)
        {
            var Extension = Path.GetExtension(File.FileName);
            if (!AllowedExtensions.Contains(Extension.ToLower()))
                return null;
            if(File.Length==0 || File.Length>MaxSize)
                return null;
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);
            var FileName = $"{Guid.NewGuid()}_{File.FileName}";
            var FilePath = Path.Combine(FolderPath, FileName);
            using FileStream FS= new FileStream(FilePath, FileMode.Create);
            File.CopyTo(FS);
            return FileName;
        }

        public bool Delete(string FilePath)
        {
            if(!File.Exists(FilePath))
                return false;
            else
            {
                File.Delete(FilePath);
                return true;
            }
        }

    }
}
