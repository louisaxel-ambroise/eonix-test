using Eonix.Api.Database;
using Eonix.Api.Model;
using Eonix.Api.Contracts;
using Eonix.Api.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Eonix.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
public class PersonneController : ControllerBase
{
    private readonly EonixContext _context;

    public PersonneController(EonixContext context)
    {
        _context = context;
    }

    [HttpGet(Name = nameof(ListAsync))]
    [ProducesResponseType(200, Type = typeof(IEnumerable<PersonneResponse>))]
    public async Task<IActionResult> ListAsync([FromQuery] PersonneFilter filter, CancellationToken requestAborted)
    {
        var personnes = await _context.Personnes.AsNoTracking().Apply(filter).ToListAsync(requestAborted);

        return Ok(personnes.Select(x => new PersonneResponse(x)));
    }

    [HttpGet("{id}", Name = nameof(GetAsync))]
    [ProducesResponseType(200, Type=typeof(PersonneResponse))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetAsync(Guid id, CancellationToken requestAborted)
    {
        var personne = await _context.Personnes.LoadAsync(id, requestAborted);

        return Ok(new PersonneResponse(personne));
    }

    [HttpPost(Name = nameof(CreateAsync))]
    [ProducesResponseType(201, Type = typeof(PersonneResponse))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateAsync([FromBody] CreatePersonneRequest request, CancellationToken requestAborted)
    {
        var personne = new Personne(request.Prenom, request.Nom);

        _context.Personnes.Add(personne);
        await _context.SaveChangesAsync(requestAborted);

        return CreatedAtAction(nameof(GetAsync), new { personne.Id }, new PersonneResponse(personne));
    }

    [HttpPut("{id}", Name = nameof(UpdateAsync))]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateAsync(Guid id, [BindRequired, FromBody] UpdatePersonneRequest request, CancellationToken requestAborted)
    {
        var personne = await _context.Personnes.LoadAsync(id, requestAborted);

        personne.Prenom = request.Prenom;
        personne.Nom = request.Nom;

        await _context.SaveChangesAsync(requestAborted);

        return NoContent();
    }

    [HttpDelete("{id}", Name = nameof(DeleteAsync))]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> DeleteAsync(Guid id, CancellationToken requestAborted)
    {
        var personne = await _context.Personnes.LoadAsync(id, requestAborted);

        _context.Personnes.Remove(personne);
        await _context.SaveChangesAsync(requestAborted);

        return NoContent();
    }
}
