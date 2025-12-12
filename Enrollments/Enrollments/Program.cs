using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enrollments
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program obj = new Program();
            obj.Display_Courses();
            obj.Add_Student();
            obj.Search_ByDepartment();
            obj.Enrolled_Courses();
            obj.Update_Grade();
        }

        //Show: CourseId, CourseName, Credits, Semester 
        public void Display_Courses()
        {
            SqlConnection conn = new SqlConnection("Integrated security=true;database=EnrollmentDb;server=(localdb)\\MSSQLLocalDB");
            conn.Open();

            string query = "SELECT CourseId, CourseName, Credits, Semester FROM Courses";

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(reader["CourseId"] + " - " + reader["CourseName"] + " - " + reader["Credits"] + " - " + reader["Semester"]);
            }
            conn.Close();
        }

        // Add a new student 
        public void Add_Student()
        {
            Console.Write("Enter Full Name: ");
            string fullName = Console.ReadLine();

            Console.Write("Enter Email: ");
            string email = Console.ReadLine();

            Console.Write("Enter Department: ");
            string department = Console.ReadLine();

            Console.Write("Enter Year of Study: ");
            int yearOfStudy = int.Parse(Console.ReadLine());
            SqlConnection conn = new SqlConnection("Integrated security=true;database=EnrollmentDb;server=(localdb)\\MSSQLLocalDB");
            conn.Open();

            string query = "INSERT INTO Students (FullName, Email, Department, YearOfStudy) " +
                           "VALUES (@FullName, @Email, @Department, @YearOfStudy)";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@FullName", fullName);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Department", department);
            cmd.Parameters.AddWithValue("@YearOfStudy", yearOfStudy);

            cmd.ExecuteNonQuery();

            Console.WriteLine("Student added successfully");
            conn.Close();
        }

        // Search students by department 
        public void Search_ByDepartment()
        {
            Console.Write("Enter the Department Name: ");
            string department = Console.ReadLine();

            SqlConnection conn = new SqlConnection("Integrated security=true;database=EnrollmentDb;server=(localdb)\\MSSQLLocalDB");
            conn.Open();

            string query = "SELECT StudentId, FullName, Email, Department, YearOfStudy " +
                           "FROM Students WHERE Department = @Department";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Department", department);

            SqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("Students in " + department);

            while (reader.Read())
            {
                Console.WriteLine(reader["StudentId"] + " - " +reader["FullName"] + " - " +reader["Email"] + " - " +reader["YearOfStudy"]);
            }
            conn.Close();
        }

        //Display enrolled courses for a student 
        public void Enrolled_Courses()
        {
            Console.Write("Enter StudentId: ");
            int studentId = Convert.ToInt32(Console.ReadLine());

            SqlConnection conn = new SqlConnection("Integrated security=true;database=EnrollmentDb;server=(localdb)\\MSSQLLocalDB");
            conn.Open();

            string query =
                "SELECT c.CourseName, c.Credits, e.EnrollDate, e.Grade " +
                "FROM Enrollments e " +"JOIN Courses c ON e.CourseId = c.CourseId " +
                "WHERE e.StudentId = @StudentId";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@StudentId", studentId);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(reader["CourseName"] + " | " +reader["Credits"] + " | " +
                 Convert.ToDateTime(reader["EnrollDate"]).ToShortDateString() + " | " + reader["Grade"].ToString()
                );
            }
            conn.Close();
        }

        // Update grade (Connected Mode) 
        public void Update_Grade()
        {
            Console.Write("Enter the EnrollmentId: ");
            int enrollmentId = int.Parse(Console.ReadLine());

            Console.Write("Enter New Grade (A/B/C/D/F): ");
            string grade = Console.ReadLine();

            SqlConnection conn = new SqlConnection("Integrated security=true;database=EnrollmentDb;server=(localdb)\\MSSQLLocalDB");
            conn.Open();

            string query = "UPDATE Enrollments SET Grade = @Grade WHERE EnrollmentId = @EnrollmentId";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Grade", grade);
            cmd.Parameters.AddWithValue("@EnrollmentId", enrollmentId);

            int rows = cmd.ExecuteNonQuery();

            if (rows > 0)
                Console.WriteLine("Grade updated successfully");
            else
                Console.WriteLine("No record found with that EnrollmentId");

            conn.Close();
        }

    }
}


