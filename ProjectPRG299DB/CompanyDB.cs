using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ProjectPRG299DB
{
    public static class CompanyDB
    {
        public static List<Company> GetCompany()
        {
            List<Company> companyList = new List<Company>();
            SqlConnection connection = PRG299DB.GetConnection();
            string selectStatement = "SELECT CompanyID, CompanyName, BuildingName, BuildingNumber, StreetAddress, " +
                "City, State, ZipCode, Website, AdditionalNotes FROM dbo.Company";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                int cIDOrd = reader.GetOrdinal("CompanyID"),
                    cNOrd = reader.GetOrdinal("CompanyName"),
                    cBNaOrd = reader.GetOrdinal("BuildingName"),
                    cBNuOrd = reader.GetOrdinal("BuildingNumber"),
                    cAOrd = reader.GetOrdinal("StreetAddress"),
                    cCOrd = reader.GetOrdinal("City"),
                    cSOrd = reader.GetOrdinal("State"),
                    cZCOrd = reader.GetOrdinal("ZipCode"),
                    cPOrd = reader.GetOrdinal("Website"),
                    cEOrd = reader.GetOrdinal("AdditionalNotes");
                while (reader.Read())
                {
                    Company comp = new Company();
                    comp.CompanyID = reader.GetInt32(cIDOrd);
                    comp.CompanyName = reader.GetString(cNOrd);
                    comp.BuildingName = reader.GetString(cBNaOrd);
                    comp.BuildingNumber = reader.GetString(cBNuOrd);
                    comp.StreetAddress = reader.GetString(cAOrd);
                    comp.City = reader.GetString(cCOrd);
                    comp.State = reader.GetString(cSOrd);
                    comp.ZipCode = reader.GetString(cZCOrd);
                    comp.Website = reader.GetString(cPOrd);
                    comp.AdditionalNotes = reader.GetString(cEOrd);
                    companyList.Add(comp);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return companyList;
        }
        public static List<Company> GetCompanyByRow()
        {
            throw new System.NotImplementedException();
        }

        public static bool DeleteCompany()
        {
            throw new System.NotImplementedException();
        }

        public static int AddCompany()
        {
            throw new System.NotImplementedException();
        }

        public static bool UpdateCompany()
        {
            throw new System.NotImplementedException();
        }
    }
}