namespace Common.Interfaces
{
    public interface ITokenManager<AppUser>
    {
        Task<string> GenerateJwtToken(AppUser appUser, List<string> roles);
    }
}
