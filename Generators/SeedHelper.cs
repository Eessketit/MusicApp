using System.Security.Cryptography;
using System.Text;

namespace MusicApp.Generators;

public static class SeedHelper
{
    public static int ToInt32(ulong seed)
    {
        return unchecked((int)(seed % int.MaxValue));
    }

    public static ulong Hash(params object[] parts)
    {
        using var sha = SHA256.Create();
        var input = string.Join("|", parts);
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
        return BitConverter.ToUInt64(bytes, 0);
    }
}