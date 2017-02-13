namespace iOS_Backup_Browser.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.SQLite;
    using System.IO;
    using System.Linq;

    public class MChat
    {
        public int ChatId { get; set; }

        public string ChatIdentifier { get; set; }

        public string ServiceName { get; set; }

        public string AccountLogin { get; set; }

        public string LastAddressedHandle { get; set; }

        private List<MMessage> _messages = new List<MMessage>();

        private string _filename;

        public List<MMessage> Messages
        {
            get
            {
                // Lazy load
                if (LastAddressedHandle.Any() && _messages != null && _messages.Count > 0)
                {
                    return _messages;
                }

                _messages = MMessage.Load(_filename, ChatId);
                return _messages;
            }
        }

        public static List<MChat> Load(string filename)
        {
            var returns = new List<MChat>();

            if (!File.Exists(filename))
            {
                return returns;
            }

            using (var conn = new SQLiteConnection($"Data Source={filename};Version=3;"))
            {
                conn.Open();

                var command = new SQLiteCommand("select * FROM chat", conn);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var ch = new MChat
                    {
                        ChatId = int.Parse(reader["ROWID"].ToString()),
                        ChatIdentifier = reader["chat_identifier"].ToString(),
                        ServiceName = reader["service_name"].ToString(),
                        AccountLogin = reader["account_login"].ToString(),
                        LastAddressedHandle = reader["last_addressed_handle"].ToString(),
                        _filename = filename
                    };

                    returns.Add(ch);
                }
            }

            return returns;
        }

        public override string ToString()
        {
            return ChatIdentifier;
        }
    }

    public class MMessage
    {
        public int MessageId { get; set; }

        public string Text { get; set; }

        public static List<MMessage> Load(string filename, int ChatId)
        {
            var returns = new List<MMessage>();

            if (!File.Exists(filename))
            {
                return returns;
            }

            using (var conn = new SQLiteConnection($"Data Source={filename};Version=3;"))
            {
                conn.Open();

                var command = new SQLiteCommand("select * FROM chat_message_join WHERE chat_id = " + ChatId, conn);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var message = new MMessage
                        {
                            MessageId = int.Parse(reader["message_id"].ToString())
                        };

                        var command2 = new SQLiteCommand("select * FROM message WHERE ROWID = " + message.MessageId, conn);
                        using (var reader2 = command2.ExecuteReader())
                        {
                            reader2.Read();

                            message.Text = reader2["text"].ToString();
                        }

                        returns.Add(message);
                    }
                }
            }

            return returns;
        }
    }
}