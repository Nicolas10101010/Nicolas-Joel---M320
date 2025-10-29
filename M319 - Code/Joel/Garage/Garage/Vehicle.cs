namespace Garage
{
    public abstract class Vehicle
    {
        public string Id { get; }
        protected decimal Rate { get; }
        public bool IsRepaired { get; private set; }
        public abstract string Type { get; }

        protected Vehicle(string id, decimal rate)
        {
            Id = id;
            Rate = rate;
        }
        public virtual decimal Estimate(decimal hours) => Rate * hours;

        public decimal Estimate(decimal hours, decimal parts) => Estimate(hours) + parts;

        public void MarkRepaired() => IsRepaired = true;

        public override string ToString() => $"{Type} {Id} {(IsRepaired ? "repariert" : "offen")}";
    }
}