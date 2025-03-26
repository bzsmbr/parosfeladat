public class CompetitionService(AppDbContext dbContext) : ICompetitionService
{
    private const int ROW_COUNT = 10;

    public async Task<ErrorOr<CompetitionModel>> CreateAsync(CompetitionModel model)
    {
        bool exists = await dbContext.Competitions.AnyAsync(x => x.StreetId == uint.Parse(model.Street.Value.Id) &&
                                                x.Name.ToLower() == model.Name.Value.ToLower().Trim() &&
                                                x.Date == model.Date.Value);

        if (exists)
        {
            return Error.Conflict(description: "Competition already exists!");
        }

        var competition = model.ToEntity();
        competition.PublicId = Guid.NewGuid().ToString();

        await dbContext.Competitions.AddAsync(competition);
        await dbContext.SaveChangesAsync();

        return new CompetitionModel(competition)
        {
            Street = model.Street
        };
    }
    public async Task<ErrorOr<Success>> UpdateAsync(CompetitionModel model)
    {
        var result = await dbContext.Competitions.AsNoTracking()
                                        .Include(x => x.Street)
                                        .Where(x => x.PublicId == model.Id)
                                        .ExecuteUpdateAsync(x => x.SetProperty(p => p.PublicId, model.Id)
                                                                  .SetProperty(p => p.StreetId, uint.Parse(model.Street.Value.Id))
                                                                  .SetProperty(p => p.Name, model.Name.Value)
                                                                  .SetProperty(p => p.Date, model.Date.Value));
        return result > 0 ? Result.Success : Error.NotFound();
    }
    public async Task<ErrorOr<Success>> DeleteAsync(string competitionId)
    {
        var result = await dbContext.Competitions.AsNoTracking()
                                        .Include(x => x.Street)
                                        .Where(x => x.PublicId == competitionId)
                                        .ExecuteDeleteAsync();

        return result > 0 ? Result.Success : Error.NotFound();
    }
    public async Task<ErrorOr<CompetitionModel>> GetByIdAsync(string competitionId) { }
    public async Task<ErrorOr<List<CompetitionModel>>> GetAllAsync() { }
    public async Task<ErrorOr<PaginationModel<CompetitionModel>>> GetPagedAsync(int page = 0) { }
}