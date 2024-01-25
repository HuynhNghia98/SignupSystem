using SignupSystem.Models;

namespace SignupSystem.DataAccess.Repository.IRepository
{
	public interface IPaymentRepository : IRepository<Payment>
	{
		void Update(Payment Payment);
	}
}
