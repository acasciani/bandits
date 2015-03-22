using Bandits.Utils;
using BanditsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bandits.ObjectCreation
{
    public class PersonFactoryCreation : IFactoryCreation<Person>
    {
        public Person AddNewObject(Person entity)
        {
            // Set creation values
            entity.CreateUser = UserManagement.GetCurrentWebUser();
            entity.CreateDate = DateTime.Now;
            entity.IsDeleted = false;

            using (PeopleController c = new PeopleController())
            {
                return c.AddNew(entity);
            }
        }
    }
}