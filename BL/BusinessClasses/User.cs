using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpGrenadesGASource.BL.BusinessClasses
{
    public class User
    {

        private int id;
        private string firstname;
        private string lastname;
        private string password;
        private string role;
        private string userName;
        private string confirmPassword;
        private byte[] userImage;

        public string Firstname
        {
            get
            {
                return firstname;
            }

            set
            {
                firstname = value;
            }
        }

        public string Lastname
        {
            get
            {
                return lastname;
            }

            set
            {
                lastname = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }

        public string ConfirmPassword
        {
            get
            {
                return confirmPassword;
            }

            set
            {
                confirmPassword = value;
            }
        }

        public byte[] UserImage
        {
            get
            {
                return userImage;
            }

            set
            {
                userImage = value;
            }
        }

        public string Role
        {
            get
            {
                return role;
            }

            set
            {
                role = value;
            }
        }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string UserName
        {
            get
            {
                return userName;
            }

            set
            {
                userName = value;
            }
        }
    }
}
