create database EnrollmentDb

create table Students
(
StudentId INT IDENTITY(1,1) PRIMARY KEY,
FullName VARCHAR(100) NOT NULL,
Email VARCHAR(100) UNIQUE,
Department VARCHAR(50) NOT NULL,
YearOfStudy INT NOT NULL
)
insert into Students (FullName, Email, Department, YearOfStudy)
values ('Sakthivel', 'sakthi@gmail.com', 'Computer Science', 4),('Sachin', 'sachin@gmail.com', 'AIDS', 3),
('Karthik', 'karthi@gmail.com', 'IT', 4),('Madusree', 'sree@gmail.com', 'ECE', 3),('Selvi', 'selvi@gmail.com', 'MECH', 2);

create table Courses
(
 CourseId INT IDENTITY(1,1) PRIMARY KEY,
 CourseName VARCHAR(100) NOT NULL,
 Credits INT NOT NULL,
 Semester VARCHAR(20) NOT NULL
)

insert into Courses (CourseName, Credits, Semester) 
values('Data Structures', 4, 'Semester 8'),('Software engineerning', 3, 'Semester 6'),
('Operating system', 4, 'Semester 8'),('Cloud computing', 3, 'Semester 6'),
('Database Management Systems', 4, 'Semester 4');

create table Enrollments
(
EnrollmentId INT IDENTITY(1,1) PRIMARY KEY,
StudentId INT NOT NULL,FOREIGN KEY (StudentId) REFERENCES Students(StudentId),
CourseId INT NOT NULL,FOREIGN KEY (CourseId) REFERENCES Courses(CourseId),
EnrollDate DATETIME NOT NULL,
Grade VARCHAR(5),
)

insert into Enrollments (StudentId, CourseId, EnrollDate, Grade)
values(1, 1, '2024-01-10', 'A'),(2, 3, '2024-01-12', 'B+'),
(3, 2, '2024-01-15', 'A'),(4, 5, '2024-01-18', NULL),(5, 4, '2024-01-20', 'B');

create procedure usp_GetCoursesBySemester
    @semester NVARCHAR(50)
as
begin
    select CourseId,CourseName,Credits,Semester
    from Courses where Semester = @semester;
end;



