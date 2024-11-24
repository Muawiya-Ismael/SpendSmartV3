using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpendSmartV3.Data.Core.Interfaces;
using SpendSmartV3.Objects.Models.Expense;
using SpendSmartV3.Services;

namespace SpendSmartV3.Controllers
{
    public class ExpenseController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountServices _accountServices;
        private readonly IExpenseServices _expenseServices;

        public List<Expense> expensesList = new List<Expense>();

        public ExpenseController(IUnitOfWork unitOfWork, IAccountServices accountServices, IExpenseServices expenseServices)
        {
            _unitOfWork = unitOfWork;
            _accountServices = accountServices;
            _expenseServices = expenseServices;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index(string? searchTerm)
        {
            var user = await _accountServices.getUser();
            if (user == null)
            {
                return Unauthorized();
            }
            var allExpenses = await _expenseServices.GetAllExpenses(user.Id);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                allExpenses = allExpenses
                    .Where(e => (e.Discerption != null && e.Discerption.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
                                 e.Value.ToString().Contains(searchTerm))
                    .ToList();
            }

            var totalExpenses = allExpenses.Sum(e => e.Value);
            ViewBag.Expenses = totalExpenses;

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_ExpensesTable", allExpenses);
            }

            return View(allExpenses);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> CreateEditExpense(int id)
        {
            if (id == null)
            {
                return View();
            }

            var user = await _accountServices.getUser();
            if (user == null)
            {
                return Unauthorized();
            }
            else
            {
                if (id == null)
                {
                    return View();
                }
                else
                {
                    var expenseInDb = await _expenseServices.GetExpense(user.Id, id);

                    return View(expenseInDb ?? new Expense());
                }

            }

        }

        [Authorize]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var user = await _accountServices.getUser();
            if (user != null)
            {
                var expenseInDb = await _expenseServices.GetExpense(user.Id, id);

                if (expenseInDb == null)
                {
                    return NotFound();
                }
                await _expenseServices.DeleteExpense(id);
                await _expenseServices.Commit();

                return RedirectToAction("Index");
            }

            return Unauthorized();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEditExpenseForm(Expense model)
        {
            var user = await _accountServices.getUser();

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var existingExpense = await _expenseServices.GetExpense(user.Id, model.Id);

            if (existingExpense == null)
            {
                model.UserId = user.Id;
                await _expenseServices.AddExpense(model);
                await _expenseServices.Commit();
            }
            else
            {
                await _expenseServices.UpdateExpense(model);
                await _expenseServices.Commit();
            }

            return RedirectToAction("Index");
        }

    }
}
