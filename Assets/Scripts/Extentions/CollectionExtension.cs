using System;

public static class CollectionExtension
{
    private static readonly Random Random = new();

    public static T PickRandom<T>(this T[] array)
    {
        return array[Random.Next(array.Length)];
    }
}