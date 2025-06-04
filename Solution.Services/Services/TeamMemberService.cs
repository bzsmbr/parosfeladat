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

        var member = model.ToEntity();
        member.PublicId = Guid.NewGuid().ToString();

        await dbContext.TeamMembers.AddAsync(member);
        await dbContext.SaveChangesAsync();

        return new TeamMemberModel(member);
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
        var member = await dbContext.TeamMembers.FirstOrDefaultAsync(x => x.PublicId == teamMemberId);

        if (member is null)
        {
            return Error.NotFound(description: "Team member not found.");
        }

        return new TeamMemberModel(member);
    }
    
    public async Task<ErrorOr<List<TeamMemberModel>>> GetAllAsync() =>
        await dbContext.TeamMembers.AsNoTracking()
                           .Select(x => new TeamMemberModel(x))
                           .ToListAsync();
                           
    public async Task<ErrorOr<PaginationModel<TeamMemberModel>>> GetPagedAsync(int page = 0)
    {
        page = page < 0 ? 0 : page - 1;

        var members = await dbContext.TeamMembers.AsNoTracking()
                                                       .Skip(page * ROW_COUNT)
                                                       .Take(ROW_COUNT)
                                                       .Select(x => new TeamMemberModel(x))
                                                       .ToListAsync();

        var paginationModel = new PaginationModel<TeamMemberModel>
        {
            Items = members,
            Count = await dbContext.TeamMembers.CountAsync()
        };

        return paginationModel;
    }
    
    public async Task<ErrorOr<List<TeamMemberModel>>> GetAvailableTeamMembersAsync()
    {
        try
        {
            var members = await dbContext.TeamMembers
                .AsNoTracking()
                .ToListAsync();
            
            if (members == null || members.Count == 0)
            {
                return new List<TeamMemberModel>();
            }
            
            return members.Select(m => new TeamMemberModel(m)).ToList();
        }
        catch (Exception ex)
        {
            return Error.Failure(description: $"Failed to retrieve team members: {ex.Message}");
        }
    }
    
    public async Task<ErrorOr<List<TeamModel>>> GetTeamsByMemberIdAsync(string teamMemberId)
    {
        try
        {
            var member = await dbContext.TeamMembers
                .Include(m => m.Teams)
                .FirstOrDefaultAsync(m => m.PublicId == teamMemberId);
            
            if (member == null)
                return Error.NotFound(description: "Team member not found.");
            
            var teams = member.Teams?.Select(team => new TeamModel(team)).ToList() ?? new List<TeamModel>();
            return teams;
        }
        catch (Exception ex)
        {
            return Error.Failure(description: $"Failed to retrieve teams: {ex.Message}");
        }
    }
    
    public async Task<ErrorOr<Success>> AssignTeamMemberToTeamsAsync(string teamMemberId, List<string> teamIds)
    {
        try
        {
            var member = await dbContext.TeamMembers
                .FirstOrDefaultAsync(m => m.PublicId == teamMemberId);
                
            if (member == null)
                return Error.NotFound(description: "Team member not found.");

            var teams = new List<TeamEntity>();
            if (teamIds.Count > 0)
            {
                teams = await dbContext.Teams
                    .Where(t => teamIds.Contains(t.PublicId))
                    .ToListAsync();
                    
                if (teams.Count == 0)
                    return Error.NotFound(description: "No teams found with the provided IDs.");
            }
            
            await using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                await RemoveAllTeamMemberRelationships(member.Id);
                
                if (teams.Count > 0)
                {
                    foreach (var team in teams)
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
            return Error.Failure(description: $"Failed to assign teams to member: {ex.Message}");
        }
    }
    
    private async Task RemoveAllTeamMemberRelationships(uint memberId)
    {
        var sql = "DELETE FROM TeamEntityTeamMemberEntity WHERE TeamMembersId = @memberId;";
        
        var memberIdParam = new Microsoft.Data.SqlClient.SqlParameter("@memberId", System.Data.SqlDbType.BigInt)
        {
            Value = Convert.ToInt64(memberId)
        };
        
        await dbContext.Database.ExecuteSqlRawAsync(sql, memberIdParam);
    }
}