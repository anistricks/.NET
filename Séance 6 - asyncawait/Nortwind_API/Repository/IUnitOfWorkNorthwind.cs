using Nortwind_API.Entities;

namespace Nortwind_API.Repository
{
     interface IUnitOfWorkNorthwind
    {
        IRepository<Employee> EmployeesRepository { get; }

        IRepository<Order> OrdersRepository { get; }

    }
}
