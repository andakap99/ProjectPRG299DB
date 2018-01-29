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
        public frmPRG299()
        {
            InitializeComponent();
            
        }

        private void frmPRG299_Load(object sender, EventArgs e)
        {
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
                cm = (CurrencyManager)companyDataGridView.BindingContext[contactList];
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
                companyDataGridView.DataSource = coPoList;
                cm = (CurrencyManager)companyDataGridView.BindingContext[coPoList];
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
                companyDataGridView.DataSource = interviewList;
                cm = (CurrencyManager)companyDataGridView.BindingContext[interviewList];
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
                companyList = CompanyDB.GetCompany();
                companyDataGridView.DataSource = companyList;
                cm = (CurrencyManager)companyDataGridView.BindingContext[companyList];
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
                companyList = CompanyDB.GetCompany();
                companyDataGridView.DataSource = companyList;
                cm = (CurrencyManager)companyDataGridView.BindingContext[companyList];
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
                companyDataGridView.DataSource = schoolList;
                cm = (CurrencyManager)companyDataGridView.BindingContext[schoolList];
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

    }
}
