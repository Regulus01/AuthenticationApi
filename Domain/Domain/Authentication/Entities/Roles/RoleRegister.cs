namespace Domain.Authentication.Entities.Roles;

public static class RoleRegister
{
    public struct Admin
    {
        public const string Id = "24b8a637-0342-4e44-a7d2-659e78ee7303";
        public const string Nome = "ADMIN";
    }
    
    public struct Usuario
    {
        public const string Id = "4cf226b0-d84d-4394-82fc-80eab1b7aa5e";
        public const string Nome = "USUARIO";
    }
}