[Table("Competition")]
public class CompetitionEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public uint Id { get; set; }

    [StringLength(128)]
    [Required]
    public string PublicId { get; set; }

    [Required]
    [StringLength(32)]
    public DateTime Date { get; set; }

    [Required]
    [StringLength(256)]
    public string StreetName { get; set; }

    [Required]
    [StringLength(128)]
    public string Name { get; set; }

    [Required]
    [ForeignKey("Street")]
    public uint StreetId { get; set; }

    public virtual StreetEntity Street { get; set; }

    public virtual IReadOnlyCollection<TeamEntity> Teams { get; set; }

    public virtual IReadOnlyCollection<JuryEntity> Juries { get; set; }
}


