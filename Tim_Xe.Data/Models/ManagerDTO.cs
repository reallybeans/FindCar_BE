using System;
using System.Collections.Generic;
using System.Text;

namespace Tim_Xe.Data.Models
{
    public class ManagerDTO : RoleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        public string RoleName { get; set; }
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword

        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }
       
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public string? img { get; set; }
        public string? Status { get; set; }
        public DateTime? CreateAt { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    }
}
