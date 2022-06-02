using System.Data.SqlClient;
using System.Data;

namespace DataLayerEClinic
{
    public class DataLayerClass
    {
        public static SqlDataReader dr;
        public static SqlCommand cmd;
        private static SqlConnection GetCon()
        {
            SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=ECLinic;Integrated Security=true");
            connection.Open();
            return connection;
        }

        public static SqlDataReader FetchStaffDetails(string username)
        {
            try
            {
                SqlConnection connection = GetCon();
                string user=Convert.ToString(username);
                String value = String.Format("select * from OfficeStaff where Username='{0}'", user);
                SqlCommand cmd = new SqlCommand(value);
                cmd.Connection = connection;
                dr = cmd.ExecuteReader();

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return dr;

        }

        public static SqlDataReader FetchAllDoctor()
        {
            try
            {
                SqlConnection connection = GetCon();
                String value = String.Format("select * from Doctor");
                SqlCommand cmd = new SqlCommand(value);
                cmd.Connection = connection;
                dr = cmd.ExecuteReader();

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return dr;
        }

        public static SqlDataReader FetchDoc(int ID)
        {
            try
            {
                SqlConnection connection = GetCon();
                String value = String.Format("select * from Doctor where DoctorID={0}",ID);
                SqlCommand cmd = new SqlCommand(value);
                cmd.Connection = connection;
                dr = cmd.ExecuteReader();

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return dr;
        }

        public  int AddPatient(string FirstName,string LastName, string Sex, int Age, string DOB)
        {
            int Flags = 0;
            try
            {
                
                SqlConnection connection = GetCon();
                String value = String.Format("insert into Patient values ('{0}','{1}','{2}',{3},'{4}') ",FirstName,LastName,Sex,Age,DOB);
                SqlCommand cmd = new SqlCommand(value);
                cmd.Connection = connection;
                int i = cmd.ExecuteNonQuery();
                if (i == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    throw new Exception("PLEASE CHECK YOUR INPUTS AND TRY AGAIN !!");
                    Flags= 0;
                    
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("PATIENT RECORD CREATED SUCCESSFULLY");
                    Flags = 1;
                    
                }
                
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return Flags;
        }


        public void AddApp(int DocID,int PatID,string date,int slot)
        {
            try
            {
                Console.WriteLine(date);
                SqlConnection connection = GetCon();
                
                String value = String.Format("insert into Appointment values ({0},{1},'{2}',{3}) ", DocID,  PatID, date,  slot);
                SqlCommand cmd = new SqlCommand(value);
                cmd.Connection = connection;
                int i = cmd.ExecuteNonQuery();
                if (i == 0)
                {
                    throw new Exception("Please check you input and try again");
                }
                else
                {
                    Console.WriteLine("Appointment Created Sucessfully");
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }


        public static SqlDataReader CheckForApp(int ID,string AppDate)
        {
            try
            {
                SqlConnection connection = GetCon();
                String value = String.Format("select * from Appointment where DocID={0} and DateOfApp='{1}'", ID,AppDate);
                SqlCommand cmd = new SqlCommand(value);
                cmd.Connection = connection;
                dr = cmd.ExecuteReader();

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return dr;
        }


        public static SqlDataReader CheckForPatientID(int PatID)
        {
            try
            {
                SqlConnection connection = GetCon();
                String value = String.Format("select * from Patient where PatientID = {0}",PatID);
                SqlCommand cmd = new SqlCommand(value);
                cmd.Connection = connection;
                dr = cmd.ExecuteReader();

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return dr;
        }


        public static SqlDataReader GetPatientAppointment(int PatID, string Date)
        {
            try
            {
                SqlConnection connection = GetCon();
                String value = String.Format("select * from Appointment where PatientID = {0} and DateOfApp = '{1}'",PatID,Date);
                SqlCommand cmd = new SqlCommand(value);
                cmd.Connection = connection;
                dr = cmd.ExecuteReader();

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return dr;
        }

        public static void DeleteAppointment(int PatID,string date,int slot)
        {
            try
            {
                SqlConnection connection = GetCon();
                String value = String.Format("delete from Appointment where PatientID={0} and DateOfApp='{1}' and Slot={2}",PatID,date,slot);
                SqlCommand cmd = new SqlCommand(value);
                cmd.Connection = connection;
                int i = cmd.ExecuteNonQuery();
                if (i == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    throw new Exception("PLEASE CHECK YOUR INPUTS AND TRY AGAIN");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("APPOINTMENT DELETED SUCCESSFULLY");
                }

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

    }
}