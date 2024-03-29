﻿using Manager.Domain.Scopes;
using System.Collections.Generic;

namespace Manager.Domain.Entities
{
    public class Vehicle: BaseEntity
    {
        public string Name { get; private set; }
        public string Year { get; private set; }
        public string Model { get; private set; }
        public int FuelId { get; private set; }
        public Fuel Fuel { get; private set; }
        public decimal Amount { get; private set; }
        public bool IsSold { get; private set; }

        public virtual ICollection<Opportunity>  Oportunidades { get; set; }

        protected Vehicle() {}

        public Vehicle(string name, string year, string model, Fuel fuel, decimal amount)
        {
            SetProperties(name, year, model, fuel, amount);
        }

        public void Update(string name, string year, string model, Fuel fuel, decimal amount)
        {
            SetProperties(name, year, model, fuel, amount);
        }

        private void SetProperties(string name, string year, string model, Fuel fuel, decimal amount)
        {
            Name = name;
            Year = year;
            Model = model;
            Fuel = fuel;
            Amount = amount;

            this.ScopesValid();
        }

        public void Sold()
        {
            IsSold = true;
        }
    }
}
