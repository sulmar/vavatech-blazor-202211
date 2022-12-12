using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vavatech.Shopper.Domain.Validators
{
    // dotnet add package FluentValidation
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator() 
        { 
            RuleFor(p=>p.Name)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(50);

            RuleFor(p => p.Description)
                .NotEmpty();

            RuleFor(p => p.Price)
                .InclusiveBetween(1, 1000);

            RuleFor(p => p.Discount)
                .InclusiveBetween(1, 500)
                .When(p => p.HasDiscount);
        }
    }
}
