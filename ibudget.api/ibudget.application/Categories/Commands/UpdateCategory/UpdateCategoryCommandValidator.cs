using FluentValidation;
using ibudget.application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ibudget.application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCategoryCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Name is required.");
        }
    }
}
