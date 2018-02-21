using System;
using System.Collections.Generic;
using CSharpGrenadesGASource.BL.BusinessClasses;
using CSharpGrenadesGASource.DAL;
using System.Linq;
using System.Text;

namespace CSharpGrenadesGASource.BL
{
    public class GroupBL
    {
        private ProviderBase providerBase;

        public GroupBL(string Provider)
        {
            _SetupProviderBase(Provider);
        }

        public List<GroupDetail> SelectAllGroups()
        {
            return providerBase.SelectAllGroups();
        }
        public List<ProjectDetail> SelectAllProjects()
        {
            return providerBase.SelectAllProjects();
        }
        public List<StudentDetail> SelectAllStudents()
        {
            return providerBase.SelectAllStudents();
        }

        public int SelectGroup(string GroupID, ref GroupDetail GroupObj)
        {
            return providerBase.SelectGroup(GroupID, ref GroupObj);
        }

        public int InsertGroup(GroupDetail GroupObj)
        {
            return providerBase.InsertGroup(GroupObj);
        }

        public int InsertProject(ProjectDetail ProjectObj)
        {
            return providerBase.InsertProject(ProjectObj);
        }

        public int InsertStudent(StudentDetail StudentObj)
        {
            return providerBase.InsertStudent(StudentObj);
        }

        public int UpdateStudent(StudentDetail StudentObj)
        {
            return providerBase.UpdateStudent(StudentObj);
        }
        public int UpdateGroup(GroupDetail GroupObj)
        {
            return providerBase.UpdateGroup(GroupObj);
        }
        public int UpdateProject(ProjectDetail ProjectObj)
        {
            return providerBase.UpdateProject(ProjectObj);
        }

        public int DeleteGroup(GroupDetail groupName)
        {
            return providerBase.DeleteGroup(groupName);
        }

        public int DeleteStudent(StudentDetail studentName)
        {
            return providerBase.DeleteStudent(studentName);
        }

        public int DeleteProject(ProjectDetail projectName)
        {
            return providerBase.DeleteProject(projectName);
        }


        private void _SetupProviderBase(string Provider)
        {
            //
            //Method Name : void _SetupProviderBase()
            //Purpose     : Helper method to select the correct data provider
            //Re-use      : None
            //Input       : string Provider
            //              - The name of the data provider to use
            //Output      : None
            //
            if (Provider.Equals("SQLProvider"))
            {
                providerBase = new SQLProvider();//set a new memory to the object,causes the PersonProviderBaseSQLite to execute
            }//end if

        }
    }

}
