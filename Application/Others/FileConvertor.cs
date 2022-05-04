using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Application.Others
{
    public static class FileConvertor
    {
        public static string SaveFile(IFormFile file)
        {
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            string pathImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", fileName);
            using (var stream = new FileStream(pathImage, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return fileName;
        }
        public static void RemoveFile(string fileName)
        {
            if (fileName != null)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", fileName);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
        }
    }
}