namespace Kaffeeproject
{
    public class Kaffeemaschine 
    {
        public double Kaffeebohnen { get; set; }

        public double Wasser { get; set; }

        public double gesamtKaffeeproduziert { get; private set; }

        public double maxWasser { get => getWasser(); }

        public double maxBohnen { get => getBohnen(); }
        public double getWasser()
        {
            double Wasser = 2.5;
            return Wasser;
        }

        public double getBohnen()
        {
            double Bohnen = 2.5;
            return Bohnen;
        }


    }
}