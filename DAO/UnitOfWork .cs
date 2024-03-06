using BO.Models;
using DAO;
using DataAccessObject.DAOs;



namespace DataAccessObject
{
    public class UnitOfWork
    {
        private HospitalManagementDBContext context = new HospitalManagementDBContext();
        private GenericDAO<StaffAccount> staffAccountDAO;
        private GenericDAO<Department> departmentDAO;
        private GenericDAO<DoctorInformation> doctorDAO;

        public GenericDAO<StaffAccount> StaffAccountDAO => staffAccountDAO ??= new StaffAccountDAO(context);
        public GenericDAO<Department> DepartmentDAO => departmentDAO ??= new DepartmentDAO(context);
        public GenericDAO<DoctorInformation> DoctorDao => doctorDAO ??= new DoctorDAO(context);
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
