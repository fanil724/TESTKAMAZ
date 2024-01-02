
List<Employees> emp = ReadEmployees("test.txt");
if (emp.Count > 0) PrintEmployeeTree(1, 0, emp);


void PrintEmployeeTree(int id, int count, List<Employees> employ)
{
    if (employ.Count == 0) return;
    for (int i = 0; i < count; i++) Console.Write("---");
    Console.WriteLine(employ.First(x => x.id == id).Surname);
    foreach (Employees emp in employ)
    {
        if (emp.idhead == id)
        {
            PrintEmployeeTree(emp.id, count + 1, employ);
        }
    }
}


List<Employees> ReadEmployees(string path)
{
    List<Employees> employees = new List<Employees>();
    try
    {
        using (StreamReader sr = new StreamReader(path))
        {
            while (!sr.EndOfStream)
            {
                string? line = sr.ReadLine();
                if (line == null) continue;
                var str = line.Split("|");
                employees.Add(new Employees(Convert.ToInt32(str[0]), Convert.ToInt32(str[1]), str[2]));
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ошибка при выполнение  {ex.Message}");
        return employees;
    }
    return employees;
}

record Employees(int id, int idhead, string Surname);