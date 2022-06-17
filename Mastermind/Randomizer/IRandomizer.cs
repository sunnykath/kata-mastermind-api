namespace Mastermind.Randomizer
{
    public interface IRandomizer
    {
        public Colours[] GetRandomColours(int numberOfColoursToSelect);
        
    }
}