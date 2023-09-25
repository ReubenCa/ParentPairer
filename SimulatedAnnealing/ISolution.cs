namespace SimulatedAnnealing
{
    public interface ISolution
    {
        public double GetScore();

        public ISolution Mutate(double Temperature);

        public ISolution Clone();
    }
}