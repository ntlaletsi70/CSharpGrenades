using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpGrenadesGASource.BL.BusinessClasses
{
    public class ProjectDetail
    {
        private int projectID;
        private string projectName;
        private string projectDescription;

        public int ProjectID
        {
            get { return projectID; }
            set { projectID = value; }
        }

        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
        }

        public string ProjectDescription
        {
            get { return projectDescription; }
            set { projectDescription = value; }
        }

        public override string ToString()
        {
            return ProjectID +  " " + ProjectName + " " + ProjectDescription;
        }
    }
}
