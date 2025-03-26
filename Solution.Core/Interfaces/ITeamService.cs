public interface ITeamService
{
    Task<ErrorOr<TeamModel>> CreateAsync(TeamModel model);
    Task<ErrorOr<Success>> UpdateAsync(TeamModel model);
    Task<ErrorOr<Success>> DeleteAsync(string teamId);
    Task<ErrorOr<TeamModel>> GetByIdAsync(string teamId);
    Task<ErrorOr<List<TeamModel>>> GetAllAsync();
    Task<ErrorOr<PaginationModel<TeamModel>>> GetPagedAsync(int page = 0);
}