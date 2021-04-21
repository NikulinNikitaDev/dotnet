using FluentValidation;
using StudentsApp.API.Resources;

namespace StudentsApp.API.Validators
{
    public class SaveMarkResourceValidator : AbstractValidator<SaveMarkResource>
    {
        public SaveMarkResourceValidator()
        {
            const int maxLength = 50;
            const string errorMsg = "'Student Id' must be greater than 0.";
            
            RuleFor(m => m.Name).NotEmpty().MaximumLength(maxLength);
            
            RuleFor(m => m.StudentId).NotEmpty().WithMessage(errorMsg);
        }
    }
}