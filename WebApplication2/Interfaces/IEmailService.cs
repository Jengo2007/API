using WebApplication2.DTO;
using WebApplication2.Migrations;

namespace WebApplication2.Interfaces;

public interface IEmailService
{
    Task SendEmail(EmailDto emailDto);
}