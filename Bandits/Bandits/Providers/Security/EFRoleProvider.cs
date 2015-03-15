using BanditsModel;
using Bandits;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Hosting;
using System.Web.Security;

namespace Bandits.Providers.Security
{
    public class EFRoleProvider : RoleProvider
    {
        public override string ApplicationName { get { return "Bandits"; } set { } }

        #region public methods
        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null) { throw new ArgumentNullException("config"); }
            if (string.IsNullOrEmpty(name)) { name = "EFRoleProvider"; }

            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Law Application EF Role Provider");
            }

            // Initialize the abstract base class.
            base.Initialize(name, config);
        }

        public override void AddUsersToRoles(string[] emails, string[] roleNames)
        {
            // get all roles as objects
            IEnumerable<UserRole> roles = GetAllUserRoles().Where(r => roleNames.Contains(r.RoleName));

            if (roles == null || roles.Count() == 0) return;

            // get all users as objects
            using (WebUsersController swuc = new WebUsersController())
            {
                IEnumerable<WebUser> users = swuc.GetWhere(u => emails.Contains(u.Email));

                foreach (UserRole role in roles)
                {
                    foreach (WebUser user in users)
                    {
                        if (!user.UserRoles.Contains(role))
                        {
                            user.UserRoles.Add(role);
                            swuc.Update(user);
                        }
                    }
                }
            }
        }

        public override void CreateRole(string roleName)
        {
            using (UserRolesController urc = new UserRolesController())
            {
                UserRole role = new UserRole()
                {
                    RoleName = roleName
                };

                urc.AddNew(role);
            }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string emailToMatch)
        {
            IEnumerable<WebUser> users;
            using (WebUsersController swuc = new WebUsersController())
            {
                users = swuc.GetWhere(u => u.Email == emailToMatch && u.UserRoles.Where(r => r.RoleName == roleName).Count() > 0);
            }

            if (users == null) return new string[0];

            return users.Select(u => u.Email).ToArray();
        }

        public override string[] GetAllRoles()
        {
            IEnumerable<UserRole> roles = GetAllUserRoles();
            if (roles == null) return new string[0];

            return roles.Select(r => r.RoleName).ToArray();
        }

        public override string[] GetRolesForUser(string email)
        {
            WebUser user = GetUser(u => u.Email == email);
            return user.UserRoles.Select(result => result.RoleName).ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            UserRole role = GetRole(r => r.RoleName == roleName);
            if (role == null) return new string[0];

            using (WebUsersController wuc = new WebUsersController())
            {
                return wuc.GetWhere(u => u.UserRoles.Contains(role)).Select(r => r.Email).ToArray();
            }
        }

        public override bool IsUserInRole(string email, string roleName)
        {
            WebUser user = GetUser(u => u.Email == email);

            if (user == null)
            {
                // couldn't find user
                return false;
            }

            return user.UserRoles.Where(r => r.RoleName == roleName).Count() == 1;
        }

        public override void RemoveUsersFromRoles(string[] emails, string[] roleNames)
        {
            IEnumerable<UserRole> roles = GetAllUserRoles().Where(r => roleNames.Contains(r.RoleName));

            if (roles == null) return;

            using (WebUsersController swuc = new WebUsersController())
            {
                IEnumerable<WebUser> users = swuc.GetWhere(u => emails.Contains(u.Email));

                if (users == null) return;

                foreach (WebUser user in users)
                {
                    foreach (UserRole role in roles)
                    {
                        if (user.UserRoles.Contains(role))
                        {
                            user.UserRoles.Remove(role);
                            swuc.Update(user);
                        }
                    }
                }
            }
        }

        public override bool RoleExists(string roleName)
        {
            return GetRole(r => r.RoleName == roleName) != null;
        }
        #endregion


        #region private methods
        private WebUser GetUser(Expression<Func<WebUser, bool>> predicate)
        {
            using (WebUsersController swuc = new WebUsersController())
            {
                WebUser user;
                try
                {
                    user = swuc.GetWhere(predicate).FirstOrDefault();
                }
                catch (ProviderException)
                {
                    user = null;
                }

                return user;
            }
        }

        private UserRole GetRole(Func<UserRole, bool> predicate)
        {
            UserRole role = GetAllUserRoles().Where(predicate).FirstOrDefault();
            return role;
        }

        private IEnumerable<UserRole> GetAllUserRoles()
        {
            IEnumerable<UserRole> roles;
            using (UserRolesController urc = new UserRolesController())
            {
                return urc.Get();
            }
        }
        #endregion
    }
}