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
    public class Car
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(50)]
        public string Brand { get; set; } = string.Empty;
        public int Model { get; set; }

        [StringLength(100)]
        public string Reference { get; set; } = string.Empty;

        [StringLength(50)]
        public string Colour { get; set; } = string.Empty;

        [StringLength(10)]
        public string LicensePlate { get; set; } = string.Empty;

        private Car(Guid id,
            string brand,
            int model,
            string reference,
            string colour,
            string licensePlate)
        {
            Id = id;
            Brand = brand;
            Model = model;
            Reference = reference;
            Colour = colour;
            LicensePlate = licensePlate;
        }

        public static Result<Car> Build(string withBrand,
            int withModel,
            string withReference,
            string withColour,
            string withLicensePlate)
        {
            if (withLicensePlate.Length > 6)
                return Result.Failure<Car>(CarContextExceptionEnum.LicensePlateError.GetErrorMessage());
            if (withModel > DateTime.Now.Year)
                return Result.Failure<Car>(CarContextExceptionEnum.InvalidModel.GetErrorMessage());
            return new Car(Guid.NewGuid(), withBrand, withModel, withReference, withColour, withLicensePlate);
        }

        public static Result<Car> Load(Guid withId,string withBrand,
            int withModel,
            string withReference,
            string withColour,
            string withLicensePlate)
        {

            return new Car(withId, withBrand, withModel, withReference, withColour, withLicensePlate);
        }

        public void UpdateCar(string? newBrand = null, int? newModel = null, string? newReference = null, string? newColour = null, string? newLicensePlate = null) 
        { 
            if(!string.IsNullOrWhiteSpace(newBrand))
                Brand = newBrand;
            if (newModel.HasValue)
                Model = newModel.Value;
            if(!string.IsNullOrWhiteSpace(newReference))
                Reference = newReference;
            if(!string.IsNullOrWhiteSpace(newColour))
                Colour = newColour;
            if(!string.IsNullOrWhiteSpace(newLicensePlate))
                LicensePlate = newLicensePlate;
        }

    }
}
