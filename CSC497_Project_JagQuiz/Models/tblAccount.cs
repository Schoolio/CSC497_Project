//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CSC497_Project_JagQuiz.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblAccount
    {
        public int AccountID { get; set; }
        public string JagNumber { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<int> AccountType { get; set; }
    }
}
