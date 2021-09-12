using System;
using Model;
using HRMS;




namespace HRM
{
    class HRMSCLASS
    {
        static void Main(string[] args)
        {
            int choice ;

            dAL C = new dAL();
            
            C.DisplayHRMS();
            start:
            Console.WriteLine("enter your choice");
            Console.WriteLine("PRESS 1 TO ADD NEW EMPLOYEE");
            Console.WriteLine("PRESS 2 TO EDIT EXISTING EMPLOYEE");
            Console.WriteLine("press 3 TO DELETE THE SPECIFIC EMPLOYEE DETAIL");
            Console.WriteLine("press 4 TO SEARCH THE EMPLOYEE");

            // METHOD CALLS FROM MAIN METHOD


            choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {

                case 1:
                    Console.WriteLine("ADD NEW EMPLOYEE DETAILS");
                    model isEmpadd = C.GetInputFromUser();
                    C.AddNewHrms(isEmpadd);
                    C.DisplayHRMS();
                    break;
                case 2:
                    Console.WriteLine("EDIT EMPLOYEE DETAILS REFERING THE ABOVE DATABASE");
                    model isEmpedit = C.GetInputFromUser();
                    C.EditNewHrms(isEmpedit);
                    C.DisplayHRMS();
                    break;


                case 3:
                    C.DeleteNewHrms();
                    C.DisplayHRMS();
                    break;
                case 4:
                    C.SearchInHrms();
                    C.DisplayHRMS();
                    break;
            }
            Console.WriteLine("press 1 to continue");
            Console.WriteLine("press 2 to exit");
            int option = Convert.ToInt32(Console.ReadLine());

            if(option == 1)
            {
                goto start;
            }
            else
            {
                Console.WriteLine("thank you");
            }












           




        }
    }
}
