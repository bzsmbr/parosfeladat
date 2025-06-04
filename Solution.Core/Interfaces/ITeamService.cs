public interface ITeamService
{
    Task<ErrorOr<TeamModel>> CreateAsync(TeamModel model);
    Task<ErrorOr<Success>> UpdateAsync(TeamModel model);
    Task<ErrorOr<Success>> DeleteAsync(string teamId);
    Task<ErrorOr<TeamModel>> GetByIdAsync(string teamId);
    Task<ErrorOr<List<TeamModel>>> GetAllAsync();
    Task<ErrorOr<PaginationModel<TeamModel>>> GetPagedAsync(int page = 0);
    Task<ErrorOr<List<TeamModel>>> GetByCompetitionIdAsync(string competitionId);
    Task<ErrorOr<Success>> AssignTeamToCompetitionsAsync(string teamId, List<string> competitionIds);
    Task<ErrorOr<List<CompetitionModel>>> GetAvailableCompetitionsForTeamAsync();
    Task<ErrorOr<List<TeamMemberModel>>> GetTeamMembersAsync(string teamId);
    Task<ErrorOr<Success>> AssignTeamMembersToTeamAsync(string teamId, List<string> teamMemberIds);
}