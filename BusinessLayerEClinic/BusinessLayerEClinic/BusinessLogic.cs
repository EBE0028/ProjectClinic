using DataLayerEClinic;
using System.Collections;
using System.Data.SqlClient;
using System.Globalization;

namespace BusinessLayerEClinic
{
    public class BusinessLogic
    {

        public static SqlDataReader data;

        public static void DisplaySlotTiming()
        {
 
            Console.WriteLine("SLOT TIMINGS : ");
            Console.WriteLine("SLOT 1 - 9 AM - 10 AM");
            Console.WriteLine("SLOT 2 - 10 AM - 11 AM");
            Console.WriteLine("SLOT 3 - 11 AM - 12 PM");
            Console.WriteLine("SLOT 4 - 1 PM - 2 PM");
            Console.WriteLine("SLOT 5 - 2 PM - 3 PM");
            Console.WriteLine("SLOT 6 - 3 PM - 4 PM");
            Console.WriteLine("SLOT 7 - 5 PM - 6 PM");
            Console.WriteLine("SLOT 8 - 7 PM - 8 PM");
            Console.WriteLine("!! IMPORTANT TO NOTE : SLOTS AVAILABILITY IS BASED ON DOCTOR'S VISTING HOURS");
            

        }
        private static int CalculateAge(DateTime dateOfBirth)
        {
            int age = 0;
            age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age = age - 1;

            return age;
        }

        public int UserNamePasswordValidation(string username,string password)
        {
            int Flag = 0;
            data = DataLayerClass.FetchStaffDetails(username);
            while (data.Read())
            {
                for (int i = 0; i < data.FieldCount; i++)
                {
                    if (Convert.ToString(data[3]) == username && Convert.ToString(data[4]) == password)
                    {
                        Flag = 1;
                        break;
                    }
                }
            }
            return Flag;
        }


        public void ViewDoctor()
        {
            data = DataLayerClass.FetchAllDoctor();
            while (data.Read())
            {
                for (int i = 0; i < data.FieldCount; i++)
                {
                    Console.Write("Doctor's ID :" + data[0]+" | ");
                    Console.Write("Doctor's First Name :" + data[1] + " | ");
                    Console.Write("Doctor's Last Name :" + data[2] + " | ");
                    Console.Write("Doctor's Sex :" + data[3] + " | ");
                    Console.Write("Doctor's Specialization :" + data[4] + " | ");
                    Console.Write("Doctor's Visiting hours Slots :" + data[5] + " | ");
                    break;

                }
                Console.WriteLine();
            }
        }

        public int AddPatient(string FName,string LName,string Sex,DateTime DOB)
        {
            DataLayerClass Obj = new DataLayerClass();
            int Age=CalculateAge(DOB);
            if(Age>120 && Age<0)
            {
                return 0;
                Console.ForegroundColor = ConsoleColor.Red;
                throw new Exception("AGE CAN'T BE GREATER THAN 120 YEARS AND LESS THAN 0 YEARS");
            }
            string FinalFormattedDate = DOB.ToString("d/M/yyyy", CultureInfo.InvariantCulture);
            int res = Obj.AddPatient(FName, LName, Sex, Age, FinalFormattedDate);
            return res;
        }

        public void ScheduleAppointment(int DocID,int PaTID,string AppDate)
        {
            var FilledAppointment = new ArrayList();
            FilledAppointment.Add(0);
            var AvailableSlot = new ArrayList();
            int DoctorExists = 0;
            data = DataLayerClass.FetchDoc(DocID);
            int visitingHr = 0;
            while (data.Read())
            {
                for (int i = 0; i < data.FieldCount; i++)
                {
                    DoctorExists = 1;
                    visitingHr =Convert.ToInt32(data[5]);
                    break;

                }
                Console.WriteLine();
            }
            if(DoctorExists==0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new Exception("DOCTOR ID YOU HAVE ENTERED IS WRONG !!");

            }

            data = DataLayerClass.CheckForApp(DocID,AppDate);
            while (data.Read())
            {
                for (int i = 0; i < data.FieldCount; i++)
                {
                    DoctorExists=1;
                    FilledAppointment.Add(Convert.ToInt32(data[4]));
                    break;

                }
                Console.WriteLine();
            }
            if (DoctorExists == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new Exception("DOCTOR ID YOU HAVE ENTERED IS WRONG !!");

            }
            Console.WriteLine();
            for (int i=0;i<=visitingHr;i++)
            {
                if (!FilledAppointment.Contains(i))
                {
                    AvailableSlot.Add(i);
                    Console.WriteLine("AVAILABLE SLOTS : "+ i);
                }
            }
            Console.WriteLine();
            DisplaySlotTiming();
            Console.WriteLine("Please enter the Slot to be booked");
            int slotEnteredByUser = Convert.ToInt32(Console.ReadLine());
            if(!AvailableSlot.Contains(slotEnteredByUser))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new Exception("YOU HAVE ENTERED THE SLOT WHICH IS ALREADY BOOKED PLEASE TRY BOOK ONLY ON THE SLOT WHICH IS AVAILABLE !! ");
                
            }
            
            int PatientExists = 0;
            data = DataLayerClass.CheckForPatientID(PaTID);
            while(data.Read())
            {
                for (int i = 0; i < data.FieldCount; i++)
                {

                    PatientExists = 1;
                    break;

                }
            }
            if(PatientExists==0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new Exception("ENTERED USER IS INVALID PLEASE BOOK THE SLOT AGAIN !!");
            }

            DataLayerClass Obj = new DataLayerClass();
            Obj.AddApp(DocID, PaTID, AppDate, slotEnteredByUser);

        }


        public void DeleteAppointment(int PatientID, string AppDate)
        {
            var BookSlot = new ArrayList();
            int AppointmentPresent = 0;
            data = DataLayerClass.GetPatientAppointment(PatientID, AppDate);
            while (data.Read())
            {
                for (int i = 0; i < data.FieldCount; i++)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("Appointment ID : " + data[0] + " | ");
                    Console.Write("Doctor's ID : " + data[1] + " | ");
                    Console.Write("Your ID : " + data[2] + " | ");
                    Console.Write("Date of Appointment : " + data[3] + " | ");
                    Console.Write("Slot Number Booked : " + data[4] + " | ");
                    BookSlot.Add(Convert.ToInt32(data[4]));
                    AppointmentPresent = 1;
                    break;

                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
            if (AppointmentPresent == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                throw new Exception("There is no active Appointment for the mentioned day  ");
            }
            Console.WriteLine("Enter the Slot number : ");
            int SlotEnteredByUser = Convert.ToInt32(Console.ReadLine());
            if (AppointmentPresent == 1)
            { 
                if (!BookSlot.Contains(SlotEnteredByUser))
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    throw new Exception("Please enter the valid slot number !!");
                }
            }
            DataLayerClass.DeleteAppointment(PatientID,AppDate,SlotEnteredByUser);

        }

    }
}