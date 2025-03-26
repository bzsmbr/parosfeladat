[Table("City")]
[Index(nameof(Name), IsUnique = true)]
public class CityEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public uint Id { get; set; }

    [Required]
    [Range(1000, 9999)]
    public uint PostalCode { get; set; }

    [Required]
    [StringLength(64)]
    public string Name { get; set; }

    public virtual IReadOnlyCollection<StreetEntity> Streets { get; set; }
}

