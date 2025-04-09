public class CompetitionService(AppDbContext dbContext) : ICompetitionService
{
    private const int ROW_COUNT = 10;

    public async Task<ErrorOr<CompetitionModel>> CreateAsync(CompetitionModel model)
    {
        bool exists = await dbContext.Competitions.AnyAsync(x => x.Name.ToLower() == model.Name.Value.ToLower().Trim() &&
                                                x.Date.Date == model.Date.Value.Date);

        if (exists)
        {
            return Error.Conflict(description: "Competition already exists!");
        }

        // Check if the Street exists
        var street = await dbContext.Streets.FirstOrDefaultAsync(s => s.Id == model.Street.Value.Id);
        if (street == null)
        {
            // Create and save the Street entity if it does not exist
            street = model.Street.Value.ToEntity();
            await dbContext.Streets.AddAsync(street);
            await dbContext.SaveChangesAsync();
        }

        var competition = model.ToEntity();
        competition.PublicId = Guid.NewGuid().ToString();
        competition.StreetId = street.Id; // Set the StreetId

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
                                                                  .SetProperty(p => p.StreetId, model.Street.Value.Id)
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
    public async Task<ErrorOr<CompetitionModel>> GetByIdAsync(string competitionId)
    {
        var competition = await dbContext.Competitions.Include(x => x.Street)
                                            .FirstOrDefaultAsync(x => x.PublicId == competitionId);

        if (competition is null)
        {
            return Error.NotFound(description: "Competition not found.");
        }

        return new CompetitionModel(competition);
    }
    public async Task<ErrorOr<List<CompetitionModel>>> GetAllAsync() =>
        await dbContext.Competitions.AsNoTracking()
                   .Include(x => x.Street)
                   .Select(x => new CompetitionModel(x))
                   .ToListAsync();
    public async Task<ErrorOr<PaginationModel<CompetitionModel>>> GetPagedAsync(int page = 0)
    {
        page = page < 0 ? 0 : page - 1;

        var competitions = await dbContext.Competitions.AsNoTracking()
                                                       .Include(x => x.Street).ThenInclude(x => x.City)
                                                       .Skip(page * ROW_COUNT)
                                                       .Take(ROW_COUNT)
                                                       .Select(x => new CompetitionModel(x))
                                                       .ToListAsync();

        var paginationModel = new PaginationModel<CompetitionModel>
        {
            Items = competitions,
            Count = await dbContext.Competitions.CountAsync()
        };

        return paginationModel;
    }
}