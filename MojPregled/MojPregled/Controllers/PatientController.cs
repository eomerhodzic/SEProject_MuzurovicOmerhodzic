using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace MojPregled.Controllers
{
    public class PatientController : Controller
    {
        string connectionString = @"Data Source=DESKTOP-SHHT07O\SQLEXPRESS;Initial Catalog=MojPregled;Integrated Security=True";
        List <PatientModel> Patients = new List<PatientModel>();
        List<string> listPatient = new List<string>();
        
        public ActionResult Index()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = "SELECT * FROM Patient";
                connection.Open();
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            PatientModel model = new PatientModel();
                            model.patient_id = (int) reader[0];
                            model.patient_name = (string) reader[1];
                            model.patient_surname = (string) reader[2];
                            model.patient_dateofbirth = (DateTime)reader[3];
                            model.patient_address = (string)reader[4];
                            model.patient_phonenumber = (string)reader[5];
                            model.patient_city = (string)reader[6];
                            Patients.Add(model);
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error");
                    }
                }
                return View(Patients);
            }
        }

        public ActionResult Edit(int? id)
        {
            PatientModel model = new PatientModel();
            if (id != null)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string queryString = "SELECT * FROM Patient WHERE patient_id = " + id;
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        try
                        {
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                model.patient_id = (int)reader[0];
                                model.patient_name = (string)reader[1];
                                model.patient_surname = (string)reader[2];
                                model.patient_dateofbirth = (DateTime)reader[3];
                                model.patient_address = (string)reader[4];
                                model.patient_phonenumber = (string)reader[5];
                                model.patient_city = (string)reader[6];
                            }
                            reader.Close();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error");
                        }
                    }
                }
            }
            TempData["message"] = "Edited";
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(PatientModel model)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string editString = @"UPDATE Patient SET patient_name =  @patient_name , patient_surname =  @patient_surname WHERE patient_id =  @patient_id";
                connection.Open();
                using (SqlCommand command = new SqlCommand(editString, connection))
                {
                    command.Parameters.AddWithValue("@TaskID", model.patient_id);
                    command.Parameters.AddWithValue("@TaskName", model.patient_name);
                    command.Parameters.AddWithValue("@TaskDescription", model.patient_surname);
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error");
                    }
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Add(PatientModel model)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string insertString = @"INSERT INTO Patient(patient_name, patient_surname) VALUES (@patient_name, @patient_surname)";
                connection.Open();
                using (SqlCommand command = new SqlCommand(insertString, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@patient_name", model.patient_name);
                        command.Parameters.AddWithValue("@patient_surname", model.patient_surname);
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error");
                    }
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Add()
        {
            TempData["message"] = "Added";
            return View();
        }

        [HttpGet]
        public ActionResult Delete()
        {
            TempData["message"] = "Deleted";
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string deleteString = @"DELETE FROM Patient WHERE patient_id = @id";
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(deleteString, connection))
                    {
                        try
                        {
                            command.Parameters.AddWithValue("@id", id);
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                PatientModel model = new PatientModel();
                                model.patient_id = (int)reader[0];
                                model.patient_name = (string)reader[1];
                                model.patient_surname = (string)reader[2];
                                Patients.Remove(model);
                            }
                            reader.Close();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error");
                        }
                    }
                }
            }
            return RedirectToAction("Index");
        }

    }
}