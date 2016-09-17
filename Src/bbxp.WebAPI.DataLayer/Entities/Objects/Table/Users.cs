using System;

namespace bbxp.WebAPI.DataLayer.Entities.Objects.Table {
    public class Users {
        public int ID { get; set; }

        public DateTime Modified { get; set; }

        public DateTime Created { get; set; }

        public bool Active { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string Password { get; set; }

        public bool IsConfirmed { get; set; }

        public string Username { get; set; }
    }
}