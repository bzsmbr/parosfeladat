public interface ITeamMemberService
{
    Task<ErrorOr<TeamMemberModel>> CreateAsync(TeamMemberModel model);
    Task<ErrorOr<Success>> UpdateAsync(TeamMemberModel model);
    Task<ErrorOr<Success>> DeleteAsync(string teamMemberId);
    Task<ErrorOr<TeamMemberModel>> GetByIdAsync(string teamMemberId);
    Task<ErrorOr<List<TeamMemberModel>>> GetAllAsync();
    Task<ErrorOr<PaginationModel<TeamMemberModel>>> GetPagedAsync(int page = 0);
    Task<ErrorOr<List<TeamMemberModel>>> GetAvailableTeamMembersAsync();
    Task<ErrorOr<List<TeamModel>>> GetTeamsByMemberIdAsync(string teamMemberId);
    Task<ErrorOr<Success>> AssignTeamMemberToTeamsAsync(string teamMemberId, List<string> teamIds);
}
