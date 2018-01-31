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
        private List<Client> clientList;
        private List<Company> companyList;
        private List<Contact> contactList;
        private List<ContactPosition> coPoList;
        private List<Interview> interviewList;
        private List<Position> positionList;
        private List<Resume> resumeList;
        private List<School> schoolList;
        CurrencyManager cm;
        private ContextMenu aMD;

        public frmPRG299()
        {
            InitializeComponent();
            
        }

        private void frmPRG299_Load(object sender, EventArgs e)
        {
            aMD = new ContextMenu();
            LoadClientList();

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
                cm = (CurrencyManager)companyDataGridView.BindingContext[companyList];
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
                cm = (CurrencyManager)contactDataGridView.BindingContext[contactList];
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
                cm = (CurrencyManager)contactPositionDataGridView.BindingContext[coPoList];
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
                cm = (CurrencyManager)interviewDataGridView.BindingContext[interviewList];
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
                cm = (CurrencyManager)positionDataGridView.BindingContext[positionList];
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
                cm = (CurrencyManager)resumeDataGridView.BindingContext[resumeList];
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
                cm = (CurrencyManager)schoolDataGridView.BindingContext[schoolList];
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

        private void AddToolStripMenu_MouseHover(object sender, EventArgs e)
        {
            AddToolStripMenu.ToolTipText = "Add new row to the " + tabControl1.SelectedTab.Text + " table";

        }

        private void modifyToolStripMenu_MouseHover(object sender, EventArgs e)
        {
            modifyToolStripMenu.ToolTipText = "Modify the selected row of the " + tabControl1.SelectedTab.Text + " table";
        }

        private void deleteToolStripMenu_MouseHover(object sender, EventArgs e)
        {
            deleteToolStripMenu.ToolTipText = "Delete selected row from " + tabControl1.SelectedTab.Text + " table";
        }
    }
}
