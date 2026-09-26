using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar
{
    public sealed class CreateCarCommandValidator : AbstractValidator<CreateCarCommand>
    {
        public CreateCarCommandValidator()
        {
            RuleFor(p => p.name).NotEmpty().WithMessage("Araç adı boş olamaz!");
            RuleFor(p => p.name).NotNull().WithMessage("Araç adı boş olamaz!");
            RuleFor(p => p.name).MinimumLength(3).WithMessage("Araç adı en az 3 karakter olmalıdır!");
            RuleFor(p => p.model).NotEmpty().WithMessage("Araç modeli boş olamaz!");
            RuleFor(p => p.model).NotNull().WithMessage("Araç modeli boş olamaz!");
            RuleFor(p => p.model).MinimumLength(3).WithMessage("Araç modeli en az 3 karakter olmalıdır!");
            RuleFor(p => p.enginePower).NotEmpty().WithMessage("Araç motor gücü boş olamaz!");
            RuleFor(p => p.enginePower).NotNull().WithMessage("Araç motor gücü boş olamaz!");
            RuleFor(p => p.enginePower).GreaterThan(0).WithMessage("Araç motor gücü 0'dan büyük olmalıdır!");
        }
    }
}
