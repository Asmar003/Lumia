using Bilet.DAL;
using Bilet.Models;
using Bilet.ViewModels.Employee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Bilet.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class EmployeeController : Controller
    {
        readonly AppDbContext _context;
        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Employees.ToList());
        }
        public IActionResult Create()
        {
            ViewBag.Position = new SelectList(_context.Positions, nameof(Position.Id), nameof(Position.Name));
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateEmployeeVM employeeVM)
        {
            if (!ModelState.IsValid) return View();
            IFormFile file = employeeVM.Image;
            if (!file.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Image", "sekıl yukleyın zehmet olmasa");
                return View();
            }
            if (file.Length > 200 * 1024)
            {
                ModelState.AddModelError("Image", "sekılın olcusu 200 kbdan artıq olmaz");
                return View();
            }
            string fileName = Guid.NewGuid() + file.FileName;
            using (var stream = new FileStream("C:\\Users\\ca.r215.16\\source\\repos\\Bilet\\Bilet\\wwwroot\\assets\\img\\" + fileName, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            Employee employee = new Employee
            {
                Name = employeeVM.Name,
                ImageUrl = fileName,
                FacebookUrl = employeeVM.FacebookUrl,
                InstagramUrl = employeeVM.InstagramUrl,
                TwitterUrl = employeeVM.TwitterUrl,
                LinkedinUrl=employeeVM.LinkedinUrl,
                PositionId = employeeVM.PositionId,
                Description= employeeVM.Description
            };
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if (id is null || id==0 ) return NotFound();
            Employee employee=_context.Employees.Find(id);
            if (employee == null) return BadRequest();
            UpdateEmployeeVM employeeVM = new UpdateEmployeeVM
            {
                Name = employee.Name,
                FacebookUrl= employee.FacebookUrl,
                TwitterUrl= employee.TwitterUrl,
                InstagramUrl= employee.InstagramUrl,
                LinkedinUrl= employee.LinkedinUrl,
                Image =employee.ImageUrl,
                Description= employee.Description,
                PositionId= employee.PositionId,
            };
            return View(employeeVM);
        }
        [HttpPost]
        public IActionResult Update(int? id,Employee employee)
        {
            Employee exist = _context.Employees.Find(id);
            exist.Name=employee.Name;
            exist.FacebookUrl=employee.FacebookUrl;
            exist.TwitterUrl=employee.TwitterUrl;
            exist.Description=employee.Description;
            exist.InstagramUrl=employee.InstagramUrl;
            exist.LinkedinUrl=employee.LinkedinUrl;
            exist.ImageUrl=employee.ImageUrl;
            exist.PositionId=employee.PositionId;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            if(id is null || id==0 ) return NotFound();
            Employee employee= _context.Employees.Find(id);
            if (employee == null) return BadRequest();
            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
