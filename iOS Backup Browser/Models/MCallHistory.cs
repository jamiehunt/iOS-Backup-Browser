namespace iOS_Backup_Browser.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.SQLite;
    using System.IO;

    public class MCallHistory
    {
        public string Address { get; set; }

        public DateTime Date { get; set; }

        public int Duration { get; set; }

        public int Flags { get; set; }

        public int CountryCode { get; set; }

        public int NetworkCode { get; set; }

        public static List<MCallHistory> Load(string filename)
        {
            var returns = new List<MCallHistory>();

            if (!File.Exists(filename))
            {
                return returns;
            }

            using (var conn = new SQLiteConnection($"Data Source={filename};Version=3;"))
            {
                conn.Open();

                var command = new SQLiteCommand("select * FROM call", conn);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var ch = new MCallHistory
                    {
                        Address = reader["address"].ToString(),
                        Date = Extensions.UnixTimeStampToDateTime(long.Parse(reader["date"].ToString())),
                        Duration = int.Parse(reader["duration"].ToString()),
                        Flags = int.Parse(reader["flags"].ToString()),
                        CountryCode = int.Parse(reader["country_code"].ToString()),
                        NetworkCode = int.Parse(reader["network_code"].ToString())
                    };

                    returns.Add(ch);
                }
            }

            return returns;
        }
    }
}