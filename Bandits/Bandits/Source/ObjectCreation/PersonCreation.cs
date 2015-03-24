using Bandits.Utils;
using BanditsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanditsModel
{    
    public static class PersonCreation
    {
        public static Person Create()
        {
            return new Person() { CreateDate = DateTime.Now, IsDeleted = false, CreateUser = UserManagement.GetCurrentWebUser() };
        }

        public static Person WithName(this Person refr, string first, char? middleI, string last)
        {
            refr.FName = first;
            refr.MInitial = middleI;
            refr.LName = last;
            return refr;
        }

        public static Person WithName(this Person refr, string first, string middleI, string last)
        {
            return refr.WithName(first, (middleI.Trim().Length > 0 ? middleI.Trim().ToCharArray()[0] : (char?)null), last);
        }

        public static Person WithDOB(this Person refr, DateTime? dob)
        {
            refr.DOB = dob;
            return refr;
        }

        public static Person WithGender(this Person refr, Gender gender) {
            refr.Gender = gender == Gender.Male ? 'M' : gender == Gender.Female ? 'F' : (char?)null;
            return refr;
        }

        public static Gender ToGender(this char refr)
        {
            switch (refr)
            {
                case 'M': return Gender.Male;
                case 'F': return Gender.Female;
                default: return Gender.Undefined;
            }
        }
    }
}