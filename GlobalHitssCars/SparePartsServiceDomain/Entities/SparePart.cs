using CSharpFunctionalExtensions;
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

        public static Result<SparePart> Load(Guid withId,string withSpareName, string withBrandSpare, string withBrandCar, int withModelCar, string withReferenceCar, bool withIsInStock, int withStock)
        {
            return new SparePart(withId, withSpareName, withBrandSpare, withBrandCar, withModelCar, withReferenceCar, withIsInStock, withStock);
        }

    }
}
