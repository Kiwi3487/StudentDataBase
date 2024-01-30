// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;

// Define the Student class
public class Student
{
    public string StudentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<Course> EnrolledCourses { get; set; } = new List<Course>();
    public Dictionary<Course, Grades> CourseGrades { get; set; } = new Dictionary<Course, Grades>();
}

// Define the Course class
public class Course
{
    public string CourseCode { get; set; }
    public string CourseName { get; set; }
    public List<Student> EnrolledStudents { get; set; } = new List<Student>();
}

public class Grades
{
    public double Assignments { get; set; }
    public double Quizzes { get; set; }
    public double Exams { get; set; }
    public double FinalMark { get; set; }
    }

class Program
{
    static List<Student> students = new List<Student>();
    static List<Course> courses = new List<Course>();

    static void Main()
    {
        bool exitProgram = false;

        do
        {
            Console.WriteLine("1. Add Student, Course, and Grades");
            Console.WriteLine("2. Display All Information");
            Console.WriteLine("3. Exit");

            Console.Write("Select an option (1-3): ");
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    AddStudentCourseAndGrades();
                    break;

                case "2":
                    DisplayAllInformation();
                    break;

                case "3":
                    exitProgram = true;
                    break;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }

        } while (!exitProgram);
    }

    static void AddStudentCourseAndGrades()
    {
        Console.Write("Enter student first name: ");
        string firstName = Console.ReadLine();

        Console.Write("Enter student last name: ");
        string lastName = Console.ReadLine();

        Student newStudent = new Student
        {
            FirstName = firstName,
            LastName = lastName
        };

        students.Add(newStudent);

        Console.WriteLine($"{firstName} {lastName} has been added to the system!");

        Console.Write("Enter course name: ");
        string courseName = Console.ReadLine();

        Course newCourse = new Course
        {
            CourseName = courseName
        };

        courses.Add(newCourse);

        Console.WriteLine($"{courseName} has been added to the student!");

        int assignmentGrade, quizGrade, examGrade;

        //only accept numbers between 0-100
        do
        {
            Console.Write("Enter assignment grade (0-100): ");
        } while (!int.TryParse(Console.ReadLine(), out assignmentGrade) || assignmentGrade < 0 || assignmentGrade > 100);

        do
        {
            Console.Write("Enter quiz grade (0-100): ");
        } while (!int.TryParse(Console.ReadLine(), out quizGrade) || quizGrade < 0 || quizGrade > 100);

        do
        {
            Console.Write("Enter exam grade (0-100): ");
        } while (!int.TryParse(Console.ReadLine(), out examGrade) || examGrade < 0 || examGrade > 100);

        //calculate final grade with proper weighting
        double weightedAssignments = assignmentGrade * 0.4;
        double weightedQuizzes = quizGrade * 0.3;
        double weightedExams = examGrade * 0.3;

        Grades grades = new Grades
        {
            Assignments = weightedAssignments,
            Quizzes = weightedQuizzes,
            Exams = weightedExams,
            FinalMark = (weightedAssignments + weightedQuizzes + weightedExams)
        };

        newStudent.EnrolledCourses.Add(newCourse);
        newStudent.CourseGrades[newCourse] = grades;

        Console.WriteLine($"Grades added successfully!");
    }

    static void DisplayAllInformation()
    {
        foreach (var student in students)
        {
            Console.WriteLine($"Student: {student.FirstName} {student.LastName}");

            foreach (var course in student.EnrolledCourses)
            {
                Console.WriteLine($"Course:  {course.CourseName}");
                var grades = student.CourseGrades.ContainsKey(course) ? student.CourseGrades[course] : null;
                if (grades != null)
                {
                    Console.WriteLine($"    Assignments: {grades.Assignments}, Quizzes: {grades.Quizzes}, Exams: {grades.Exams}, Final Mark: {grades.FinalMark}%");
                }
                else
                {
                    Console.WriteLine("    No grades recorded for this course.");
                }
            }

            Console.WriteLine();
        }
    }
}
