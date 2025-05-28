[Table("Team")]
[Index(nameof(Name), IsUnique = true)]
public class TeamEntity
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
    public uint? Points { get; set; }

    [StringLength(128)]
    public string? ImageId { get; set; }

    [StringLength(512)]
    public string? WebContentLink { get; set; }

    public virtual IReadOnlyCollection<CompetitionEntity> Competitions { get; set; }

    public virtual IReadOnlyCollection<TeamMemberEntity> TeamMembers { get; set; }
}

