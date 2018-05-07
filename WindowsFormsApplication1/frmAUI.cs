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
using static WindowsFormsApplication1.Validator;
namespace WindowsFormsApplication1
{
    public partial class frmAUI : Form
    {
        public frmAUI()
        {
            InitializeComponent();
        }
        public bool btnModifyClicked;
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
        public Company newCompany;
        public Contact contact;
        public Contact newContact;
        public ContactPosition contactPosition;
        public ContactPosition newContactPosition;
        public Interview interview;
        public Interview newInterview;
        public Position position;
        public Position newPostion;
        public Resume resume;
        public Resume newResume;
        public School school;
        public School newSchool;
        public List<string> listComboBox1 = new List<string>();
        private frmPRG299 frm = new frmPRG299();

        private void frmAUI_Load(object sender, EventArgs e)
        {
            btnModifyClicked = true;
            StateComboBoxes();
            if (addMenuClicked)
            {
                btnInsertUpdate.Text = "Add";
                if (Text == "Add Client") // TEXT IS THE FORM.TEXT
                {
                    newClient = new Client();
                    stateList = StateDB.GetStateList();
                    stateComboBox.DataSource = stateList;
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
                    interviewBindingSource.Add(newInterview);
                }
                if (Text == "Add Position")
                {
                    newPostion = new Position();
                    positionBindingSource.Clear();
                    positionBindingSource.Add(newPostion);
                }
                if (Text == "Add Resume")
                {
                    newResume = new Resume();
                    resumeBindingSource.Clear();
                    resumeBindingSource.Add(newResume);
                }
                if (Text == "Add School")
                {
                    newSchool = new School();
                    schoolBindingSource.Clear();
                    schoolBindingSource.Add(newSchool);
                }
            }
            else
            {
                btnInsertUpdate.Text = "Modify";
                if (Text == "Modify Client")
                {
                    newClient = new Client();
                    stateComboBox.DataSource = stateList;
                    PutNewClient();
                    clientBindingSource.Clear();
                    clientBindingSource.Add(newClient);

                }

                if (Text == "Modify Company")
                {
                    newCompany = new Company();
                    PutNewCompany();
                    companyBindingSource.Clear();
                    companyBindingSource.Add(newCompany);
                }

                if (Text == "Modify Contact")
                {
                    newContact = new Contact();
                    PutNewContact();
                    contactBindingSource.Clear();
                    contactBindingSource.Add(newContact);
                }

                if (Text == "Modify Contact Position")
                {
                    newContactPosition = new ContactPosition();
                    PutNewContactPosition();
                    contactPositionBindingSource.Clear();
                    contactPositionBindingSource.Add(newContactPosition);
                }

                if (Text == "Modify Interview")
                {
                    newInterview = new Interview();
                    PutNewInterview();
                    interviewBindingSource.Clear();
                    interviewBindingSource.Add(newInterview);
                }

                if (Text == "Modify Position")
                {
                    newPostion = new Position();
                    PutNewPosition();
                    positionBindingSource.Clear();
                    positionBindingSource.Add(newPostion);
                }

                if (Text == "Modify Resume")
                {
                    newResume = new Resume();
                    PutNewResume();
                    resumeBindingSource.Clear();
                    resumeBindingSource.Add(newResume);
                }

                if (Text == "Modify School")
                {
                    newSchool = new School();
                    PutNewSchool();
                    schoolBindingSource.Clear();
                    schoolBindingSource.Add(newSchool);
                }
            }
        }

        private void StateComboBoxes() // FILLS IN THE COMBOBOXES.COLLECTIONS WITH STATE NAMES
        {
            try
            {
             //  stateList = StateDB.GetStateList();
                if (newClient != null || newCompany != null || newSchool != null)
                {
                    if (cliLVVisible)
                    {
                        stateComboBox.SelectedValue = newClient.State;
                    }
                    else if (comLVVisible)
                    {
                        stateComboBox1.SelectedValue = newCompany.State;
                    }
                    else if (schLVVisible)
                    {
                        stateComboBox2.SelectedValue = newSchool.State;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }
        public void PutNewClient() // SETS THE NEW CLIENT 
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }
        public void PutNewCompany() // SETS THE NEW COMPANY
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }

        }
        private void PutNewContact() // SETS THE NEW CONTACT
        {
            try
            { 
                newContact.ContactID = contact.ContactID;
                newContact.FirstName = contact.FirstName;
                newContact.LastName = contact.LastName;
                newContact.EmailAddress = contact.EmailAddress;
                newContact.PhoneNumber = contact.PhoneNumber;
                newContact.CellPhone = contact.CellPhone;
                newContact.AdditionalNotes = contact.AdditionalNotes;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }

        }

        private void PutNewContactPosition() // SETS THE NEW CONTACT POSITION
        {
            try { 
            newContactPosition.ContactID = contactPosition.ContactID;
            newContactPosition.PositionID = contactPosition.PositionID;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }

        }
        private void PutNewInterview() //SETS THE NEW INTERVIEW
        {
            try
            {
                newInterview.InterviewID = interview.InterviewID;
                newInterview.PositionID = interview.PositionID;
                newInterview.CompanyID = interview.CompanyID;
                newInterview.ContactID = interview.ContactID;
                newInterview.DateTimeInterview = interview.DateTimeInterview;
                newInterview.AdditionalNotes = interview.AdditionalNotes;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }

        }
        private void PutNewPosition() // SETS THE NEW POSITION
        {
            try { 
            newPostion.PositionID = position.PositionID;
            newPostion.PositionName = position.PositionName;
            newPostion.Description = position.Description;
            newPostion.CompanyID = position.CompanyID;
            newPostion.AdditionalNotes = position.AdditionalNotes;
            newPostion.ResumeID = position.ResumeID;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }

        }
        private void PutNewResume() //SETS THE NEW RESUME
        {
            try { 
            newResume.ResumeID = resume.ResumeID;
            newResume.RSCDirectoryPath = resume.RSCDirectoryPath;
            newResume.SchoolID = resume.SchoolID;
            newResume.SchoolID = resume.SchoolID;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }

        }
        private void PutNewSchool() //SETS THE NEW SCHOOL
        {
            try { 
            newSchool.SchoolID = school.SchoolID;
            newSchool.SchoolName = school.SchoolName;
            newSchool.StreetName = school.StreetName;
            newSchool.City = school.City;
            newSchool.State = school.State;
            newSchool.ZipCode = school.ZipCode;
            newSchool.NumberOfYearsAttended = school.NumberOfYearsAttended;
            newSchool.Graduated = school.Graduated;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }

        }

        public void AllLVVisible(string visible) // CHECKS A STRING AND SETS WHICH ONE OF THE LIST VIEW ARE VISIBLE
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
        public void AllListView() //CALLS ALL THE LISTVIEWS METHODS
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

        private void ClientListView() // CHECKS WHICH CONTROLS TO SET VISIBLE TO TRUE OR FALSE  
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
        private void CompanyListView()// CHECKS WHICH CONTROLS TO SET VISIBLE TO TRUE OR FALSE
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
        private void ContactListView()// CHECKS WHICH CONTROLS TO SET VISIBLE TO TRUE OR FALSE
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
        private void ContactPositionListView()// CHECKS WHICH CONTROLS TO SET VISIBLE TO TRUE OR FALSE
        {
            if (conPosLVVisible)
            {
                contactIDLabel1.Visible = true;
                contactIDComboBox1.Visible = true;
                positionIDLabel.Visible = true;
                positionIDComboBox.Visible = true;
            }
            else
            {
                contactIDLabel1.Visible = false;
                contactIDComboBox1.Visible = false;
                positionIDLabel.Visible = false;
                positionIDComboBox.Visible = false;
            }
        }
        private void InterviewListView()// CHECKS WHICH CONTROLS TO SET VISIBLE TO TRUE OR FALSE
        {
            if (intLVVisible)
            {
                interviewIDLabel.Visible = true;
                interviewIDTextBox.Visible = true;
                positionIDLabel1.Visible = true;
                positionIDComboBox1.Visible = true;
                companyIDLabel1.Visible = true;
                companyIDComboBox1.Visible = true;
                contactIDLabel2.Visible = true;
                contactIDComboBox2.Visible = true;
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
                positionIDComboBox1.Visible = false;
                companyIDLabel1.Visible = false;
                companyIDComboBox1.Visible = false;
                contactIDLabel2.Visible = false;
                contactIDComboBox2.Visible = false;
                dateTimeInterviewLabel.Visible = false;
                dateTimeInterviewDateTimePicker.Visible = false;
                additionalNotesLabel2.Visible = false;
                additionalNotesTextBox2.Visible = false;
            }
        }
        private void PositionListView()// CHECKS WHICH CONTROLS TO SET VISIBLE TO TRUE OR FALSE
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
                companyIDComboBox2.Visible = true;
                additionalNotesLabel3.Visible = true;
                additionalNotesTextBox3.Visible = true;
                resumeIDLabel.Visible = true;
                resumeIDComboBox.Visible = true;
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
                companyIDComboBox2.Visible = false;
                additionalNotesLabel3.Visible = false;
                additionalNotesTextBox3.Visible = false;
                resumeIDLabel.Visible = false;
                resumeIDComboBox.Visible = false;
            }
        }
        private void ResumeListView()// CHECKS WHICH CONTROLS TO SET VISIBLE TO TRUE OR FALSE
        {
            if (resLVVisible)
            {
                resumeIDLabel1.Visible = true;
                resumeIDTextBox1.Visible = true;
                rSCDirectoryPathLabel.Visible = true;
                rSCDirectoryPathTextBox.Visible = true;
                schoolIDLabel.Visible = true;
                schoolIDComboBox.Visible = true;
                clientIDLabel1.Visible = true;
                clientIDComboBox1.Visible = true;
            }
            else
            {
                resumeIDLabel1.Visible = false;
                resumeIDTextBox1.Visible = false;
                rSCDirectoryPathLabel.Visible = false;
                rSCDirectoryPathTextBox.Visible = false;
                schoolIDLabel.Visible = false;
                schoolIDComboBox.Visible = false;
                clientIDLabel1.Visible = false;
                clientIDComboBox1.Visible = false;
            }
        }
        private void SchoolListView()// CHECKS WHICH CONTROLS TO SET VISIBLE TO TRUE OR FALSE
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

        private void btnInsertUpdate_Click(object sender, EventArgs e)// CHECKS IF THE DATA ENTERED IS VALID  THEN WHICH BUTTON IS CLICKED  
        {                                                                            // EXECUTES THE ADD() OR UPDATE() METHODS 

            if (IsDataValid())
            {
                if (addMenuClicked)
                {
                    try
                    {
                        if (cliLVVisible)
                        {
                            ClientDB.AddClient(newClient);
                            client = newClient;
                            DialogResult = DialogResult.Retry;
                        }
                        if (comLVVisible)
                        {
                            CompanyDB.AddCompany(newCompany);
                            company = newCompany;
                            DialogResult = DialogResult.Retry;
                        }
                        if (conLVVisible)
                        {
                            ContactDB.AddContact(newContact);
                            contact = newContact;
                            DialogResult = DialogResult.Retry;
                        }
                        if (conPosLVVisible)
                        {
                            ContactPositionDB.AddContactPosition(newContactPosition);
                            contactPosition = newContactPosition;
                            DialogResult = DialogResult.Retry;
                        }
                        if (intLVVisible)
                        {
                            InterviewDB.AddInterview(newInterview);
                            interview = newInterview;
                            DialogResult = DialogResult.Retry;
                        }
                        if (posLVVisible)
                        {
                            PositionDB.AddPosition(newPostion);
                            position = newPostion;
                            DialogResult = DialogResult.Retry;
                        }
                        if (resLVVisible)
                        {
                            ResumeDB.AddResume(newResume);
                            resume = newResume;
                            DialogResult = DialogResult.Retry;
                        }
                        if (schLVVisible)
                        {
                            SchoolDB.AddSchool(newSchool);
                            school = newSchool;
                            DialogResult = DialogResult.Retry;
                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message, ex.GetType().ToString());
                    }
                }
                else
                {
                    try
                    {
                        if (cliLVVisible)
                        {
                            if (!ClientDB.UpdateClient(client, newClient))
                            {
                                MessageBox.Show("Client was modified or deleted", "Error");
                                DialogResult = DialogResult.Retry;
                            }
                            else
                            {
                                client = newClient;
                                DialogResult = DialogResult.OK;
                            }
                        }
                        if (comLVVisible)
                        {
                            if (!CompanyDB.UpdateCompany(company, newCompany))
                            {
                                MessageBox.Show("Company was modified or deleted", "Error");
                                DialogResult = DialogResult.Retry;
                            }
                            else
                            {
                                company = newCompany;
                                DialogResult = DialogResult.OK;
                            }
                        }
                        if (conLVVisible)
                        {
                            if (!ContactDB.UpdateContact(contact, newContact))
                            {
                                MessageBox.Show("Contact was modified or deleted", "Error");
                                DialogResult = DialogResult.Retry;
                            }
                            else
                            {
                                contact = newContact;
                                DialogResult = DialogResult.OK;
                            }
                        }
                        if (conPosLVVisible)
                        {
                            if (!ContactPositionDB.UpdateContactPosition(contactPosition, newContactPosition))
                            {
                                MessageBox.Show("Contact Position was modified or deleted", "Error");
                                DialogResult = DialogResult.Retry;
                            }
                            else
                            {
                                contactPosition = newContactPosition;
                                DialogResult = DialogResult.OK;
                            }
                        }
                        if (intLVVisible)
                        {
                            if (!InterviewDB.UpdateInterview(interview, newInterview))
                            {
                                MessageBox.Show("Interview was modified or deleted", "Error");
                                DialogResult = DialogResult.Retry;
                            }
                            else
                            {
                                interview = newInterview;
                                DialogResult = DialogResult.OK;
                            }
                        }
                        if (posLVVisible)
                        {
                            if (!PositionDB.UpdatePosition(position, newPostion))
                            {
                                MessageBox.Show("Positon was modified or deleted", "Error");
                                DialogResult = DialogResult.Retry;
                            }
                            else
                            {
                                position = newPostion;
                                DialogResult = DialogResult.OK;
                            }
                        }
                        if (resLVVisible)
                        {
                            if (!ResumeDB.UpdateResume(resume, newResume))
                            {
                                MessageBox.Show("Resume was modified or deleted", "Error");
                                DialogResult = DialogResult.Retry;
                            }
                            else
                            {
                                resume = newResume;
                                DialogResult = DialogResult.OK;
                            }
                        }
                        if (schLVVisible)
                        {
                            if (!SchoolDB.UpdateSchool(school, newSchool))
                            {
                                MessageBox.Show("School was modified or deleted", "Error");
                                DialogResult = DialogResult.Retry;
                            }
                            else
                            {
                                school = newSchool;
                                DialogResult = DialogResult.OK;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString());
                    }

                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) // CLOSES THE FORM WHICH AUTOMATICALLY DISPOSES THE ATTEMPED CHANGES 
        {
            Close();
        }

        private void btnMHover(object sender, EventArgs e) //CHANGES THE FONTSTYLE TO BOLD IN THE TEXT OF THE BUTTONS  
        {
            if (btnInsertUpdate.Focus())
                btnInsertUpdate.Font = new Font(btnInsertUpdate.Font, FontStyle.Bold);
            if (btnCancel.Focus())
                btnCancel.Font = new Font(btnCancel.Font, FontStyle.Bold);

        }

        private void btnMLeave(object sender, EventArgs e)// CHANGES THE FONTSTYLE TO NORMAL IN THE TEXT OF THE BUTTONS
        {
            btnInsertUpdate.Font = new Font(btnInsertUpdate.Font, FontStyle.Regular);
            btnCancel.Font = new Font(btnCancel.Font, FontStyle.Regular);

        }

        public bool IsDataValid()// PREFORMS A TEST ON ALL CONTROLS OF THE FORM TO SEE IF THE ENTERED DATA IS VALID 
        {
            bool isDVB = false;
            

            foreach (Control ctrl in Controls)
            {
                
                if (IsPresent(ctrl))
                {
                    isDVB = true;
                }
                else
                {
                    isDVB = false;
                    break;
                }
            }
            if (IsPhoneNumber(cellPhoneTextBox) & IsInt32(zipCodeTextBox)
                | IsPhoneNumber(cellPhoneTextBox1) & IsPhoneNumber(phoneNumberTextBox)
                | IsInt32(zipCodeTextBox1) & IsInt32(buildingNumberTextBox)
                | IsInt32(zipCodeTextBox2))
            {
                isDVB = true;
                int firstZip = 0;
                int lastZip = 0;
                int firstZip1 = 0;
                int lastZip1 = 0;
                int firstZip2 = 0;
                int lastZip2 = 0;
                try { 
                if (Text.Contains("Client"))
                {
                    firstZip = stateList[stateComboBox.SelectedIndex].FirstZipCode;
                    lastZip = stateList[stateComboBox.SelectedIndex].LastZipCode;
                }
                if (Text.Contains("Company"))
                {
                    firstZip1 = stateList[stateComboBox1.SelectedIndex].FirstZipCode;
                    lastZip1 = stateList[stateComboBox1.SelectedIndex].LastZipCode;
                }
                if (Text.Contains("School"))
                {
                    firstZip2 = stateList[stateComboBox2.SelectedIndex].FirstZipCode;
                    lastZip2 = stateList[stateComboBox2.SelectedIndex].LastZipCode;
                }
                if (IsStateZipCode(zipCodeTextBox, firstZip, lastZip)
                || IsStateZipCode(zipCodeTextBox1, firstZip1, lastZip1)
                || IsStateZipCode(zipCodeTextBox2, firstZip2, lastZip2))
                {
                    isDVB = true;
                }
                else
                    isDVB = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
                isDVB = false;


            return isDVB;
        
        }

        private void stateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (newClient != null || newCompany != null || newSchool != null)
            {
                if (cliLVVisible)
                {
                    newClient.State = (string)stateComboBox.SelectedValue;
                }
                else if (comLVVisible)
                {
                    newCompany.State = (string)stateComboBox1.SelectedValue;
                }
                else if (schLVVisible)
                {
                    newSchool.State = (string)stateComboBox2.SelectedValue;
                }
            }
        }

        private void contactIDComboBox1_Click(object sender, EventArgs e)
        {
            try
            { 
                contactBindingSource.DataSource = ContactDB.GetContactCombobox();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void positionIDComboBox_Click(object sender, EventArgs e)
        {
            try
            { 
                positionBindingSource.DataSource = PositionDB.GetPosition();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void positionIDComboBox1_Click(object sender, EventArgs e)
        {
            try
            {
                positionBindingSource.DataSource = PositionDB.GetPosition();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void clientIDComboBox1_Click(object sender, EventArgs e)
        {
            try
            { 
                clientBindingSource.DataSource = ClientDB.GetClient();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void companyIDComboBox1_Click(object sender, EventArgs e)
        {
            try
            { 
                companyBindingSource.DataSource = CompanyDB.GetCompany();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void contactIDComboBox2_Click(object sender, EventArgs e)
        {
            try
            { 
                contactBindingSource.DataSource = ContactDB.GetContact();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void companyIDComboBox2_Click(object sender, EventArgs e)
        {
            try
            { 
                companyBindingSource.DataSource = CompanyDB.GetCompany();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void resumeIDComboBox_Click(object sender, EventArgs e)
        {
            try
            {
                resumeBindingSource.DataSource = ResumeDB.GetResume();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void schoolIDComboBox_Click(object sender, EventArgs e)
        {
            try
            {
                schoolBindingSource.DataSource = SchoolDB.GetSchool();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAUI addeditform = new frmAUI();
            addeditform.ShowDialog();
        }
    }
}
