using Application.Dto.JWT;
using Application.Dto.User;

namespace Application.Utility
{
    public static class UserValidator
    {
        public static Response<JWTResponse> ValidateUser(this UserSignUpDto request)
        {
            // the follwing logic can be done through FluentValidation (AbstractValidator<T>) 
            // but actually i'm not in the mood of using it

            if (request == null)
                return Response<JWTResponse>.Failure("InvalidRequest");

            if (string.IsNullOrWhiteSpace(request.UserName) || request.UserName.Length < 3)
                return Response<JWTResponse>.Failure("InvalidUserName");

            if (string.IsNullOrWhiteSpace(request.Password) || request.Password.Length < 6)
                return Response<JWTResponse>.Failure("PasswordShouldBeMoreThanSixCharacters");

            if (request.Password != request.PasswordConfirm)
                return Response<JWTResponse>.Failure("PasswordNotEqualWithPasswordConfirm");

            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(request.Email);
                    if (addr.Address != request.Email)
                        return Response<JWTResponse>.Failure("InvalidEmail");
                }
                catch
                {
                    return Response<JWTResponse>.Failure("InvalidEmail");
                }
            }

            if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(request.PhoneNumber, @"^(?:\+989|09)\d{9}$"))
                    return Response<JWTResponse>.Failure("InvalidPhoneNumber");
            }

            if (string.IsNullOrWhiteSpace(request.FirstName))
                return Response<JWTResponse>.Failure("FirstNameRequired");

            return Response<JWTResponse>.Succeeded();
        }

    }
}
