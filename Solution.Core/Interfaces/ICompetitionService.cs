public interface ICompetitionService
{
    Task<ErrorOr<CompetitionModel>> CreateAsync(CompetitionModel model);
    Task<ErrorOr<Success>> UpdateAsync(CompetitionModel model);
    Task<ErrorOr<Success>> UpdateAsync2(StreetModel model);
    Task<ErrorOr<Success>> DeleteAsync(string competitionId);
    Task<ErrorOr<CompetitionModel>> GetByIdAsync(string competitionId);
    Task<ErrorOr<List<CompetitionModel>>> GetAllAsync();
    Task<ErrorOr<PaginationModel<CompetitionModel>>> GetPagedAsync(int page = 0);
}