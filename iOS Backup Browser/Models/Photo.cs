namespace iOS_Backup_Browser.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.SQLite;
    using System.Security.Cryptography;
    using System.Text;

    public sealed class Photo
    {
        public string Filename { get; set; }

        public DateTime DateCreated { get; set; }

        public string FileNameDisk
        {
            get
            {
                using (var sha1 = new SHA1Managed())
                {
                    var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes("CameraRollDomain-Media/" + Filename));
                    var sb = new StringBuilder(hash.Length * 2);

                    foreach (var b in hash)
                    {
                        // can be "x2" if you want lowercase
                        sb.Append(b.ToString("x2"));
                    }

                    return sb.ToString();
                }
            }
        }

        public static List<Photo> Load(SQLiteConnection databaseConnection)
        {
            const string extendedCommand = "SELECT *, cast(ZDATECREATED as integer) as UnixDateCreated FROM ZGENERICASSET WHERE ZDATECREATED is not null";
            var returnPhotos = new List<Photo>();

            var command = new SQLiteCommand(extendedCommand, databaseConnection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var newPhoto = new Photo
                {
                    Filename = reader["ZDIRECTORY"] + "/" + reader["ZFILENAME"]
                };

                double dateCreated;
                if (!string.IsNullOrEmpty(reader["UnixDateCreated"].ToString()) && double.TryParse(reader["UnixDateCreated"].ToString(), out dateCreated))
                {
                    newPhoto.DateCreated = Extensions.UnixTimeStampToDateTime(dateCreated);
                }

                returnPhotos.Add(newPhoto);
            }

            return returnPhotos;
        }
    }
}