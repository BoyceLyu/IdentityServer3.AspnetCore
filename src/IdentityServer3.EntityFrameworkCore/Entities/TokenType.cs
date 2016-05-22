namespace IdentityServer3.EntityFrameworkCore.Entities
{
    public enum TokenType : short
    {
        AuthorizationCode = 1,
        TokenHandle = 2,
        RefreshToken = 3
    }
}