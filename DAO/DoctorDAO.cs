using BO.Models;
using DataAccessObject.DAOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObject
{
    internal class DoctorDAO : GenericDAO<DoctorInformation>
    {
        public DoctorDAO(HospitalManagementDBContext context) : base(context)
        {
        }

        public override DoctorInformation? GetById(object? id)
        {
            if (id == null)
            {
                return null;
            }

            string stringId = id.ToString(); // Convert id to string
            return Get(filter: o => o.DoctorId == stringId, includeProperties: "Department").FirstOrDefault();

        }




    }
}