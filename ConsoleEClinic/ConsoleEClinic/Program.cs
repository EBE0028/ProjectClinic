using System;
using System.Globalization;
using BusinessLayerEClinic;
namespace ConsoleEClinic
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BusinessLogic s1 = new BusinessLogic();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("############   WELCOME TO E-CLINIC   ##############");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            string NotIncludedChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,1234567890";
            //Login Screen validation :
            Console.WriteLine("PLEASE LOGIN WITH CORRECT USERNAME AND LOGIN ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("PLEASE ENTER YOUR USERNAME");
            string? username = Convert.ToString(Console.ReadLine());

            Console.WriteLine("PLEASE ENTER YOUR PASSWORD");
            string? password = Convert.ToString(Console.ReadLine());

            if (username.Length == 0 && password.Length == 0 && !password.Contains('@'))
            {
                throw new Exception("Please enter the valid Inputs to proceed");
            }
            int res = s1.UserNamePasswordValidation(username, password);
            if (res == 1)
            {
                Console.Clear();
                Console.WriteLine("Login Sucessfull");
                int Execute = 1;
                while (Execute==1)
                {
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("PLEASE SELECT ANYONE OF THE BELOW OPTION \n1.VIEW DOCTOR\n2.ADD PATIENT\n3.SCHEDULE AN APPOINTMENT\n4.DELETE AN APPOINTMENT\n5. PRESS ANY OTHER KEY TO LOGOUT");
                        int UserOption = Convert.ToInt32(Console.ReadLine());
                        //VIEW DOCTOR
                        if (UserOption == 1)
                        {
                            try
                            {
                                s1.ViewDoctor();
                                BusinessLogic.DisplaySlotTiming();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }

                            continue;

                        }
                        //ADD PATIENT
                        if (UserOption == 2)
                        {
                            try
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("ENTER YOUR FIRST NAME : ");
                                string? FName = Convert.ToString(Console.ReadLine());

                                Console.WriteLine("ENTER YOUR LAST NAME : ");
                                string? LName = Convert.ToString(Console.ReadLine());

                                Console.WriteLine("PLEASE ENTER ANY ONE \n1.M\n2.F\n3.O");
                                int Sex = Convert.ToInt32(Console.ReadLine());
                                string sex = "M";
                                if (Sex == 1)
                                {
                                    sex = "M";
                                }
                                if (Sex == 2)
                                {
                                    sex = "F";
                                }
                                if (Sex == 3)
                                {
                                    sex = "O";
                                }

                                Console.WriteLine("ENTER YOUR DATE OF BIRTH (DD/MM/YYYY-FORMAT): ");
                                DateTime UserDOB = DateTime.ParseExact(Console.ReadLine(), "d/M/yyyy", null);
                                foreach(var item in NotIncludedChar)
                                {
                                    if(FName.Contains(item) && LName.Contains(item))
                                    {
                                        throw new Exception("FIRST NAME AND LAST NAME CAN ONLY CONTAIN ALPHABETS ");
                                        break;
                                    }
                                }
                                if (Sex != 1 && Sex != 2 && Sex != 3 && FName.Length == 0 && LName.Length == 0 || UserDOB > Convert.ToDateTime(DateTime.Now.ToString("d/M/yyyy")))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("PLEASE ENTER ONLY THE VALID OPTION!!");
                                }
                                else
                                {
                                    s1.AddPatient(FName, LName, sex,UserDOB);
                                }
                                
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            continue;

                        }
                        //SCHEDULE APPOINTMENT
                        if (UserOption == 3)
                        {
                            try
                            {
                                Console.WriteLine("LET'S  BOOK AN APPOINTMENT");
                                s1.ViewDoctor();

                                Console.WriteLine("PLEASE ENTER THE DOCTOR'S ID FOR BOOKING AN APPOINTMENT ");
                                int DocID = Convert.ToInt32(Console.ReadLine());

                                Console.WriteLine("PLEASE ENTER THE DATE WHICH MUST BE FROM TOMORROW");
                                DateTime AppointmentDate = DateTime.ParseExact(Console.ReadLine(), "d/M/yyyy", null);

                                Console.WriteLine("PLEASE ENTER YOUR VALID PATIENT ID : ");
                                int PatID = Convert.ToInt32(Console.ReadLine());

                                if (AppointmentDate > DateTime.Now)
                                {
                                    s1.ScheduleAppointment(DocID, PatID,Convert.ToString(AppointmentDate));
                                    Console.WriteLine("\n");
                                }
                                else
                                {
                                    Console.WriteLine("APPOINTMENT DATE CAN'T BE A PAST DATE  !! PLEASE CHECK AND TRY AGAIN !!");
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            continue;
                        }
                        //DELETE APPOINTMENT
                        if (UserOption == 4)
                        {
                            try
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("LET'S CANCEL YOUR APPOINTMENT ");

                                Console.WriteLine("ENTER YOUR VALID PATIENT ID : ");
                                int PatientID = Convert.ToInt32(Console.ReadLine());

                                Console.WriteLine("ENTER YOUR DATE OF APPOINTMENT : ");
                                DateTime DateOfAppointment = DateTime.ParseExact(Console.ReadLine(), "d/M/yyyy", null);
                                if (PatientID == 0 && Convert.ToString(DateOfAppointment).Length==0)
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    throw new Exception("PLEASE CHECK YOUR INPUTS AND TRY AGAIN !!");
                                }
                                s1.DeleteAppointment(PatientID,Convert.ToString( DateOfAppointment));
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("Sorry you have entered a wrong option !! LOGING OUT !!");
                            Execute = 0;

                        }
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            else
            {
                Console.WriteLine("Username and Password Does not match.Please try again !!");
            }












        }



    }
}