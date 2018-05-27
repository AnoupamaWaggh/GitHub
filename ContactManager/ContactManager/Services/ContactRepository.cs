using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ContactManager.Models;

namespace ContactManager.Services
{
    public class ContactRepository
    {
        public IEnumerable<Contact> GetAllContacts
        {
            get
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ContactManagerCS"].ConnectionString;
                List<Contact> contacts = new List<Contact>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spGetAllContacts", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Contact contact = new Contact();
                        contact.ID = Convert.ToInt32(rdr["ID"]);
                        contact.FirstName = rdr["FirstName"].ToString();
                        contact.LastName = rdr["LastName"].ToString();
                        contact.Email = rdr["Email"].ToString();
                        contact.PhoneNumber = rdr["PhoneNumber"].ToString();
                        contact.Status = rdr["Status"].ToString();

                        contacts.Add(contact);
                    }
                }
                return contacts;
            }
        }

        //Add a contact
        public void AddContact(Contact contact)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ContactManagerCS"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddContact", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramFirstName = new SqlParameter();
                paramFirstName.ParameterName = "@FirstName";
                paramFirstName.Value = contact.FirstName;
                cmd.Parameters.Add(paramFirstName);

                SqlParameter paramLastName = new SqlParameter();
                paramLastName.ParameterName = "@LastName";
                paramLastName.Value = contact.LastName;
                cmd.Parameters.Add(paramLastName);

                SqlParameter paramEmail = new SqlParameter();
                paramEmail.ParameterName = "@Email";
                paramEmail.Value = contact.Email;
                cmd.Parameters.Add(paramEmail);

                SqlParameter paramPhoneNumber = new SqlParameter();
                paramPhoneNumber.ParameterName = "@PhoneNumber";
                paramPhoneNumber.Value = contact.PhoneNumber;
                cmd.Parameters.Add(paramPhoneNumber);

                SqlParameter paramStatus = new SqlParameter();
                paramStatus.ParameterName = "@Status";
                paramStatus.Value = contact.Status;
                cmd.Parameters.Add(paramStatus);

                SqlParameter paramID = new SqlParameter();
                paramID.ParameterName = "@Id";
                paramID.SqlDbType = SqlDbType.Int;
                paramID.Direction = ParameterDirection.Output;
                paramID.Size = 5;
                cmd.Parameters.Add(paramID);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    contact.ID = Convert.ToInt32(cmd.Parameters["@id"].Value);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //Save contact
        public void SaveContact(Contact contact)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ContactManagerCS"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spSaveContact", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@Id";
                paramId.Value = contact.ID;
                cmd.Parameters.Add(paramId);

                SqlParameter paramFirstName = new SqlParameter();
                paramFirstName.ParameterName = "@FirstName";
                paramFirstName.Value = contact.FirstName;
                cmd.Parameters.Add(paramFirstName);

                SqlParameter paramLastName = new SqlParameter();
                paramLastName.ParameterName = "@LastName";
                paramLastName.Value = contact.LastName;
                cmd.Parameters.Add(paramLastName);

                SqlParameter paramEmail = new SqlParameter();
                paramEmail.ParameterName = "@Email";
                paramEmail.Value = contact.Email;
                cmd.Parameters.Add(paramEmail);

                SqlParameter paramPhoneNumber = new SqlParameter();
                paramPhoneNumber.ParameterName = "@PhoneNumber";
                paramPhoneNumber.Value = contact.PhoneNumber;
                cmd.Parameters.Add(paramPhoneNumber);

                SqlParameter paramStatus = new SqlParameter();
                paramStatus.ParameterName = "@Status";
                paramStatus.Value = contact.Status;
                cmd.Parameters.Add(paramStatus);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        //Delete a contact
        public void DeleteContact(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ContactManagerCS"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteContact", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@Id";
                paramId.Value = id;
                cmd.Parameters.Add(paramId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}