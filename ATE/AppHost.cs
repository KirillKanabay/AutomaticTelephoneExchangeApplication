﻿using System;
using System.Linq;
using System.Threading;
using ATE.Builders;
using ATE.Core.Interfaces;
using ATE.Core.Interfaces.Builders;
using ATE.Entities;
using ATE.Entities.Billings;
using ATE.Entities.Users;
using ATE.Factories;

namespace ATE
{
    internal class AppHost
    {
        public void Run()
        {
            User user1 = new User("Kirill", "Kanabay");
            User user2 = new User("Ivan", "Ivanov");

            ICompanyBuilder builder = new Company.Builder("MTC");
            ICompany company = new DefaultCompanyDirector(builder).Make();

            Subscriber subscriber1 = company.Subscribe(new SubscriberFactory(user1));
            Subscriber subscriber2 = company.Subscribe(new SubscriberFactory(user2));

            TerminalView terminal1View = new TerminalView(subscriber1.Terminal);
            TerminalView terminal2View = new TerminalView(subscriber2.Terminal);

            var ate = company.AteCollection.FirstOrDefault();
            
            subscriber1.BillingAccount.Deposit(5);
            subscriber2.BillingAccount.Deposit(5);

            subscriber1.Terminal.ConnectTo(ate);
            subscriber2.Terminal.ConnectTo(ate);

            subscriber1.Terminal.CallTo(subscriber2.Terminal.Number);
            if (subscriber1.Terminal.CurrentCall != null)
            {
                Thread.Sleep(5000);
                subscriber1.Terminal.EndCall();
            }

            subscriber2.Terminal.CallTo(subscriber1.Terminal.Number);
            if (subscriber1.Terminal.CurrentCall != null)
            {
                Thread.Sleep(3000);
                subscriber2.Terminal.EndCall();
            }

            var presenter1 = new CallPresenter(new CallReporter(company.BillingSystem, subscriber1.BillingAccount));
            var presenter2 = new CallPresenter(new CallReporter(company.BillingSystem, subscriber2.BillingAccount));

            presenter1.Present();
            Console.WriteLine(new string('=', 80));
            presenter2.Present();
        }
    }
}
