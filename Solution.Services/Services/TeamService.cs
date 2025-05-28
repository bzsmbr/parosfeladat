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
                                                          .SetProperty(p => p.Points, model.Points.Value)
                                                          .SetProperty(p => p.ImageId, model.ImageId)
                                                          .SetProperty(p => p.WebContentLink, model.WebContentLink));
        return result > 0 ? Result.Success : Error.NotFound();
    }
    public async Task<ErrorOr<Success>> DeleteAsync(string teamId)
    {
        var result = await dbContext.Teams.AsNoTracking()
                                .Where(x => x.PublicId == teamId)
                                .ExecuteDeleteAsync();

        return result > 0 ? Result.Success : Error.NotFound();
    }
    public async Task<ErrorOr<TeamModel>> GetByIdAsync(string teamId)
    {
        var team = await dbContext.Teams.FirstOrDefaultAsync(x => x.PublicId == teamId);

        if (team is null)
        {
            return Error.NotFound(description: "Team not found.");
        }

        return new TeamModel(team);
    }
    public async Task<ErrorOr<List<TeamModel>>> GetAllAsync() =>
        await dbContext.Teams.AsNoTracking()
                           .Select(x => new TeamModel(x))
                           .ToListAsync();
    public async Task<ErrorOr<PaginationModel<TeamModel>>> GetPagedAsync(int page = 0)
    {
        page = page < 0 ? 0 : page - 1;

        var teams = await dbContext.Teams.AsNoTracking()
                                                       .Skip(page * ROW_COUNT)
                                                       .Take(ROW_COUNT)
                                                       .Select(x => new TeamModel(x))
                                                       .ToListAsync();

        var paginationModel = new PaginationModel<TeamModel>
        {
            Items = teams,
            Count = await dbContext.Teams.CountAsync()
        };

        return paginationModel;
    }
}