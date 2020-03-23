using GravityDTO;
using GravityServices.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace GravityServices.Implementations
{
    public class FileSaver : IFileSaver
    {
        private readonly string uploadFolderName = @"\Upload\";
        private string savingPath = "";
        public string EnvironmentString { get; private set; }



        public async Task<string> Save(string envString, IFormFile file)
        {
            EnvironmentString = envString;

            try
            {
                if (CreateDir())
                {
                    var modString = file.FileName.Replace(" ", "_");//may add guid if desired
                    using (FileStream fileStream = File.Create(savingPath + modString))
                    {
                        await file.CopyToAsync(fileStream);
                        fileStream.Flush();
                        return (@"https://localhost:44390" + uploadFolderName + modString).Replace(@"\", "/");
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
                if (!Directory.Exists(EnvironmentString + uploadFolderName))
                {
                    Directory.CreateDirectory(EnvironmentString + uploadFolderName);
                }
            }
            catch (IOException IOex)
            {
                //log it
                Console.WriteLine(IOex.Message);
                return false;
            }

            savingPath = EnvironmentString + uploadFolderName;

            return true;

        }
    }
}
