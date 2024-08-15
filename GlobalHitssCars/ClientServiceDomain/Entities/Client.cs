using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServiceDomain.Entities
{
    public class Client
    {
        public Guid Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        [StringLength(50)]
        public string SurName { get; set; } = string.Empty;

        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        private Client(Guid id, string name, string surName, string email)
        {
            Id = id;
            Name = name;
            SurName = surName;
            Email = email;
        }

        public static Result<Client> Build(string withName, string withSurName, string withEmail)
        {
            return new Client(Guid.NewGuid(), withName, withSurName, withEmail);
        }

        public static Result<Client> Load(Guid withId, string withName, string withSurName, string withEmail)
        {
            return new Client(withId, withName, withSurName, withEmail);
        }

    }
}
