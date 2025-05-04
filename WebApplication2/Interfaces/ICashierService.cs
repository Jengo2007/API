using Microsoft.AspNetCore.Mvc;
using WebApplication2.DTO;

namespace WebApplication2.Interfaces;

public interface ICashierService
{
    public byte[] GenerateCasiersFile();
    public bool RegisterCashier(RegisterCashierDto registerCashierDto);

}