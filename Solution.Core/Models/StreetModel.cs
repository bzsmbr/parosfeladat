namespace Solution.Core.Models;

public partial class StreetModel : IObjectValidator<uint>
{
    public uint Id { get; set; }

    public string Name { get; set; }

    public StreetModel()
    {
    }

    public StreetModel(uint id, string name)
    {
        Id = id;
        Name = name;
    }

    public StreetModel(StreetEntity entity)
    {
        if(entity is null)
        {
            return;
        }

        Id = entity.Id;
        Name = entity.Name;
    }

    public override bool Equals(object? obj)
    {
        return obj is StreetModel model &&
               Id == model.Id &&
               Name == model.Name;
    }
}