﻿public interface IJuryService
{
    Task<ErrorOr<JuryModel>> CreateAsync(JuryModel model);
    Task<ErrorOr<Success>> UpdateAsync(JuryModel model);
    Task<ErrorOr<Success>> DeleteAsync(string juryId);
    Task<ErrorOr<JuryModel>> GetByIdAsync(string juryId);
    Task<ErrorOr<List<JuryModel>>> GetAllAsync();
    Task<ErrorOr<PaginationModel<JuryModel>>> GetPagedAsync(int page = 0);
    Task<ErrorOr<List<JuryModel>>> GetByCompetitionIdAsync(string competitionId);
    Task<ErrorOr<Success>> AssignJuryToCompetitionsAsync(string juryId, List<string> competitionIds);
    Task<ErrorOr<List<CompetitionModel>>> GetAvailableCompetitionsAsync();
}
