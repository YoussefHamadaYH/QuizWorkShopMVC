using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using QuizWorkShopMVC.ClientServices;
using QuizWorkShopMVC.DTO;

namespace QuizWorkShopMVC.Controllers
{
    public class QuizController : Controller
    {
        private readonly HttpClientService _httpClientService;

        public QuizController(HttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }
        public async Task<IActionResult> QuizList()
        {
            var quizzes = await _httpClientService.GetAllQuizzesAsync();
            return View(quizzes); 
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            QuizVM quizVM = new QuizVM();

            if (id.HasValue)
            {
                var quiz = await _httpClientService.GetQuizByIdAsync(id.Value);
                if (quiz != null)
                {
                    quizVM.QuizId = quiz.QuizId;
                    quizVM.QuizName = quiz.QuizName;
                    quizVM.QuizDescription = quiz.QuizDescription;
                    quizVM.Date = quiz.Date;
                    quizVM.ImageUrl = quiz.ImageUrl;
                }
            }

            return View(quizVM);
        }
        [HttpPost]
        public async Task<IActionResult> SaveEdit(QuizVM quizVM)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClientService.UpdateQuizAsync(quizVM);
                if (response)
                {
                    return RedirectToAction("QuizList");
                }
                else
                {
                    ModelState.AddModelError("", "Failed to update the quiz. Please try again.");
                }
            }
            return View("Edit", quizVM);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View("Add");
        }
        [HttpPost]
        public async Task<IActionResult> SaveAdd(QuizVM quizVM)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClientService.InsertQuizAsync(quizVM);
                if (response)
                {
                    return RedirectToAction("QuizList");
                }
                else
                {
                    ModelState.AddModelError("", "Failed to add the quiz. Please try again.");
                }
            }
            return View("Add", quizVM);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var quiz = await _httpClientService.GetQuizByIdAsync(id);
            if (quiz != null)
            {
                return View(quiz);
            }

            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var quiz = await _httpClientService.GetQuizByIdAsync(id);
            if (quiz == null)
            {
                return NotFound($"Quiz with ID {id} not found.");
            }

            return View(quiz);
        }

    }
}
