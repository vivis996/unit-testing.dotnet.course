namespace TestNinja.Mocking;

public class EmployeeStorage : IEmployeeStorage
{
    private EmployeeContext _db;

    public EmployeeStorage()
    {
        this._db = new EmployeeContext();
    }

    public void DeleteEmployee(int id)
    {
        var employee = this._db.Employees.Find(id);
        if (employee == null)
            return;

        this._db.Employees.Remove(employee);
        this._db.SaveChanges();
    }
}

public interface IEmployeeStorage
{
    void DeleteEmployee(int id);
}
