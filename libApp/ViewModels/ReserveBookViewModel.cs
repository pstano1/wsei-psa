using libApp.Models;

namespace libApp.ViewModels
{
    public class ReserveBookViewModel
    {
        public Book Book { get; set; }
        public List<Customer> Customers { get; set; }
        public BookCustomer BC { get; set; }
    }
}

