namespace Solution.Core.Models;

public partial class CityModel : IObjectValidator<uint>
{
    public uint PostalCode { get; set; }

    public string Name { get; set; }

    public CityModel()
    {
    }

    public CityModel(uint postalCode, string name)
    {
        PostalCode = postalCode;
        Name = name;
    }

    public CityModel(CityEntity entity)
    {
        if (entity is null)
        {
            return;
        }

        PostalCode = entity.PostalCode;
        Name = entity.Name;
    }

    public override bool Equals(object? obj)
    {
        return obj is CityModel model &&
               PostalCode == model.PostalCode &&
               Name == model.Name;
    }
}