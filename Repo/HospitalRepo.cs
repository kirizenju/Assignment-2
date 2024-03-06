using BO.Models;
using DataAccessObject;
using DataAccessObject.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo
{
    public class HospitalRepo : IHospitalRepo
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public DoctorInformation AddNewDoctor(DoctorInformation doctorInformation)
        {
            doctorInformation.DoctorId = unitOfWork.DoctorDao.Get().Max(o => o.DoctorId) + 1;
            unitOfWork.DoctorDao.Create(doctorInformation);
            unitOfWork.Save();
            return unitOfWork.DoctorDao.GetById(doctorInformation.DoctorId);
        }

        public StaffAccount? CheckLogin(string memberId)
        {
            return unitOfWork.StaffAccountDAO.Get(filter: o => o.HraccountId == memberId).FirstOrDefault();
        }


        public void DeleteDoctor(object id)
        {
            unitOfWork.DoctorDao.DeleteById(id);
            unitOfWork.Save();
        }

       
        public DoctorInformation GetDoctorById(object id)
        {
            return unitOfWork.DoctorDao.GetById(id);
        }

        public List<Department> GetDepartments()
        {
            return unitOfWork.DepartmentDAO.Get();

        }

        public StaffAccount? Login(string memberId, string password)
        {
            return unitOfWork.StaffAccountDAO.Get(filter: o => o.Hremail.ToLower().Equals(memberId.ToLower()) && o.Hrpassword.Equals(password)).FirstOrDefault();

        }

        public DoctorInformation UpdateDoctor(DoctorInformation doctorInformation)
        {
            unitOfWork.DoctorDao.Update(doctorInformation);
            unitOfWork.Save();
            return unitOfWork.DoctorDao.GetById(doctorInformation.DoctorId);
        }
        public Pagination<DoctorInformation> GetAuthorPaginationSpecialEntity(int pageIndex, int pageSize, string keyId)
        {
            var entities = unitOfWork.DoctorDao.Get(includeProperties: "Department", orderBy: q => q.OrderBy(author => author.DoctorId != keyId));
            return unitOfWork.DoctorDao.ToPagination(entities, pageIndex, pageSize);
        }

        public Pagination<DoctorInformation> GetDoctorPagination(int pageIndex, int pageSize)
        {
            var entities = unitOfWork.DoctorDao.Get(includeProperties: nameof(DoctorInformation.Department));
            return unitOfWork.DoctorDao.ToPagination(entities, pageIndex, pageSize);
        }

        public Pagination<DoctorInformation> GetDoctorPaginationSearch(int pageIndex, int pageSize, string key, int type = 0)
        {
            // 1 = year
            // 2 = address
            switch (type)
            {

                case 1:
                    {
                        var entities = unitOfWork.DoctorDao.Get(filter: o => o.GraduationYear.ToString().Contains(key.ToLower()), includeProperties: "Department");
                        return unitOfWork.DoctorDao.ToPagination(entities, pageIndex, pageSize);

                    }
                case 2:
                    {
                        var entities = unitOfWork.DoctorDao.Get(filter: o => o.DoctorAddress.ToString().ToLower().Contains(key.ToLower()), includeProperties: "Department");
                        return unitOfWork.DoctorDao.ToPagination(entities, pageIndex, pageSize);

                    }
            }

            return new Pagination<DoctorInformation>();
        }

    }
}
