using WebApplication2.DTO;
using WebApplication2.Entities;
using Task = DocumentFormat.OpenXml.Office2021.DocumentTasks.Task;

namespace WebApplication2.Interfaces;

public interface ICashierRepository
{
    /// <summary>
    ///Создание нового кассира
    /// /// </summary>
    /// <param name="cashier"></param>
    /// <returns></returns>
    public Task<Cashier> AddCashier(Cashier cashier);
    /// <summary>
    /// Вывод всех кассиров
    /// </summary>
    /// <returns></returns>
    Task<List<CashierResponceDto>> GetAllCashiers();

    /// <summary>
    /// Обновление Кассира
    /// </summary>
    /// <param name="cashier"></param>
    /// <param name="id"></param>
    /// <returns></returns>
     Task<Cashier> UpdateCashierById(CashierDto cashier, Guid id);
    /// <summary>
    /// Удаление кассира
    /// </summary>
    /// <param name="cashier"></param>
    /// <returns></returns>
    Task< Cashier> DeleteCashierById(Guid id);
    /// <summary>
    /// /// /// Вывод кассира по ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task< CashierResponceDto?> GetCashierById(Guid id);
}