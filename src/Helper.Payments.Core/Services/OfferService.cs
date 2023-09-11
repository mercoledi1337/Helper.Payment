using Helper.Payments.Core.Data;
using Helper.Payments.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Helper.Payments.Core.Services
{
    internal class OfferService : IOfferService
    {
        private readonly PaymentDbContext _paymentDbContext;

        public OfferService(PaymentDbContext paymentDbContext)
        {
            _paymentDbContext = paymentDbContext;
        }
        //public async Task<List<Offer>> GetOfferById(Guid offerId) => await _paymentDbContext.Offers.ToListAsync<Offer>();

    }
}
