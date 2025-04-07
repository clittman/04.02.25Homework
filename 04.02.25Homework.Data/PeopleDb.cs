using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04._02._25Homework.Data
{
    public class PeopleDb
    {
        private readonly string _connectionString;

        public PeopleDb(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Person> GetPeople()
        {
            List<Person> people = new();

            using SqlConnection connection = new(_connectionString);
            using SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM People";
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                people.Add(new Person
                {
                    Id = (int)reader["Id"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Age = (int)reader["Age"]
                });
            }

            return people;
        }

        public void AddPeople(List<Person> people)
        {
            using SqlConnection connection = new(_connectionString);
            using SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO PEOPLE " +
                "VALUES (@firstName, @lastName, @age)";
            connection.Open();

            foreach(Person p in people)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@firstName", p.FirstName);
                cmd.Parameters.AddWithValue("@lastName", p.LastName);
                cmd.Parameters.AddWithValue("@age", p.Age);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteMany(List<int> ids)
        {
            string parameters = $"@id{0}";
            for(int i = 0; i < ids.Count; i++)
            {
                parameters += $", @id{i}";
            }

            using SqlConnection connection = new(_connectionString);
            using SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = $"Delete FROM People WHERE Id IN ({parameters})";

            for (int i = 0; i < ids.Count; i++)
            {
                cmd.Parameters.AddWithValue($"@id{i}", ids[i]);
            }
            connection.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}
