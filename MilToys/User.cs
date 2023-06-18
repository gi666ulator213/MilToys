using MySql.Data.MySqlClient;
using System;
using System.Text;
using static MilToys.Utils;

namespace MilToys {
    public class User {
        private string _connStr = DatabaseSettings.ConnectionString;
        public string UserName { get; private set;  }
        public string UserLogin { get; private set; }
        public bool EditAccess { get; private set; }

        public bool Login(string login, string password) {
            bool res = false;

            MySqlConnection conn = new MySqlConnection(_connStr);

            try {
                conn.Open();

                string sql = $"SELECT * FROM Users WHERE login = '{login}' AND password = '{sha256(password)}'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read()) {
                    UserLogin = rdr[1].ToString();
                    UserName = rdr[2].ToString();
                    EditAccess = Convert.ToBoolean(rdr[3]);
                    res = true;
                }

                rdr.Close();
            }
            catch (Exception ex) {
                ShowError(ex.Message);
            }

            conn.Close();

            return res;
        }

        private string sha256(string str) {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(str));
            foreach (byte theByte in crypto) {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
