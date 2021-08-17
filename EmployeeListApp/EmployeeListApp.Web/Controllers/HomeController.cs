using AspNetCoreHero.ToastNotification.Abstractions;
using EmployeeListApp.DAL.Interfaces;
using EmployeeListApp.WEB.Attributes;
using EmployeeListApp.WEB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace EmployeeListApp.WEB.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(UserActionAttribute))]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IToastifyService _notifyService;
        private readonly IUnitOfWork _uow;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork uow, IToastifyService notifyService)
        {
            _notifyService = notifyService;
            _logger = logger;
            _uow = uow;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 3, string searchString = "")
        {
            var empl = new List<DAL.Entities.Employee>();
            if (string.IsNullOrEmpty(searchString))
            {
                empl = await _uow.EmployeeRepository.GetAllAsync();
            }
            else
            {
                ViewBag.Search = searchString;
                empl = await _uow.EmployeeRepository.SearchAsync(searchString);
            }

            var result = await empl.Select(p => new EmployeeItemViewModel()
            {
                Id = p.Id,
                Name = p.Name,
                SecondName = p.SecondName,
                Gender = (Gender)p.Gender,
                Age = p.Age,
                DepartmentFloor = p.Department.Floor,
                DepartmentName = p.Department.Name,
                Experience = p.WorkExperiences
                    .Select(x => x.Language.Name).ToList(),
            }).ToPagedListAsync(pageNumber, pageSize);
            return View(result);
        }


        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var viewModel =  new EmployeeViewModel();
            await SetDepartmentsAndLanguages(viewModel);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(EmployeeViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var experience = new List<DAL.Entities.Experience>();
                    viewModel.LanguagesId
                        .ForEach(p => experience.Add(new DAL.Entities.Experience() { LanguageId = p }));

                    var employee = new DAL.Entities.Employee()
                    {
                        Name = viewModel.Name,
                        SecondName = viewModel.SecondName,
                        Age = viewModel.Age,
                        DepartmentId = viewModel.DepartmentId,
                        Gender = (int)viewModel.Gender,
                        WorkExperiences = experience
                    };

                    await _uow.EmployeeRepository.AddAsync(employee);
                    _notifyService.Success("Успешно сохранено!");

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _notifyService.Error("Ошибка сохранения");
            }
            finally
            {
                await SetDepartmentsAndLanguages(viewModel);
            }
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var employee = await _uow.EmployeeRepository.FindAsync(id);
            if (employee == null)
                return NotFound();

            var viewModel = new EmployeeViewModel()
            {
                Name = employee.Name,
                SecondName = employee.SecondName,
                Age = employee.Age,
                DepartmentId = employee.DepartmentId,
                Gender = (Gender)employee.Gender,
                LanguagesId = employee.WorkExperiences.Select(x => x.LanguageId).ToList()
            };

            await SetDepartmentsAndLanguages(viewModel);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var employee = await _uow.EmployeeRepository.FindAsync(viewModel.Id);
                    if (employee == null)
                        return NotFound();

                    employee.Name = viewModel.Name;
                    employee.SecondName = viewModel.SecondName;
                    employee.Age = viewModel.Age;
                    employee.Gender = (int)viewModel.Gender;
                    employee.DepartmentId = viewModel.DepartmentId;
                    employee.WorkExperiences = viewModel.LanguagesId.Select(langId => new DAL.Entities.Experience()
                    {
                        LanguageId = langId,
                        EmployeeId = employee.Id
                    }).ToList();

                    await _uow.EmployeeRepository.UpdateAsync(employee);
                    _notifyService.Success("Успешно сохранено!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _notifyService.Error("Ошибка сохранения");
            }
            finally
            {
                await SetDepartmentsAndLanguages(viewModel);
            }
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _uow.EmployeeRepository.RemoveAsync(id);
                _notifyService.Success("Успешно удалено!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _notifyService.Error("Ошибка удаления");
            }

            return RedirectToAction("Index");
        }


        private async Task SetDepartmentsAndLanguages(EmployeeViewModel viewModel)
        {
            var departments = await _uow.DepartmentRepository.GetAllAsync();
            var languages = await _uow.LanguageRepository.GetAllAsync();

            viewModel.Departments = departments.Select(p => new ItemView(p.Id, p.Name)).ToList();
            viewModel.Languages = languages.Select(p => new ItemView(p.Id, p.Name)).ToList();
        }

        ///TODO
        public IActionResult NameAutoComplete()
        {
            var value = HttpContext.Request.Query["term"].ToString();
            var namesList = new List<string>() { "Иван", "Степан", "Влад" };
            var names = namesList.Where(p => p.ToUpper().StartsWith(value.ToUpper()));
            return Ok(names);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        protected override void Dispose(bool disposing)
        {
            _uow.Dispose();
            base.Dispose(disposing);
        }
    }
}
