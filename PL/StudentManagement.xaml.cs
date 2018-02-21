using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSharpGrenadesGASource.BL.BusinessClasses;
using CSharpGrenadesGASource.BL;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Drawing.Imaging;
using CSharpGrenadesGASource.ErrorHandling;
using CSharpGrenadesGASource.Services;

namespace CSharpGrenadesGASource.PL
{
    /// <summary>
    /// Interaction logic for StudentManagement.xaml
    /// </summary>
    public partial class StudentManagement : Page
    {

        GroupDetail GroupObj { get; set; }
        ProjectDetail ProjectObj { get; set; }

        StudentDetail StudentObj { get; set; }
        List<GroupDetail> Group { get; set; }

        List<ProjectDetail> Project { get; set; }

        List<StudentDetail> Student { get; set; }

        CustomDialogOK ohk;
        byte[] data;
        ErrorLogWriter logWriter = new ErrorLogWriter();
        GroupManagementService service;


        GroupBL groupBL;
        public StudentManagement()
        {
            InitializeComponent();
            GroupObj = new GroupDetail();
            ProjectObj = new ProjectDetail();
            StudentObj = new StudentDetail();


            Group = new List<GroupDetail>();
            Project = new List<ProjectDetail>();
            Student = new List<StudentDetail>();
            ohk = new CustomDialogOK();

            groupBL = new GroupBL("SQLProvider");
          

        }

        private void btnBrowsePicture_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog(); dlg.ShowDialog();
                FileStream fs = new FileStream(dlg.FileName, FileMode.Open, FileAccess.Read);
                data = new byte[fs.Length]; fs.Read(data, 0, System.Convert.ToInt32(fs.Length)); fs.Close();
                ImageSourceConverter imgs = new ImageSourceConverter();
                studentImage.SetValue(Image.SourceProperty, imgs.ConvertFromString(dlg.FileName.ToString()));
            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);
                ohk = new CustomDialogOK();
                ohk.SetMessage(ex.Message);
                ohk.Show();
            }
        }



        private void btnAddStudent_Click_1(object sender, RoutedEventArgs e)
        {

            GroupDetail groupDetail = null;
            try
            {

                ProjectDetail detail = new ProjectDetail();
                detail.ProjectName = txtProjectName.Text;
                detail.ProjectDescription = txtProjectDescription.Text;


                int rcProject = groupBL.InsertProject(detail);

                if (rcProject == 0)
                {
                    if (txtGroupName.Text.Length != 0)
                    {
                        groupDetail = new GroupDetail();
                        groupDetail.ProjectID.ProjectID = detail.ProjectID;
                        groupDetail.GroupName = txtGroupName.Text;

                        int rcGroup = groupBL.InsertGroup(groupDetail);
                    }
                    else
                    {
                        ohk = new CustomDialogOK();
                        ohk.SetMessage("Please enter all fields!");
                        ohk.Show();
                    }
                }

                else
                {

                    ohk = new CustomDialogOK();
                    ohk.SetMessage("Please enter all fields!");
                    ohk.Show();

                }


                if ((txtProjectName.Text.Length != 0 || txtProjectDescription.Text.Length != 0))
                {
                    if (studentImage.Source.ToString().Contains("Images/default-avatar.png"))
                    {
                        ohk = new CustomDialogOK();
                        ohk.SetMessage("Please select an image");
                        ohk.Show();
                    }

                    StudentObj = new StudentDetail();
                    //StudentObj.StudentId = Convert.ToInt32(txtGroupID1.Text);
                    StudentObj.GroupID = groupDetail.GroupID;
                    StudentObj.StudentName = txtStudentName1.Text;
                    StudentObj.StudentNumber = Convert.ToInt32(txtStudentNumber1.Text);
                    StudentObj.StudentImage = data;

                    int rc = groupBL.InsertStudent(StudentObj);
                    if (rc == 0)
                    {
                        ohk = new CustomDialogOK();
                        ohk.SetMessage(StudentObj.StudentName + "  Successfully");
                        ohk.Show();
                    }
                    else
                    {
                        ohk = new CustomDialogOK();
                        ohk.SetMessage(StudentObj.StudentName + " Not Saved");
                        ohk.Show();
                    }
                }
                else
                {

                    ohk = new CustomDialogOK();
                    ohk.SetMessage("Please enter all fields!");
                    ohk.Show();
                }
            }

            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);
                ohk = new CustomDialogOK();
                ohk.SetMessage(ex.Message);
                ohk.Show();
            }
            //Refresh();
            //return;
        }




        private void lstManagement_Loaded(object sender, RoutedEventArgs e)
        {
            Project = new List<ProjectDetail>();
            Group = new List<GroupDetail>();
            Student = new List<StudentDetail>();



            try
            {
                Project = groupBL.SelectAllProjects();
                Student = groupBL.SelectAllStudents();
                Group = groupBL.SelectAllGroups();
               
                lstManagement.Items.Clear();

                for (int i = 0; i < Student.Count; i++)
                {
                    service = new GroupManagementService(Group[i], Student[i], Project[i]);
                    lstManagement.Items.Add(service);
                }

               //

            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);
                ohk = new CustomDialogOK();
                ohk.SetMessage(ex.Message);
                ohk.Show();
            }
        }

        private void lstManagement_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                if (lstManagement.Items.Count > 0)
                {
                    StudentObj = new StudentDetail();
                    StudentObj = lstManagement.SelectedItem as StudentDetail;
                    //txtGroupID1.Text = StudentObj.GroupID.ToString();
                    txtStudentName1.Text = StudentObj.StudentName;
                    txtStudentNumber1.Text = StudentObj.StudentNumber.ToString();
                    MemoryStream ms = new MemoryStream();
                    ms.Write(StudentObj.StudentImage, 0, StudentObj.StudentImage.Length);
                    System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    MemoryStream mss = new MemoryStream();
                    img.Save(mss, ImageFormat.Bmp);
                    mss.Seek(0, SeekOrigin.Begin);
                    bi.StreamSource = mss;
                    bi.EndInit();
                    studentImage.Source = bi;
                }
            }
            catch (Exception ex)
            {
                logWriter.WriteToLog(ex.Message, ex);
                ohk = new CustomDialogOK();
                ohk.SetMessage(ex.Message);
                ohk.Show();
            }
        }

        private void btnUpdateStudent_Click(object sender, RoutedEventArgs e)
        {

            if (txtStudentName1.Text.Length != 0 && txtStudentNumber1.Text.Length != 0)
            {

                try
                {
                    if (lstManagement.Items.Count > 0)
                    {
                        StudentObj = new StudentDetail();
                        StudentObj = lstManagement.SelectedItem as StudentDetail;
                        //.Text = StudentObj.GroupID.ToString();
                        txtStudentName1.Text = StudentObj.StudentName;
                        txtStudentNumber1.Text = StudentObj.StudentNumber.ToString();
                        StudentObj.StudentImage = data;

                        int rc = groupBL.UpdateStudent(StudentObj);
                        if (rc == 0)
                        {
                            ohk = new CustomDialogOK();
                            ohk.SetMessage(StudentObj.StudentName + " " + " Edited successfully");
                            ohk.Show();


                        }
                        else
                        {
                            ohk = new CustomDialogOK();
                            ohk.SetMessage(StudentObj.StudentName + " Not edited " + " \n" + " Please enter all fields");
                            ohk.Show();

                        }
                    }
                }
                catch (Exception ex)
                {
                    logWriter.WriteToLog(ex.Message, ex);
                    ohk = new CustomDialogOK();
                    ohk.SetMessage("Please enter all fields");
                    ohk.Show();

                }

            }
            else
            {
                ohk = new CustomDialogOK();
                ohk.SetMessage("Please select an item to update or " + "\n" +
                    "enter all fields");
                ohk.Show();

            }
        }

        private void btnDeleteStudent_Click(object sender, RoutedEventArgs e)
        {
            if (lstManagement.Items.Count > 0)
            {

                StudentObj = new StudentDetail();
                StudentObj = lstManagement.SelectedItem as StudentDetail;
                try
                {
                    int rc = groupBL.DeleteStudent(StudentObj);

                    if (rc == 0 && StudentObj != null)
                    {
                        ohk = new CustomDialogOK();
                        ohk.SetMessage("Deleted Successfully!");
                        ohk.Show();
                        


                    }
                    else
                    {
                        ohk = new CustomDialogOK();
                        ohk.SetMessage("Not Deleted!");
                        ohk.Show();
                    }
                }
                catch (Exception ex)
                {
                    logWriter.WriteToLog(ex.Message, ex);
                    ohk = new CustomDialogOK();
                    ohk.SetMessage("Please enter select a student to delete");
                    ohk.Show();

                }
            }


            //    public void Mouse_Clicked(object sender, RoutedEventArgs e)
            //    {

            //    }


            //    private void btDelete_Click(object sender, RoutedEventArgs e)
            //    {


            //        if (ListProject.Items.Count > 0)
            //        {

            //            ProjectObj = new ProjectDetail();



            //            ProjectObj = ListProject.SelectedItem as ProjectDetail;

            //            try
            //            {
            //                int rc = groupBL.DeleteProject(ProjectObj);

            //                if (rc == 0)
            //                {
            //                    MessageBox.Show(ProjectObj.ProjectName + " " + "deleted successfully", "Success");

            //                }
            //                else
            //                {
            //                    MessageBox.Show(ProjectObj.ProjectName + " " + "deleted successfully", "Success");
            //                }
            //            }
            //            catch (Exception ex)
            //            {
            //                MessageBox.Show(ex.Message, "Error");
            //            }
            //            return;
            //        }



            //        if (ListGroup.Items.Count > 0)
            //        {
            //            GroupObj = new GroupDetail();


            //            GroupObj = ListGroup.SelectedItem as GroupDetail;

            //            try
            //            {
            //                int rc = groupBL.DeleteGroup(GroupObj);

            //                if (rc == 0)
            //                {
            //                    MessageBox.Show(GroupObj.GroupName + " " + "deleted successfully", "Success");

            //                }
            //                else
            //                {
            //                    MessageBox.Show(GroupObj.GroupName + " " + "deleted successfully", "Success");
            //                }
            //            }
            //            catch (Exception ex)
            //            {
            //                MessageBox.Show(ex.Message, "Error");
            //            }
            //            return;
            //        }


            //        if (ListStudent.Items.Count > 0)
            //        {

            //            StudentObj = new StudentDetail();


            //            StudentObj = ListStudent.SelectedItem as StudentDetail;


            //            try
            //            {
            //                int rc = groupBL.DeleteStudent(StudentObj);

            //                if (rc == 0)
            //                {
            //                    MessageBox.Show(StudentObj.StudentName + " " + "deleted successfully", "Success");

            //                }
            //                else
            //                {
            //                    MessageBox.Show(StudentObj.StudentName + " " + "deleted successfully", "Success");
            //                }
            //            }
            //            catch (Exception ex)
            //            {
            //                MessageBox.Show(ex.Message, "Error");
            //            }
            //            return;
            //        }
            //        Refresh();
            //    }

            //    private void btAdd_Click(object sender, RoutedEventArgs e)
            //    {

            //        if ((txtGroupID.Text.Length != 0 || txtGroupProjectID.Text.Length != 0 || txtGroupName.Text.Length != 0))
            //        {
            //            try
            //            {
            //                GroupObj = new GroupDetail();



            //                GroupObj.GroupID = Convert.ToInt32(txtGroupID.Text);
            //                GroupObj.ProjectID = Convert.ToInt32(txtGroupProjectID.Text);
            //                GroupObj.GroupName = txtGroupName.Text;



            //                int rc = groupBL.InsertGroup(GroupObj);

            //                if (rc == 0)
            //                {
            //                    MessageBox.Show(GroupObj.GroupName + " " + "saved successfully", "Success");

            //                }
            //                else
            //                {
            //                    MessageBox.Show(GroupObj.GroupName + " " + "not saved successfully", "Error");
            //                }
            //            }
            //            catch (Exception ex)
            //            {
            //                MessageBox.Show(ex.Message, "Error");
            //            }
            //            Refresh();
            //            return;
            //        }
            //        else
            //        {
            //            MessageBox.Show("Do sure all boxes  are not empty");
            //        }






            //        if ((txtProjectDetailID.Text.Length != 0 || txtProjectName.Text.Length != 0 || txtProjectDescription.Text.Length != 0))
            //        {
            //            try
            //            {

            //                ProjectObj = new ProjectDetail();


            //                ProjectObj.ProjectDescription = txtProjectDescription.Text;
            //                ProjectObj.ProjectID = Convert.ToInt32(txtProjectDetailID.Text);
            //                ProjectObj.ProjectName = txtProjectName.Text;

            //                int rc = groupBL.InsertProject(ProjectObj);


            //                if (rc == 0)
            //                {
            //                    MessageBox.Show(ProjectObj.ProjectName + " " + "saved successfully", "Success");

            //                }
            //                else
            //                {
            //                    MessageBox.Show(ProjectObj.ProjectName + " " + "not saved successfully", "Error");
            //                }
            //            }
            //            catch (Exception ex)
            //            {
            //                MessageBox.Show(ex.Message, "Error");
            //            }
            //            Refresh();
            //            return;
            //        }
            //        else
            //        {
            //            MessageBox.Show("Do sure all boxes  are not empty");
            //        }


            //        if ((txtStudentID.Text.Length != 0 || txtStudentGroupID.Text.Length != 0 || txtStudentName.Text.Length != 0 || txtStudentNumber.Text.Length != 0))
            //        {
            //            try
            //            {

            //           







            //                int rc = groupBL.InsertStudent(StudentObj);


            //                if (rc == 0)
            //                {
            //                    MessageBox.Show(StudentObj.StudentName + " " + "saved successfully", "Success");

            //                }
            //                else
            //                {
            //                    MessageBox.Show(StudentObj.StudentName + " " + "not saved successfully", "Error");
            //                }
            //            }
            //            catch (Exception ex)
            //            {
            //                MessageBox.Show(ex.Message, "Error");
            //            }
            //            Refresh();
            //            return;
            //        }
            //        else
            //        {
            //            MessageBox.Show("Do sure all boxes  are not empty");
            //        }



            //    }



            //    private void btEdit_Click(object sender, RoutedEventArgs e)
            //    {

            //        if ((txtProjectDetailID.Text.Length != 0 || txtProjectName.Text.Length != 0 || txtProjectDescription.Text.Length != 0))
            //        {
            //            try
            //            {
            //                if (ListProject.Items.Count > 0)
            //                {
            //                    ProjectObj = new ProjectDetail();


            //                    ProjectObj.ProjectID = Convert.ToInt32(txtProjectDetailID.Text);
            //                    ProjectObj.ProjectDescription = txtProjectDescription.Text;
            //                    ProjectObj.ProjectName = txtProjectName.Text;

            //                    int rc = groupBL.UpdateProject(ProjectObj);
            //                    if (rc == 0)
            //                    {
            //                        MessageBox.Show(ProjectObj.ProjectName + " " + "Edited successfully", "Succcess");
            //                        Refresh();
            //                    }
            //                    else
            //                    {
            //                        MessageBox.Show(ProjectObj.ProjectName + " " + "Not edited successfully", "Error");
            //                    }
            //                }
            //            }
            //            catch (Exception ex)
            //            {
            //                MessageBox.Show(ex.Message, "Error");
            //            }
            //            return;
            //        }
            //        if ((txtGroupID.Text.Length != 0 || txtGroupProjectID.Text.Length != 0 || txtGroupName.Text.Length != 0))
            //        {

            //            try
            //            {
            //                if (ListGroup.Items.Count > 0)
            //                {
            //                    GroupObj = new GroupDetail();


            //                    GroupObj.GroupID = Convert.ToInt32(txtGroupID.Text);
            //                    GroupObj.GroupName = txtGroupName.Text;
            //                    GroupObj.ProjectID = Convert.ToInt32(txtGroupProjectID.Text);

            //                    int rc = groupBL.UpdateGroup(GroupObj);
            //                    if (rc == 0)
            //                    {
            //                        MessageBox.Show(GroupObj.GroupName + " " + "Edited successfully", "Succcess");
            //                        Refresh();
            //                    }
            //                    else
            //                    {
            //                        MessageBox.Show(GroupObj.GroupName + " " + "Not edited successfully", "Error");
            //                    }
            //                }
            //            }
            //            catch (Exception ex)
            //            {
            //                MessageBox.Show(ex.Message, "Error");
            //            }
            //            return;
            //        }

            //        if ((txtStudentID.Text.Length != 0 || txtStudentGroupID.Text.Length != 0 || txtStudentName.Text.Length != 0 || txtStudentNumber.Text.Length != 0))
            //        {
            //            try
            //            {
            //                if (ListStudent.Items.Count > 0)
            //                {
            //                    StudentObj = new StudentDetail();


            //                    StudentObj.GroupID = Convert.ToInt32(txtStudentGroupID.Text);
            //                    StudentObj.StudentId = Convert.ToInt32(txtStudentID.Text);
            //                    StudentObj.StudentName = txtStudentName.Text;
            //                    StudentObj.StudentNumber = Convert.ToInt32(txtStudentNumber.Text);

            //                    int rc = groupBL.UpdateStudent(StudentObj);
            //                    if (rc == 0)//test if 0 is returned
            //                    {
            //                        MessageBox.Show(StudentObj.StudentName + " " + "Edited successfully", "Succcess");
            //                        Refresh();//refresh your listview for changes to take eefect
            //                    }
            //                    else
            //                    {
            //                        MessageBox.Show(StudentObj.StudentName + " " + "Not edited successfully", "Error");
            //                    }
            //                }
            //            }
            //            catch (Exception ex)
            //            {
            //                MessageBox.Show(ex.Message, "Error");
            //            }
            //            return;
            //        }


            //    }
            //    private void StudentDetailEnter(object sender, MouseEventArgs e)
            //    {
            //        //var bc = new BrushConverter();
            //        //ShadowAssist.SetShadowDepth((ColorZone)sender, ShadowDepth.Depth5);
            //        //studentDetailsColorZone.Background = (Brush)bc.ConvertFrom("#FF02ADEC");
            //    }

            //    private void studentDetailsLeave(object sender, MouseEventArgs e)
            //    {
            //        //var bc = new BrushConverter();
            //        //ShadowAssist.SetShadowDepth((ColorZone)sender, ShadowDepth.Depth0);
            //        //studentDetailsColorZone.Background = (Brush)bc.ConvertFrom("#ffffff");
            //    }

            //    private void ProjectDetailsEnter(object sender, MouseEventArgs e)
            //    {

            //        //var bc = new BrushConverter();

            //        //studentDetailsColorZone.Background = (Brush)bc.ConvertFrom("#FF02ADEC");
            //    }

            //    private void ProjectDetailsLeave(object sender, MouseEventArgs e)
            //    {
            //        //    var bc = new BrushConverter();

            //        //    studentDetailsColorZone.Background = (Brush)bc.ConvertFrom("#ffffff");
            //    }

            //    private void ColorZone_Loaded(object sender, RoutedEventArgs e)
            //    {
            //    }

            //    private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
            //    {
            //    }

            //    private void listView_Loaded(object sender, RoutedEventArgs e)
            //    {
            //        Project = new List<ProjectDetail>();
            //        Group = new List<GroupDetail>();
            //        Student = new List<StudentDetail>();

            //        try
            //        {
            //            Project = groupBL.SelectAllProjects();//now we use the business layer class to ask the DAL class to give us a list of people
            //            if (Project.Count != 0)//we only want to poplulate our listview if there are people found
            //            {
            //                ListProject.DataContext = Project;

            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show(ex.Message, "Error");
            //        }


            //        try
            //        {
            //            Group = groupBL.SelectAllGroups();//now we use the business layer class to ask the DAL class to give us a list of people
            //            if (Project.Count != 0)//we only want to poplulate our listview if there are people found
            //            {
            //                ListGroup.DataContext = Group;

            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show(ex.Message, "Error");
            //        }

            //        try
            //        {
            //            Student = groupBL.SelectAllStudents();//now we use the business layer class to ask the DAL class to give us a list of people
            //            if (Project.Count != 0)//we only want to poplulate our listview if there are people found
            //            {
            //                ListStudent.DataContext = Student;

            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show(ex.Message, "Error");
            //        }


            //    }
            //    void Refresh()
            //    {


            //        try
            //        {
            //            txtGroupID.Text = String.Empty;
            //            txtGroupName.Text = String.Empty;
            //            txtGroupProjectID.Text = String.Empty;
            //            txtProjectDescription.Text = String.Empty;
            //            txtProjectDetailID.Text = String.Empty;
            //            txtProjectName.Text = String.Empty;
            //            txtStudentGroupID.Text = String.Empty;
            //            txtStudentID.Text = String.Empty;
            //            txtStudentName.Text = String.Empty;
            //            txtProjectDetailID.Text = String.Empty;
            //            txtStudentNumber.Text = String.Empty;


            //            Student = new List<StudentDetail>();
            //            Project = new List<ProjectDetail>();
            //            Group = new List<GroupDetail>();

            //            Student = groupBL.SelectAllStudents();
            //            Project = groupBL.SelectAllProjects();
            //            Group = groupBL.SelectAllGroups();



            //            if (Student.Count != 0)
            //            {
            //                ListStudent.DataContext = null;
            //                ListStudent.DataContext = Student;

            //            }

            //            if (Group.Count != 0)
            //            {
            //                ListGroup.DataContext = null;
            //                ListGroup.DataContext = Group;

            //            }

            //            if (Project.Count != 0)
            //            {
            //                ListProject.DataContext = null;
            //                ListProject.DataContext = Project;

            //            }

            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show(ex.Message, "Error");
            //        }
            //    }

            //    private void ListProject_SelectionChanged(object sender, SelectionChangedEventArgs e)
            //    {
            //        if (ListProject.Items.Count > 0)
            //        {
            //            ProjectObj = new ProjectDetail();
            //            ProjectObj = ListProject.SelectedItem as ProjectDetail;
            //            txtProjectDescription.Text = ProjectObj.ProjectDescription.ToString();
            //            txtProjectDetailID.Text = ProjectObj.ProjectID.ToString();
            //            txtProjectName.Text = ProjectObj.ProjectName;
            //        }
            //    }

            //    private void ListGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
            //    {

            //        if (ListGroup.Items.Count > 0)
            //        {
            //            GroupObj = new GroupDetail();
            //            GroupObj = ListGroup.SelectedItem as GroupDetail;
            //            txtGroupName.Text = GroupObj.GroupName.ToString();
            //            txtGroupID.Text = GroupObj.GroupID.ToString();
            //            txtGroupProjectID.Text = GroupObj.ProjectID.ToString();
            //        }
            //    }

            //    private void ListStudent_SelectionChanged(object sender, SelectionChangedEventArgs e)
            //    {
            //        if (ListStudent.Items.Count > 0)
            //        {
            //            StudentObj = new StudentDetail();
            //            StudentObj = ListStudent.SelectedItem as StudentDetail;
            //            txtStudentGroupID.Text = StudentObj.GroupID.ToString();
            //            txtStudentID.Text = StudentObj.StudentId.ToString();
            //            txtStudentName.Text = StudentObj.StudentName.ToString();
            //            txtStudentNumber.Text = StudentObj.StudentNumber.ToString();
            //        }
            //    }

            //    private void Page_Loaded(object sender, RoutedEventArgs e)
            //    {

            //    }
            //}
        }
    }
}
