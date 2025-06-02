using DocumentFormat.OpenXml.Spreadsheet;
using WebApplication2.Entities;

namespace WebApplication2.ViewModels;

        public class AdminViewModel
        {
                public List<Cashier> Cashiers { get; set; }
                public List<User> Users { get; set; }
            
        }