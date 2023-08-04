using Microsoft.EntityFrameworkCore;

namespace TestNinja.Mocking;

public class EmployeeController
{
    private IEmployeeStorage _storage;

    public EmployeeController(IEmployeeStorage storage)
    {
        this._storage = storage;
    }

    public ActionResult DeleteEmployee(int id)
    {
        this._storage.DeleteEmployee(id);
        return RedirectToAction("Employees");
    }

    private ActionResult RedirectToAction(string employees)
    {
        return new RedirectResult();
    }
}

public class ActionResult { }

public class RedirectResult : ActionResult { }

public class EmployeeContext
{
    public DbSet<Employee> Employees { get; set; }

    public void SaveChanges()
    {
    }
}

public class Employee
{
}