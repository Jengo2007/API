using Microsoft.AspNetCore.Mvc;
using WebApplication2.DTO;

namespace WebApplication2.Interfaces;

public interface ICashierService
{
    Task< byte[]> GenerateCasiersFile();
    Task<bool> RegisterCashier(RegisterCashierDto registerCashierDto);

}