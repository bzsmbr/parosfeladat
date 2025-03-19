[Table("Competition")]
public class CompetitionEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public uint Id { get; set; }

    [Required]
    [StringLength(32)]
    public string Date { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    [ForeignKey("Street")]
    public uint StreetId { get; set; }

    public virtual StreetEntity Street { get; set; }

    public virtual IReadOnlyCollection<TeamEntity> Team { get; set; }

    public virtual IReadOnlyCollection<JuryEntity> Jury { get; set; }
}


