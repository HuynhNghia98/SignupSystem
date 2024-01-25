using SignupSystem.DataAccess.Data;
using SignupSystem.DataAccess.Repository.IRepository;
using SignupSystem.Models;

namespace SignupSystem.DataAccess.Repository
{
	public class PaymentRepository : Repository<Payment>, IPaymentRepository
	{
		public PaymentRepository(ApplicationDbContext db) : base(db)
		{
		}

		public void Update(Payment Payment)
		{
			_db.Update(Payment);
		}
	}
}
