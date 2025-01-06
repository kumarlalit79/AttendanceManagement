using AttendanceManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace AttendanceManagement
{
    public class WebRoleProvider : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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

        public override string[] GetRolesForUser(string username)
        {
            using (var db = new AttendanceTrackrEntities())
            {
                // First, find out if the user is a student or a teacher
                var student = db.student_tbl.FirstOrDefault(s => s.student_id.ToString() == username);
                var teacher = db.Teacher_tbl.FirstOrDefault(t => t.Faculty_Id.ToString() == username);
                var admin = db.Admins.FirstOrDefault(a => a.Admin_ID.ToString() == username);

                if (student != null)
                {
                    // If the user is a student, get the associated role from UserRole table
                    var roles = (from ur in db.UserRoles
                                 join r in db.Roles on ur.Role_Id equals r.Role_Id
                                 where ur.Student_Id == student.student_id
                                 select r.Role_Name).ToArray();
                    return roles;
                }
                else if (teacher != null)
                {
                    // If the user is a teacher, get the associated role from UserRole table
                    var roles = (from ur in db.UserRoles
                                 join r in db.Roles on ur.Role_Id equals r.Role_Id
                                 where ur.Faculty_Id == teacher.Faculty_Id
                                 select r.Role_Name).ToArray();
                    return roles;
                }
                

                else if (admin != null)
                {
                    // If the user is an admin, get the associated roles from UserRole table
                    var roles = (from ur in db.UserRoles
                                 join r in db.Roles on ur.Role_Id equals r.Role_Id
                                 where ur.Admin_Id == admin.Admin_ID  // Use the correct property to match Admin_Id
                                 select r.Role_Name).ToArray();
                    return roles;
                }
            }

            // If no roles are found for the user, return an empty array
            return new string[] { };
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