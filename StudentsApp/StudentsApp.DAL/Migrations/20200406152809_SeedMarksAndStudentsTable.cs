using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentsApp.DAL.Migrations
{
    public partial class SeedMarksAndStudentsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Students (Name) Values ('Ivan Vasin')");
            migrationBuilder.Sql("INSERT INTO Students (Name) Values ('Petya Petrov')");
            migrationBuilder.Sql("INSERT INTO Students (Name) Values ('Nikita Sidorov')");
            migrationBuilder.Sql("INSERT INTO Students (Name) Values ('Feodor Trushyn')");
                
            migrationBuilder.Sql("INSERT INTO Marks (Grade, StudentId) Values (5, (SELECT Id FROM Students WHERE Name = 'Feodor Trushyn'))");
            migrationBuilder.Sql("INSERT INTO Marks (Grade, StudentId) Values (4, (SELECT Id FROM Students WHERE Name = 'Feodor Trushyn'))");
        
            
            migrationBuilder.Sql("INSERT INTO Marks (Grade, StudentId) Values (2, (SELECT Id FROM Students WHERE Name = 'Ivan Vasin'))");
        
            
            migrationBuilder.Sql("INSERT INTO Marks (Grade, StudentId) Values (3, (SELECT Id FROM Students WHERE Name = 'Petya Petrov'))");
            migrationBuilder.Sql("INSERT INTO Marks (Grade, StudentId) Values (4, (SELECT Id FROM Students WHERE Name = 'Petya Petrov'))");
            migrationBuilder.Sql("INSERT INTO Marks (Grade, StudentId) Values (5, (SELECT Id FROM Students WHERE Name = 'Petya Petrov'))");

            
            migrationBuilder.Sql("INSERT INTO Marks (Grade, StudentId) Values (3, (SELECT Id FROM Students WHERE Name = 'Nikita Sidorov'))");
            migrationBuilder.Sql("INSERT INTO Marks (Grade, StudentId) Values (3, (SELECT Id FROM Students WHERE Name = 'Nikita Sidorov'))");
       
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Marks");

            migrationBuilder.Sql("DELETE FROM Students");
        }
    }
}
