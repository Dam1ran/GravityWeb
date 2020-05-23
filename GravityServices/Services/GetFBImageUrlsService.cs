using GravityDTO;
using GravityServices.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;



namespace GravityServices.Services
{
    public class GetFBImageUrlsService : IGetFBImageUrlsService
    {        
        public async Task<GalleryImagesDTO> GetImagesUrlAsync(string nav, string token)
        {
                        
            var cURL = $"https://graph.facebook.com/v6.0/2687083138192321/photos?{token}&pretty=0&fields=images%2Cpicture&limit=10";

            string url = "";

            if (nav=="first")
            {
                url = cURL;
            }
            else
            {           
                url= nav.Replace("{}", token);
            }
                       
            var listOfUrls = new List<string>();

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var body = await response.Content.ReadAsStringAsync();

                    var jsonObj = JObject.Parse(body);

                    string next = null;
                    string previous = null;

                    foreach (var item in jsonObj.SelectToken("data"))
                    {
                        listOfUrls.Add(item.SelectToken("images")[0].SelectToken("source").Value<string>());
                    }
                    
                    try
                    {                        
                        next = jsonObj["paging"]["next"].ToString().Replace(token, "{}");
                    }
                    catch (NullReferenceException)
                    {
                        Console.WriteLine("Key Is Empty");
                    }
                    try
                    {
                        previous = jsonObj["paging"]["previous"].ToString().Replace(token, "{}");                        
                    }
                    catch (NullReferenceException)
                    {
                        Console.WriteLine("Key Is Empty");
                    }

                    var galleryImagesDTO = new GalleryImagesDTO { listOfUrls=listOfUrls,next=next,previous=previous };

                    return galleryImagesDTO;

                }
            }
            
            return null;

        }
    }
}
