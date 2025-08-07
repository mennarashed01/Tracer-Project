using System;
using System.Threading.Channels;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tracer_project
{
    internal class Program
    {
        static string[,] Tasks = new string[1000, 5];
        static int numberOfTasks = 0;

        //Run Program 
        static void RunProgram()
        {
            while (true)
            {
                
                Console.WriteLine("_____________________________________________________");
                Console.WriteLine("1. Add Task");
                Console.WriteLine("2. Update Status");
                Console.WriteLine("3. View Tasks");
                Console.WriteLine("4. Delete Task");
                Console.WriteLine("5. Exit");
                Console.WriteLine("_____________________________________________________");
                int.TryParse(Console.ReadLine(), out int choice);

                if (choice == 1)
                    AddTask();
                else if (choice == 2)
                    UpdateTask();
                else if (choice == 3)
                {
                    Console.WriteLine("1.View all Tasks. ");
                    Console.WriteLine("2. View Tasks by category. "); 
                    Console.WriteLine("_____________________________________________________");

                    int.TryParse(Console.ReadLine(), out int num);

                    if (num == 1)
                        ViewAllTask();
                    else if (num == 2)
                        ViewTasksByCategory();
                    else
                        Console.WriteLine("enter valid Choice");
                }
                else if (choice == 4)
                    DeleteTask();
                else if (choice == 5)
                    return;
                else
                    Console.WriteLine("Enter Valid Choice");
            }
 
        }

        //Add task
        static void AddTask()
        {
            
            Console.Clear();

            string title, description, due_date, priority;
            Console.WriteLine("Add New Task: ");

            Console.Write("Enter Title: ");
            title = Console.ReadLine();

            Console.Write("Enter Description: ");
            description = Console.ReadLine();

            Console.Write("Enter Due Date: ");
            due_date = Console.ReadLine();

            Console.Write("Enter Priority: ");
            priority = Console.ReadLine();

            Tasks[numberOfTasks, 0] = title;
            Tasks[numberOfTasks, 1] = description;
            Tasks[numberOfTasks, 2] = due_date;
            Tasks[numberOfTasks, 3] = priority;
            Tasks[numberOfTasks, 4] = "pending";

            numberOfTasks++;

            Console.WriteLine("Task Added Successfully :)");
        }

        //Update Status
        static void UpdateTask()
        {
            Console.Write("Enter number of Task: ");
            int.TryParse(Console.ReadLine(), out int numoftask);

            if ( numoftask < 1 || numoftask > numberOfTasks)
            {
                Console.WriteLine("Task not found");
                return;
            }

            Console.WriteLine($"Current status: {Tasks[numoftask -1, 4]}");
            Console.Write("Enter new Status (Pending , In Progress, Completed) : ");
            string newStatus = Console.ReadLine();

            Tasks[numoftask -1 , 4] = newStatus;
            Console.WriteLine("Status Updated Successfully :)");
        }

        //view Tasks
        static void ViewAllTask()
        {
            Console.Clear();
            
            for (int i = 0; i < numberOfTasks; i++)
            {
                printTask(i);
            }
        }

        //view Tasks by category
        static void ViewTasksByCategory ()
        {
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Choose category to view:");
            Console.WriteLine("1. Active Tasks"); 
            Console.WriteLine("2. Overdue Tasks "); 
            Console.WriteLine("3. Completed Tasks");
            Console.WriteLine("-----------------------------------");

            
            int.TryParse(Console.ReadLine(), out int num);
            bool found = false;
            

            for (int i = 0; i < numberOfTasks; i++)
            {
                DateTime dueDate;
                string status = Tasks[i, 4];
                string dueDateStr = Tasks[i, 2];
                bool validDate = DateTime.TryParse(dueDateStr, out dueDate);

                if (num == 1 && status.ToLower() != "completed" )
                {
                    printTask(i);
                    found = true;
                }
                else if (num == 2 && validDate &&  status.ToLower() != "completed" && dueDate < DateTime.Today)
                {
                    printTask(i);
                    found = true;
                }
                else if (num == 3 && status.ToLower() == "completed" )
                {
                    printTask(i);
                    found = true;
                }
            }

            if(!found)
            {
                Console.WriteLine("No Tasks in This Category");
            }
            
        }
        
        //print one task
        static void printTask(int t)
        {
            Console.WriteLine($"Task {t + 1}");
            Console.WriteLine($"Title: {Tasks[t, 0]}");
            Console.WriteLine($"Description: {Tasks[t, 1]}");
            Console.WriteLine($"Due Date: {Tasks[t, 2]}");
            Console.WriteLine($"Priority: {Tasks[t, 3]}");
            Console.WriteLine($"Status: {Tasks[t, 4]} ");
            Console.WriteLine("********************************************************\n");
        }

        //Delete tasks
        static void DeleteTask()
        {
            Console.Write("Enter number of Task to delete it: ");
            int.TryParse(Console.ReadLine(), out int deletenum);

            if(deletenum <1 || deletenum > numberOfTasks)
            {
                Console.WriteLine("Task not found");
                return;
            }

            Console.WriteLine($"Are you sure to delete Task {deletenum}? y or n");
            string ch = Console.ReadLine();
            if (ch.ToLower() == "y")
            {
                int index = deletenum - 1;
                for(int i = index; i < numberOfTasks -1; i++)
                {
                    for (int j = 0; j< 5; j++ )
                    {
                        Tasks[i, j] = Tasks[i + 1, j];
                    }
                    
                }

                for (int i = 0;i < 5; i++)
                {
                    Tasks[numberOfTasks - 1, i] = null;
                }
                numberOfTasks--;

                Console.WriteLine("Task Deleted Successfully :) ");
            }
            else
            {
                return;
            }

        }


        static void Main(string[] args)
        {
            RunProgram();
        }
    }
}
