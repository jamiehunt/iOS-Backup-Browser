namespace iOS_Backup_Browser.Models.Contacts
{
    using System.Collections.Generic;
    using System.Data.SQLite;

    public class Contact
    {
        public int RecordId { get; set; }

        public string First { get; set; }

        public string Last { get; set; }

        public string DisplayName
        {
            get
            {
                if (string.IsNullOrEmpty(First) && string.IsNullOrEmpty(Last))
                {
                    return "Unknown";
                }

                if (string.IsNullOrEmpty(First))
                {
                    return Last;
                }

                return First + " " + Last;
            }
        }

        public string JobTitle { get; set; }

        public string Organization { get; set; }

        public string Department { get; set; }

        public string PhoneWork { get; set; }

        public string PhoneHome { get; set; }

        public string PhoneMobile { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public static List<Contact> Load(SQLiteConnection conn)
        {
            var returns = new List<Contact>();

            const string extendedCommand = @"select ABPerson.ROWID
     , ABPerson.first as First
     , ABPerson.last as Last
     , ABPerson.Organization as organization
     , ABPerson.Department as department
     , ABPerson.Birthday as birthday
     , ABPerson.JobTitle as jobtitle

     , (select value from ABMultiValue where property = 3 and record_id = ABPerson.ROWID and label = (select ROWID from ABMultiValueLabel where value = '_$!<Work>!$_')) as PhoneWork
     , (select value from ABMultiValue where property = 3 and record_id = ABPerson.ROWID and label = (select ROWID from ABMultiValueLabel where value = '_$!<Mobile>!$_')) as PhoneMobile
     , (select value from ABMultiValue where property = 3 and record_id = ABPerson.ROWID and label = (select ROWID from ABMultiValueLabel where value = '_$!<Home>!$_')) as PhoneHome

     , (select value from ABMultiValue where property = 4 and record_id = ABPerson.ROWID and label is null) as Email
     
     , (select value from ABMultiValueEntry where parent_id in (select ROWID from ABMultiValue where record_id = ABPerson.ROWID) and key = (select ROWID from ABMultiValueEntryKey where lower(value) = 'street')) as Address
     , (select value from ABMultiValueEntry where parent_id in (select ROWID from ABMultiValue where record_id = ABPerson.ROWID) and key = (select ROWID from ABMultiValueEntryKey where lower(value) = 'city')) as City
  from ABPerson
order by ABPerson.ROWID";

            var command = new SQLiteCommand(extendedCommand, conn);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var contact = new Contact
                {
                    RecordId = int.Parse(reader["ROWID"].ToString()),
                    First = reader["First"].ToString(),
                    Last = reader["Last"].ToString(),
                    JobTitle = reader["JobTitle"].ToString(),
                    Organization = reader["Organization"].ToString(),
                    Department = reader["Department"].ToString(),
                    PhoneWork = reader["PhoneWork"].ToString(),
                    PhoneHome = reader["PhoneHome"].ToString(),
                    PhoneMobile = reader["PhoneMobile"].ToString(),
                    Email = reader["Email"].ToString(),
                    Address = reader["Address"].ToString(),
                    City = reader["City"].ToString()
                };

                returns.Add(contact);
            }

            return returns;
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}