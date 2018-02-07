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

        CurrencyManager cm;
        public static frmPRG299 mainForm;
        private ContextMenu aMD;

        public frmPRG299()
        {
            InitializeComponent();
            
        }

        private void frmPRG299_Load(object sender, EventArgs e)
        {
            aMD = new ContextMenu();
            LoadClientList();
            LoadCompanyList();
            LoadContactList();
            LoadContactPostionList();
            LoadInterviewList();
            LoadPositionList();
            LoadResumeList();
            LoadSchoolList();

            AddToolStripMenu.ToolTipText = "Add new row to the " + tabControl1.SelectedTab.Text + " table";
            modifyToolStripMenu.ToolTipText = "Modify the selected row of the " + tabControl1.SelectedTab.Text + " table";
            deleteToolStripMenu.ToolTipText = "Delete selected row from " + tabControl1.SelectedTab.Text + " table";


        }




        private void LoadClientList()
        {
            try
            {
                clientList = ClientDB.GetClient();
                clientDataGridView.DataSource = clientList;
                cm = (CurrencyManager)clientDataGridView.BindingContext[clientList];
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
        private void DotShow( DataGridViewCellMouseEventArgs e)
        {
            aMD.Dispose();
            if (e.Button == MouseButtons.Right)
            {

                aMD.MenuItems.Add("Add New " + tabControl1.SelectedTab.Text + " Row");
                aMD.MenuItems.Add("Modify " + tabControl1.SelectedTab.Text + " Table");
                aMD.MenuItems.Add("Delete Row From " + tabControl1.SelectedTab.Text + " Table");
                aMD.Show(this, new Point(e.X + 5, e.Y + 80));
            }
        }
        private void clientDataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DotShow(e);
        }

        private void companyDataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DotShow(e);
        }

        private void contactDataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DotShow(e);
        }

        private void contactPositionDataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DotShow(e);
        }

        private void interviewDataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DotShow(e);
        }

        private void positionDataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DotShow(e);
        }

        private void resumeDataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DotShow(e);
        }

        private void schoolDataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DotShow(e);
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            AddToolStripMenu.ToolTipText = "Add new row to the " + tabControl1.SelectedTab.Text + " table";
            modifyToolStripMenu.ToolTipText = "Modify the selected row of the " + tabControl1.SelectedTab.Text + " table";
            deleteToolStripMenu.ToolTipText = "Delete selected row from " + tabControl1.SelectedTab.Text + " table";
        }

        private void AddModifyToolStripMenu_Click(object sender, EventArgs e)
        {
            frmAUI UpdateInsertForm = new frmAUI();
            try
            {
                if (AddToolStripMenu.Selected)
                {
                    UpdateInsertForm.Text =  "Add " + tabControl1.SelectedTab.Text;
                    UpdateInsertForm.addMenuClicked = true;
                    if(tabControl1.SelectedTab.Text == "Client")
                    {
                        client = UpdateInsertForm.newClient;                       
                        UpdateInsertForm.stateList = StateDB.GetStateList();
                        UpdateInsertForm.stateComboBox.DataSource = UpdateInsertForm.stateList;
                        UpdateInsertForm.clientBindingSource.Clear();
                        UpdateInsertForm.clientBindingSource.Add(UpdateInsertForm.newClient);
                        clientDataGridView.DataSource = clientList;
                        UpdateInsertForm.AllLVVisible(tabControl1.SelectedTab.Text);
                        UpdateInsertForm.AllListView();
                        
                    }
                    else if (tabControl1.SelectedTab.Text == "Company")
                    {
                        company = UpdateInsertForm.newCompany;
                        UpdateInsertForm.stateList = StateDB.GetStateList();
                        UpdateInsertForm.stateComboBox1.DataSource = UpdateInsertForm.stateList;
                        UpdateInsertForm.companyBindingSource.Clear();
                        UpdateInsertForm.companyBindingSource.Add(UpdateInsertForm.newCompany);
                        companyDataGridView.DataSource = companyList;
                        UpdateInsertForm.AllLVVisible(tabControl1.SelectedTab.Text);
                        UpdateInsertForm.AllListView();
                    }
                    else if (tabControl1.SelectedTab.Text == "Contact")
                    {
                        contact = UpdateInsertForm.newContact;
                        UpdateInsertForm.contactBindingSource.Clear();
                        UpdateInsertForm.contactBindingSource.Add(UpdateInsertForm.newContact);
                        contactDataGridView.DataSource = contactList;
                        UpdateInsertForm.AllLVVisible(tabControl1.SelectedTab.Text);
                        UpdateInsertForm.AllListView();
                    }
                    else if (tabControl1.SelectedTab.Text == "Contact Position")
                    {
                        coPo = UpdateInsertForm.newContactPosition;
                        UpdateInsertForm.contactPositionBindingSource.Clear();
                        UpdateInsertForm.contactPositionBindingSource.Add(UpdateInsertForm.newContactPosition);
                        contactPositionDataGridView.DataSource = coPoList;
                        UpdateInsertForm.AllLVVisible(tabControl1.SelectedTab.Text);
                        UpdateInsertForm.AllListView();
                    }
                    else if (tabControl1.SelectedTab.Text == "Interview")
                    {
                        interview = UpdateInsertForm.newInterview;
                        UpdateInsertForm.interviewBindingSource.Clear();
                        UpdateInsertForm.interviewBindingSource.Add(UpdateInsertForm.newInterview);
                        interviewDataGridView.DataSource = interviewList;
                        UpdateInsertForm.AllLVVisible(tabControl1.SelectedTab.Text);
                        UpdateInsertForm.AllListView();
                    }
                    else if (tabControl1.SelectedTab.Text == "Position")
                    {
                        position = UpdateInsertForm.newPostion;
                        UpdateInsertForm.positionBindingSource.Clear();
                        UpdateInsertForm.positionBindingSource.Add(UpdateInsertForm.newPostion);
                        positionDataGridView.DataSource = positionList;
                        UpdateInsertForm.AllLVVisible(tabControl1.SelectedTab.Text);
                        UpdateInsertForm.AllListView();
                    }
                    else if (tabControl1.SelectedTab.Text == "Resume")
                    {
                        resume = UpdateInsertForm.newResume;
                        UpdateInsertForm.resumeBindingSource.Clear();
                        UpdateInsertForm.resumeBindingSource.Add(UpdateInsertForm.newResume);
                        resumeDataGridView.DataSource = resumeList;
                        UpdateInsertForm.AllLVVisible(tabControl1.SelectedTab.Text);
                        UpdateInsertForm.AllListView();
                    }
                    else if (tabControl1.SelectedTab.Text == "School")
                    {
                        school = UpdateInsertForm.newSchool;
                        UpdateInsertForm.stateList = StateDB.GetStateList();
                        UpdateInsertForm.stateComboBox2.DataSource = UpdateInsertForm.stateList;
                        UpdateInsertForm.schoolBindingSource.Clear();
                        UpdateInsertForm.schoolBindingSource.Add(UpdateInsertForm.newSchool);
                        UpdateInsertForm.AllLVVisible(tabControl1.SelectedTab.Text);
                        UpdateInsertForm.AllListView();
                    }                    
                }
                else
                {
                    UpdateInsertForm.Text = "Modify " + tabControl1.SelectedTab.Text;
                    UpdateInsertForm.addMenuClicked = false;
                    if (tabControl1.SelectedTab.Text == "Client")
                    {
                        UpdateInsertForm.client = ClientDB.GetClientByRow((int)clientDataGridView.CurrentCell.Value);
                        UpdateInsertForm.clientBindingSource.Clear();
                        UpdateInsertForm.clientBindingSource.Add(UpdateInsertForm.client);
                        UpdateInsertForm.AllLVVisible(tabControl1.SelectedTab.Text);
                        UpdateInsertForm.AllListView();

                    }
                    else if (tabControl1.SelectedTab.Text == "Company")
                    {
                        UpdateInsertForm.company = CompanyDB.GetCompanyByRow((int)companyDataGridView.CurrentCell.Value);
                        UpdateInsertForm.companyBindingSource.Clear();
                        UpdateInsertForm.companyBindingSource.Add(UpdateInsertForm.company);
                        UpdateInsertForm.AllLVVisible(tabControl1.SelectedTab.Text);
                        UpdateInsertForm.AllListView();
                    }
                    else if (tabControl1.SelectedTab.Text == "Contact")
                    {
                        UpdateInsertForm.contact = ContactDB.GetContactByRow((int)contactDataGridView.CurrentCell.Value);
                        UpdateInsertForm.contactBindingSource.Clear();
                        UpdateInsertForm.contactBindingSource.Add(UpdateInsertForm.contact);
                        UpdateInsertForm.AllLVVisible(tabControl1.SelectedTab.Text);
                        UpdateInsertForm.AllListView();
                    }
                    else if (tabControl1.SelectedTab.Text == "Contact Position")
                    {
                        UpdateInsertForm.contactPosition = ContactPositionDB.GetContactPositionByRow((int)contactPositionDataGridView.CurrentCell.Value);
                        UpdateInsertForm.contactPositionBindingSource.Clear();
                        UpdateInsertForm.contactPositionBindingSource.Add(UpdateInsertForm.contactPosition);
                        UpdateInsertForm.AllLVVisible(tabControl1.SelectedTab.Text);
                        UpdateInsertForm.AllListView();
                    }
                    else if (tabControl1.SelectedTab.Text == "Interview")
                    {
                        UpdateInsertForm.interview = InterviewDB.GetInterviewByRow((int)interviewDataGridView.CurrentCell.Value);
                        UpdateInsertForm.interviewBindingSource.Clear();
                        UpdateInsertForm.interviewBindingSource.Add(UpdateInsertForm.interview);
                        UpdateInsertForm.AllLVVisible(tabControl1.SelectedTab.Text);
                        UpdateInsertForm.AllListView();
                    }
                    else if (tabControl1.SelectedTab.Text == "Position")
                    {
                        UpdateInsertForm.position = PositionDB.GetPositionByRow((int)positionDataGridView.CurrentCell.Value);
                        UpdateInsertForm.positionBindingSource.Clear();
                        UpdateInsertForm.positionBindingSource.Add(UpdateInsertForm.position);
                        UpdateInsertForm.AllLVVisible(tabControl1.SelectedTab.Text);
                        UpdateInsertForm.AllListView();
                    }
                    else if (tabControl1.SelectedTab.Text == "Resume")
                    {
                        UpdateInsertForm.resume = ResumeDB.GetResumeByRow((int)resumeDataGridView.CurrentCell.Value);
                        UpdateInsertForm.resumeBindingSource.Clear();
                        UpdateInsertForm.resumeBindingSource.Add(UpdateInsertForm.resume);
                        UpdateInsertForm.AllLVVisible(tabControl1.SelectedTab.Text);
                        UpdateInsertForm.AllListView();
                    }
                    else if (tabControl1.SelectedTab.Text == "School")
                    {
                        UpdateInsertForm.school = SchoolDB.GetSchoolByRow((int)schoolDataGridView.CurrentCell.Value);
                        UpdateInsertForm.schoolBindingSource.Clear();
                        UpdateInsertForm.schoolBindingSource.Add(UpdateInsertForm.school);
                        UpdateInsertForm.AllLVVisible(tabControl1.SelectedTab.Text);
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
        }

    }
}
