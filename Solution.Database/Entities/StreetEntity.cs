[Table("Street")]
public class StreetEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public uint Id { get; set; }

    [StringLength(128)]
    [Required]
    public string PublicId { get; set; }

    [Required]
    [StringLength(32)]
    public string Name { get; set; }

    [Required]
    public uint? HouseNumber { get; set; }

    [Required]
    [ForeignKey("City")]
    public uint CityId { get; set; }

    public virtual CityEntity City { get; set; }

    public virtual IReadOnlyCollection<CompetitionEntity> Competitions { get; set; }
}

