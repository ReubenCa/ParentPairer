namespace SimulatedAnnealing
{
    public interface ISolution
    {
        public double GetScore();

        public ISolution Mutate();

        public ISolution Clone();
    }
}