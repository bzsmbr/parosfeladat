
[Table("TeamMember")]
public class TeamMemberEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public uint Id { get; set; }

    [Required]
    [StringLength(32)]
    public string Name { get; set; }

    [Required]
    [ForeignKey("Team")]
    public uint TeamId { get; set; }

    public virtual TeamEntity Team { get; set; }
}

