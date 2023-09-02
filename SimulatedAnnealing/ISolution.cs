namespace SimulatedAnnealing
{
    public interface ISolution
    {
        public double getScore();

        public ISolution Mutate();

        public ISolution Clone();
    }
}