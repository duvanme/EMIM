using EMIM.Data;
using EMIM.Models;
using EMIM.Services;
using EMIM.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class QuestionService : IQuestionService
{
    private readonly EmimContext _context;
    private readonly ILogger<QuestionService> _logger;

    public QuestionService(EmimContext context, ILogger<QuestionService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<QuestionViewModel>> GetQuestionsByProductIdAsync(int productId, QuestionStatus? status = null)
    {
        _logger.LogInformation("Obteniendo preguntas para Producto {ProductId}, Estado: {Status}", productId, status);

        var query = _context.Questions
            .Where(q => q.IdProducto == productId);

        // Si se proporciona un filtro de estado, lo aplicamos
        if (status.HasValue)
        {
            query = query.Where(q => q.Status == status.Value);
        }

        var questions = await query
            .Select(q => new QuestionViewModel
            {
                Id = q.Id,
                ProductId = q.IdProducto,
                ProductName = q.Producto.Name,
                UserId = q.UserId,
                UserName = q.User.UserName,
                QuestionText = q.QuestionText,
                Answer = q.Answer,
                Status = q.Status.ToString()
            }).ToListAsync();

        _logger.LogInformation("Preguntas encontradas: {Count}", questions.Count);

        return questions;
    }

    public async Task<int> GetProductIdByQuestionIdAsync(int questionId)
    {
        var question = await _context.Questions.FindAsync(questionId);
        if (question == null)
        {
            throw new KeyNotFoundException($"No se encontró una pregunta con ID {questionId}");
        }
        return question.IdProducto;
    }

    public async Task<List<QuestionViewModel>> GetAnsweredQuestionsByProductIdAsync(int productId)
    {
        var answeredQuestions = await _context.Questions
            .Where(q => q.IdProducto == productId && q.Status == QuestionStatus.Answered)
            .Include(q => q.Producto)
            .Include(q => q.User)
            .Select(q => new QuestionViewModel
            {
                Id = q.Id,
                ProductId = q.IdProducto,
                ProductName = q.Producto.Name,
                UserId = q.UserId,
                UserName = q.User.UserName,
                QuestionText = q.QuestionText,
                Answer = q.Answer,
                Status = q.Status.ToString()
            }).ToListAsync();

        return answeredQuestions;
    }

    // Implementación correcta en QuestionService.cs
    public async Task<List<QuestionViewModel>> GetQuestionsByStoreIdAsync(string storeId)
    {
        if (!int.TryParse(storeId, out int storeIdInt))
        {
            return new List<QuestionViewModel>();
        }

        Console.WriteLine($"Obteniendo preguntas para Tienda {storeIdInt}");

        var questions = await _context.Questions
            .Include(q => q.Producto)
            .Include(q => q.User)
            .Where(q => q.Producto.StoreId == storeIdInt)
            .Select(q => new QuestionViewModel
            {
                Id = q.Id,
                ProductId = q.IdProducto,
                ProductName = q.Producto.Name,
                UserId = q.UserId,
                UserName = q.User.UserName,
                QuestionText = q.QuestionText,
                Answer = q.Answer,
                Status = q.Status.ToString()
            }).ToListAsync();

        _logger.LogInformation("Preguntas encontradas: {Count}", questions.Count);

        return questions;
    }

    public async Task<bool> CanStoreAnswerQuestionAsync(int questionId, string storeId)
    {
        // Verificar si podemos convertir storeId a int
        if (!int.TryParse(storeId, out int storeIdInt))
        {
            // Si no se puede convertir, no tiene permiso
            return false;
        }

        var question = await _context.Questions
            .Include(q => q.Producto)
            .FirstOrDefaultAsync(q => q.Id == questionId);

        if (question == null)
            return false;

        // Verificar que el producto pertenezca a la tienda actual
        return question.Producto.StoreId == storeIdInt;
    }

    public async Task<bool> SaveQuestionAsync(Question question)
    {
        _context.Questions.Add(question);
        try
        {
            _logger.LogInformation("Guardando pregunta para producto {ProductId}", question.IdProducto);
            return await _context.SaveChangesAsync() > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al guardar pregunta para producto {ProductId}", question.IdProducto);
            return false;
        }
    }

    public async Task<bool> AnswerQuestionAsync(int id, string answer)
    {
        var question = await _context.Questions.FindAsync(id);
        if (question != null)
        {
            question.Answer = answer;
            question.Status = QuestionStatus.Answered;
            return await _context.SaveChangesAsync() > 0;
        }
        return false;
    }
}
