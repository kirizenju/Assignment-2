using BO.Models;
using DataAccessObject.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class StaffAccountDAO : GenericDAO<StaffAccount>
    {
        public StaffAccountDAO(HospitalManagementDBContext context) : base(context)
        {
        }

        public override StaffAccount? GetById(object? id)
        {
            if (id == null)
            {
                return null;
            }

            string stringId = id.ToString(); // Convert id to string
            return Get(filter: o => o.HraccountId == stringId, includeProperties: "SA").FirstOrDefault();
        }

    }
}
