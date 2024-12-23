using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace TurOtomasyon.Helpers
{
    public static class DatabaseHelper
    {
        private static readonly string _connectionString = "server=localhost; port=5432; Database=Tur; username=postgres; password=3785";

        // Bağlantı nesnesi oluşturur
        public static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}

