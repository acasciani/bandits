using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanditsModel;
using Bandits.Utils;

namespace Bandits
{
    public partial class PeopleController : OpenAccessBaseApiController<BanditsModel.Person, BanditsModel.BanditsModel>
    {


        #region validation checks 
        public bool IsValid(Person person)
        {
            bool isNameValid = IsNameValid(person.FName, person.MInitial.ToString(), person.LName);
            bool isDOBValid = IsDOBValid(person.DOB);
            bool isGenderValid = IsGenderValid(person.Gender);

            return isNameValid && isDOBValid && isGenderValid;
        }

        public bool IsNameValid(string fName, string mInitial, string lName)
        {
            return (fName.Length > 0 && fName.Length <= 75) && (mInitial.Length <= 1) && (lName.Length > 0 && lName.Length <= 75);
        }

        public bool IsDOBValid(DateTime? dob)
        {
            return true;
        }

        public bool IsDOBValid(string dob)
        {
            DateTime tryParse;
            return DateTime.TryParse(dob, out tryParse);
        }

        public bool IsGenderValid(char? gender)
        {
            return gender.GetGender() != Gender.Undefined;
        }

        public bool IsGenderValid(char gender)
        {
            return gender.GetGender() != Gender.Undefined;
        }

        public bool IsGenderValid(string gender)
        {
            return gender.GetGender() != Gender.Undefined;
        }
        #endregion
    }
}