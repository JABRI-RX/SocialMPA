using SocialMediaPlatformAPI.Models;

namespace SocialMediaPlatformAPI.Helpers;

public static class MyLogger
{
    public static void LogMe(object _object)
    {
        Console.WriteLine("--------------------------");
        if (_object is List<Comment> os)
        {
            foreach (var obj in os)
            {
                Console.WriteLine(obj.ToString());
            }
        }
        else
        {
            Console.WriteLine($"{_object}");
        }
        Console.WriteLine("--------------------------");
    }
}