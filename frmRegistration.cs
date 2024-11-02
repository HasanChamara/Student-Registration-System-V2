﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Windows;

namespace StudentRegistrationSystem
{
    public partial class frmRegistration : Form
    {
        MySqlConnection cn = new MySqlConnection();
        MySqlCommand cm = new MySqlCommand();
        DBConnection dbcon = new DBConnection();

        public frmRegistration()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {
            comboRegNo.Text = "";
            txtFirstname.Clear();
            txtLastname.Clear();
            dateTimePicker1.Value = DateTime.Now;
            radioMale.Checked = true;
            txtAddress.Clear();
            txtEmail.Clear();
            txtMobilePhone.Clear();
            txtHomePhone.Clear();
            txtParentName.Clear();
            txtNic.Clear();
            txtContactNo.Clear();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string regno = comboRegNo.Text;
            string firstname = txtFirstname.Text;
            string lastname = txtLastname.Text;
            string dob = dateTimePicker1.Value.ToString();
            string gender = radioMale.Checked ? radioMale.Text : radioFemale.Text;
            string address = txtAddress.Text;
            string email = txtEmail.Text;
            string mobilephone = txtMobilePhone.Text;
            string homephone = txtHomePhone.Text;
            string parentname = txtParentName.Text;
            string nic = txtNic.Text;
            string contactno = txtContactNo.Text;

            try
            {
                cn.ConnectionString = dbcon.MyConnection();
                cn.Open();


                cm = new MySqlCommand("INSERT INTO register (regNo, firstName, lastName, dateOfBirth, gender, address, email, mobilePhone, homePhone, parentName, nic, contactNo) " +
                                    "VALUES (@regNo, @firstName, @lastName, @dateOfBirth, @gender, @address, @email, @mobilePhone, @homePhone, @parentName, @nic, @contactNo)", cn);


                cm.Parameters.AddWithValue("@regNo", regno);
                cm.Parameters.AddWithValue("@firstName", firstname);
                cm.Parameters.AddWithValue("@lastName", lastname);
                cm.Parameters.AddWithValue("@dateOfBirth", dob);
                cm.Parameters.AddWithValue("@gender", gender);
                cm.Parameters.AddWithValue("@address", address);
                cm.Parameters.AddWithValue("@email", email);
                cm.Parameters.AddWithValue("@mobilePhone", mobilephone);
                cm.Parameters.AddWithValue("@homePhone", homephone);
                cm.Parameters.AddWithValue("@parentName", parentname);
                cm.Parameters.AddWithValue("@nic", nic);
                cm.Parameters.AddWithValue("@contactNo", contactno);


                cm.ExecuteNonQuery();

                MessageBox.Show("Registration successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }

        }

        private void comboRegNo_DropDown(object sender, EventArgs e)
        {
            
            try
            {
                comboRegNo.Items.Clear();
                cn.ConnectionString = dbcon.MyConnection();
                cn.Open();

                cm = new MySqlCommand("SELECT regNo FROM register", cn);
                MySqlDataReader dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    comboRegNo.Items.Add(dr["regNo"].ToString());
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
        }

        private void comboRegNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cn.ConnectionString = dbcon.MyConnection();
                cn.Open();

                cm = new MySqlCommand("SELECT * FROM register WHERE regNo = @regNo", cn);
                cm.Parameters.AddWithValue("@regNo", comboRegNo.Text);
                MySqlDataReader dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    txtFirstname.Text = dr["firstName"].ToString();
                    txtLastname.Text = dr["lastName"].ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(dr["dateOfBirth"].ToString());

                    string databaseGender = dr["gender"].ToString();
                    
                    if (databaseGender == "Male")
                    {
                        radioMale.Checked = true;
                    }
                    else if (databaseGender == "Female")
                    {
                        radioFemale.Checked = true;
                    }


                    txtAddress.Text = dr["address"].ToString();
                    txtEmail.Text = dr["email"].ToString();
                    txtMobilePhone.Text = dr["mobilePhone"].ToString();
                    txtHomePhone.Text = dr["homePhone"].ToString();
                    txtParentName.Text = dr["parentName"].ToString();
                    txtNic.Text = dr["nic"].ToString();
                    txtContactNo.Text = dr["contactNo"].ToString();

                }
                dr.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {


            string regno = comboRegNo.Text;
            string firstname = txtFirstname.Text;
            string lastname = txtLastname.Text;
            string dob = dateTimePicker1.Value.ToString();
            string gender = radioMale.Checked ? radioMale.Text : radioFemale.Text;
            string address = txtAddress.Text;
            string email = txtEmail.Text;
            string mobilephone = txtMobilePhone.Text;
            string homephone = txtHomePhone.Text;
            string parentname = txtParentName.Text;
            string nic = txtNic.Text;
            string contactno = txtContactNo.Text;

            try
            {
                cn.ConnectionString = dbcon.MyConnection();
                cn.Open();

                cm = new MySqlCommand("UPDATE register SET firstName = @firstName, lastName = @lastName, dateOfBirth = @dateOfBirth, gender = @gender, address = @address, email = @email, mobilePhone = @mobilePhone, homePhone = @homePhone, parentName = @parentName, nic = @nic, contactNo = @contactNo WHERE regNo = @regNo",cn);

                cm.Parameters.AddWithValue("@firstName", firstname);
                cm.Parameters.AddWithValue("@lastName", lastname);
                cm.Parameters.AddWithValue("@dateOfBirth",dob);
                cm.Parameters.AddWithValue("@gender", gender);
                cm.Parameters.AddWithValue("@address", address);
                cm.Parameters.AddWithValue("@email", email);
                cm.Parameters.AddWithValue("@mobilePhone", mobilephone);
                cm.Parameters.AddWithValue("@homePhone", homephone);
                cm.Parameters.AddWithValue("@parentName", parentname);
                cm.Parameters.AddWithValue("@nic", nic);
                cm.Parameters.AddWithValue("@contactNo", contactno);
                cm.Parameters.AddWithValue("@regNo", regno);

                int rowsAffected = cm.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Record Updated Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                }
                else
                {
                    MessageBox.Show("No records updated!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                cn.Close();
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            string regno = comboRegNo.Text;

            DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    cn.ConnectionString = dbcon.MyConnection();
                    cn.Open();

                    cm = new MySqlCommand("DELETE FROM register WHERE regNo = @regNo", cn);
                    cm.Parameters.AddWithValue("@regNo", regno);

                    
                    int rowsAffected = cm.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record Deleted Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Clear();
                    }
                    else
                    {
                        MessageBox.Show("No records deleted. Check if the record exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cn.Close();
                }
            }
        }

        private void linkExit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Application.Exit();
        }

        private void linkLogout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Form1 frm1 = new Form1();
            frm1.Show();
        }

    }
}
