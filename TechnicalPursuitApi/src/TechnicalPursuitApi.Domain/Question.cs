using TechnicalPursuitApi.Domain.Common.Models;
using TechnicalPursuitApi.Domain.ValueObjects;

namespace TechnicalPursuitApi.Domain;

public class Question : EntityId<QuestionId>
{
    public Question()
    {
    }

    public Question(string id, string intitule, IEnumerable<string> possibleAnswers, string goodAnswer)
    {
        this.Id = id;
        this.Intitule = intitule;
        this.PossibleAnswers = possibleAnswers;
        this.GoodAnswer = goodAnswer;
    }

    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string Intitule { get; set; } = string.Empty;

    public IEnumerable<string> PossibleAnswers { get; set; } = new List<string>();

    public string GoodAnswer { get; set; } = string.Empty;
}