using Microsoft.EntityFrameworkCore;

using TechnicalPursuitApi.Application.Interfaces;
using TechnicalPursuitApi.Domain;
using TechnicalPursuitApi.Domain.JoueurAggregate;

namespace TechnicalPursuitApi.Infrastructure.Persistence.Repositories;

public class JoueurRepository : IJoueurRepository
{
    private readonly TechnicalPursuitApiDbContext _context;

    public JoueurRepository(TechnicalPursuitApiDbContext context)
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

    public async Task<Joueur?> GetItem(string joueurId)
    {
        return await _context.Joueurs.Where(x => x.Id.Value.ToString() == joueurId).SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Question>> GetManyItems(string questionId)
    {
        return await _context.Questions.Where(x => x.Id == questionId).ToListAsync();
    }
}