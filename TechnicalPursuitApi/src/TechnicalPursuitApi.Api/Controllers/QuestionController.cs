using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechnicalPursuitApi.Application.Rules.Queries.GetRules;
using TechnicalPursuitApi.Domain;

namespace TechnicalPursuitApi.Api.Controllers;

public class QuestionController : ApiController
{
    private readonly IMediator _mediator;
    public QuestionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get rules for beneficiaire.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of rules for Beneficiaire domaine.</returns>
    /// <response code="200">Returns list of collaborateurs rules.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="500"></response>
    [HttpGet("rules")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetQuestions(CancellationToken cancellationToken)
    {
        var getRulesQuery = new GetRulesQuery();
        var rulesResult = await _mediator.Send(getRulesQuery, cancellationToken);

        return rulesResult.Match(
            rules => Ok(new Question()),
            Problem);
    }
}