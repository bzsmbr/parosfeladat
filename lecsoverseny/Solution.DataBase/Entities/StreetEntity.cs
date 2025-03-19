
[Table("Street")]
public class StreetEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public uint Id { get; set; }

    [Required]
    [StringLength(32)]
    public string Name { get; set; }

    [Required]
    [ForeignKey("City")]
    public uint CityId { get; set; }

    public virtual CityEntity City { get; set; }

    public virtual IReadOnlyCollection<CompetitionEntity> Competition { get; set; }
}

