using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCSecondProject.Data;
using MVCSecondProject.Models;
using MVCSecondProject.Models.Domain;

namespace MVCSecondProject.Controllers
{
    public class ContactController : Controller
    {
        private readonly MyDbContext db;

        public ContactController(MyDbContext db) 
        {
            this.db = db;
        }
        [HttpGet] 
        public IActionResult Add()
        {
            return View();
        }
        public async Task<IActionResult> Add(AddContactViewModel conReq)
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Name = conReq.Name,
                Email = conReq.Email,
                primaryPhone = conReq.primaryPhone,
                secondaryPhone = conReq.secondaryPhone,
                DateOfBirth = conReq.DateOfBirth,
            };
            await db.Contacts.AddAsync(contact);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
          var contact = await db.Contacts.ToListAsync();
            return View(contact);
        }
        [HttpGet]
        public async Task<IActionResult> View(Guid Id)
        {
            var contact = await db.Contacts.FirstOrDefaultAsync(x => x.Id == Id);
            if (contact != null)
            {
                var viewmodel = new UpdateContactViewModel()
                {
                    Id = contact.Id,
                    Name = contact.Name,
                    Email = contact.Email,
                    primaryPhone = contact.primaryPhone,
                    secondaryPhone = contact.secondaryPhone,
                    DateOfBirth = contact.DateOfBirth,
                };
                return await Task.Run(()=> View("View",viewmodel));
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateContactViewModel model)
        {
            var contact = await db.Contacts.FindAsync(model.Id);
            if (contact != null)
            {
                contact.Name = model.Name;
                contact.Email = model.Email;
                contact.primaryPhone = model.primaryPhone;
                contact.secondaryPhone = model.secondaryPhone;
                contact.DateOfBirth = model.DateOfBirth;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateContactViewModel model)
        {
            var contact = await db.Contacts.FindAsync(model.Id);
            if (contact != null)
            {
                db.Contacts.Remove(contact);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
