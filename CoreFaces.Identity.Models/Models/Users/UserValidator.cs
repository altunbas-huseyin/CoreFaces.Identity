using FluentValidation;
using FluentValidation.Results;
using CoreFaces.Identity.Models.Domain;
using System;
using System.Collections.Generic;

namespace CoreFaces.Identity.Models.Users
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Lütfen adınızı giriniz.");
            //RuleFor(p => p.Name).Length(2, 100).WithMessage("Ad alanını kontrol ediniz");

            RuleFor(p => p.SurName).NotEmpty().WithMessage("Lütfen soyadınızı giriniz.");
            //RuleFor(p => p.SurName).Length(2, 100).WithMessage("Soyad alanını kontrol ediniz");

            RuleFor(p => p.Password).Length(4, 20).WithMessage("Şifreniz 4 ila 20 karakter olmalıdır.");

            RuleFor(p => p.ParentId).Must(ParentId).WithMessage("ParentId alanını kontrol ediniz.");//bu alan zaten guid biliyorum :-). fakat custom bir validate nasıl yapılırın örneği olarak burada duruyor.
        }

        private bool ParentId(Guid ParentId)
        {
            try
            {
                Guid.Parse(ParentId.ToString());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static IList<ValidationFailure> FieldValidate(User user)
        {
            UserValidator validator = new UserValidator();
            ValidationResult results = validator.Validate(user);
            bool validationSucceeded = results.IsValid;
            IList<ValidationFailure> failures = results.Errors;

            return failures;
        }
    }
}
