using Bilet.DAL;
using Bilet.Models;
using Bilet.ViewModels.Position;
using Microsoft.AspNetCore.Mvc;

namespace Bilet.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class PositionController : Controller
    {
        readonly AppDbContext _context;

        public PositionController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Positions.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreatePositionVM positionVM)
        {
            if (!ModelState.IsValid) return View();
            Position position = new Position
            {
                Name = positionVM.Name
            };
            _context.Positions.Add(position);
            _context.SaveChanges();
            return (RedirectToAction(nameof(Index)));
        }
        public IActionResult Delete(int? id)
        {
            if(id == 0) return NotFound();
            Position position = _context.Positions.Find(id);
            if (position == null) return NotFound();
            _context.Positions.Remove(position);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if (id is null || id == 0) return BadRequest();
            Position position = _context.Positions.Find(id);
            if (position == null) return NotFound();
            UpdatePositionVM positionVM = new UpdatePositionVM
            {
                Name = position.Name
            };
            return View(positionVM);
        }
        [HttpPost]
        public IActionResult Update(int? id,Position position)
        {
            Position exist=_context.Positions.Find(id);
            if (exist == null) return NotFound();
            exist.Name = position.Name;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
