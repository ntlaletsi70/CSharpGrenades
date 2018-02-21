using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpGrenadesGASource.BL.BusinessClasses
{
    public class GroupDetail
    {
        private int groupID;
        private ProjectDetail project;
        private string groupName;


        public int GroupID
        {
            get { return groupID; }
            set { groupID = value; }
        }

        public ProjectDetail ProjectID
        {
            get { return  new ProjectDetail(); }
            set { project = value; }
        }

        public string GroupName
        {
            get { return groupName; }
            set { groupName = value; }
        }

        public override string ToString()
        {
            return GroupID + " " + ProjectID.ProjectID + "" + GroupName;

        }
    }
}
