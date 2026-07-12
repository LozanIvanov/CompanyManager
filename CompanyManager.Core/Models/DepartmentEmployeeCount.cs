using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManager.Core.Models
{
    public class DepartmentEmployeeCount
    {
        public string DepartmentName { get; set; } = string.Empty;

        public int EmployeeCount { get; set; }
    }
}
