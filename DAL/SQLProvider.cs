using System;
using System.Collections.Generic;
using CSharpGrenadesGASource.BL.BusinessClasses;
using CSharpGrenadesGASource.ErrorHandling;
using System.Linq;
using System.Data.SQLite;
using CSharpGrenadesGASource.BL;
using System.Data;
using System.Text;

namespace CSharpGrenadesGASource.DAL
{
    public class SQLProvider : ProviderBase
    {
      
        private string conString = "Data Source = c:\\DataStores\\CSharpGrenades\\CSharpGrenadesGA.s3db;Version=3;";
        private SQLiteConnection _sqlConn;
        List<GroupDetail> groups;
        List<ProjectDetail> projects;
        List<StudentDetail> students;
        List<User> Users;
        ErrorLogWriter logWriter = new ErrorLogWriter();


        //select all 

        public override List<GroupDetail> SelectAllGroups()
        {
    
            try
            {
                _sqlConn = new SQLiteConnection(conString);
                bool readSucces = false;
                groups = new List<GroupDetail>();
                _sqlConn.Open();

                string selectQuery = "SELECT * FROM GroupDetail";
                SQLiteCommand sqlCommand = new SQLiteCommand(selectQuery, _sqlConn);
                SQLiteDataReader reader = sqlCommand.ExecuteReader();

                readSucces = reader.Read();
                while (readSucces == true)
                {
                    GroupDetail GroupObj = new GroupDetail();
                    GroupObj.GroupID = Convert.ToInt32(reader["GroupId"]);
                    GroupObj.ProjectID.ProjectID = Convert.ToInt32(reader["ProjectId"]);
                    GroupObj.GroupName = Convert.ToString(reader["GroupName"]);

                    groups.Add(GroupObj);
                    readSucces = reader.Read();
                }
                reader.Close();
               
            }
            catch (Exception ex)
            {
                //Error.WriteLine(ex);
                logWriter.WriteToLog(ex.Message, ex);
            }

            finally
            {
                _sqlConn.Close();
            }

            return groups;

        }

        public override List<ProjectDetail> SelectAllProjects()
        {

            try
            {
                _sqlConn = new SQLiteConnection(conString);
                bool readSucces = false;
                projects = new List<ProjectDetail>();
                _sqlConn.Open();

                string selectQuery = "SELECT * FROM ProjectDetail";
                SQLiteCommand sqlCommand = new SQLiteCommand(selectQuery, _sqlConn);
                SQLiteDataReader reader = sqlCommand.ExecuteReader();

                readSucces = reader.Read();
                while (readSucces == true)
                {
                    ProjectDetail ProjectObj = new ProjectDetail();
                    ProjectObj.ProjectID = Convert.ToInt32(reader["ProjectId"]);
                    ProjectObj.ProjectName = Convert.ToString(reader["ProjectName"]);
                    ProjectObj.ProjectDescription = Convert.ToString(reader["ProjectDescription"]);
                    projects.Add(ProjectObj);
                    readSucces = reader.Read();
                }

            
            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);

            }

            finally
            {
                _sqlConn.Close();
            }

            return projects;

        }

        public override List<StudentDetail> SelectAllStudents()
        {

        
            try
            {
                _sqlConn = new SQLiteConnection(conString);
                bool readSucces = false;
                students = new List<StudentDetail>();
                _sqlConn.Open();

                string selectQuery = "SELECT * FROM StudentDetail";
                SQLiteCommand sqlCommand = new SQLiteCommand(selectQuery, _sqlConn);
                SQLiteDataReader reader = sqlCommand.ExecuteReader();

                readSucces = reader.Read();
                while (readSucces == true)
                {
                    StudentDetail StudentObj = new StudentDetail();
                    StudentObj.StudentId = Convert.ToInt32(reader["StudentID"]);
                    StudentObj.StudentName = Convert.ToString(reader["StudentName"]);
                    StudentObj.StudentNumber = Convert.ToInt32(reader["StudentNumber"]);
                    StudentObj.GroupID = Convert.ToInt32(reader["GroupID"]);
                    StudentObj.StudentImage = (byte[])reader["StudentImage"];
                    students.Add(StudentObj);
                    readSucces = reader.Read();
                }

               
            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);

            }

            finally
            {
                _sqlConn.Close();
            }

            return students;

        }

        //select 

        public override int SelectGroup(string groupID, ref GroupDetail GroupDetail)
        {
            int rc = 0;
          
            try
            {
                _sqlConn = new SQLiteConnection(conString);
                _sqlConn.Open();
                string selectedQery = "SELECT * FROM GroupDetail WHERE [GroupId] = '" + groupID + "'";
                SQLiteCommand sqlCommand = new SQLiteCommand(selectedQery, _sqlConn);
                SQLiteDataReader reader = sqlCommand.ExecuteReader();

                bool readSucces = false;
                readSucces = reader.Read();

                if (readSucces == true)
                {
                    GroupDetail groupDetailObj = new GroupDetail();

                    groupDetailObj.GroupID = Convert.ToInt32(reader["GroupId"]);
                    groupDetailObj.ProjectID.ProjectID = Convert.ToInt32(reader["ProjectId"]);
                    groupDetailObj.GroupName = Convert.ToString(reader["GroupName"]);
                    GroupDetail = groupDetailObj;
                    rc = 0;
                   
                }

                else
                {
                    rc = -1;
                    
                }

            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);
            }

            return rc;

        }

        public override int SelectProject(string projectID, ref ProjectDetail ProjectDetail)
        {

            int rc = 0;


            try
            {
                _sqlConn = new SQLiteConnection(conString);
                _sqlConn.Open();
                string selectedQery = "SELECT * FROM ProjectDetail WHERE [ProjectId] = '" + projectID + "'";
                SQLiteCommand sqlCommand = new SQLiteCommand(selectedQery, _sqlConn);
                SQLiteDataReader reader = sqlCommand.ExecuteReader();

                bool readSucces = false;
                readSucces = reader.Read();

                if (readSucces == true)
                {
                    ProjectDetail projectDetailObj = new ProjectDetail();

                    projectDetailObj.ProjectID = Convert.ToInt32(reader["ProjectId"]);
                    projectDetailObj.ProjectName = Convert.ToString(reader["ProjectName"]);
                    projectDetailObj.ProjectDescription = Convert.ToString(reader["ProjectDescription"]);
                    ProjectDetail = projectDetailObj;
                    rc = 0;

                }

                else
                {
                    rc = -1;
                }

            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);

            
            }
            return rc;
        }

        public override int SelectStudent(string studentID, ref StudentDetail StudentDetail)
        {

            int rc = 0;
          
            try
            {
                _sqlConn = new SQLiteConnection(conString);
                _sqlConn.Open();
                string selectedQery = "SELECT * FROM StudentDetail WHERE [StudentId] = '" + studentID + "'";
                SQLiteCommand sqlCommand = new SQLiteCommand(selectedQery, _sqlConn);
                SQLiteDataReader reader = sqlCommand.ExecuteReader();

                bool readSucces = false;
                readSucces = reader.Read();

                if (readSucces == true)
                {
                    StudentDetail studentDetailObj = new StudentDetail();

                    studentDetailObj.StudentId = Convert.ToInt32(reader["StudentId"]);
                    studentDetailObj.GroupID = Convert.ToInt32(reader["GroupId"]);
                    studentDetailObj.StudentName = Convert.ToString(reader["StudentName"]);
                    studentDetailObj.StudentNumber = Convert.ToInt32(reader["StudentNumber"]);
                    StudentDetail = studentDetailObj;
                    rc = 0;
                 
                }

                else
                {
                    rc = -1;
                  
                }

            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);
            }

            return rc;

        }

        //STUDENTS

        public override int InsertStudent(BL.BusinessClasses.StudentDetail Student)
        {
            int rc = 1;
           
            try
            {
                _sqlConn = new SQLiteConnection(conString);

                _sqlConn.Open();


                string InsertQuery = "INSERT INTO StudentDetail([GroupId],[StudentName],[StudentNumber],[StudentImage]) VALUES(@GroupId,@StudentName,@StudentNumber,@StudentImage)";


                SQLiteCommand sqlComm = new SQLiteCommand(InsertQuery, _sqlConn);

                SQLiteParameter[] sqlParams = new SQLiteParameter[]
                {
                    new SQLiteParameter("@GroupId", DbType.Int32),
                    new SQLiteParameter("@StudentName", DbType.String),
                    new SQLiteParameter("@StudentNumber", DbType.Int32),
                    new SQLiteParameter("StudentImage",DbType.Binary)


                };


              
                sqlParams[0].Value = Student.GroupID;
                sqlParams[1].Value = Student.StudentName;
                sqlParams[2].Value = Student.StudentNumber;
                sqlParams[3].Value = Student.StudentImage;

                sqlComm.Parameters.AddRange(sqlParams);

                int rowsAffected = sqlComm.ExecuteNonQuery();

                if (rowsAffected == 1)
                {

                    rc = 0;
                 
                }


            }

            catch (SQLiteException ex)
            {
                if (ex.ErrorCode.Equals(SQLiteErrorCode.Constraint))
                {

                    rc = -1;
                  
                }
            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);
            }
            finally
            {
                _sqlConn.Close();
            }
            return rc;
        }

        public override int DeleteStudent(StudentDetail studentId)
        {
            int rc = 0;
           
            try
            {
                int rowsAffected = 0;
                _sqlConn = new SQLiteConnection(conString);
                _sqlConn.Open();

                string deletQuery = string.Format("DELETE FROM StudentDetail WHERE [StudentId] = '{0}'", studentId.StudentId);
                SQLiteCommand sqlCommand = new SQLiteCommand(deletQuery, _sqlConn);
                rowsAffected = sqlCommand.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    rc = -1;
                   
                }
                else
                {
                    rc = 0;
                  
                }

            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);
            }
            finally
            {
                _sqlConn.Close();
            }

            return rc;
        }

        public override int UpdateStudent(StudentDetail Student)
        {
            int rowsAffected = 1;
           
            try
            {

                _sqlConn = new SQLiteConnection(conString);
                _sqlConn.Open();
                string updateQuery = String.Format("UPDATE StudentDetail SET [GroupId]=@GroupId,[StudentName] = @StudentName,[StudentNumber] = @StudentNumber WHERE [StudentId] = '{0}'", Student.StudentId);
                SQLiteCommand sqlCommand = new SQLiteCommand(updateQuery, _sqlConn);

                SQLiteParameter[] sqlParams = new SQLiteParameter[]
                {

                   new SQLiteParameter("@GroupId",DbType.String),
                   new SQLiteParameter("@StudentName", DbType.String),
                   new SQLiteParameter("@StudentNumber", DbType.Int32)
                   //new SQLiteParameter("@StudentImage",DbType.Binary)

                };

                sqlParams[0].Value = Student.GroupID;
                sqlParams[1].Value = Student.StudentName;
                sqlParams[2].Value = Student.StudentNumber;
                //sqlParams[3].Value = Student.StudentImage;


                sqlCommand.Parameters.AddRange(sqlParams);
                rowsAffected = sqlCommand.ExecuteNonQuery();

                if (rowsAffected == 1)
                {
                    rowsAffected = 0;

                }
                else
                {
                    rowsAffected = -1;

                }

            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);
            }
            finally
            {
                _sqlConn.Close();
            }
            return rowsAffected;
        }


        //PROJECTS

        public override int InsertProject(ProjectDetail Project)
        {

            int rc = 1;
       

            try
            {
                _sqlConn = new SQLiteConnection(conString);

                _sqlConn.Open();


                string insertQuery = "INSERT INTO ProjectDetail([ProjectName],[ProjectDescription]) VALUES(@ProjectName,@ProjectDescription)";


                SQLiteCommand sqlComm = new SQLiteCommand(insertQuery, _sqlConn);

                SQLiteParameter[] sqlParams = new SQLiteParameter[]
                {

                    
                    new SQLiteParameter("@ProjectName", DbType.String),
                    new SQLiteParameter("@ProjectDescription", DbType.String)


                };

                sqlParams[0].Value = Project.ProjectName;
                sqlParams[1].Value = Project.ProjectDescription;



                sqlComm.Parameters.AddRange(sqlParams);

                int rowsAffected = sqlComm.ExecuteNonQuery();

                if (rowsAffected == 1)
                {

                    rc = 0;            
                }

            }

            catch (SQLiteException ex)
            {
                if (ex.ErrorCode.Equals(SQLiteErrorCode.Constraint))
                {

                    logWriter.WriteToLog(ex.Message, ex);
                    rc = -1;

                }
            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);
            }
            finally
            {
                _sqlConn.Close();
            }
            return rc;
        }

        public override int DeleteProject(ProjectDetail ProjectId)
        {
            int rc = 0;

            try
            {
                int rowsAffected = 0;
                _sqlConn = new SQLiteConnection(conString);
                _sqlConn.Open();

                string deletQuery = string.Format("DELETE FROM ProjectDetail WHERE [ProjectId] = '{0}'", ProjectId.ProjectID);
                SQLiteCommand sqlCommand = new SQLiteCommand(deletQuery, _sqlConn);
                rowsAffected = sqlCommand.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    rc = -1;

                }
                else
                {
                    rc = 0;

                }

            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);
            }
            finally
            {
                _sqlConn.Close();
            }

            return rc;
        }

        public override int UpdateProject(ProjectDetail Project)
        {


            int rowsAffected = 0;
          
            try
            {

                _sqlConn = new SQLiteConnection(conString);
                _sqlConn.Open();
                string updateQuery = string.Format("UPDATE ProjectDetail SET [ProjectDescription] = @ProjectDescription," + "[ProjectName] = @ProjectName WHERE [ProjectId] = '{0}'", Project.ProjectID);
                SQLiteCommand sqlCommand = new SQLiteCommand(updateQuery, _sqlConn);

                SQLiteParameter[] sqlParams = new SQLiteParameter[]
                {

                      new SQLiteParameter("@ProjectDescription", DbType.String),
                      new SQLiteParameter("@ProjectName", DbType.String)

                };

                sqlParams[0].Value = Project.ProjectDescription;
                sqlParams[1].Value = Project.ProjectName;

                sqlCommand.Parameters.AddRange(sqlParams);
                rowsAffected = sqlCommand.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    rowsAffected = 0;
                    
                }
                else
                {
                    rowsAffected = -1;
                  
                }

            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);
            }
            finally
            {
                _sqlConn.Close();
            }
            return rowsAffected;
        }

        //GROUP
        public override int InsertGroup(GroupDetail Group)
        {


            int rc = 1;

            try
            {
                _sqlConn = new SQLiteConnection(conString);

                _sqlConn.Open();


                string insertQuery = "INSERT INTO GroupDetail([ProjectId],[GroupName]) VALUES(@ProjectId,@GroupName)";


                SQLiteCommand sqlComm = new SQLiteCommand(insertQuery, _sqlConn);

                SQLiteParameter[] sqlParams = new SQLiteParameter[]
                {

                 
                   new SQLiteParameter("@ProjectId", DbType.Int32),
                   new SQLiteParameter("@GroupName", DbType.String)


                };


                
                sqlParams[0].Value = Group.ProjectID.ProjectID;
                sqlParams[1].Value = Group.GroupName;



                sqlComm.Parameters.AddRange(sqlParams);

                int rowsAffected = sqlComm.ExecuteNonQuery();

                if (rowsAffected == 1)
                {

                    rc = 0;

                }


            }

            catch (SQLiteException ex)
            {
                if (ex.ErrorCode.Equals(SQLiteErrorCode.Constraint))
                {
                    rc = -1;
                    logWriter.WriteToLog(ex.Message, ex);

                }
            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);
            }
            finally
            {
                _sqlConn.Close();
            }
            return rc;
        }

        public override int DeleteGroup(GroupDetail GroupId)
        {
            int rc = 0;
          
            try
            {
                int rowsAffected = 0;
                _sqlConn = new SQLiteConnection(conString);
                _sqlConn.Open();

                string deletQuery = string.Format("DELETE FROM GroupDetail WHERE [GroupId] = '{0}'", GroupId.GroupID);
                SQLiteCommand sqlCommand = new SQLiteCommand(deletQuery, _sqlConn);
                rowsAffected = sqlCommand.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    rc = -1;
                }
                else
                {
                    rc = 0;
                }

            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);
            }
            finally
            {
                _sqlConn.Close();
            }

            return rc;
        }

        public override int UpdateGroup(GroupDetail Group)
        {

            int rowsAffected = 1;
           
            try
            {

                _sqlConn = new SQLiteConnection(conString);
                _sqlConn.Open();
                string updateQuery = string.Format("UPDATE GroupDetail SET [GroupName] = @GroupName,[ProjectId] = @projectId WHERE [GroupId] = '{0}'", Group.GroupID);
                SQLiteCommand sqlCommand = new SQLiteCommand(updateQuery, _sqlConn);

                SQLiteParameter[] sqlParams = new SQLiteParameter[]
                {

                   new SQLiteParameter("@GroupName", DbType.String),
                   new SQLiteParameter("@ProjectID", DbType.Int32)

                };

                sqlParams[0].Value = Group.GroupName;
                sqlParams[1].Value = Group.ProjectID;
                sqlCommand.Parameters.AddRange(sqlParams);

                rowsAffected = sqlCommand.ExecuteNonQuery();

                if (rowsAffected == 1)
                {
                    rowsAffected = 0;
                  
                }
                else
                {
                    rowsAffected = -1;
                    
                }

            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);
            }
            finally
            {
                _sqlConn.Close();
            }
            return rowsAffected;
        }

      
        public override List<User> SelectAllUsers()
        {

        
            try
            {
                _sqlConn = new SQLiteConnection(conString);
                bool readSucces = false;
                Users = new List<User>();
                _sqlConn.Open();

                string selectQuery = "SELECT * FROM User";
                SQLiteCommand sqlCommand = new SQLiteCommand(selectQuery, _sqlConn);
                SQLiteDataReader reader = sqlCommand.ExecuteReader();

                readSucces = reader.Read();
                while (readSucces == true)
                {
                    User UserObj = new User();
                    UserObj.Id = Convert.ToInt32(reader["Id"]);
                    UserObj.Firstname = Convert.ToString(reader["Firstname"]);
                    UserObj.Lastname = Convert.ToString(reader["Lastname"]);
                    UserObj.Password = Convert.ToString(reader["Password"]);
                    UserObj.ConfirmPassword = Convert.ToString(reader["ConfirmPassword"]);
                    UserObj.Role = Convert.ToString(reader["Role"]);
                    //UserObj.UserImage = (byte[])reader["UserImage"];
                    UserObj.UserName = Convert.ToString(reader["UserName"]);

                    Users.Add(UserObj);
                    readSucces = reader.Read();
                }

                reader.Close();
              
            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);
            }

            finally
            {
                _sqlConn.Close();
            }

            return Users;

        }

        public override int InsertUser(User User)
        {
          

            int rc = 1;

            try
            {
                _sqlConn = new SQLiteConnection(conString);

                _sqlConn.Open();

                string insertQuery = "INSERT INTO User([Firstname],[Lastname],[Password],[ConfirmPassword],[Role],[Image],[Username]) VALUES(@Firstname,@Lastname,@Password,@ConfirmPassword,@Role,@Image,@Username)";
                SQLiteCommand sqlComm = new SQLiteCommand(insertQuery, _sqlConn);

                SQLiteParameter[] sqlParams = new SQLiteParameter[]
                {


                   new SQLiteParameter("@Firstname", DbType.String),
                   new SQLiteParameter("@Lastname", DbType.String),
                   new SQLiteParameter("@Password", DbType.String),
                   new SQLiteParameter("@ConfirmPassword", DbType.String),
                   new SQLiteParameter("@Role", DbType.String),
                   new SQLiteParameter("@Image", DbType.Binary),
                   new SQLiteParameter("@UserName", DbType.String),

                };


                sqlParams[0].Value = User.Firstname;
                sqlParams[1].Value = User.Lastname;
                sqlParams[2].Value = User.Password;
                sqlParams[3].Value = User.ConfirmPassword;
                sqlParams[4].Value = User.UserImage;
                sqlParams[5].Value = User.Role;
                sqlParams[6].Value = User.UserName;




                sqlComm.Parameters.AddRange(sqlParams);

                int rowsAffected = sqlComm.ExecuteNonQuery();

                if (rowsAffected == 1)
                {
                    rc = 0;
                  
                }
            }

            catch (SQLiteException ex)
            {
                if (ex.ErrorCode.Equals(SQLiteErrorCode.Constraint))
                {
                    rc = -1;
                    logWriter.WriteToLog(ex.Message, ex);
                }
            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);
            }
            finally
            {
                _sqlConn.Close();
            }
            return rc;
        }




    }
}
