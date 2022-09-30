using Microsoft.AspNetCore.Mvc;

namespace APIDemo.Controllers;
[Route("api/[controller]")]
[ApiController]
public class SuperHeroController : ControllerBase
{

    private readonly DataContext _context;
    public SuperHeroController(DataContext context)
    {
        _context = context;
    }
    [HttpGet]
    public async Task<ActionResult<List<SuperHero>>> Get()
    {
        return Ok(await _context.Super.ToListAsync());
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<SuperHero>> Get(int id)
    {
        var hero = await _context.Super.FindAsync(id);
        return hero == null ? BadRequest("Hero not found.") : Ok(hero);
    }
    
    [HttpPost]
    public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
    {
        _context.Super.Add(hero);
        await _context.SaveChangesAsync();
        return Ok(await _context.Super.ToListAsync());
    }
    
    [HttpPut]
    public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero req)
    {
        var dbHero = await _context.Super.FindAsync(req.Id);
        if (dbHero == null) return BadRequest("Hero not found");
        dbHero.Name = req.Name;
        dbHero.Place = req.Place;
        dbHero.FirstName = req.FirstName;
        dbHero.LastName = req.LastName;

        await _context.SaveChangesAsync();
        return Ok(await _context.Super.ToListAsync());
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<List<SuperHero>>> Delete(int id)
    {
        var hero = await _context.Super.FindAsync(id);
        if (hero == null) BadRequest("Hero not found.");
        if (hero != null)
        {
            _context.Super.Remove(hero);
        }

        await _context.SaveChangesAsync();
            return Ok(await _context.Super.ToListAsync());
    }
}