using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Application.Others
{
    public static class ImageConvertor
    {
        public static string SaveImage(IFormFile file)
        {
            var imageName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            string pathImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "image", imageName);
            using (var stream = new FileStream(pathImage, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return imageName;
        }
        
        public static bool IsImage(this IFormFile file)
        {
            try
            {
                var check = System.Drawing.Image.FromStream(file.OpenReadStream());
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static void RemoveImage(string imageName)
        {
            string defaultImage = "masterAvatar.png";
            if (imageName != defaultImage)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "image", imageName);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
        }
    }
}
