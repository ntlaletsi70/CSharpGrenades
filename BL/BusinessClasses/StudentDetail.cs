using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpGrenadesGASource.BL.BusinessClasses
{
    public class StudentDetail
    {
        private int studentId;
        private int groupID;
        private string studentName;
        private int studentNumber;
        private byte[] studentImage;
        public int StudentId
        {
            get { return studentId; }
            set { studentId = value; }
        }

        public int GroupID
        {
            get { return groupID; }
            set { groupID = value; }
        }


        public string StudentName
        {
            get { return studentName; }
            set { studentName = value; }
        }


        public int StudentNumber
        {
            get { return studentNumber; }
            set { studentNumber = value; }
        }

        public byte[] StudentImage
        {
            get
            {
                return studentImage;
            }

            set
            {
                studentImage = value;
            }
        }

        public override string ToString()
        {
            return StudentId + StudentName + " " + StudentNumber + " " + GroupID;
        }

    }
}
