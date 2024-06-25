using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MedicalReferral
{
    public static class DatabaseHelper
    {
        private const string ConnectionString = "Data Source=MedicalReferrals.db;Version=3;";

        public static void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string createTableQuery = @"CREATE TABLE IF NOT EXISTS Referrals (
                                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                            PatientName TEXT NOT NULL,
                                            DoctorName TEXT NOT NULL,
                                            ExaminationType TEXT NOT NULL,
                                            Date TEXT NOT NULL)";
                using (var command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void AddReferral(Referral referral)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string insertQuery = "INSERT INTO Referrals (PatientName, DoctorName, ExaminationType, Date) VALUES (@PatientName, @DoctorName, @ExaminationType, @Date)";
                using (var command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@PatientName", referral.PatientName);
                    command.Parameters.AddWithValue("@DoctorName", referral.DoctorName);
                    command.Parameters.AddWithValue("@ExaminationType", referral.ExaminationType);
                    command.Parameters.AddWithValue("@Date", referral.Date.ToString("yyyy-MM-dd"));
                    command.ExecuteNonQuery();
                }
            }
        }

        public static List<Referral> GetAllReferrals()
        {
            var referrals = new List<Referral>();

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string selectQuery = "SELECT * FROM Referrals";
                using (var command = new SQLiteCommand(selectQuery, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        referrals.Add(new Referral
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            PatientName = reader["PatientName"].ToString(),
                            DoctorName = reader["DoctorName"].ToString(),
                            ExaminationType = reader["ExaminationType"].ToString(),
                            Date = DateTime.Parse(reader["Date"].ToString())
                        });
                    }
                }
            }

            return referrals;
        }
    }
}
