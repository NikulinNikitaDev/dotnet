using FluentValidation;
using StudentsApp.API.Resources;

namespace StudentsApp.API.Validators
{
    public class SaveStudentResourceValidator : AbstractValidator<SaveStudentResource>
    {
        public SaveStudentResourceValidator()
        {
            const int maxLength = 50;
            
            RuleFor(a => a.Name).NotEmpty().MaximumLength(maxLength);
        }
    }
}