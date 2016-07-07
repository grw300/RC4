using System;
using System.Collections.Generic;
using System.Linq;

namespace RC4
{
    public static class RC4
    {
        public static byte[] KeyInitialization(byte[] key)
        {
            byte[] s = Enumerable.Range(0, 256)
                                 .Select(b => (byte)b)
                                 .ToArray();

            for (int i = 0, j = 0; i < 256; i++)
            {
                j = (j + s[i] + key[i % key.Length]) % 256;
                Swap(s, i, j);
            }

            return s;
        }

        public static int GetKeyInitializationPeriod(byte[] key, int position)
        {
            byte[] s = Enumerable.Range(0, 1000000)
                                 .Select(b => (byte)b)
                                 .ToArray();

            bool startCheck = false;

            for (int i = 0, j = 0; i < 1000000; i++)
            {
                j = (j + s[i] + key[i % key.Length]) % 256;
                Swap(s, i, j);

                if (startCheck)
                {
                    if (s[i] == s[position])
                        return i - position;
                }

                if (i > position)
                    startCheck = true;
            }
            return -1;
        }

        public static IEnumerable<byte> Decrypt(IEnumerable<byte> key, IEnumerable<byte> cipher)
        {
            return Encrypt(key, cipher);
        }

        public static IEnumerable<byte> Encrypt(IEnumerable<byte> key, IEnumerable<byte> data)
        {
            byte[] s = KeyInitialization(key.ToArray());

            int i = 0, j = 0;

            return data.Select(b =>
            {
                i = (i + 1) % 256;
                j = (j + s[i]) % 256;

                Swap(s, i, j);

                return (byte)(b ^ s[(s[i] + s[j]) % 256]);
            }).ToArray();
        }

        private static void Swap(byte[] s, int i, int j)
        {
            byte c = s[i];
            s[i] = s[j];
            s[j] = c;
        }
    }
}

