using System;
using System.Data;
using System.Data.SqlClient;
using Model;
using System.Linq;
using System.Text.RegularExpressions;


namespace HRMS
{
    public class dAL
    {
        static string constr = "data source=LAPTOP-RI6VRBHC\\SQLEXPRESS;initial catalog=HRDATA;integrated security=TRUE;"; //CONNECTION STRING TO STORE THE ADDRESS
        public void DisplayHRMS()
        {
            DataTable DT = ExecuteData("select * from HRMDATA");
            if (DT.Rows.Count > 0)
            {
                Console.Write(Environment.NewLine);
                Console.WriteLine("=====================================================================");
                Console.WriteLine("DATABASE RECORDS FROM HUMAN RESOURCE DEPARTMENT");
                Console.WriteLine("=====================================================================");
                foreach (DataRow row in DT.Rows)
                {
                    Console.WriteLine("");
                       Console.WriteLine("|| " + row["EMPNO"].ToString() + "|| " + row["EMPPHONENO"].ToString() + "| " + row["EMPNAME"].ToString() + "|| " + row["EMPSALARY"].ToString() + "|| " 
                        + row["EMPROLE"].ToString() + "|| " + row["EMPADDR"].ToString() + "|| " + row["DEPARTMENT"] + "|| " + row["EMAILID"]);
                }
                Console.WriteLine("======================================================================" + Environment.NewLine);
            }
            else
            {
                Console.Write(Environment.NewLine);
                Console.WriteLine("No Employee found in database table!!!");
                Console.Write(Environment.NewLine);
            }
        }
        public DataTable ExecuteData(String Query)
        {
            DataTable result = new DataTable();

            try
            {
                using (SqlConnection sqlcon = new SqlConnection(constr))
                {
                    sqlcon.Open();
                    SqlCommand cmd = new SqlCommand(Query, sqlcon);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(result);
                    sqlcon.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }

       

        public bool ExecuteCommand(string queury)                       // THIS METHOD IS USED TO EXECUTE THE QUERIES INTO THE DATABASE
        { 
                                                    
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(constr))
                {
                    sqlcon.Open();
                    SqlCommand cmd = new SqlCommand(queury, sqlcon);
                    cmd.ExecuteNonQuery();
                    sqlcon.Close();

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
                throw;
            }
            return true;
        }

        public model GetInputFromUser()
        {
            string EMPNO = string.Empty;
            long EMPPHONENO ;
            string EMPNAME = string.Empty;
            string EMPSALARY = string.Empty;
            string EMPROLE = string.Empty;
            string EMPADDR = string.Empty;
            string DEPARTMENT = string.Empty;
            string EMAILID = string.Empty;

            Console.WriteLine("Enter EmpNo;");
            EMPNO = Console.ReadLine();

            

            while (true)                                                      // validation of phone number 
            {
                Console.WriteLine("Enter EMP PHONE again;");
                EMPPHONENO = Convert.ToInt64(Console.ReadLine());
                bool status = isValidMobileNumber(EMPPHONENO);

                if (status == true)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("enter valid phone number");
                    continue;
                }
            }

            


            while (true)                                                    // validation of name
            { 
                    Console.WriteLine("PLEASE ENTER EMPLOYEE NAME");
                    Console.WriteLine("NO BLANK NAMES ALLOWED nor ANY SPECIAL CHARACTER ");
                    EMPNAME = Console.ReadLine();
                if(Regex.IsMatch(EMPNAME, "^[A-Za-z0-9]*$"))
                {
                    break;

                }
                   
                else
                    {
                     Console.WriteLine("You have to enter valid name");
                   
                    continue;
                    }

                }
            
            Console.Write("enter the salary = ");
            EMPSALARY = Console.ReadLine();

            Console.Write("enter the ROLE =");
            EMPROLE = Console.ReadLine();

            Console.Write("enter the address =");
            EMPADDR = Console.ReadLine();

            Console.Write("enter the DEPARTMENT = ");
            DEPARTMENT = Console.ReadLine();
            
           
            while (true)                                                // to validate the email id 
            {
                Console.WriteLine("***********************");
                Console.WriteLine("Enter Email : ");
                EMAILID = Console.ReadLine();
                bool status = isValidEmail(EMAILID);
                if (status == true)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("please enter valid email");
                    continue;
                }

            }
           
            model mdl = new model()
            {
                EMPNO = EMPNO,
                EMPPHONENO = EMPPHONENO,
                EMPNAME = EMPNAME,
                EMPSALARY = EMPSALARY,
                EMPROLE = EMPROLE,
                EMPADDR = EMPADDR,
                DEPARTMENT = DEPARTMENT,
                EMAILID = EMAILID,
            };
            return mdl;
        }
        public static bool isValidMobileNumber(long inputMobileNumber)  // METHOD TO EXAMINE PHONE NUMBER VALIDITY
        {
            string strRegex = @"(^[0-9]{10}$)|(^\+[0-9]{2}\s+[0-9] {2}[0-9]{8}$)|(^[0-9]{3}-[0-9]{4}-[0-9]{4}$)";


            Regex re = new Regex(strRegex);


            if (re.IsMatch(Convert.ToString(inputMobileNumber)))
            {
                return (true);
            }

            else
            {
                return (false);
            }

        }

        public static bool isValidEmail(string inputEmail)
        {

                                                                    // This Pattern is use to verify the email
            string strRegex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

            Regex re = new Regex(strRegex, RegexOptions.IgnoreCase);

            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }
        



                                                                     // BELOW ARE CRUD OPERATION METHODS

        public void AddNewHrms(model mdl)
        {


            ExecuteCommand(String.Format("Insert into HRMDATA(EMPNO,EMPPHONENO,EMPNAME,EMPSALARY,EMPROLE,EMPADDR,DEPARTMENT,EMAILID) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                mdl.EMPNO, mdl.EMPPHONENO, mdl.EMPNAME, mdl.EMPSALARY, mdl.EMPROLE, mdl.EMPADDR, mdl.DEPARTMENT,mdl.EMAILID));

        }


        public void EditNewHrms(model mdl)                          // METHOD TO EDIT THE SPECIFIC EMPLOYEE RECORD
        {
           

            ExecuteCommand(String.Format("Update HRMDATA set EMPNO = '{0}', EMPPHONENO = '{1}', EMPNAME = '{2}',  EMPSALARY='{3}',EMPROLE ='{4}', EMPADDR='{5}', DEPARTMENT = '{6}' where EMPNO = '{0}'",
                mdl.EMPNO, mdl.EMPPHONENO, mdl.EMPNAME, mdl.EMPSALARY, mdl.EMPROLE, mdl.EMPADDR,mdl.DEPARTMENT,mdl.EMAILID));

        }


        public void DeleteNewHrms()                                 // METHOD TO DELETE THE SELECTED EMPLOYEE FROM DATABASE/TABLE
        {
            Console.WriteLine("DELETE EXISTING EMPLOYEE");

            Console.WriteLine(" ENTER THE ENP NO:");

           int  EMPNO = Convert.ToInt32(Console.ReadLine());


            ExecuteCommand(String.Format("Delete from HRMDATA where EMPNO = '{0}'", EMPNO));


        }

        public void SearchInHrms()                                // SEARCH THE RECORD OF EMPLOYEE IN DATABASE
        {
            Console.WriteLine(" ENTER THE ENPLOYEES NO YOU WANT TO SEARCH FOR :");
            int EMPNO = Convert.ToInt32(Console.ReadLine());

            DataTable DT = ExecuteData("select *  from HRMDATA WHERE EMPNO ='"+EMPNO+"'" );
            if (DT.Rows.Count > 0)
            {
                Console.Write(Environment.NewLine);
                Console.WriteLine("=====================================================================");
                Console.WriteLine("DATABASE RECORDS FROM HUMAN RESOURCE DEPARTMENT");
                Console.WriteLine("=====================================================================");
                foreach (DataRow row in DT.Rows)
                {
                    Console.WriteLine("");
                    Console.WriteLine("|| " + row["EMPNO"].ToString() + "|| " + row["EMPPHONENO"].ToString() + "|| " + row["EMPNAME"].ToString() + "|| " + row["EMPSALARY"].ToString() + "|| "
                     + row["EMPROLE"].ToString() + "|| " + row["EMPADDR"].ToString() + "|| " + row["DEPARTMENT"] + "|| " + row["EMAILID"]);
                }
                Console.WriteLine("======================================================================" + Environment.NewLine);
            }
            else
            {
                Console.Write(Environment.NewLine);
                Console.WriteLine("No Employee found in database table!!!");
                Console.Write(Environment.NewLine);
            }


        }
       










    }
}



   




