using System.Security.Cryptography;

namespace guessing_number;

/*
This file defines a random number generator, so that it is possible to mock it in tests;

Don't worry too much about this implementation :)
*/

public interface IRandomGenerator
{
    int GetInt(int min, int max);
}

public class DefaultRandom : IRandomGenerator
{
    public int GetInt(int min, int max)
    {
        return RandomNumberGenerator.GetInt32(min, max);
    }
}