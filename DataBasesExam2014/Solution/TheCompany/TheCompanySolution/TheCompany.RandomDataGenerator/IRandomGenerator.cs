namespace TheCompany.RandomDataGenerator
{
    using System;

    public interface IRandomGenerator
    {
        int GetRandomNumber(int min, int max);

        string GetRandomString(int minLength, int maxLength);

        DateTime GetRandomDateTime(DateTime startDate, DateTime endDate);
    }
}
