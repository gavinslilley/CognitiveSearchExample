using GL.CSE.Interfaces.DataAccess;
using GL.CSE.Models;

namespace GL.CSE.DataAccess;
public class DataRepository : IDataRepository
{
    private static readonly Random random = new Random();

    private static readonly List<string> firstNames = new List<string> { "John", "Jane", "Michael", "Mary", "William", "Emma", "David", "Olivia", "Daniel", "Sophia", "James", "Elizabeth", "Thomas", "Marie", "Charles", "Anne", "Matthew", "Louise", "Andrew", "Grace" };
    private static readonly List<string> familyNames = new List<string> { "Smith", "Johnson", "Brown", "Taylor", "Miller", "Davis", "Garcia", "Rodriguez", "Wilson", "Anderson" };
    private static readonly List<string> titles = new List<string> { "Dr.", "Mr.", "Mrs.", "Ms.", "Prof." };


    public IEnumerable<SearchResult> BuildSearchIndex()
    {
        List<SearchResult> results = new List<SearchResult>();

        for (int i = 0; i < 1000; i++)
        {
            results.Add(new SearchResult
            {
                CaseNumber = GenerateRandomString(10),
                FirstName = GenerateRandomName(firstNames),
                MiddleName = GenerateRandomName(firstNames),
                FamilyName = GenerateRandomName(familyNames),
                Title = GenerateRandomName(titles)
            });
        }

        return results;
    }

    private string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        char[] buffer = new char[length];

        for (int i = 0; i < length; i++)
        {
            buffer[i] = chars[random.Next(chars.Length)];
        }

        return new string(buffer);
    }
    private static string GenerateRandomName(List<string> names)
    {
        int index = random.Next(names.Count);
        return names[index];
    }

}
