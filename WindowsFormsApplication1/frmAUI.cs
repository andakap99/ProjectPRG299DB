using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjectPRG299BLL;
using ProjectPRG299DB;
namespace WindowsFormsApplication1
{
    public partial class frmAUI : Form
    {
        public frmAUI()
        {
            InitializeComponent();
        }
        public bool addMenuClicked;
        public bool cliLVVisible = false;
        public bool comLVVisible = false;
        public bool conLVVisible = false;
        public bool conPosLVVisible = false;
        public bool intLVVisible = false;
        public bool posLVVisible = false;
        public bool resLVVisible = false;
        public bool schLVVisible = false;
        public List<State> stateList;
        public Client client;
        public Client newClient;
        public Company company;
        private Company newCompany;
        public Contact contact;
        private Contact newContact;
        public ContactPosition contactPosition;
        private ContactPosition newContactPosition;
        public Interview interview;
        private Interview newInterview;
        public Position position;
        private Position newPostion;
        public Resume resume;
        private Resume newResume;
        public School school;
        private School newSchool;

        private void frmAUI_Load(object sender, EventArgs e)
        {

            StateComboBoxes();
            if (addMenuClicked)
            {
                btnInsertUpdate.Text = "Add";
                if (Text == "Add Client")
                {
                    newClient = new Client();
                    clientBindingSource.Clear();
                    clientBindingSource.Add(newClient);

                }
                if (Text == "Add Company")
                {
                    newCompany = new Company();
                    companyBindingSource.Clear();
                    companyBindingSource.Add(newCompany);
                }
                if (Text == "Add Contact")
                {
                    newContact = new Contact();
                    contactBindingSource.Clear();
                    contactBindingSource.Add(newContact);
                }
                if (Text == "Add Contact Position")
                {
                    newContactPosition = new ContactPosition();
                    contactPositionBindingSource.Clear();
                    contactPositionBindingSource.Add(newContactPosition);
                }
                if (Text == "Add Interview")
                {
                    newInterview = new Interview();
                    interviewBindingSource.Clear();
                }
                if (Text == "Add Position")
                {
                    newPostion = new Position();

                }
                if (Text == "Add Resume")
                {
                    newResume = new Resume();
                }
                if (Text == "Add School")
                {
                    newSchool = new School();
                }
            }
            else
            {
                btnInsertUpdate.Text = "Modify";
                if (Text == "Modify Client")
                {
                    newClient = new Client();
                    PutNewClient();
                }

                if (Text == "Modify Company")
                {
                    newCompany = new Company();
                    PutNewCompany();
                }

                if (Text == "Modify Contact")
                {
                    newContact = new Contact();
                    PutNewContact();
                }

                if (Text == "Modify Contact Position")
                {
                    newContactPosition = new ContactPosition();
                    PutNewContactPosition();
                }

                if (Text == "Modify Interview")
                {
                    newInterview = new Interview();
                    PutNewInterview();
                }

                if (Text == "Modify Position")
                {
                    newPostion = new Position();
                    PutNewPosition();
                }

                if (Text == "Modify Resume")
                {
                    newResume = new Resume();
                    PutNewResume();
                }

                if (Text == "Modify School")
                {
                    newSchool = new School();
                    PutNewSchool();
                }
            }
        }

        private void StateComboBoxes()
        {
            try
            {
                stateList = StateDB.GetStateList();
                //stateComboBox.DataSource = stateList;
                //stateComboBox1.DataSource = stateList;
                stateComboBox2.DataSource = stateList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }
        private void PutNewClient()
        {
            newClient.ClientID = client.ClientID;
            newClient.FirstName = client.FirstName;
            newClient.LastName = client.LastName;
            newClient.BirthDate = client.BirthDate;
            newClient.StreetName = client.StreetName;
            newClient.City = client.City;
            newClient.State = client.State;
            newClient.ZipCode = client.ZipCode;
            newClient.CellPhone = client.CellPhone;                      
        }
        private void PutNewCompany()
        {
            newCompany.CompanyID = company.CompanyID;
            newCompany.CompanyName = company.CompanyName;
            newCompany.BuildingName = company.BuildingName;
            newCompany.BuildingNumber = company.BuildingNumber;
            newCompany.StreetAddress = company.StreetAddress;
            newCompany.City = company.City;
            newCompany.State = company.State;
            newCompany.ZipCode = company.ZipCode;
            newCompany.Website = company.Website;
            newCompany.AdditionalNotes = company.AdditionalNotes;
        }
        private void PutNewContact()
        {
            newContact.ContactID = contact.ContactID;
            newContact.FirstName = contact.FirstName;
            newContact.LastName = contact.LastName;
            newContact.EmailAddress = contact.EmailAddress;
            newContact.PhoneNumber = contact.PhoneNumber;
            newContact.CellPhone = contact.CellPhone;
            newContact.AdditionalNotes = contact.AdditionalNotes;
        }
        private void PutNewContactPosition()
        {
            newContactPosition.ContactID = contactPosition.ContactID;
            newContactPosition.PositionID = contactPosition.PositionID;
        }
        private void PutNewInterview()
        {
            newInterview.InterviewID = interview.InterviewID;
            newInterview.PositionID = interview.PositionID;
            newInterview.CompanyID = interview.CompanyID;
            newInterview.ContactID = interview.ContactID;
            newInterview.DateTimeInterview = interview.DateTimeInterview;
            newInterview.AdditionalNotes = interview.AdditionalNotes;
        }
        private void PutNewPosition()
        {
            newPostion.PositionID = position.PositionID;
            newPostion.PositionName = position.PositionName;
            newPostion.Description = position.Description;
            newPostion.CompanyID = position.CompanyID;
            newPostion.AdditionalNotes = position.AdditionalNotes;
            newPostion.ResumeID = position.ResumeID;
        }
        private void PutNewResume()
        {
            newResume.ResumeID = resume.ResumeID;
            newResume.RSCDirectoryPath = resume.RSCDirectoryPath;
            newResume.SchoolID = resume.SchoolID;
            newResume.SchoolID = resume.SchoolID;
        }
        private void PutNewSchool()
        {
            newSchool.SchoolID = school.SchoolID;
            newSchool.SchoolName = school.SchoolName;
            newSchool.StreetName = school.StreetName;
            newSchool.City = school.City;
            stateComboBox2.SelectedValue = school.State;
            newSchool.ZipCode = school.ZipCode;
            newSchool.NumberOfYearsAttended = school.NumberOfYearsAttended;
            newSchool.Graduated = school.Graduated;
        }

        public void AllLVVisible(string visible)
        {
            if ("Client" != visible)
                cliLVVisible = false;
            else
                cliLVVisible = true;
            if ("Company" != visible)
                comLVVisible = false;
            else
                comLVVisible = true;
            if ("Contact" != visible)
                conLVVisible = false;
            else
                conLVVisible = true;
            if ("Contact Position" != visible)
                conPosLVVisible = false;
            else
                conPosLVVisible = true;
            if ("Interview" != visible)
                intLVVisible = false;
            else
                intLVVisible = true;
            if ("Position" != visible)
                posLVVisible = false;
            else
                posLVVisible = true;
            if ("Resume" != visible)
                resLVVisible = false;
            else
                resLVVisible = true;
            if ("School" != visible)
                schLVVisible = false;
            else
                schLVVisible = true;
        }
        public void AllListView()
        {
            ClientListView();
            CompanyListView();
            ContactListView();
            ContactPositionListView();
            InterviewListView();
            PositionListView();
            ResumeListView();
            SchoolListView();
        }

        private void ClientListView()
        {
            if (cliLVVisible)
            {
                clientIDLabel.Visible = true;
                clientIDTextBox.Visible = true;
                firstNameLabel.Visible = true;
                firstNameTextBox.Visible = true;
                lastNameLabel.Visible = true;
                lastNameTextBox.Visible = true;
                birthDateLabel.Visible = true;
                birthDateDateTimePicker.Visible = true;
                streetNameLabel.Visible = true;
                streetNameTextBox.Visible = true;
                cityLabel.Visible = true;
                cityTextBox.Visible = true;
                stateLabel.Visible = true;
                stateComboBox.Visible = true;
                zipCodeLabel.Visible = true;
                zipCodeTextBox.Visible = true;
                cellPhoneLabel.Visible = true;
                cellPhoneTextBox.Visible = true;
            }
            else
            {
                clientIDLabel.Visible = false;
                clientIDTextBox.Visible = false;
                firstNameLabel.Visible = false;
                firstNameTextBox.Visible = false;
                lastNameLabel.Visible = false;
                lastNameTextBox.Visible = false;
                birthDateLabel.Visible = false;
                birthDateDateTimePicker.Visible = false;
                streetNameLabel.Visible = false;
                streetNameTextBox.Visible = false;
                cityLabel.Visible = false;
                cityTextBox.Visible = false;
                stateLabel.Visible = false;
                stateComboBox.Visible = false;
                zipCodeLabel.Visible = false;
                zipCodeTextBox.Visible = false;
                cellPhoneLabel.Visible = false;
                cellPhoneTextBox.Visible = false;
            }
        }
        private void CompanyListView()
        {
            if (comLVVisible)
            {
                companyIDLabel.Visible = true;
                companyIDTextBox.Visible = true;
                companyNameLabel.Visible = true;
                companyNameTextBox.Visible = true;
                buildingNameLabel.Visible = true;
                buildingNameTextBox.Visible = true;
                buildingNumberLabel.Visible = true;
                buildingNumberTextBox.Visible = true;
                streetAddressLabel.Visible = true;
                streetAddressTextBox.Visible = true;
                cityLabel1.Visible = true;
                cityTextBox1.Visible = true;
                stateLabel1.Visible = true;
                stateComboBox1.Visible = true;
                zipCodeLabel1.Visible = true;
                zipCodeTextBox1.Visible = true;
                websiteLabel.Visible = true;
                websiteTextBox.Visible = true;
                additionalNotesLabel.Visible = true;
                additionalNotesTextBox.Visible = true;
            }
            else
            {
                companyIDLabel.Visible = false;
                companyIDTextBox.Visible = false;
                companyNameLabel.Visible = false;
                companyNameTextBox.Visible = false;
                buildingNameLabel.Visible = false;
                buildingNameTextBox.Visible = false;
                buildingNumberLabel.Visible = false;
                buildingNumberTextBox.Visible = false;
                streetAddressLabel.Visible = false;
                streetAddressTextBox.Visible = false;
                cityLabel1.Visible = false;
                cityTextBox1.Visible = false;
                stateLabel1.Visible = false;
                stateComboBox1.Visible = false;
                zipCodeLabel1.Visible = false;
                zipCodeTextBox1.Visible = false;
                websiteLabel.Visible = false;
                websiteTextBox.Visible = false;
                additionalNotesLabel.Visible = false;
                additionalNotesTextBox.Visible = false;
            }
        }
        private void ContactListView()
        {
            if (conLVVisible)
            {
                contactIDLabel.Visible = true;
                contactIDTextBox.Visible = true;
                firstNameLabel1.Visible = true;
                firstNameTextBox1.Visible = true;
                lastNameLabel1.Visible = true;
                lastNameTextBox1.Visible = true;
                emailAddressLabel.Visible = true;
                emailAddressTextBox.Visible = true;
                phoneNumberLabel.Visible = true;
                phoneNumberTextBox.Visible = true;
                cellPhoneLabel1.Visible = true;
                cellPhoneTextBox1.Visible = true;
                additionalNotesLabel1.Visible = true;
                additionalNotesTextBox1.Visible = true;
            }
            else
            {
                contactIDLabel.Visible = false;
                contactIDTextBox.Visible = false;
                firstNameLabel1.Visible = false;
                firstNameTextBox1.Visible = false;
                lastNameLabel1.Visible = false;
                lastNameTextBox1.Visible = false;
                emailAddressLabel.Visible = false;
                emailAddressTextBox.Visible = false;
                phoneNumberLabel.Visible = false;
                phoneNumberTextBox.Visible = false;
                cellPhoneLabel1.Visible = false;
                cellPhoneTextBox1.Visible = false;
                additionalNotesLabel1.Visible = false;
                additionalNotesTextBox1.Visible = false;
            }
        }
        private void ContactPositionListView()
        {
            if (conPosLVVisible)
            {
                contactIDLabel1.Visible = true;
                contactIDTextBox1.Visible = true;
                positionIDLabel.Visible = true;
                positionIDTextBox.Visible = true;
            }
            else
            {
                contactIDLabel1.Visible = false;
                contactIDTextBox1.Visible = false;
                positionIDLabel.Visible = false;
                positionIDTextBox.Visible = false;
            }
        }
        private void InterviewListView()
        {
            if (intLVVisible)
            {
                interviewIDLabel.Visible = true;
                interviewIDTextBox.Visible = true;
                positionIDLabel1.Visible = true;
                positionIDTextBox1.Visible = true;
                companyIDLabel1.Visible = true;
                companyIDTextBox1.Visible = true;
                contactIDLabel2.Visible = true;
                contactIDTextBox2.Visible = true;
                dateTimeInterviewLabel.Visible = true;
                dateTimeInterviewDateTimePicker.Visible = true;
                additionalNotesLabel2.Visible = true;
                additionalNotesTextBox2.Visible = true;
            }
            else
            {
                interviewIDLabel.Visible = false;
                interviewIDTextBox.Visible = false;
                positionIDLabel1.Visible = false;
                positionIDTextBox1.Visible = false;
                companyIDLabel1.Visible = false;
                companyIDTextBox1.Visible = false;
                contactIDLabel2.Visible = false;
                contactIDTextBox2.Visible = false;
                dateTimeInterviewLabel.Visible = false;
                dateTimeInterviewDateTimePicker.Visible = false;
                additionalNotesLabel2.Visible = false;
                additionalNotesTextBox2.Visible = false;
            }
        }
        private void PositionListView()
        {
            if (posLVVisible)
            {
                positionIDLabel2.Visible = true;
                positionIDTextBox2.Visible = true;
                positionNameLabel.Visible = true;
                positionNameTextBox.Visible = true;
                descriptionLabel.Visible = true;
                descriptionTextBox.Visible = true;
                companyIDLabel2.Visible = true;
                companyIDTextBox2.Visible = true;
                additionalNotesLabel3.Visible = true;
                additionalNotesTextBox3.Visible = true;
                resumeIDLabel.Visible = true;
                resumeIDTextBox.Visible = true;
            }
            else
            {
                positionIDLabel2.Visible = false;
                positionIDTextBox2.Visible = false;
                positionNameLabel.Visible = false;
                positionNameTextBox.Visible = false;
                descriptionLabel.Visible = false;
                descriptionTextBox.Visible = false;
                companyIDLabel2.Visible = false;
                companyIDTextBox2.Visible = false;
                additionalNotesLabel3.Visible = false;
                additionalNotesTextBox3.Visible = false;
                resumeIDLabel.Visible = false;
                resumeIDTextBox.Visible = false;
            }
        }
        private void ResumeListView()
        {
            if (resLVVisible)
            {
                resumeIDLabel1.Visible = true;
                resumeIDTextBox1.Visible = true;
                rSCDirectoryPathLabel.Visible = true;
                rSCDirectoryPathTextBox.Visible = true;
                schoolIDLabel.Visible = true;
                schoolIDTextBox.Visible = true;
                clientIDLabel1.Visible = true;
                clientIDTextBox1.Visible = true;
            }
            else
            {
                resumeIDLabel1.Visible = false;
                resumeIDTextBox1.Visible = false;
                rSCDirectoryPathLabel.Visible = false;
                rSCDirectoryPathTextBox.Visible = false;
                schoolIDLabel.Visible = false;
                schoolIDTextBox.Visible = false;
                clientIDLabel1.Visible = false;
                clientIDTextBox1.Visible = false;
            }
        }
        private void SchoolListView()
        {
            if (schLVVisible)
            {
                schoolIDLabel1.Visible = true;
                schoolIDTextBox1.Visible = true;
                schoolNameLabel.Visible = true;
                schoolNameTextBox.Visible = true;
                streetNameLabel1.Visible = true;
                streetNameTextBox1.Visible = true;
                cityLabel2.Visible = true;
                cityTextBox2.Visible = true;
                stateLabel2.Visible = true;
                stateComboBox2.Visible = true;
                zipCodeLabel2.Visible = true;
                zipCodeTextBox2.Visible = true;
                numberOfYearsAttendedLabel.Visible = true;
                numberOfYearsAttendedTextBox.Visible = true;
                graduatedLabel.Visible = true;
                graduatedTextBox.Visible = true;
            }
            else
            {
                schoolIDLabel1.Visible = false;
                schoolIDTextBox1.Visible = false;
                schoolNameLabel.Visible = false;
                schoolNameTextBox.Visible = false;
                streetNameLabel1.Visible = false;
                streetNameTextBox1.Visible = false;
                cityLabel2.Visible = false;
                cityTextBox2.Visible = false;
                stateLabel2.Visible = false;
                stateComboBox2.Visible = false;
                zipCodeLabel2.Visible = false;
                zipCodeTextBox2.Visible = false;
                numberOfYearsAttendedLabel.Visible = false;
                numberOfYearsAttendedTextBox.Visible = false;
                graduatedLabel.Visible = false;
                graduatedTextBox.Visible = false;
            }
        }

        private void btnInsertUpdate_Click(object sender, EventArgs e)
        {
            if(addMenuClicked)
            {
                //newClient.State = stateComboBox.SelectedValue.ToString();
                //newCompany.State = stateComboBox1.SelectedValue.ToString();
                //newSchool.State = stateComboBox2.SelectedValue.ToString();
                try
                {
                    if(cliLVVisible)
                    {
 //                       newClient.BirthDate = birthDateDateTimePicker.Value;
                        ClientDB.AddClient(newClient);
                        client = newClient;
                        DialogResult = DialogResult.OK;
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }
            }
            else
            {


            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}