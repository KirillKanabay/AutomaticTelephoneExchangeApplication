using System;
using ATE.Entities.Company.Contracts;
using ATE.Entities.Terminal;
using ATE.Entities.Users;

namespace ATE.Entities.Company
{
    public class Client
    {
        public User User { get; set; }
        public BaseContract Contract { get; set; }
        public decimal Balance { get; set; }
        public BaseTerminal Terminal { get; set; }
    }
}