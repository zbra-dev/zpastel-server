using System;
using ZPastel.Model;

namespace ZPastel.Service.Validators
{
    public class UserValidator
    {
        public void Validate(User candidate)
        {
            if (string.IsNullOrEmpty(candidate.Email))
            {
                throw new ArgumentException("Email cannot be null or empty");
            }

            if (string.IsNullOrEmpty(candidate.Name))
            {
                throw new ArgumentException("UserName cannot be null or empty");
            }

            if (string.IsNullOrEmpty(candidate.PhotoUrl))
            {
                throw new ArgumentException("PhotoUrl cannot be null or empty");
            }
        }
    }
}
