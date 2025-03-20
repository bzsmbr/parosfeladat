[Table("TeamMember")]
public class TeamMemberEntity
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

    public virtual IReadOnlyCollection<TeamEntity> Teams { get; set; }
}

