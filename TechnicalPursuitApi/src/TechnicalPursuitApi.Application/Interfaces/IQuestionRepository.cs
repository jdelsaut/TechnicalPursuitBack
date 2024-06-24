using TechnicalPursuitApi.Domain;

namespace TechnicalPursuitApi.Application.Interfaces;

public interface IQuestionRepository
{
    Task AddItemAsync(Question question);
    Task AddManyItemsAsync(IEnumerable<Question> questions);
    Task<IEnumerable<Question>> GetManyItems(string questionId);
    Task<Question?> GetItem(string questionId);
}