namespace melkikerapostgrescrud.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public String Username { get; set; } = string.Empty;
        public String Email { get; set; } = string.Empty ;
        public String Password { get; set; } = string.Empty;    
    }
}
