using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WIP.Models;

namespace WIP.Controllers
{
    public class HomeController : Controller
    {

        // GET: Home
        public ActionResult Index()
        {
            Session["UserName"] = "";
            return View();
        }
        [HttpPost]
        public ActionResult Index(UserLoginModel login)
        {
            try
            {
                // Create the connection.
                using (SqlConnection connection = new SqlConnection(DBManager.Sqlconn()))
                {
                    SqlCommand com = new SqlCommand("Sp_Login", connection);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@email ", login.Email);
                    com.Parameters.AddWithValue("@Password ", login.Password);
                  
                    connection.Open();
                    
                    SqlDataReader sdr = com.ExecuteReader();
                    //connection.Close();
                    if (sdr.Read())
                    {
                        Session["Email"] = login.Email.ToString();
                        Session["UserName"] = sdr["FirstName"].ToString();
                        //return RedirectToAction("~/Product/AuthorDetail");
                        return RedirectToAction("welcome", "Home", new { area = "" });
                    }
                    else
                    {

                    }
                }
            }
            catch
            {

            }

            return View();
        }

        public ActionResult Registration()
        {
            Session["UserName"] = "";
            return View();
        }

        [HttpPost]
        public ActionResult Registration(UserRegistrationModel Registration)
        {
            
            try
            {
                // Create the connection.
                using (SqlConnection connection = new SqlConnection(DBManager.Sqlconn()))
                {
                    // Create SqlCommand and identify it as a stored procedure.
                    using (SqlCommand cmd = new SqlCommand("proRegistration", connection))
                    {
                        connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FirstName", Registration.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", Registration.LastName);
                        cmd.Parameters.AddWithValue("@OrganizationName", Registration.OrganizationName);
                        cmd.Parameters.AddWithValue("@Email", Registration.Email);
                        cmd.Parameters.AddWithValue("@Password", Registration.Password);
                        cmd.Parameters.AddWithValue("@EmailVerification", false);
                        cmd.Parameters.AddWithValue("@ActivetionCode ", Guid.NewGuid());

                        cmd.Parameters.Add("@ERROR", SqlDbType.Char, 500);
                        cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                        
                        //message = (string)cmd.Parameters["@ERROR"].Value;
                        int status = cmd.ExecuteNonQuery();
                        if (status > 0)
                        {
                            ViewBag.Message = "Your record has been saved";
                            return View();
                        }
                        else
                        {
                            ViewBag.Message = (string)cmd.Parameters["@ERROR"].Value;
                            return View();
                        }

                    }
                   
                }
                
            }
            catch(Exception ex)
            {
                return View();
            }
            
        }

        public ActionResult welcome(AuthorModel Author)
        {
            //
            try
            {
                // Create the connection.
                using (SqlConnection connection = new SqlConnection(DBManager.Sqlconn()))
                {
                    List<AuthorModel> lstAuthor = new List<AuthorModel>();
                    AuthorModel obj = new AuthorModel();
                    SqlCommand com = new SqlCommand("sp_GetAuthorDetails", connection);
                    com.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                   // SqlDataReader sdr = com.ExecuteReader();

                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataTable dt = new DataTable();

                  
                    da.Fill(dt);
                    connection.Close();
                    //Bind EmpModel generic list using dataRow     
                    foreach (DataRow sdr in dt.Rows)
                    {
                            obj.Name = sdr["Name"].ToString();
                            obj.DocumentUpload = sdr["DocumentUpload"].ToString();
                            obj.BooKDescription = sdr["BooKDescription"].ToString();
                            obj.NumberofAuthors = sdr["NumberofAuthors"].ToString();
                            obj.Remark = sdr["Remark"].ToString();
                            lstAuthor.Add(obj);
                    }
                    return View(lstAuthor);
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }


        public ActionResult AuthorForm()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AuthorForm(AuthorModel Author)
        {
            try
            {
                // Create the connection.
                using (SqlConnection connection = new SqlConnection(DBManager.Sqlconn()))
                {
                    // Create SqlCommand and identify it as a stored procedure.
                    using (SqlCommand cmd = new SqlCommand("SP_InsertAuthor", connection))
                    {
                        connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Name", Author.Name);
                        cmd.Parameters.AddWithValue("@DocumentUpload", Author.DocumentUpload);
                        cmd.Parameters.AddWithValue("@BooKDescription", Author.BooKDescription);
                        cmd.Parameters.AddWithValue("@NumberofAuthors", Author.NumberofAuthors);
                        cmd.Parameters.AddWithValue("@Remark", Author.Remark);
                        cmd.Parameters.Add("@ERROR", SqlDbType.Char, 500);
                        cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;

                        //message = (string)cmd.Parameters["@ERROR"].Value;
                        int status = cmd.ExecuteNonQuery();
                        if (status > 0)
                        {
                            ViewBag.Message = "Your record has been saved";
                        }
                        else
                        {
                            ViewBag.Message = (string)cmd.Parameters["@ERROR"].Value;
                            return View();
                        }

                    }
                }
                return RedirectToAction("welcome");
            }
            catch
            {
                return View();
            }

        }
        

        #region Check Email Exists or not in DB 
        
        
        
        
           
        public bool IsEmailExists(string eMail)
        {
            var IsCheck = true;
           // var IsCheck = objCon.UserMs.Where(email => email.Email == eMail).FirstOrDefault();
            return IsCheck != null;
        }
        #endregion
    }
}