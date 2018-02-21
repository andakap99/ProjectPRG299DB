using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjectPRG299BLL;
using ProjectPRG299DB;

namespace WindowsFormsApplication1
{
    public partial class frmPRG299 : Form
    {
        private Client client;
        private Company company;
        private Contact contact;
        private ContactPosition coPo;
        private Interview interview;
        private Position position;
        private Resume resume;
        private School school;
        private List<Client> clientList;
        private List<Company> companyList;
        private List<Contact> contactList;
        private List<ContactPosition> coPoList;
        private List<Interview> interviewList;
        private List<Position> positionList;
        private List<Resume> resumeList;
        private List<School> schoolList;
        private bool btnClientClicked = false, btnCompanyClicked = false, btnContactClicked = false, btnContactPositionClicked = false, btnInterviewClicked = false, btnPositionClicked = false, btnResumeClicked = false, btnSchoolClicked = false;
        public static frmPRG299 mainForm;

        public frmPRG299()
        {
            InitializeComponent();
            
        }

        private void frmPRG299_Load(object sender, EventArgs e)
        {
            LoadClientList();
            LoadCompanyList();
            LoadContactList();
            LoadContactPostionList();
            LoadInterviewList();
            LoadPositionList();
            LoadResumeList();
            LoadSchoolList();

            //AddToolStripMenu.ToolTipText = "Add new row to the " + tabControl1.SelectedTab.Text + " table";
            //modifyToolStripMenu.ToolTipText = "Modify the selected row of the " + tabControl1.SelectedTab.Text + " table";
            //deleteToolStripMenu.ToolTipText = "Delete selected row from " + tabControl1.SelectedTab.Text + " table";


        }




        private void LoadClientList()
        {
            try
            {
                clientList = ClientDB.GetClient();
                clientDataGridView.DataSource = clientList;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }


        private void LoadCompanyList()
        {
            try
            {
                companyList = CompanyDB.GetCompany();
                companyDataGridView.DataSource = companyList;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }
        private void LoadContactList()
        {
            try
            {
                contactList = ContactDB.GetContact();
                contactDataGridView.DataSource = contactList;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }
        private void LoadContactPostionList()
        {
            try
            {
                coPoList = ContactPositionDB.GetContactPosition();
                contactPositionDataGridView.DataSource = coPoList;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }
        private void LoadInterviewList()
        {
            try
            {
                interviewList = InterviewDB.GetInterview();
                interviewDataGridView.DataSource = interviewList;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }
        private void LoadPositionList()
        {
            try
            {
                positionList = PositionDB.GetPosition();
                positionDataGridView.DataSource = positionList;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }
        private void LoadResumeList()
        {
            try
            {
                resumeList = ResumeDB.GetResume();
                resumeDataGridView.DataSource = resumeList;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }
        private void LoadSchoolList()
        {
            try
            {
                schoolList = SchoolDB.GetSchool();
                schoolDataGridView.DataSource = schoolList;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        public string AddTextToFrmAdUpInDotText()
        {
            string addingText = "";

            if (btnClientClicked)
                addingText = button1.Text;
            if (btnCompanyClicked)
                addingText = button2.Text;
            if (btnContactClicked)
                addingText = button3.Text;
            if (btnContactPositionClicked)
                addingText = button4.Text;
            if (btnInterviewClicked)
                addingText = button5.Text;
            if (btnPositionClicked)
                addingText = button6.Text;
            if (btnResumeClicked)
                addingText = button7.Text;
            if (btnSchoolClicked)
                addingText = button8.Text;
            return addingText;
        }

        private void AddModifyToolStripMenu_Click(object sender, EventArgs e)
        {
            frmAUI UpdateInsertForm = new frmAUI();
            try
            {
                if (AddToolStripMenu.Selected)
                {
                    UpdateInsertForm.Text = "Add " + AddTextToFrmAdUpInDotText();
                    UpdateInsertForm.addMenuClicked = true;
                    if(btnClientClicked)
                    {
         //               client = UpdateInsertForm.newClient;                       
                        UpdateInsertForm.stateList = StateDB.GetStateList();
                        UpdateInsertForm.stateComboBox.DataSource = UpdateInsertForm.stateList;
                        clientBindingSource.Clear();
                        clientBindingSource.Add(client);
                        clientDataGridView.DataSource = clientList;
                        UpdateInsertForm.AllLVVisible(button1.Text);
                        UpdateInsertForm.AllListView();
                    }
                    if (btnCompanyClicked)
                    {
                        company = UpdateInsertForm.newCompany;
                        UpdateInsertForm.stateList = StateDB.GetStateList();
                        UpdateInsertForm.stateComboBox1.DataSource = UpdateInsertForm.stateList;
                        UpdateInsertForm.companyBindingSource.Clear();
                        UpdateInsertForm.companyBindingSource.Add(UpdateInsertForm.newCompany);
                        companyDataGridView.DataSource = companyList;
                        UpdateInsertForm.AllLVVisible(button2.Text);
                        UpdateInsertForm.AllListView();
                    }
                     if (btnContactClicked)
                    {
                        contact = UpdateInsertForm.newContact;
                        UpdateInsertForm.contactBindingSource.Clear();
                        UpdateInsertForm.contactBindingSource.Add(UpdateInsertForm.newContact);
                        contactDataGridView.DataSource = contactList;
                        UpdateInsertForm.AllLVVisible(button3.Text);
                        UpdateInsertForm.AllListView();
                    }
                    if (btnContactPositionClicked)
                    {
                        coPo = UpdateInsertForm.newContactPosition;
                        UpdateInsertForm.contactPositionBindingSource.Clear();
                        UpdateInsertForm.contactPositionBindingSource.Add(UpdateInsertForm.newContactPosition);
                        contactPositionDataGridView.DataSource = coPoList;
                        UpdateInsertForm.AllLVVisible(button4.Text);
                        UpdateInsertForm.AllListView();
                    }
                    if (btnInterviewClicked)
                    {
                        interview = UpdateInsertForm.newInterview;
                        UpdateInsertForm.interviewBindingSource.Clear();
                        UpdateInsertForm.interviewBindingSource.Add(UpdateInsertForm.newInterview);
                        interviewDataGridView.DataSource = interviewList;
                        UpdateInsertForm.AllLVVisible(button5.Text);
                        UpdateInsertForm.AllListView();
                    }
                    if (btnPositionClicked)
                    {
                        position = UpdateInsertForm.newPostion;
                        UpdateInsertForm.positionBindingSource.Clear();
                        UpdateInsertForm.positionBindingSource.Add(UpdateInsertForm.newPostion);
                        positionDataGridView.DataSource = positionList;
                        UpdateInsertForm.AllLVVisible(button6.Text);
                        UpdateInsertForm.AllListView();
                    }
                    if (btnResumeClicked)
                    {
                        resume = UpdateInsertForm.newResume;
                        UpdateInsertForm.resumeBindingSource.Clear();
                        UpdateInsertForm.resumeBindingSource.Add(UpdateInsertForm.newResume);
                        resumeDataGridView.DataSource = resumeList;
                        UpdateInsertForm.AllLVVisible(button7.Text);
                        UpdateInsertForm.AllListView();
                    }
                    if (btnSchoolClicked)
                    {
                        school = UpdateInsertForm.newSchool;
                        UpdateInsertForm.stateList = StateDB.GetStateList();
                        UpdateInsertForm.stateComboBox2.DataSource = UpdateInsertForm.stateList;
                        UpdateInsertForm.schoolBindingSource.Clear();
                        UpdateInsertForm.schoolBindingSource.Add(UpdateInsertForm.newSchool);
                        UpdateInsertForm.AllLVVisible(button8.Text);
                        UpdateInsertForm.AllListView();
                    }                    
                }
                else
                {
                    UpdateInsertForm.Text = "Modify " + AddTextToFrmAdUpInDotText();
                    UpdateInsertForm.addMenuClicked = false;
                    if (btnClientClicked)
                    {
                       UpdateInsertForm.stateList = StateDB.GetStateList();
                        UpdateInsertForm.stateComboBox.DataSource = UpdateInsertForm.stateList;
                        UpdateInsertForm.newClient = ClientDB.GetClientByRow((int)clientDataGridView.CurrentCell.Value);
                       UpdateInsertForm.client = ClientDB.GetClientByRow((int)clientDataGridView.CurrentCell.Value);
                        UpdateInsertForm.clientBindingSource.Clear();
                        //foreach (DataGridViewRow row in clientDataGridView.Rows)
                        //{
                        //    UpdateInsertForm.newClient = (Client)row.DataBoundItem;
                            client = UpdateInsertForm.newClient;
                        //}
                        UpdateInsertForm.clientBindingSource.Add(client);
                        UpdateInsertForm.AllLVVisible(button1.Text);
                        UpdateInsertForm.AllListView();

                    }
                    else if (btnCompanyClicked)
                    {
                        UpdateInsertForm.stateList = StateDB.GetStateList();
                        UpdateInsertForm.stateComboBox1.DataSource = UpdateInsertForm.stateList;
                        UpdateInsertForm.company = CompanyDB.GetCompanyByRow((int)companyDataGridView.CurrentCell.Value);
                        UpdateInsertForm.companyBindingSource.Clear();
                        UpdateInsertForm.companyBindingSource.Add(UpdateInsertForm.company);
                        UpdateInsertForm.AllLVVisible(button2.Text);
                        UpdateInsertForm.AllListView();
                    }
                    else if (btnContactClicked)
                    {
                        UpdateInsertForm.contact = ContactDB.GetContactByRow((int)contactDataGridView.CurrentCell.Value);
                        UpdateInsertForm.contactBindingSource.Clear();
                        UpdateInsertForm.contactBindingSource.Add(UpdateInsertForm.contact);
                        UpdateInsertForm.AllLVVisible(button3.Text);
                        UpdateInsertForm.AllListView();
                    }
                    else if (btnContactPositionClicked)
                    {
                        UpdateInsertForm.contactPosition = ContactPositionDB.GetContactPositionByRow((int)contactPositionDataGridView.CurrentCell.Value);
                        UpdateInsertForm.contactPositionBindingSource.Clear();
                        UpdateInsertForm.contactPositionBindingSource.Add(UpdateInsertForm.contactPosition);
                        UpdateInsertForm.AllLVVisible(button4.Text);
                        UpdateInsertForm.AllListView();
                    }
                    else if (btnInterviewClicked)
                    {
                        UpdateInsertForm.interview = InterviewDB.GetInterviewByRow((int)interviewDataGridView.CurrentCell.Value);
                        UpdateInsertForm.interviewBindingSource.Clear();
                        UpdateInsertForm.interviewBindingSource.Add(UpdateInsertForm.interview);
                        UpdateInsertForm.AllLVVisible(button5.Text);
                        UpdateInsertForm.AllListView();
                    }
                    else if (btnPositionClicked)
                    {
                        UpdateInsertForm.position = PositionDB.GetPositionByRow((int)positionDataGridView.CurrentCell.Value);
                        UpdateInsertForm.positionBindingSource.Clear();
                        UpdateInsertForm.positionBindingSource.Add(UpdateInsertForm.position);
                        UpdateInsertForm.AllLVVisible(button6.Text);
                        UpdateInsertForm.AllListView();
                    }
                    else if (btnResumeClicked)
                    {
                        UpdateInsertForm.resume = ResumeDB.GetResumeByRow((int)resumeDataGridView.CurrentCell.Value);
                        UpdateInsertForm.resumeBindingSource.Clear();
                        UpdateInsertForm.resumeBindingSource.Add(UpdateInsertForm.resume);
                        UpdateInsertForm.AllLVVisible(button7.Text);
                        UpdateInsertForm.AllListView();
                    }
                    else if (btnSchoolClicked)
                    {
                        UpdateInsertForm.stateList = StateDB.GetStateList();
                        UpdateInsertForm.stateComboBox2.DataSource = UpdateInsertForm.stateList;
                        UpdateInsertForm.school = SchoolDB.GetSchoolByRow((int)schoolDataGridView.CurrentCell.Value);
                        UpdateInsertForm.schoolBindingSource.Clear();
                        UpdateInsertForm.schoolBindingSource.Add(UpdateInsertForm.school);
                        UpdateInsertForm.AllLVVisible(button8.Text);
                        UpdateInsertForm.AllListView();
                    }
                }
                
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message, x.GetType().ToString());
            }
            finally
            {

                UpdateInsertForm.MdiParent = mainForm;
                UpdateInsertForm.ShowDialog();
               

            }
            LoadClientList();
        }

        private void deleteToolStripMenu_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (panel3.Visible)
                {
                    if (MessageBox.Show("Are you sure you want to delete the row?", "Delete " + button1.Text + " row", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        ClientDB.DeleteClient((int)clientDataGridView.CurrentCell.Value);
                    }                
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message, x.GetType().ToString());

            }
            LoadClientList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel11.Visible = true;
            panel11.Height = button1.Height;
            panel11.Top = button1.Top;
            btnClientClicked = true; btnCompanyClicked = false; btnContactClicked = false; btnContactPositionClicked = false;
            btnInterviewClicked = false; btnPositionClicked = false; btnResumeClicked = false; btnSchoolClicked = false;
            panel3.Dock = DockStyle.Fill;
            panel3.Visible = true; panel1.Visible = false; panel9.Visible = false; panel6.Visible = false;
            panel8.Visible = false; panel5.Visible = false; panel7.Visible = false; panel4.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel11.Visible = true;
            panel11.Height = button2.Height;
            panel11.Top = button2.Top;
            btnClientClicked = false; btnCompanyClicked = true; btnContactClicked = false; btnContactPositionClicked = false;
            btnInterviewClicked = false; btnPositionClicked = false; btnResumeClicked = false; btnSchoolClicked = false;
            panel1.Dock = DockStyle.Fill;
            panel3.Visible = false; panel1.Visible = true; panel9.Visible = false; panel6.Visible = false;
            panel8.Visible = false; panel5.Visible = false; panel7.Visible = false; panel4.Visible = false;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel11.Visible = true;
            panel11.Height = button3.Height;
            panel11.Top = button3.Top;
            btnClientClicked = false; btnCompanyClicked = false; btnContactClicked = true; btnContactPositionClicked = false;
            btnInterviewClicked = false; btnPositionClicked = false; btnResumeClicked = false; btnSchoolClicked = false;
            panel9.Dock = DockStyle.Fill;
            panel3.Visible = false; panel1.Visible = false; panel9.Visible = true; panel6.Visible = false;
            panel8.Visible = false; panel5.Visible = false; panel7.Visible = false; panel4.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel11.Visible = true;
            panel11.Height = button4.Height;
            panel11.Top = button4.Top;
            btnClientClicked = false; btnCompanyClicked = false; btnContactClicked = false; btnContactPositionClicked = true;
            btnInterviewClicked = false; btnPositionClicked = false; btnResumeClicked = false; btnSchoolClicked = false;
            panel6.Dock = DockStyle.Fill;
            panel3.Visible = false; panel1.Visible = false; panel9.Visible = false; panel6.Visible = true;
            panel8.Visible = false; panel5.Visible = false; panel7.Visible = false; panel4.Visible = false;
        }


        private void button5_Click(object sender, EventArgs e)
        {
            panel11.Visible = true;
            panel11.Height = button5.Height;
            panel11.Top = button5.Top;
            btnClientClicked = false; btnCompanyClicked = false; btnContactClicked = false; btnContactPositionClicked = false;
            btnInterviewClicked = true; btnPositionClicked = false; btnResumeClicked = false; btnSchoolClicked = false;
            panel8.Dock = DockStyle.Fill;
            panel3.Visible = false; panel1.Visible = false; panel9.Visible = false; panel6.Visible = false;
            panel8.Visible = true; panel5.Visible = false; panel7.Visible = false; panel4.Visible = false;
        }



        private void button6_Click(object sender, EventArgs e)
        {
            panel11.Visible = true;
            panel11.Height = button6.Height;
            panel11.Top = button6.Top;
            btnClientClicked = false; btnCompanyClicked = false; btnContactClicked = false; btnContactPositionClicked = false;
            btnInterviewClicked = false; btnPositionClicked = true; btnResumeClicked = false; btnSchoolClicked = false;
            panel5.Dock = DockStyle.Fill;
            panel3.Visible = false; panel1.Visible = false; panel9.Visible = false; panel6.Visible = false;
            panel8.Visible = false; panel5.Visible = true; panel7.Visible = false; panel4.Visible = false;
        }

        private void companyDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel11.Visible = true;
            panel11.Height = button7.Height;
            panel11.Top = button7.Top;
            btnClientClicked = false; btnCompanyClicked = false; btnContactClicked = false; btnContactPositionClicked = false;
            btnInterviewClicked = false; btnPositionClicked = false; btnResumeClicked = true; btnSchoolClicked = false;
            panel7.Dock = DockStyle.Fill;
            panel3.Visible = false; panel1.Visible = false; panel9.Visible = false; panel6.Visible = false;
            panel8.Visible = false; panel5.Visible = false; panel7.Visible = true; panel4.Visible = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel11.Visible = true;
            panel11.Height = button8.Height;
            panel11.Top = button8.Top;
            btnClientClicked = false; btnCompanyClicked = false; btnContactClicked = false; btnContactPositionClicked = false;
            btnInterviewClicked = false; btnPositionClicked = false; btnResumeClicked = false; btnSchoolClicked = true;
            panel4.Dock = DockStyle.Fill;
            panel3.Visible = false; panel1.Visible = false; panel9.Visible = false; panel6.Visible = false;
            panel8.Visible = false; panel5.Visible = false; panel7.Visible = false; panel4.Visible = true;
        }

        private void pnlMouseHover(object sender, EventArgs e)
        {
            panel10.Width = 199;
            if (btnClientClicked) { panel11.Location = new Point(183, 0); }
            else if (btnCompanyClicked) { panel11.Location = new Point(183, 70); }
            else if (btnContactClicked) { panel11.Location = new Point(183, 140);  }
            else if (btnContactPositionClicked) { panel11.Location = new Point(183, 210); }
            else if (btnInterviewClicked) { panel11.Location = new Point(183, 280); }
            else if (btnPositionClicked) { panel11.Location = new Point(183, 350); }
            else if (btnResumeClicked) { panel11.Location = new Point(183, 420); }
            else if (btnSchoolClicked) { panel11.Location = new Point(183, 490); }

        }

        private void pnlMouseLeave(object sender, EventArgs e)
        {
            
            if (btnClientClicked) { panel11.Location = button1.Location; panel10.Width = 10; }
            else if (btnCompanyClicked) { panel11.Location = button2.Location; panel10.Width = 10; }
            else if (btnContactClicked) { panel11.Location = button3.Location; panel10.Width = 10; }
            else if (btnContactPositionClicked) { panel11.Location = button4.Location; panel10.Width = 10; }
            else if (btnInterviewClicked) { panel11.Location = button5.Location; panel10.Width = 10; }
            else if (btnPositionClicked) { panel11.Location = button6.Location; panel10.Width = 10; }
            else if (btnResumeClicked) { panel11.Location = button7.Location; panel10.Width = 10; }
            else if (btnSchoolClicked) { panel11.Location = button8.Location; panel10.Width = 10; }
            
            
        }

        private void panel2_MouseHover(object sender, EventArgs e)
        {

        }

        private void panel2_MouseLeave(object sender, EventArgs e)
        {

        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
