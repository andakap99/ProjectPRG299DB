﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class Validator
    {
        private static string title = "Entry Error";

        public static string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }

        public static bool IsPresent(Control control) // CHECKS IF ALL THE REQUIRED FEILD ARE FILLED IN.
        {
            if (control.GetType().ToString() == "System.Windows.Forms.TextBox")
            {
                TextBox textBox = (TextBox)control;
                if (textBox.Text == "" && textBox.Visible && textBox.Enabled)
                {
                    if (textBox.Name.Contains("cellPhoneTextBox") || textBox.Name.Contains("websiteTextBox")
                        || textBox.Name.Contains("additionalNotesTextBox") || textBox.Name.Contains("emailAddressTextBox")
                        || textBox.Name.Contains("phoneNumberTextBox") || textBox.Name.Contains("cellPhoneTextBox1")
                        || textBox.Name.Contains("additionalNotesTextBox1") || textBox.Name.Contains("additionalNotesTextBox2")
                        || textBox.Name.Contains("positionNameTextBox") || textBox.Name.Contains("descriptionTextBox")
                        || textBox.Name.Contains("additionalNotesTextBox3")
                        || textBox.Name.Contains("rSCDirectoryPathTextBox")
                        || textBox.Name.Contains("streetNameTextBox1")
                        || textBox.Name.Contains("cityTextBox2") 
                        || textBox.Name.Contains("numberOfYearsAttendedTextBox") || textBox.Name.Contains("graduatedTextBox")
                        || textBox.Name.Contains("buildingNameTextBox") || textBox.Name.Contains("buildingNumberTextBox"))
                    { return true; }

                        MessageBox.Show( textBox.Name.ToString() + " is a  required field.", Title);
                    textBox.Focus();
                    return false;
                }
                else
                {
                    return true;
                     
                }
            }
            else if (control.GetType().ToString() == "System.Windows.Forms.ComboBox")
            {
                ComboBox comboBox = (ComboBox)control;
                if (comboBox.SelectedIndex == -1 && comboBox.Visible && comboBox.Name.Contains("stateComboBox"))
                {
                    MessageBox.Show("State required.", Title);
                    comboBox.Focus();
                    return false;
                }

                else
                {
                    return true;
                }
            }
            else if (control.GetType().ToString()== "System.Windows.Forms.DateTimePicker")
            {
                return true;
            }
                return true;
        }

        public static bool IsDecimal(TextBox textBox) // CHECKS IF THE REQUIRED DECIMAL VALUES ARE A DECIMAL
        {
            try
            {
                Convert.ToDecimal(textBox.Text);
                return true;
            }
            catch (FormatException)
            {
                MessageBox.Show(textBox.Tag.ToString() + " must be a decimal value.", Title);
                textBox.Focus();
                return false;
            }
        }

        public static bool IsInt32(TextBox textBox) // CHECKS IF THE REQUIRED INTEGER VALUE IS AN INTEGER 
        {
            try
            {
                if (textBox.Visible && textBox.Enabled && IsPresent(textBox))
                {
                    if (textBox.Text!="")
                    {
                    Convert.ToInt32(textBox.Text);

                    }
                    return true;
                }
                else if (textBox.Visible && textBox.Name.Contains("buildingNumberTextBox") && textBox.Text=="") { return true; }
                else
                {
                    if (textBox.Visible && textBox.Text!="")
                    {
                        Convert.ToInt32(textBox.Text);
                    }
                    

                    return false;
                    
                }
            }
            catch (FormatException)
            {
                MessageBox.Show(textBox.Name.ToString() + " must be an integer value.", Title);
                textBox.Focus();
                return false;
            }
       }

        public static bool IsStateZipCode(TextBox textBox, int firstZip, int lastZip) // CHECKS IF THE ZIPCODE IS PROPER FOR THE STATE
        {
            int zipCode = 0;
            if (textBox.Text != "")
            {
                zipCode = Convert.ToInt32(textBox.Text);
            }
            if ( textBox.Name.Contains("zipCode")!= true)
            { return true; }
            if (textBox.Text == "" && textBox.Visible && firstZip==0 || lastZip==0 && textBox.Visible)
            { return false; }
            else if (zipCode < firstZip && textBox.Visible || zipCode > lastZip && textBox.Visible)
            {
                MessageBox.Show("ZipCode must be within this range: " +
                    firstZip + " to " + lastZip + ".", Title);
                textBox.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsPhoneNumber(TextBox textBox) // CHECKS IF TEXTBOXES ARE PHONE NUMDER TEXTBOXES
        {
            string phoneChars = textBox.Text.Replace(".", "");
            try
            {   if (textBox.Text != "" && textBox.Visible)
                {
                    if (textBox.Name.Contains("cellPhoneTextBox") || textBox.Name.Contains("phoneNumberTextBox") || textBox.Name.Contains("cellPhoneTextBox1"))
                    { Convert.ToInt64(phoneChars); } return true; 
                    
                }
                else
                {
                    if (textBox.Visible)
                    {return true;}
                    return false;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Must be in this format: " +
                    "999.999.9999.", Title);
                textBox.Focus();
                return false;
            }
        }
    }
}
