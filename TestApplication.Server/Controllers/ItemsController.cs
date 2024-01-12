using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ItemsController : ControllerBase
{
    private static List<Item> items = new List<Item>
    {
        new Item { Id = 1, Name = "Item 1" },
        new Item { Id = 2, Name = "Item 2" }
    };

    [HttpGet]
    public ActionResult<IEnumerable<Item>> Get()
    {
        return Ok(items);
    }

    [HttpGet("{id}")]
    public ActionResult<Item> GetById(int id)
    {
        var item = items.Find(i => i.Id == id);
        if (item == null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    [HttpPost]
    public ActionResult<Item> Post([FromBody] Item newItem)
    {
        items.Add(newItem);
        return CreatedAtAction(nameof(GetById), new { id = newItem.Id }, newItem);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Item updatedItem)
    {
        var existingItem = items.Find(i => i.Id == id);
        if (existingItem == null)
        {
            return NotFound();
        }

        existingItem.Name = updatedItem.Name;
        return NoContent();
    }
}
