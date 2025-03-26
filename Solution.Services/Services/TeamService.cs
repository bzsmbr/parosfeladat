public class TeamService(AppDbContext dbContext) : ITeamService
{
    private const int ROW_COUNT = 10;

    public async Task<ErrorOr<TeamModel>> CreateAsync(TeamModel model)
    {
        bool exists = await dbContext.Teams.AnyAsync(x => x.Name.ToLower() == model.Name.Value.ToLower().Trim() &&
                                                        x.Points == model.Points.Value);

        if (exists)
        {
            return Error.Conflict(description: "Team already exists!");
        }

        var team = model.ToEntity();
        team.PublicId = Guid.NewGuid().ToString();

        await dbContext.Teams.AddAsync(team);
        await dbContext.SaveChangesAsync();

        return new TeamModel(team);
    }
    public async Task<ErrorOr<Success>> UpdateAsync(TeamModel model)
    {
        var result = await dbContext.Teams.AsNoTracking()
                                .Where(x => x.PublicId == model.Id)
                                .ExecuteUpdateAsync(x => x.SetProperty(p => p.PublicId, model.Id)
                                                          .SetProperty(p => p.Name, model.Name.Value)
                                                          .SetProperty(p => p.Points, model.Points.Value));
        return result > 0 ? Result.Success : Error.NotFound();
    }
    public async Task<ErrorOr<Success>> DeleteAsync(string teamId)
    {
        var result = await dbContext.Teams.AsNoTracking()
                                .Where(x => x.PublicId == teamId)
                                .ExecuteDeleteAsync();

        return result > 0 ? Result.Success : Error.NotFound();
    }
    public async Task<ErrorOr<TeamModel>> GetByIdAsync(string teamId) { }
    public async Task<ErrorOr<List<TeamModel>>> GetAllAsync() { }
    public async Task<ErrorOr<PaginationModel<TeamModel>>> GetPagedAsync(int page = 0) { }
}