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

        private void frmAUI_Load(object sender, EventArgs e)
        {
            btnModifyClicked = true;
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
        public void PutNewClient()
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
        public void PutNewCompany()
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
            if (IsDataValid())
            {
                if (addMenuClicked)
                {
                    //newClient.State = stateComboBox.SelectedValue.ToString();
                    //newCompany.State = stateComboBox1.SelectedValue.ToString();
                    //newSchool.State = stateComboBox2.SelectedValue.ToString();
                    try
                    {
                        if (cliLVVisible)
                        {
                            //                       newClient.BirthDate = birthDateDateTimePicker.Value;
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnMHover(object sender, EventArgs e)
        {
            if (btnInsertUpdate.Focus())
                btnInsertUpdate.Font = new Font(btnInsertUpdate.Font, FontStyle.Bold);
            if(btnCancel.Focus())
                btnCancel.Font = new Font(btnCancel.Font, FontStyle.Bold);

        }

        private void btnMLeave(object sender, EventArgs e)
        {
            btnInsertUpdate.Font = new Font(btnInsertUpdate.Font, FontStyle.Regular);
            btnCancel.Font = new Font(btnCancel.Font, FontStyle.Regular);

        }

        public bool IsDataValid()
        {
            bool isDVB = false;
            

            foreach (Control ctrl in Controls)
            {
                
                if (IsPresent(ctrl))
                {
                    isDVB = true;
                }
                //else if (textBox.Text == "")
                //{
                //    if((textBox.Visible && textBox.Text=="" && textBox == cellPhoneTextBox) 
                //        || (textBox.Visible && textBox.Text == "" && textBox == websiteTextBox && textBox == additionalNotesTextBox)
                //        || (textBox.Visible && textBox.Text == "" && textBox == emailAddressTextBox && textBox == phoneNumberTextBox && textBox == cellPhoneTextBox1 && textBox == additionalNotesTextBox1)
                //        || (textBox.Visible && textBox.Text == "" && textBox == additionalNotesTextBox2)
                //        || (textBox.Visible && textBox.Text == "" && textBox == positionNameTextBox && textBox == descriptionTextBox && textBox == companyIDTextBox1 && textBox == additionalNotesTextBox3 && textBox == resumeIDTextBox)
            //            || (textBox.Visible && textBox.Text == "" && textBox == rSCDirectoryPathTextBox && textBox == schoolIDTextBox && textBox == clientIDTextBox1)
            //            || (textBox.Visible && textBox.Text == "" && textBox == schoolNameTextBox && textBox == streetNameTextBox1 && textBox == cityTextBox2 && textBox == zipCodeTextBox2 && textBox == numberOfYearsAttendedTextBox && textBox == graduatedTextBox))

            //        {
            //        isDVB = true;
            //    }

            //}
                else
                {
                    isDVB = false;
                    break;
                }
            }
            if (IsPhoneNumber(cellPhoneTextBox) & IsInt32(zipCodeTextBox)
                | IsPhoneNumber(cellPhoneTextBox1) & IsPhoneNumber(phoneNumberTextBox)
                | IsInt32(contactIDTextBox1) & IsInt32(positionIDTextBox)
                | IsInt32(positionIDTextBox1) & IsInt32(companyIDTextBox1) & IsInt32(contactIDTextBox2)
                | IsInt32(companyIDTextBox2) & IsInt32(resumeIDTextBox)
                | IsInt32(schoolIDTextBox) & IsInt32(clientIDTextBox1)
                | IsInt32(zipCodeTextBox1) & IsInt32(buildingNumberTextBox)
                | IsInt32(zipCodeTextBox2))
            {
                isDVB = true;
                int firstZip=0;
                int lastZip=0;
                int firstZip1=0;
                int lastZip1=0;
                int firstZip2=0;
                int lastZip2=0;
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
            else
                isDVB = false;


            return isDVB;
        
        }
    }
}