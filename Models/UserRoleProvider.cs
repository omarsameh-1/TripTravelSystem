using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace TripTravelSystem.Models
{
   
        public class UserRoleProvider : RoleProvider
        {
            public override string ApplicationName
            {
                get
                {
                    throw new NotImplementedException();
                }

                set
                {
                    throw new NotImplementedException();
                }
            }

            public override void AddUsersToRoles(string[] usernames, string[] roleNames)
            {
                throw new NotImplementedException();
            }

            public override void CreateRole(string roleName)
            {
                throw new NotImplementedException();
            }

            public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
            {
                throw new NotImplementedException();
            }

            public override string[] FindUsersInRole(string roleName, string usernameToMatch)
            {
                throw new NotImplementedException();
            }

            public override string[] GetAllRoles()
            {
                throw new NotImplementedException();
            }

            public override string[] GetRolesForUser(string email)
            {
                using (TripsTravel_DBEntities db = new TripsTravel_DBEntities())
                {
                    var userRoles = (from User in db.Users
                                     join RoleType in db.RoleTypes
                                     on User.roleTypeID equals RoleType.roleID
                                     where User.email==email
                                     select RoleType.roleName).ToArray();
                    return userRoles;
                }
            }

            public override string[] GetUsersInRole(string roleName)
            {
                throw new NotImplementedException();
            }

            public override bool IsUserInRole(string username, string roleName)
            {
                throw new NotImplementedException();
            }

            public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
            {
                throw new NotImplementedException();
            }

            public override bool RoleExists(string roleName)
            {
                throw new NotImplementedException();
            }
        }
    }

