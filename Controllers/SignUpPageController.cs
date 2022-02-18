using ASP.Net_Project1.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.Net_Project1.Controllers
{
    public class SignUpPageController : Controller
    {
        // GET: SignUpPage
        public ActionResult Index()
        {
            return View();
        }

        // GET: SignUpPage/Details/5
        public ActionResult Details()
        {
            if (Session["name1"] != null)
            {
                string name = Session["name1"].ToString();
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Project1; Integrated Security = True";
                cn.Open();

                SqlCommand cmdshow = new SqlCommand();
                cmdshow.Connection = cn;
                cmdshow.CommandType = System.Data.CommandType.Text;
                cmdshow.CommandText = "select * from SignUpData where Username='" + name + "'";
                // cmdshow.Parameters.AddWithValue("@Username", name);
                List<SignUp> list = new List<SignUp>();
                SignUp s1 = new SignUp();
             
                try
                {
                    SqlDataReader dr = cmdshow.ExecuteReader();

                    while (dr.Read())
                    {
                        s1.Full_Name = (string)dr["Full_Name"];
                        s1.EmailId = (string)dr["EmailId"];
                        s1.Username = (string)dr["Username"];
                        s1.Password = (string)dr["Password"];
                        s1.Confirm_Password = (string)dr["Confirm_Password"];
                        s1.City = (string)dr["City"];
                        s1.Phone = (string)dr["Phone"];
                    }
                    list.Add(s1);
                    return View(s1);
                  
                }
                catch (Exception ex)
                { 
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    cn.Close();
                }
            }
            return View();
        }

        // GET: SignUpPage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SignUpPage/Create
        [HttpPost]
        public ActionResult Create(SignUp obj)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Project1; Integrated Security = True";
            cn.Open();

            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.Connection = cn;
            cmdInsert.CommandType = System.Data.CommandType.Text;

            cmdInsert.CommandText = "insert into SignUpData values('" + obj.Full_Name + "','" + obj.EmailId + "', '" + obj.Username + "','" + obj.Password + "','" + obj.Confirm_Password + "','" + obj.City + "','" + obj.Phone + "')";


            //cmdInsert.CommandText = "insert into SignUpData values(@Full_Name,@EmailId,@Username,@Password,@Confirm_Password,@City,@Phone)";
            //cmdInsert.Parameters.AddWithValue("@Full_Name", obj.Full_Name);
            //cmdInsert.Parameters.AddWithValue("@EmailId", obj.EmailId);
            //cmdInsert.Parameters.AddWithValue("@Username", obj.Username);
            //cmdInsert.Parameters.AddWithValue("@Password", obj.Password);
            //cmdInsert.Parameters.AddWithValue("@Confirm_Password", obj.Confirm_Password);
            //cmdInsert.Parameters.AddWithValue("@City", obj.City);
            //cmdInsert.Parameters.AddWithValue("@Phone", obj.Phone);


            try
            {
                cmdInsert.ExecuteNonQuery();
                return RedirectToAction("Login");
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                Console.WriteLine(ex.Message);
                return View();
            }
            finally
            {
                cn.Close();
            }
            
        }

        // GET: SignUpPage/Edit/5
        public ActionResult Edit(int id=0)
        {
            if (Session["name1"] != null)
            {
                string name = Session["name1"].ToString();
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Project1; Integrated Security = True";
                cn.Open();

                SqlCommand cmdedit = new SqlCommand();
                cmdedit.Connection = cn;
                cmdedit.CommandType = System.Data.CommandType.Text;
                cmdedit.CommandText = "select * from SignUpData where UserName='" + name + "'";

                SqlDataReader dr = cmdedit.ExecuteReader();
                SignUp obj = new SignUp();
                try
                {
                    while (dr.Read())
                    {
                        obj.Full_Name = (string)dr["Full_Name"];
                        obj.EmailId = (string)dr["EmailId"];
                        obj.City = (string)dr["City"];
                        obj.Phone = (string)dr["Phone"];
                    }
                    cn.Close();
                    return View(obj);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return View();
         
           
        }

        // POST: SignUpPage/Edit/5
        [HttpPost]
        public ActionResult Edit(SignUp obj)
        {
            string name = Session["name1"].ToString();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Project1; Integrated Security = True";
            cn.Open();

            SqlCommand cmdInsert = new SqlCommand();
            cmdInsert.Connection = cn;
            cmdInsert.CommandType = System.Data.CommandType.Text;
            cmdInsert.CommandText = "update SignUpData set Full_Name=@Full_Name,EmailId=@EmailId,City=@City,Phone=@Phone where Username='"+name+"'";

            cmdInsert.Parameters.AddWithValue("@Full_Name", obj.Full_Name);
            cmdInsert.Parameters.AddWithValue("@EmailId", obj.EmailId);
            cmdInsert.Parameters.AddWithValue("@City", obj.City);
            cmdInsert.Parameters.AddWithValue("@Phone", obj.Phone);

          
            try
            { 
                cmdInsert.ExecuteNonQuery();
                ViewBag.errormessage = "Data updated sucessfully.!!!";
                return View();
            }
            catch
            {
                return View();
            }
            finally
            {
                cn.Close();
            }
        }

        // GET: SignUpPage/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SignUpPage/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Login()
        {
            return View();
        }

        // POST: SignUpPage/Edit/5
        [HttpPost]
        public ActionResult Login(SignUp obj)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Project1; Integrated Security = True";
            cn.Open();

            SqlCommand cmdlogin = new SqlCommand();
            cmdlogin.Connection = cn;
            cmdlogin.CommandType = System.Data.CommandType.Text;
            cmdlogin.CommandText="select * from SignUpData where UserName='"+obj.Username+"' and Password='"+obj.Password+"'";

            try
            {
                // cmdlogin.ExecuteNonQuery();
                SqlDataReader dr = cmdlogin.ExecuteReader();
                while (dr.Read())
                {
                    TempData["name"] =dr["Full_Name"];
                    Session["name1"] = dr["Username"];
                    //return RedirectToAction("Edit");
                    return RedirectToAction("Index");
                }
                ViewBag.erromsg = "Please check Your Credentials";
                return View();
            }
            catch
            {
                return View();
            }         
        }
        // GET: SignUpPage/logout
        public ActionResult Logoutc()
        {
            Session.Abandon();
            return RedirectToAction("Thankyoupage");
        }
        // GET: SignUpPage/logout
        public ActionResult Thankyoupage()
        {
           return View();
        }
    }


}
