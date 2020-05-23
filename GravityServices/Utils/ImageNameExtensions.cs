using System;
using System.Collections.Generic;
using System.Text;

namespace GravityServices.Utils
{
    static class ImageNameExtensions
    {
        public static string MakeUrl(this string imageName, string baseUrl, string uploadFolderName)
        {            
            return $"{baseUrl}/{uploadFolderName}/{imageName}";
        }
    }
}
