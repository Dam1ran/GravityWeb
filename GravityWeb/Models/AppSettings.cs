namespace GravityWeb.Models
{
    public class AppSettings
    {
        public string Site { get; set; }
        public string Audience { get; set; }
        public int ExpireTimeDays { get; set; }
        public string Secret { get; set; }
        public string FBAccessToken { get; set; }
        public string BaseUrl { get; set; }
        public string UploadScheduleFolderName { get; set; }
        public string UploadTeamMemberFolderName { get; set; }
    }
}
