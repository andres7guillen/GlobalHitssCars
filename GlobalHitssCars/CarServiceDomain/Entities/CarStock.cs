using CarServiceDomain.Exceptions;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarServiceDomain.Entities
{
    public class CarStock
    {
        [Key]
        public Guid Id { get; set; }
        public Guid BrandId { get; set; }
        public int Model { get; set; }

        public Guid ReferenceId { get; set; }

        [StringLength(50)]
        public string Colour { get; set; } = string.Empty;
        public int Stock { get; set; }

        private CarStock(Guid id,
            Guid brandId,
            int model,
            Guid referenceId,
            string colour)
        {
            Id = id;
            BrandId = brandId;
            Model = model;
            ReferenceId = referenceId;
            Colour = colour;
        }

        public static Result<CarStock> Build(Guid withBrandId,
            int withModel,
            Guid withReferenceId,
            string withColour)
        {
            if (withModel > DateTime.Now.Year)
                return Result.Failure<CarStock>(CarContextExceptionEnum.InvalidModel.GetErrorMessage());
            return new CarStock(Guid.NewGuid(), withBrandId, withModel, withReferenceId, withColour);
        }

        public static Result<CarStock> Load(Guid withId,Guid withBrandId,
            int withModel,
            Guid withReferenceId,
            string withColour)
        {

            return new CarStock(withId, withBrandId, withModel, withReferenceId, withColour);
        }

        public void UpdateCar(string? newColour = null) 
        { 
            if(!string.IsNullOrWhiteSpace(newColour))
                Colour = newColour;
        }

    }
}
