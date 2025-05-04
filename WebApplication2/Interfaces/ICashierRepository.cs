using WebApplication2.DTO;
using WebApplication2.Entities;

namespace WebApplication2.Interfaces;

public interface ICashierRepository
{
    /// <summary>
    ///Создание нового кассира
    /// /// </summary>
    /// <param name="cashier"></param>
    /// <returns></returns>
    public Cashiers AddCashier(Cashiers cashier);
    /// <summary>
    /// Вывод всех кассиров
    /// </summary>
    /// <returns></returns>
    public List<Cashiers> GetAllCashiers();

    /// <summary>
    /// Обновление Кассира
    /// </summary>
    /// <param name="cashier"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public Cashiers UpdateCashierById(CashierDto cashier, Guid id);
    /// <summary>
    /// Удаление кассира
    /// </summary>
    /// <param name="cashier"></param>
    /// <returns></returns>
    public Cashiers DeleteCashierById(Guid id);
    /// <summary>
    /// /// /// Вывод кассира по ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Cashiers? GetCashierById(Guid id);
}