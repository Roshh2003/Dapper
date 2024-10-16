using DappeeDemo.Data.Repository;
using DapperLib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DappeeDemo.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonRepository _personRepo;

        public PersonController(IPersonRepository personRepository)
        {
            _personRepo = personRepository;
        }

        public IActionResult Person()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Person person)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(person);
                }
                bool addPerson = await _personRepo.AddAsync(person);
                if (addPerson)
                {
                    TempData["msg"] = "Successfully Added";
                }
                else
                {
                    TempData["msg"] = "Could Not Added";
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Could Not Added";
            }
            return RedirectToAction(nameof(Add));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var person = await _personRepo.GetByIdAsync(id);
            if (person == null)
            {
                throw new Exception();
            }
            return View("Edit", person);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Person person)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(person);
                }
                var updateResult = await _personRepo.UpdateAsync(person);
                if (updateResult)
                {
                    TempData["msg"] = "Edit Successfully.";
                    return RedirectToAction(nameof(DisplayAllPerson));
                }
                else
                {
                    TempData["msg"] = "Could Not Edit.";
                    return View(person);
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Could Not Edit.";
                return View(person);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DisplayAllPerson()
        {
            try
            {
                var personAll = await _personRepo.GetAllPersonAsync();
                return View(personAll);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteResult = await _personRepo.DeleteAsync(id);
            return RedirectToAction(nameof(DisplayAllPerson));
        }


        //// GET: PersonController
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //// GET: PersonController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: PersonController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: PersonController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: PersonController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: PersonController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: PersonController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: PersonController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
