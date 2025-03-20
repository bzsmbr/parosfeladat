namespace Solution.Core.Models;

public partial class CityModel : IObjectValidator<uint>
{
    public uint Id { get; set; }

    public uint PostalCode { get; set; }

    public string Name { get; set; }

    public CityModel()
    {
    }

    public CityModel(uint id, uint postalCode, string name)
    {
        Id = id;
        PostalCode = postalCode;
        Name = name;
    }

    public CityModel(CityEntity entity)
    {
        if (entity is null)
        {
            return;
        }

        Id = entity.Id;
        PostalCode = entity.PostalCode;
        Name = entity.Name;
    }

    public override bool Equals(object? obj)
    {
        return obj is CityModel model &&
               Id == model.Id &&
               PostalCode == model.PostalCode &&
               Name == model.Name;
    }
}