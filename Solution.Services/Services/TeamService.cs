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
    
    
    public async Task<ErrorOr<List<TeamModel>>> GetByCompetitionIdAsync(string competitionId)
    {
        try
        {
            var competition = await dbContext.Competitions
                .Include(c => c.Teams)
                .FirstOrDefaultAsync(c => c.PublicId == competitionId);
            
            if (competition == null)
                return Error.NotFound(description: "Competition not found.");
            
            var teams = competition.Teams?.Select(team => new TeamModel(team)).ToList() ?? new List<TeamModel>();
            return teams;
        }
        catch (Exception ex)
        {
            return Error.Failure(description: $"Failed to retrieve teams: {ex.Message}");
        }
    }
    
    public async Task<ErrorOr<Success>> AssignTeamToCompetitionsAsync(string teamId, List<string> competitionIds)
    {
        try
        {
            var team = await dbContext.Teams
                .FirstOrDefaultAsync(t => t.PublicId == teamId);
                
            if (team == null)
                return Error.NotFound(description: "Team not found.");

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
                await RemoveAllTeamCompetitionRelationships(team.Id);
                
                if (competitions.Count > 0)
                {
                    foreach (var competition in competitions)
                    {
                        var sql = "INSERT INTO CompetitionEntityTeamEntity (CompetitionsId, TeamsId) VALUES (@compId, @teamId);";
                        
                        var compIdParam = new Microsoft.Data.SqlClient.SqlParameter("@compId", System.Data.SqlDbType.BigInt)
                        {
                            Value = Convert.ToInt64(competition.Id)
                        };
                        
                        var teamIdParam = new Microsoft.Data.SqlClient.SqlParameter("@teamId", System.Data.SqlDbType.BigInt)
                        {
                            Value = Convert.ToInt64(team.Id)
                        };
                        
                        await dbContext.Database.ExecuteSqlRawAsync(sql, compIdParam, teamIdParam);
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
            return Error.Failure(description: $"Failed to assign competitions to team: {ex.Message}");
        }
    }
    
    private async Task RemoveAllTeamCompetitionRelationships(uint teamId)
    {
        var sql = "DELETE FROM CompetitionEntityTeamEntity WHERE TeamsId = @teamId;";
        
        var teamIdParam = new Microsoft.Data.SqlClient.SqlParameter("@teamId", System.Data.SqlDbType.BigInt)
        {
            Value = Convert.ToInt64(teamId)
        };
        
        await dbContext.Database.ExecuteSqlRawAsync(sql, teamIdParam);
    }
    
    public async Task<ErrorOr<List<CompetitionModel>>> GetAvailableCompetitionsForTeamAsync()
    {
        try
        {
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
    
    public async Task<ErrorOr<List<TeamMemberModel>>> GetTeamMembersAsync(string teamId)
    {
        try
        {
            var team = await dbContext.Teams
                .Include(t => t.TeamMembers)
                .FirstOrDefaultAsync(t => t.PublicId == teamId);
            
            if (team == null)
                return Error.NotFound(description: "Team not found.");
            
            var members = team.TeamMembers?.Select(member => new TeamMemberModel(member)).ToList() ?? new List<TeamMemberModel>();
            return members;
        }
        catch (Exception ex)
        {
            return Error.Failure(description: $"Failed to retrieve team members: {ex.Message}");
        }
    }
    
    public async Task<ErrorOr<Success>> AssignTeamMembersToTeamAsync(string teamId, List<string> teamMemberIds)
    {
        try
        {
            var team = await dbContext.Teams
                .FirstOrDefaultAsync(t => t.PublicId == teamId);
                
            if (team == null)
                return Error.NotFound(description: "Team not found.");

            var members = new List<TeamMemberEntity>();
            if (teamMemberIds.Count > 0)
            {
                members = await dbContext.TeamMembers
                    .Where(m => teamMemberIds.Contains(m.PublicId))
                    .ToListAsync();
                
                    
                if (members.Count == 0)
                    return Error.NotFound(description: "No team members found with the provided IDs.");
            }
            
            await using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                await RemoveAllTeamMemberRelationships(team.Id);
                
                if (members.Count > 0)
                {
                    foreach (var member in members)
                    {
                        var sql = "INSERT INTO TeamEntityTeamMemberEntity (TeamsId, TeamMembersId) VALUES (@teamId, @memberId);";
                        
                        var teamIdParam = new Microsoft.Data.SqlClient.SqlParameter("@teamId", System.Data.SqlDbType.BigInt)
                        {
                            Value = Convert.ToInt64(team.Id)
                        };
                        
                        var memberIdParam = new Microsoft.Data.SqlClient.SqlParameter("@memberId", System.Data.SqlDbType.BigInt)
                        {
                            Value = Convert.ToInt64(member.Id)
                        };
                        
                        await dbContext.Database.ExecuteSqlRawAsync(sql, teamIdParam, memberIdParam);
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
            return Error.Failure(description: $"Failed to assign members to team: {ex.Message}");
        }
    }
    
    private async Task RemoveAllTeamMemberRelationships(uint teamId)
    {
        var sql = "DELETE FROM TeamEntityTeamMemberEntity WHERE TeamsId = @teamId;";
        
        var teamIdParam = new Microsoft.Data.SqlClient.SqlParameter("@teamId", System.Data.SqlDbType.BigInt)
        {
            Value = Convert.ToInt64(teamId)
        };
        
        await dbContext.Database.ExecuteSqlRawAsync(sql, teamIdParam);
    }
}