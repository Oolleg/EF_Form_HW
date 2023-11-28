namespace Form_HW.Data.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public String   Name { get; set; } = null!;
        public String   LastName { get; set; } = null!;
        public String   Email { get; set; } = null!;
        public String   Phone { get; set; } = null!;
        public DateTime? RegisterTime { get; set; }
    }
}
