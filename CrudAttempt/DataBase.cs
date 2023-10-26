using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.Security.Cryptography;

namespace CrudAttempt
{
    /* internal class DataBase
     {
         private SQLiteConnection connection;

         public DataBase()
         {
             string databasePath = @"D:\DataBases\CrudData.db";
             connection = new SQLiteConnection($"Data Source={databasePath};Version=3;");
             try
             {
                 connection.Open();
                 Console.WriteLine("Bağlandı Koç");
             }
             catch (Exception ex)
             {
                 Console.WriteLine($"Error: {ex.Message}");
             }
         }

         public void CloseConnection() 
         {
             if (connection.State == ConnectionState.Open)
             {
                 connection.Close();
             }
         }
     } */

    public class Student
    {
        public void InsertStudent(string Name, string SurName, int ClassID, int TeacherID, int MidTerm, int Final)
        {
            string databasePath = @"Data Source=D:\DataBases\CrudData.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(databasePath))
            {

                connection.Open();

                string insertQuery = "INSERT INTO Student (Name, SurName, ClassID, TeacherID, MidTerm, Final) VALUES (@Name, @SurName, @ClassID, @TeacherID, @Midterm, @Final)";

                using (SQLiteCommand cmd = new SQLiteCommand(insertQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@Name", Name);
                    cmd.Parameters.AddWithValue("@SurName", SurName);
                    cmd.Parameters.AddWithValue("@ClassID", ClassID);
                    cmd.Parameters.AddWithValue("@TeacherID", TeacherID);
                    cmd.Parameters.AddWithValue("@MidTerm", MidTerm);
                    cmd.Parameters.AddWithValue("@Final", Final);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Öğrenci başarıyla eklendi");
                    }
                    else
                    {
                        Console.WriteLine("Öğrenci ekleme işlemi başarısız oldu");
                    }
                }
            }
        }

        public void GetAllStudent()
        {
            string databasePath = @"Data Source=D:\DataBases\CrudData.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(databasePath))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM Student";

                using (SQLiteCommand cmd = new SQLiteCommand(selectQuery, connection))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("ID"));
                            string name = reader.GetString(reader.GetOrdinal("Name"));
                            string surName = reader.GetString(reader.GetOrdinal("SurName"));
                            int classId = reader.GetInt32(reader.GetOrdinal("ClassID"));
                            int teacherId = reader.GetInt32(reader.GetOrdinal("TeacherID"));
                            int midTerm = reader.GetInt32(reader.GetOrdinal("MidTerm"));
                            int final = reader.GetInt32(reader.GetOrdinal("Final"));

                            Console.WriteLine($"ID: {id}, Name: {name}, surName: {surName}, ClassID: {classId}, TeacherID: {teacherId}, MidTerm: {midTerm}, Final: {final}");
                        }
                    }
                }
            }
        }

        public void GetStudentByID(int ID)
        {
            string databasePath = @"Data Source=D:\DataBases\CrudData.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(databasePath))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM Student WHERE ID = @ID";

                using (SQLiteCommand cmd = new SQLiteCommand(selectQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@ID", ID);

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("ID"));
                            string name = reader.GetString(reader.GetOrdinal("Name"));
                            string surName = reader.GetString(reader.GetOrdinal("SurName"));
                            int classId = reader.GetInt32(reader.GetOrdinal("ClassID"));
                            int teacherId = reader.GetInt32(reader.GetOrdinal("TeacherID"));
                            int midTerm = reader.GetInt32(reader.GetOrdinal("MidTerm"));
                            int final = reader.GetInt32(reader.GetOrdinal("Final"));

                            Console.WriteLine($"ID: {id}, Name: {name}, surName: {surName}, ClassID: {classId}, TeacherID: {teacherId}, MidTerm: {midTerm}, Final: {final}");
                        }
                        else
                        {
                            Console.WriteLine("Belirtilen ID'ye göre bir öğretmen bulunamadı");
                        }
                    }
                }
            }
        }

        public void UpdateStudent(int ID, string Name, string SurName, int ClassID, int TeacherID, int MidTerm, int Final)
        {
            string databasePath = @"Data Source=D:\DataBases\CrudData.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(databasePath))
            {
                connection.Open();

                string updateQuery = "UPDATE Student SET Name = @Name, SurName = @SurName, ClassID = @ClassID, TeacherID = @TeacherID, MidTerm = @MidTerm, Final = @Final WHERE ID = @ID";

                using (SQLiteCommand cmd = new SQLiteCommand(updateQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@Name", Name);
                    cmd.Parameters.AddWithValue("@SurName", SurName);
                    cmd.Parameters.AddWithValue("@ClassID", ClassID);
                    cmd.Parameters.AddWithValue("@TeacherID", TeacherID);
                    cmd.Parameters.AddWithValue("@MidTerm", MidTerm);
                    cmd.Parameters.AddWithValue("@Final", Final);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Öğrenci başarıyla güncellendi");
                    }
                    else
                    {
                        Console.WriteLine("Öğrenci güncelleme işlemi başarısız oldu");
                    }
                }
            }
        }

        public void DeleteStudent(int ID)
        {
            string databasePath = @"Data Source=D:\DataBases\CrudData.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(databasePath))
            {
                connection.Open();

                string deleteQuery = "DELETE FROM Student WHERE ID = @ID";

                using (SQLiteCommand cmd = new SQLiteCommand(deleteQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@ID", ID);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Öğrenci başarıyla silindi");
                    }
                    else
                    {
                        Console.WriteLine("Öğrenci silme işlemi başarısız oldu");
                    }
                }
            }
        }
    }

    public class Lesson
    {
        public void InsertClass(string Name, int TeacherID)
        {
            string databasePath = @"Data Source=D:\DataBases\CrudData.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(databasePath))
            {
                connection.Open();

                string instertQuery = "INSERT INTO Class (Name, TeacherID) VALUES (@Name, @TeacherID)";

                using (SQLiteCommand cmd = new SQLiteCommand(@instertQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@Name", Name);
                    cmd.Parameters.AddWithValue("@TeacherID", TeacherID);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Ders başarıyla eklendi");
                    }
                    else
                    {
                        Console.WriteLine("Ders ekleme işlemi başarısız oldu");
                    }
                }
            }
        }

        public void GetAllClass()
        {
            string databasePath = @"Data Source=D:\DataBases\CrudData.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(databasePath))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM Class";

                using (SQLiteCommand cmd = new SQLiteCommand(selectQuery, connection))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("ID"));
                            string name = reader.GetString(reader.GetOrdinal("Name"));
                            int teacherId = reader.GetInt32(reader.GetOrdinal("TeacherID"));

                            Console.WriteLine($"ID: {id}, Name: {name}, TeacherID: {teacherId}");
                        }
                    }
                }
            }
        }

        public void GetClassByID(int ID)
        {
            string databasePath = @"Data Source=D:\DataBases\CrudData.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(databasePath))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM Class WHERE ID = @ID";

                using (SQLiteCommand cmd = new SQLiteCommand(selectQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@ID", ID);

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("ID"));
                            string name = reader.GetString(reader.GetOrdinal("Name"));
                            int teacherId = reader.GetInt32(reader.GetOrdinal("TeacherID"));

                            Console.WriteLine($"ID: {id}, Name: {name}, TeacherID: {teacherId}");
                        }
                        else
                        {
                            Console.WriteLine("Belirtilen ID'ye göre bir ders bulunamadı");
                        }
                    }
                }
            }
        }

        public void UpdateClass(int ID, string Name, int TeacherID)
        {
            string databasePath = @"Data Source=D:\DataBases\CrudData.db;Version=3;";
            using (SQLiteConnection connect = new SQLiteConnection(databasePath))
            {
                connect.Open();

                string updateQuery = "UPDATE Class SET Name = @Name, TeacherID = @TeacherID WHERE ID = @ID";

                using (SQLiteCommand cmd = new SQLiteCommand(@updateQuery, connect))
                {
                    cmd.Parameters.AddWithValue("@Name", Name);
                    cmd.Parameters.AddWithValue("@TeacherID", TeacherID);
                    cmd.Parameters.AddWithValue("@ID", ID);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Ders başarıyla güncellendi");
                    }
                    else
                    {
                        Console.WriteLine("Ders güncelleme işlemi başarısız oldu");
                    }
                }
            }
        }

        public void DeleteClass(int ID)
        {
            string databasePath = @"Data Source=D:\DataBases\CrudData.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(databasePath))
            {
                connection.Open();

                string deleteQuery = "DELETE FROM Class WHERE ID = @ID";

                using (SQLiteCommand cmd = new SQLiteCommand(deleteQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@ID", ID);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Ders başarıyla silindi");
                    }
                    else
                    {
                        Console.WriteLine("Ders silme işlemi başarısız oldu");
                    }
                }
            }
        }
    }

    public class Teacher
    {
        public void InsertTeacher(string Name, string SurName)
        {
            string databasePath = @"Data Source=D:\DataBases\CrudData.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(databasePath))
            {
                connection.Open();

                string insertQuery = "INSERT INTO Teacher (Name, SurName) VALUES (@Name, @SurName)";

                using (SQLiteCommand cmd = new SQLiteCommand(insertQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@Name", Name);
                    cmd.Parameters.AddWithValue("@SurName", SurName);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Öğretmen başarıyla eklendi.");
                    }
                    else
                    {
                        Console.WriteLine("Öğretmen ekleme işlemi başarısız oldu.");
                    }
                }
            }
        }

        public void GetAllTeacher()
        {
            string databasePath = @"Data Source=D:\DataBases\CrudData.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(databasePath))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM Teacher";

                using (SQLiteCommand cmd = new SQLiteCommand(selectQuery, connection))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("ID"));
                            string name = reader.GetString(reader.GetOrdinal("Name"));
                            string surName = reader.GetString(reader.GetOrdinal("SurName"));

                            Console.WriteLine($"ID: {id}, Name: {name}, SurName: {surName}");
                        }
                    }
                }
            }
        }

        public void GetTeacherByID(int ID)
        {
            string databasePath = @"Data Source=D:\DataBases\CrudData.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(databasePath))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM Teacher WHERE ID = @ID";

                using (SQLiteCommand cmd = new SQLiteCommand(selectQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@ID", ID);

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("ID"));
                            string name = reader.GetString(reader.GetOrdinal("Name"));
                            string surName = reader.GetString(reader.GetOrdinal("SurName"));

                            Console.WriteLine($"ID: {id}, Name: {name}, SurName: {surName}");
                        }
                        else
                        {
                            Console.WriteLine("Belirtilen ID'ye göre bir öğretmen bulunamadı");
                        }
                    }
                }
            }
        }

        public void UpdateTeacher(int ID, string Name, string SurName)
        {
            string databasePath = @"Data Source=D:\DataBases\CrudData.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(databasePath))
            {
                connection.Open();

                string updateQuery = "UPDATE Teacher SET Name = @Name, SurName = @SurName WHERE ID = @ID";

                using (SQLiteCommand cmd = new SQLiteCommand(updateQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@Name", Name);
                    cmd.Parameters.AddWithValue("@SurName", SurName);
                    cmd.Parameters.AddWithValue("@ID", ID);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Öğretmen başarıyla güncellendi.");
                    }
                    else
                    {
                        Console.WriteLine("Öğretmen güncelleme işlemi başarısız oldu.");
                    }
                }
            }
        }

        public void DeleteTeacher(int ID)
        {
            string databasePath = @"Data Source=D:\DataBases\CrudData.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(databasePath))
            {
                connection.Open();

                string deleteQuery = "DELETE FROM Teacher WHERE ID = @ID";

                using (SQLiteCommand cmd = new SQLiteCommand(deleteQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@ID", ID);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Öğretmen başarıyla silindi");
                    }
                    else
                    {
                        Console.WriteLine("Öğretmen silme işlemi başarısız oldu");
                    }
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Teacher teacher = new Teacher();
            Lesson lesson = new Lesson();
            Student student = new Student();

            // teacher.UpdateTeacher(1, "Kali", "Gürkaraman");
            // teacher.InsertTeacher("Hakkı", "Bulut");
            // teacher.GetTeacherByID(1);
            // teacher.GetAllTeacher();
            // teacher.DeleteTeacher(1);

            // lesson.UpdateClass(1, "Sinyaller ve Sistemler", 1);
            // lesson.InsertClass("Devreler ve Tasarım", 1);
            // lesson.GetClassByID(1);
            // lesson.GetAllClass();
            // lesson.DeleteClass(1);

            // student.UpdateStudent(1, "Ayşe", "Akbaba", 1, 1, 98, 100);
            // student.InsertStudent("Ayşenur", "Tüfekçi", 1, 1, 90, 100);
            // student.GetStudentByID(1);
            // student.GetAllStudent();
            // student.DeleteStudent(1);
        }
    }
}