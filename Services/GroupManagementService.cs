using System;
using System.Collections.Generic;
using CSharpGrenadesGASource.BL.BusinessClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpGrenadesGASource.Services
{
   public class GroupManagementService
    {
        private GroupDetail group;

        private StudentDetail student;

        private ProjectDetail projects;

        public GroupManagementService(GroupDetail group, StudentDetail student,ProjectDetail project)
        {
            this.group = group;
            this.student = student;
            //this.project = project;
        }

    }
}
