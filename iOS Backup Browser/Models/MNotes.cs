namespace iOS_Backup_Browser.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.SQLite;
    using System.IO;

    public class MNotes
    {
        public int NoteId { get; set; }

        public string NoteTitle { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string Content { get; set; }

        public static List<MNotes> Load(string filename)
        {
            var returns = new List<MNotes>();

            if (!File.Exists(filename))
            {
                return returns;
            }

            using (var conn = new SQLiteConnection($"Data Source={filename};Version=3;"))
            {
                conn.Open();

                var command = new SQLiteCommand("select ZNOTE.Z_PK as NoteId, ZTitle as NoteTitle, ZCREATIONDATE as CreateDate, ZMODIFICATIONDATE as ModifiedDate, ZCONTENT as Content FROM ZNOTE join ZNOTEBODY on ZNOTE.Z_PK = ZNOTEBODY.Z_PK", conn);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var note = new MNotes
                    {
                        NoteId = int.Parse(reader["NoteId"].ToString()),
                        NoteTitle = reader["NoteTitle"].ToString(),
                        Content = reader["Content"].ToString()
                    };

                    returns.Add(note);
                }
            }

            return returns;
        }

        public override string ToString()
        {
            return NoteTitle;
        }
    }
}