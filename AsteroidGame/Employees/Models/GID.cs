namespace Employees.Models
{
    public static class GID  // Независимая генерация Id
    {
        private static int _EmpId = 0;
        private static int _DepId = 0;

        public static int GetEmpId()
        {
            _EmpId++;
            return _EmpId;
        }

        public static int GetDepId()
        {
            _DepId++;
            return _DepId;
        }
    }
}
