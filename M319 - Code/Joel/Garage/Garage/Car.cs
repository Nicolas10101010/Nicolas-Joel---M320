namespace Garage
{
    public class Car : Vehicle
    {
        public Car(string id, decimal rate) : base(id, rate) { }
        public override string Type => "Auto";

        public override decimal Estimate(decimal hours) => Rate * hours + 20m;
    }
}