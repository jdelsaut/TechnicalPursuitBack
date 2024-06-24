using TechnicalPursuitApi.Domain.JoueurAggregate;

namespace TechnicalPursuitApi.Application.Interfaces;

public interface IJoueurRepository
{
    Task<Joueur?> GetItem(string joueurId);
}