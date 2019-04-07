using FluentValidation;
using Fleet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fleet.Service.Validators
{
    public class VehicleValidator : AbstractValidator<Vehicle>
    {
        public VehicleValidator()
        {
            RuleFor(c => c)
                .NotNull()
                .OnAnyFailure(x =>
                {
                    throw new ArgumentNullException("Can't found the object.");
                });

            RuleFor(c => c.Color)
                .NotEmpty().WithMessage("Is necessary to inform the Color.")
                .NotNull().WithMessage("Is necessary to inform the Color.");

            RuleFor(c => c.Chassis.Number)
                .NotEmpty().WithMessage("Is necessary to inform the Chassis Number.")
                .NotNull().WithMessage("Is necessary to inform the Chassis Number.");

            RuleFor(c => c.Chassis.Series)
                .NotEmpty().WithMessage("Is necessary to inform the Chassis Series.")
                .NotNull().WithMessage("Is necessary to inform the Chassis Series.");

            RuleFor(c => c.VehicleType.Id)
                .NotEmpty().WithMessage("Is necessary to inform the Type.")
                .NotNull().WithMessage("Is necessary to inform the Type.");
        }
    }
}
