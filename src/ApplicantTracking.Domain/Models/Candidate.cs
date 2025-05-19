using System;

namespace ApplicantTracking.Domain.Models
{
    public class Candidate : Entity
    {
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public DateTime Birthdate { get; private set; }
        public string Email { get; private set; }

        protected Candidate() : base() { }

        public Candidate(int id, string name, string surname, DateTime birthdate, string email) : base(id)
        {
            Name = name;
            Surname = surname;
            Birthdate = birthdate;
            Email = email;
        }

        public void Update(string name, string surname, DateTime birthdate, string email)
        {
            Name = name;
            Surname = surname;
            Birthdate = birthdate;
            Email = email;
            SetUpdated();
        }
    }
}
