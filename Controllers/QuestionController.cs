using EMIM.Services;
using EMIM.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using EMIM.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class QuestionController : Controller
{
    private readonly IQuestionService _questionService;
    private readonly IStoreService _storeService;
    private readonly IProductService _productService;

    public QuestionController(
        IQuestionService questionService,
        IStoreService storeService,
        IProductService productService)
    {
        _questionService = questionService;
        _storeService = storeService;
        _productService = productService;
    }

    [HttpPost]
    public async Task<IActionResult> AskQuestion(int ProductId, string UserId, string QuestionText)
    {
        if (string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(QuestionText))
        {
            // Manejar el caso de datos inválidos
            return RedirectToAction("productDisplay", "Product", new { id = ProductId });
        }

        // Crear directamente el objeto Question
        var question = new Question
        {
            IdProducto = ProductId,
            UserId = UserId,
            QuestionText = QuestionText,
            Status = QuestionStatus.Pending
        };

        // Agregar a la BD directamente o usar un método simple del servicio
        await _questionService.SaveQuestionAsync(question);

        // Si el usuario es vendedor, redirigir a la página de preguntas
        if (User.IsInRole("Vendor"))
        {
            return RedirectToAction("QuestionsAnswers", "Question", new { productId = ProductId });
        }
        
        // Si es un usuario normal, redirigir de vuelta a la página del producto
        return RedirectToAction("productDisplay", "Product", new { id = ProductId });
    }

    // La acción de ver preguntas filtra por tienda si es vendedor
    public async Task<IActionResult> QuestionsAnswers(int? productId)
    {
        // Si el usuario es vendedor, solo mostrar preguntas de su tienda
        if (User.IsInRole("Vendor"))
        {
            // Obtener el StoreId como entero
            int storeId = await _storeService.GetStoreIdForVendorAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (storeId == 0)
            {
                // El vendedor no tiene una tienda asociada
                return RedirectToAction("Index", "Home");
            }

            // Si se especifica un productId, filtramos por ese producto
            if (productId.HasValue)
            {
                // Verificar que el producto pertenezca a la tienda
                bool isStoreProduct = await _productService.IsProductOwnedByStoreAsync(productId.Value, storeId);

                if (!isStoreProduct)
                {
                    // Producto no pertenece a la tienda, redirigir o mostrar error
                    return RedirectToAction("Index", "Home");
                }

                var questions = await _questionService.GetQuestionsByProductIdAsync(productId.Value);
                return View(questions);
            }

            // Si no se especifica productId, mostrar todas las preguntas de la tienda
            var storeQuestions = await _questionService.GetQuestionsByStoreIdAsync(storeId.ToString());
            return View(storeQuestions);
        }

        // Si no es vendedor y hay productId, redirigir a la vista del producto
        if (productId.HasValue)
        {
            return RedirectToAction("productDisplay", "Product", new { id = productId.Value });
        }

        // Si no es vendedor y no hay productId, redirigir al inicio
        return RedirectToAction("Index", "Home");
    }
    
    [Authorize(Roles = "Vendor")]
    [HttpPost]
    public async Task<IActionResult> AnswerQuestion(int id, string answer)
    {
        if (string.IsNullOrEmpty(answer))
        {
            return RedirectToAction("QuestionsAnswers");
        }

        // Obtener el StoreId como entero
        int storeId = await _storeService.GetStoreIdForVendorAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

        if (storeId == 0)
        {
            // El vendedor no tiene una tienda asociada
            return RedirectToAction("Index", "Home");
        }

        // Verificar que la pregunta pertenezca a un producto de la tienda
        bool canAnswer = await _questionService.CanStoreAnswerQuestionAsync(id, storeId.ToString());

        if (!canAnswer)
        {
            // No tiene permiso para responder esta pregunta
            return RedirectToAction("Index", "Home");
        }

        await _questionService.AnswerQuestionAsync(id, answer);

        try
        {
            // Obtener el ID del producto para redireccionar
            var productId = await _questionService.GetProductIdByQuestionIdAsync(id);
            return RedirectToAction("QuestionsAnswers", new { productId });
        }
        catch (KeyNotFoundException)
        {
            // Si no se encuentra la pregunta, redirigir a la página principal de preguntas
            return RedirectToAction("QuestionsAnswers");
        }
    }
}