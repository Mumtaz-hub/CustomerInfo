using System;
using System.Linq;

namespace Common.Extensions
{
    public static class ByteExtensions
    {
        public static bool IsEqual(this byte[] sourceBytes, byte[] destinationBytes)
        {
            return sourceBytes.SequenceEqual(destinationBytes);
        }
    }
}
