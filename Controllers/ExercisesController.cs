using TheGymAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class ExercisesController : ControllerBase
{
    private readonly IExerciseService _exerciseService;

    public ExercisesController(IExerciseService exerciseService)
    {
        _exerciseService = exerciseService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Exercise>>> GetAll() => await _exerciseService.GetAllAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Exercise>> Get(int id)
    {
        Exercise? exercise = await _exerciseService.Get(id);

        if (exercise is null) return NotFound();

        return exercise;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Exercise exercise)
    {
        Exercise newExercise = await _exerciseService.Add(exercise);
        Console.WriteLine(newExercise);
        return CreatedAtAction(nameof(Get), new { id = exercise.Id }, exercise);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Exercise exercise)
    {
        Console.WriteLine("Controller working");
        if (exercise.Id != id)
        {
            return BadRequest();
        }

        try
        {
            Exercise? updatedExercise = await _exerciseService.Update(id, exercise);
            if (updatedExercise is null)
            {
                return BadRequest();
            }
            return NoContent();
        }
        catch (DbUpdateConcurrencyException)
        {
            return Conflict(new { message = "The update could not be completed due to a conflict. Please try again." });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> delete(int id)
    {
        try
        {
            Exercise? deletedExercise = await _exerciseService.Delete(id);
            if (deletedExercise is null)
            {
                return BadRequest();
            }
            return NoContent();
        }
        catch (DbUpdateConcurrencyException)
        {
            return Conflict(new { message = "The update could not be completed due to a conflict. Please try again." });
        }
    }
}