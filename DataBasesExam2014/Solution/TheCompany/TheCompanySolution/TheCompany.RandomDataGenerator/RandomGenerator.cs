namespace TheCompany.RandomDataGenerator
{
    using System;

    public class RandomGenerator : IRandomGenerator
    {
        private const string EnglishLetters = "ABCDEFGHIJKLMNOPQRSTUWXYZabcdefghijklmnopqrstuwxyz";

        private Random random;
        private static IRandomGenerator randomGenerator;

        private RandomGenerator()
        {
            this.random = new Random();
        }

        public static IRandomGenerator Instance
        {
            get
            {
                if (randomGenerator == null) 
                {
                    randomGenerator = new RandomGenerator();
                }
                
                return randomGenerator;
            }
        }

        public int GetRandomNumber(int min, int max)
        {
            return this.random.Next(min, max + 1);
        }

        public string GetRandomString(int minLength, int maxLength)
        {
            // TODO : CHECK what returns when the numbers are equal
            int randomStringLength = this.random.Next(minLength, maxLength);
            var result = new char[randomStringLength];
            
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = EnglishLetters[this.random.Next(0, EnglishLetters.Length - 1)];
            }

            return new string(result);
        }

        public DateTime GetRandomDateTime(DateTime startDate, DateTime endDate)
        {
            TimeSpan timeSpan = endDate - startDate;
            TimeSpan newSpan = new TimeSpan(0, random.Next(0, (int)timeSpan.TotalMinutes), 0);
            DateTime newDate = startDate + newSpan;

            return newDate;
        }
    }
}
