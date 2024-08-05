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
    }
}
