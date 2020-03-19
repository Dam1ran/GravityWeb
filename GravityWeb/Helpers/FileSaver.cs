using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GravityWeb.Helpers
{
    public class FileSaver : IFileSaver
    {        
        private readonly string uploadFolderName = @"\Upload\";
        private string savingPath = "";
        public string EnvironmentString { get;private set; }

        

        public async Task<string> Save(string envString, FileUploadAPI objFile)
        {
            EnvironmentString = envString;

            try
            {
                if (CreateDir())
                {
                    using (FileStream fileStream = File.Create(savingPath + objFile.files.FileName))
                    {
                        await objFile.files.CopyToAsync(fileStream);
                        fileStream.Flush();
                        return (@"https://localhost:44390" + uploadFolderName + objFile.files.FileName).Replace(@"\","/");
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
