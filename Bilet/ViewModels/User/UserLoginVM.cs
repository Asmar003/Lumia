using System.ComponentModel.DataAnnotations;

namespace Bilet.ViewModels.User
{
    public class UserLoginVM
    {
        public string UsernameOrEmail { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsPersistance { get; set; }
    }
}
