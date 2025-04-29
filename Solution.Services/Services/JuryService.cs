public class JuryService(AppDbContext dbContext) : IJuryService
{
    private const int ROW_COUNT = 10;

    public async Task<ErrorOr<JuryModel>> CreateAsync(JuryModel model)
    {
        bool exists = await dbContext.Juries.AnyAsync(x => x.Name.ToLower() == model.Name.Value.ToLower().Trim() &&
                                                x.Email == model.Email.Value);

        if (exists)
        {
            return Error.Conflict(description: "Jury already exists!");
        }

        var jury = model.ToEntity();
        jury.PublicId = Guid.NewGuid().ToString();

        await dbContext.Juries.AddAsync(jury);
        await dbContext.SaveChangesAsync();

        return new JuryModel(jury);
    }
    public async Task<ErrorOr<Success>> UpdateAsync(JuryModel model)
    {
        var result = await dbContext.Juries.AsNoTracking()
                                        .Where(x => x.PublicId == model.Id)
                                        .ExecuteUpdateAsync(x => x.SetProperty(p => p.PublicId, model.Id)
                                                                  .SetProperty(p => p.Name, model.Name.Value)
                                                                  .SetProperty(p => p.PhoneNumber, model.PhoneNumber.Value)
                                                                  .SetProperty(p => p.Email, model.Email.Value));
        return result > 0 ? Result.Success : Error.NotFound();
    }
    public async Task<ErrorOr<Success>> DeleteAsync(string juryId)
    {
        var result = await dbContext.Juries.AsNoTracking()
                                        .Where(x => x.PublicId == juryId)
                                        .ExecuteDeleteAsync();

        return result > 0 ? Result.Success : Error.NotFound();
    }
    public async Task<ErrorOr<JuryModel>> GetByIdAsync(string juryId)
    {
        var jury = await dbContext.Juries.FirstOrDefaultAsync(x => x.PublicId == juryId);

        if (jury is null)
        {
            return Error.NotFound(description: "Jury not found.");
        }

        return new JuryModel(jury);
    }
    public async Task<ErrorOr<List<JuryModel>>> GetAllAsync() =>
        await dbContext.Juries.AsNoTracking()
                   .Select(x => new JuryModel(x))
                   .ToListAsync();
    public async Task<ErrorOr<PaginationModel<JuryModel>>> GetPagedAsync(int page = 0)
    {
        page = page < 0 ? 0 : page - 1;

        var juries = await dbContext.Juries.AsNoTracking()
                                                       .Skip(page * ROW_COUNT)
                                                       .Take(ROW_COUNT)
                                                       .Select(x => new JuryModel(x))
                                                       .ToListAsync();

        var paginationModel = new PaginationModel<JuryModel>
        {
            Items = juries,
            Count = await dbContext.Juries.CountAsync()
        };

        return paginationModel;
    }
}