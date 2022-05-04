using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PonishaExam.Helper
{
    public static class ImageChecker
    {
        public static bool IsImage(this IFormFile file)
        {
            try
            {
                var image = System.Drawing.Image.FromStream(file.OpenReadStream());
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
