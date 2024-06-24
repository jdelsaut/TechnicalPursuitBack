using Microsoft.EntityFrameworkCore;

using TechnicalPursuitApi.Application.Interfaces;
using TechnicalPursuitApi.Domain;

namespace TechnicalPursuitApi.Infrastructure.Persistence.Repositories;

public class QuestionRepository : IQuestionRepository
{
    private readonly TechnicalPursuitApiDbContext _context;

    public QuestionRepository(TechnicalPursuitApiDbContext context)
    {
        _context = context;
    }

    public async Task AddItemAsync(Question question)
    {
        await _context.Questions.AddAsync(question);
    }

    public async Task AddManyItemsAsync(IEnumerable<Question> questions)
    {
        await _context.Questions.AddRangeAsync(questions);
    }

    public async Task<Question?> GetItem(string questionId)
    {
        return await _context.Questions.Where(x => x.Id == questionId).SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Question>> GetManyItems(string questionId)
    {
        return await _context.Questions.Where(x => x.Id == questionId).ToListAsync();
    }
}