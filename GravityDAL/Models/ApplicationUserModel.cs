namespace GravityDAL.Models
{
    public class ApplicationUserModel
    {
        public long Id { get; set; }
        public string userName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public long coachId { get; set; }
        public string CoachFullName { get; set; }
        public long userRoleId { get; set; }
        public string RoleString { get; set; }
    }
}
