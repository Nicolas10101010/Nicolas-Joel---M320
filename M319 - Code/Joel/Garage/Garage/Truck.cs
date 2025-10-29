namespace Garage
{
    public class Truck : Vehicle
    {
        public Truck(string id, decimal rate) : base(id, rate) { }
        public override string Type => "Lastwagen";

        public override decimal Estimate(decimal hours) => Rate * hours * 1.5m;
    }
}