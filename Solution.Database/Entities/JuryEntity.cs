[Table("Jury")]
public class JuryEntity
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
    [StringLength(32)]
    public string PhoneNumber { get; set; }

    [Required]
    [StringLength(32)]
    public string Email { get; set; }

    public virtual IReadOnlyCollection<CompetitionEntity> Competitions { get; set; }
}

