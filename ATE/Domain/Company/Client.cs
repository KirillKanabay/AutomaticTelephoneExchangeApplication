﻿using System;
using ATE.Abstractions.Domain.Company;
using ATE.Abstractions.Domain.Terminal;
using ATE.Domain.Users;

namespace ATE.Domain.Company
{
    public class Client
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public BaseContract Contract { get; set; }
        public string PhoneNumber => Contract?.PhoneNumber;
        public decimal Balance { get; set; }
        public BaseTerminal Terminal { get; set; }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            return this.GetHashCode() == obj?.GetHashCode();
        }
    }
}