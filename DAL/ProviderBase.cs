using System;
using System.Collections.Generic;
using CSharpGrenadesGASource.BL.BusinessClasses;
using System.Linq;
using System.Text;

namespace CSharpGrenadesGASource.DAL
{
    public abstract class ProviderBase
    {
        public abstract List<GroupDetail> SelectAllGroups();
        public abstract List<ProjectDetail> SelectAllProjects();
        public abstract List<StudentDetail> SelectAllStudents();

        public abstract int SelectGroup(string groupID, ref GroupDetail GroupsDetail);
        public abstract int SelectProject(string projectID, ref ProjectDetail ProjectsDetail);
        public abstract int SelectStudent(string studentID, ref StudentDetail StudentsDetail);

        public abstract int InsertGroup(GroupDetail GroupDetail);
        public abstract int InsertProject(ProjectDetail ProjectsDetail);
        public abstract int InsertStudent(StudentDetail StudentsDetail);

        public abstract int UpdateGroup(GroupDetail GroupDetail);
        public abstract int UpdateProject(ProjectDetail ProjectsDetail);
        public abstract int UpdateStudent(StudentDetail StudentDetail);

        public abstract int DeleteGroup(GroupDetail groupName);
        public abstract int DeleteProject(ProjectDetail projectName);
        public abstract int DeleteStudent(StudentDetail studentID);

   
     
        public abstract List<User> SelectAllUsers();
        public abstract int InsertUser(User user);
      
    }
}
