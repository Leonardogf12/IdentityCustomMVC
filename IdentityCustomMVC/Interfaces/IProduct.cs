using IdentityCustomMVC.Entities;

namespace IdentityCustomMVC.Interfaces
{
    public interface IProduct : IGeneric<Product>
    {
        bool Exists(int Id);
    }
}
