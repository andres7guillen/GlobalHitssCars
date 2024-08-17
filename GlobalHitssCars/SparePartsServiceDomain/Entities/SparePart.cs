using CSharpFunctionalExtensions;
using SparePartsServiceDomain.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace SparePartsServiceDomain.Entities
{
    public class SparePart
    {
        public Guid Id { get; set; }
        [StringLength(100)]
        public string SpareName { get; set; } = string.Empty;
        [StringLength(100)]
        public string BrandSpare { get; set; } = string.Empty;
        [StringLength(50)]
        public string BrandCar { get; set; } = string.Empty;
        public int ModelCar { get; set; }
        [StringLength(100)]
        public string ReferenceCar { get; set; } = string.Empty;
        public bool IsInStock { get; set; }
        public int Stock { get; set; }

        private SparePart(Guid id, string spareName, string brandSpare, string brandCar, int modelCar, string referenceCar, bool isInStock, int stock)
        {
            Id = id;
            SpareName = spareName;
            BrandSpare = brandSpare;
            BrandCar = brandCar;
            ModelCar = modelCar;
            ReferenceCar = referenceCar;
            IsInStock = isInStock;
            Stock = stock;
        }

        public static Result<SparePart> Build(string withSpareName, string withBrandSpare, string withBrandCar, int withModelCar, string withReferenceCar, bool withIsInStock, int withStock)
        {
            return new SparePart(Guid.NewGuid(), withSpareName, withBrandSpare, withBrandCar, withModelCar, withReferenceCar, withIsInStock, withStock);
        }

        public static Result<SparePart> Load(Guid withId, string withSpareName, string withBrandSpare, string withBrandCar, int withModelCar, string withReferenceCar, bool withIsInStock, int withStock)
        {
            return new SparePart(withId, withSpareName, withBrandSpare, withBrandCar, withModelCar, withReferenceCar, withIsInStock, withStock);
        }

        public void UpdateSpare(string? spareName = null, string? brandSpare = null, string? brandCar = null, int? model = null, string? reference = null, bool? isInStock = null, int? stock = null)
        {
            if (!string.IsNullOrWhiteSpace(spareName))
                SpareName = spareName;
            if (!string.IsNullOrWhiteSpace(brandSpare))
                BrandSpare = brandSpare;
            if (!string.IsNullOrWhiteSpace(brandCar))
                BrandCar = brandCar;
            if (model.HasValue)
                ModelCar = model.Value;
            if (!string.IsNullOrWhiteSpace(reference))
                ReferenceCar = reference;
            if (isInStock.HasValue)
                IsInStock = isInStock.Value;
            if (stock.HasValue)
                Stock = stock.Value;

        }

        public Result<bool> AddStock(int? quantity = null)
        {
            if (quantity.HasValue)
            {
                if (quantity.Value <= 0)
                {
                    return Result.Failure<bool>(SparePartContextExceptionEnum.QuantityCannotBeLessThanZero.GetErrorMessage());
                }
                else 
                {
                    Stock += quantity.Value;
                    return Result.Success(true);
                }
            }
            return Result.Failure<bool>(SparePartContextExceptionEnum.ErrorTryingToAddStock.GetErrorMessage());

        }

        public Result<bool> LessStock(int? quantity = null) 
        {
            if (quantity.HasValue)
            {
                if (quantity.Value <= 0)
                {
                    return Result.Failure<bool>(SparePartContextExceptionEnum.QuantityCannotBeLessThanZero.GetErrorMessage());
                }
                else if (quantity.Value > Stock)
                {
                    return Result.Failure<bool>(SparePartContextExceptionEnum.IsNotEnoughStockToDelete.GetErrorMessage());
                }
                else 
                { 
                    Stock -= quantity.Value;
                    return Result.Success(true);
                }
            }
            return Result.Failure<bool>(SparePartContextExceptionEnum.ErrorTryingToLessStock.GetErrorMessage());
        }

    }
}
