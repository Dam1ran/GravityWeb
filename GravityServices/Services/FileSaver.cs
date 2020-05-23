using GravityServices.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GravityServices.Services
{
    public class FileSaver : IFileSaver
    {
        private string _uploadFolderName = "";        
        private string savingPath = "";
        public string EnvironmentString { get; private set; }

        public async Task<string> SaveAsync(string envString, string uploadFolderName, IFormFile file)
        {
            EnvironmentString = envString;
            _uploadFolderName = $"/{ uploadFolderName }/";

            try
            {
                if (CreateDir())
                {
                    var modString = file.FileName.Replace(" ", "_");//may add guid if desired
                    using (FileStream fileStream = File.Create(savingPath + modString))
                    {
                        await file.CopyToAsync(fileStream);
                        fileStream.Flush();
                        return modString;                        
                    }
                }
                else
                {
                    return "Failed";
                }

            }
            catch (Exception ex)
            {
                //log it
                Console.WriteLine(ex.Message);
                return "Failed";
            }

        }

        private bool CreateDir()
        {

            try
            {
                if (!Directory.Exists(EnvironmentString + _uploadFolderName))
                {
                    Directory.CreateDirectory(EnvironmentString + _uploadFolderName);
                }
            }
            catch (IOException IOex)
            {
                //log it
                Console.WriteLine(IOex.Message);
                return false;
            }

            savingPath = EnvironmentString + _uploadFolderName;

            return true;

        }
    }
}
