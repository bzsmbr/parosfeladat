
[Table("Jury")]
public class JuryEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public uint Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string PhoneNumber { get; set; }

    [Required]
    public string Email { get; set; }

    public virtual IReadOnlyCollection<CompetitionEntity> Competition { get; set; }
}

