using People.Models;
using SQLite;
using System;
using System.Collections.Generic;

namespace People
{
    public class PersonRepository
    {
        private SQLiteConnection conn; // Declarar `conn` como miembro de la clase
        private readonly string _dbPath;

        public string StatusMessage { get; set; }

        public PersonRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        private void Init()
        {
            if (conn != null)
                return;

            try
            {
                conn = new SQLiteConnection(_dbPath); // Inicializar conexión SQLite
                conn.CreateTable<Person>(); // Crear tabla si no existe
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error initializing database: {ex.Message}";
            }
        }

        public void AddNewPerson(string name)
        {
            int result = 0;

            try
            {
                Init();

                // Validación básica
                if (string.IsNullOrEmpty(name))
                    throw new Exception("Valid name required");

                result = conn.Insert(new Person { Name = name });
                StatusMessage = $"{result} record(s) added [Name: {name}]";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to add {name}. Error: {ex.Message}";
            }
        }

        public List<Person> GetAllPeople()
        {
            try
            {
                Init();
                return conn.Table<Person>().ToList(); // Consultar todos los registros
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to retrieve data. {ex.Message}";
                return new List<Person>();
            }
        }

        public void DeletePerson(int id)
        {
            try
            {
                Init(); // Inicializa la conexión si no está lista
                conn.Delete<Person>(id); // Elimina el registro por ID
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to delete person. {ex.Message}";
            }
        }

    }
}

