namespace IdentityCustomMVC.Services
{
    public interface ISeedUserRoleInitial
    {
        Task SeedRoleAsync();

        Task SeedUserAsync();
    }
}
