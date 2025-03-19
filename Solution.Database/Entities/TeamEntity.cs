[Table("Team")]
[Index(nameof(Name), IsUnique = true)]
public class TeamEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public uint Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public uint Points { get; set; }

    public virtual IReadOnlyCollection<CompetitionEntity> Competition { get; set; }

    public virtual IReadOnlyCollection<TeamMemberEntity> TeamMembers { get; set; }
}

