using BO.Models;
using DataAccessObject.DAOs;

namespace DataAccessObject
{
    internal class DepartmentDAO : GenericDAO<Department>
    {
        public DepartmentDAO(HospitalManagementDBContext context) : base(context)
        {
        }

        public override Department? GetById(object? id)
        {
            throw new NotImplementedException();
        }
    }
}