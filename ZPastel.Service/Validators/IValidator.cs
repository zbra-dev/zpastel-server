using System.Threading.Tasks;

namespace ZPastel.Service.Validators
{
    public interface IValidator<T> where T : class
    {
        Task Validate(T candidate);
    }
}
