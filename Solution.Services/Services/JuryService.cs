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
        var jury = await dbContext.Juries
            .FirstOrDefaultAsync(x => x.PublicId == model.Id);
        
        if (jury == null)
        {
            return Error.NotFound("Jury not found.");
        }
        
        jury.Name = model.Name.Value;
        jury.PhoneNumber = model.PhoneNumber.Value;
        jury.Email = model.Email.Value;
        
        try
        {
            await dbContext.SaveChangesAsync();
            return Result.Success;
        }
        catch (Exception ex)
        {
            return Error.Failure(description: $"Failed to update jury: {ex.Message}");
        }
    }

    public async Task<ErrorOr<Success>> DeleteAsync(string juryId)
    {
        var jury = await dbContext.Juries
            .Include(j => j.Competitions)
            .FirstOrDefaultAsync(x => x.PublicId == juryId);
        
        if (jury == null)
        {
            return Error.NotFound("Jury not found.");
        }
        
        if (jury.Competitions != null)
        {
            foreach (var competition in jury.Competitions.ToList())
            {
                var competitionEntity = await dbContext.Competitions
                    .Include(c => c.Juries)
                    .FirstOrDefaultAsync(c => c.Id == competition.Id);
                
                if (competitionEntity != null)
                {
                    dbContext.Entry(competitionEntity)
                        .Collection(c => c.Juries)
                        .Load();

                    var juryInComp = competitionEntity.Juries.FirstOrDefault(j => j.Id == jury.Id);
                    if (juryInComp != null)
                    {
                        dbContext.Entry(competitionEntity).Collection(c => c.Juries).CurrentValue = 
                            competitionEntity.Juries.Where(j => j.Id != jury.Id).ToList();
                    }
                }
            }
            
            await dbContext.SaveChangesAsync();
        }
        
        dbContext.Juries.Remove(jury);
        
        try
        {
            await dbContext.SaveChangesAsync();
            return Result.Success;
        }
        catch (Exception ex)
        {
            return Error.Failure(description: $"Failed to delete jury: {ex.Message}");
        }
    }

    public async Task<ErrorOr<JuryModel>> GetByIdAsync(string juryId)
    {
        var jury = await dbContext.Juries
            .Include(j => j.Competitions)
            .FirstOrDefaultAsync(x => x.PublicId == juryId);

        if (jury is null)
        {
            return Error.NotFound(description: "Jury not found.");
        }

        return new JuryModel(jury);
    }

    public async Task<ErrorOr<List<JuryModel>>> GetAllAsync() =>
        await dbContext.Juries.AsNoTracking()
                   .Include(j => j.Competitions)
                   .Select(x => new JuryModel(x))
                   .ToListAsync();

    public async Task<ErrorOr<PaginationModel<JuryModel>>> GetPagedAsync(int page = 0)
    {
        page = page < 0 ? 0 : page - 1;

        var juries = await dbContext.Juries.AsNoTracking()
                                                       .Include(j => j.Competitions)
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

    public async Task<ErrorOr<List<JuryModel>>> GetByCompetitionIdAsync(string competitionId)
    {
        try
        {
            var competition = await dbContext.Competitions
                .Include(c => c.Juries)
                .FirstOrDefaultAsync(c => c.PublicId == competitionId);
            
            if (competition == null)
                return Error.NotFound(description: "Competition not found.");
            
            var juries = competition.Juries?.Select(j => new JuryModel(j)).ToList() ?? new List<JuryModel>();
            return juries;
        }
        catch (Exception ex)
        {
            return Error.Failure(description: $"Failed to retrieve juries: {ex.Message}");
        }
    }

    public async Task<ErrorOr<Success>> AssignJuryToCompetitionsAsync(string juryId, List<string> competitionIds)
    {
        try
        {
            var jury = await dbContext.Juries
                .FirstOrDefaultAsync(j => j.PublicId == juryId);
                
            if (jury == null)
                return Error.NotFound(description: "Jury not found.");

            
            var competitions = new List<CompetitionEntity>();
            if (competitionIds.Count > 0)
            {
                competitions = await dbContext.Competitions
                    .Where(c => competitionIds.Contains(c.PublicId))
                    .ToListAsync();
                
                    
                if (competitions.Count == 0)
                    return Error.NotFound(description: "No competitions found with the provided IDs.");
            }
            
            await using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                await RemoveAllJuryCompetitionRelationships(jury.Id);
                
                if (competitions.Count > 0)
                {
                    foreach (var competition in competitions)
                    {
                        
                        var sql = "INSERT INTO CompetitionEntityJuryEntity (CompetitionsId, JuriesId) VALUES (@compId, @juryId);";
                        
                        var compIdParam = new Microsoft.Data.SqlClient.SqlParameter("@compId", System.Data.SqlDbType.BigInt)
                        {
                            Value = Convert.ToInt64(competition.Id)
                        };
                        
                        var juryIdParam = new Microsoft.Data.SqlClient.SqlParameter("@juryId", System.Data.SqlDbType.BigInt)
                        {
                            Value = Convert.ToInt64(jury.Id)
                        };
                        
                        await dbContext.Database.ExecuteSqlRawAsync(sql, compIdParam, juryIdParam);
                    }
                }
                
                await transaction.CommitAsync();
                return Result.Success;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        catch (Exception ex)
        {
            return Error.Failure(description: $"Failed to assign competitions to jury: {ex.Message}");
        }
    }

    private async Task RemoveAllJuryCompetitionRelationships(uint juryId)
    {
        var sql = "DELETE FROM CompetitionEntityJuryEntity WHERE JuriesId = @juryId;";
        
        var juryIdParam = new Microsoft.Data.SqlClient.SqlParameter("@juryId", System.Data.SqlDbType.BigInt)
        {
            Value = Convert.ToInt64(juryId)
        };
        
        await dbContext.Database.ExecuteSqlRawAsync(sql, juryIdParam);
    }

    public async Task<ErrorOr<List<CompetitionModel>>> GetAvailableCompetitionsAsync()
    {
        try
        {
            var countSql = "SELECT COUNT(*) FROM Competition;";
            var count = await dbContext.Database.ExecuteSqlRawAsync(countSql);
            var competitions = await dbContext.Competitions.ToListAsync();
            var competitionsWithRelations = await dbContext.Competitions
                .AsNoTracking()
                .Include(c => c.Street)
                .ThenInclude(s => s.City)
                .OrderBy(c => c.Date)
                .ToListAsync();
            
            if (competitionsWithRelations == null || competitionsWithRelations.Count == 0)
            {
                return new List<CompetitionModel>();
            }
            
            
            var result = new List<CompetitionModel>();
            
            foreach (var c in competitionsWithRelations)
            {
                try {
                    var model = new CompetitionModel(c);
                    
                    if (model.Name?.Value == null)
                    {
                        model.Name.Value = c.Name ?? string.Empty;
                    }
                    
                    if (model.Date?.Value == default)
                    {
                        model.Date.Value = c.Date != default ? c.Date : DateTime.Now;
                    }
                    
                    result.Add(model);
                }
                catch (Exception ex) {
                }
            }
            
            
            return result;
        }
        catch (Exception ex)
        {
            return Error.Failure(description: $"Failed to retrieve competitions: {ex.Message}");
        }
    }
}