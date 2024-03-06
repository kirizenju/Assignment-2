using BO.Models;
using DataAccessObject.DAOs;

namespace Repo
{
    public interface IHospitalRepo
    {
        public List<Department> GetDepartments();
        public DoctorInformation AddNewDoctor(DoctorInformation doctorInformation);
        public StaffAccount? CheckLogin(string memberId);
        public void DeleteDoctor(object id);
        public DoctorInformation GetDoctorById(object id);
        public StaffAccount? Login(string memberId, string password);
        public DoctorInformation UpdateDoctor(DoctorInformation doctorInformation);
        public Pagination<DoctorInformation> GetDoctorPaginationSearch(int pageIndex, int pageSize, string key, int type = 0);
        public Pagination<DoctorInformation> GetDoctorPagination(int pageIndex, int pageSize);
        public Pagination<DoctorInformation> GetAuthorPaginationSpecialEntity(int pageIndex, int pageSize, string keyId);
    }
}