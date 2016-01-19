﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Sql.Account
{
    public class AccountEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
    }
}