public class TeamMemberService(AppDbContext dbContext) : ITeamMemberService
{
    private const int ROW_COUNT = 10;

    public async Task<ErrorOr<TeamMemberModel>> CreateAsync(TeamMemberModel model)
    {
        bool exists = await dbContext.TeamMembers.AnyAsync(x => x.Name.ToLower() == model.Name.Value.ToLower().Trim());

        if (exists)
        {
            return Error.Conflict(description: "Team member already exists!");
        }

        var teamMember = model.ToEntity();
        teamMember.PublicId = Guid.NewGuid().ToString();

        await dbContext.TeamMembers.AddAsync(teamMember);
        await dbContext.SaveChangesAsync();

        return new TeamMemberModel(teamMember);
    }
    public async Task<ErrorOr<Success>> UpdateAsync(TeamMemberModel model)
    {
        var result = await dbContext.TeamMembers.AsNoTracking()
                                .Where(x => x.PublicId == model.Id)
                                .ExecuteUpdateAsync(x => x.SetProperty(p => p.PublicId, model.Id)
                                                          .SetProperty(p => p.Name, model.Name.Value)
                                                          .SetProperty(p => p.ImageId, model.ImageId)
                                                          .SetProperty(p => p.WebContentLink, model.WebContentLink));
        return result > 0 ? Result.Success : Error.NotFound();
    }
    public async Task<ErrorOr<Success>> DeleteAsync(string teamMemberId)
    {
        var result = await dbContext.TeamMembers.AsNoTracking()
                                .Where(x => x.PublicId == teamMemberId)
                                .ExecuteDeleteAsync();

        return result > 0 ? Result.Success : Error.NotFound();
    }
    public async Task<ErrorOr<TeamMemberModel>> GetByIdAsync(string teamMemberId)
    {
        var teamMember = await dbContext.TeamMembers.FirstOrDefaultAsync(x => x.PublicId == teamMemberId);

        if (teamMember is null)
        {
            return Error.NotFound(description: "Team member not found.");
        }

        return new TeamMemberModel(teamMember);
    }
    public async Task<ErrorOr<List<TeamMemberModel>>> GetAllAsync() =>
        await dbContext.TeamMembers.AsNoTracking()
                           .Select(x => new TeamMemberModel(x))
                           .ToListAsync();
    public async Task<ErrorOr<PaginationModel<TeamMemberModel>>> GetPagedAsync(int page = 0)
    {
        page = page < 0 ? 0 : page - 1;

        var teamMembers = await dbContext.TeamMembers.AsNoTracking()
                                                       .Skip(page * ROW_COUNT)
                                                       .Take(ROW_COUNT)
                                                       .Select(x => new TeamMemberModel(x))
                                                       .ToListAsync();

        var paginationModel = new PaginationModel<TeamMemberModel>
        {
            Items = teamMembers,
            Count = await dbContext.TeamMembers.CountAsync()
        };

        return paginationModel;
    }
}