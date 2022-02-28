using Microsoft.AspNetCore.Identity;

namespace HomeWork.Data
{
    public class IdentityErrorResult:IdentityErrorDescriber
    {
        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError
            {
                Code = "Username",
                Description = "Bu Username-de user movcuddur."
            };
        }
    }
}