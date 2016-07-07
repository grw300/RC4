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
            int i = 0, j = 0;

            bool startCheck = false;

            while (true)
            {
                j = (j + s[i] + key[i % key.Length]) % 256;
                Swap(s, i, j);

                if (startCheck)
                {
                    if (s[i] == s[position])
                        return i - position;
                }

                i++;

                if (i > position)
                    startCheck = true;

                if (i >= 1000000)
                    return -1;
            }
        }

        public static byte[] Decrypt(byte[] key, byte[] cipher)
        {
            return Encrypt(key, cipher);
        }

        public static byte[] Encrypt(byte[] key, byte[] data)
        {
            byte[] s = KeyInitialization(key);

            byte[] cipher = new byte[data.Length];

            int i = 0, j = 0;

            for (int l = 0; l < data.Length; l++)
            {
                i = (i + 1) % 256;
                j = (j + s[i]) % 256;

                Swap(s, i, j);

                cipher[l] = (byte)(data[l] ^ s[(s[i] + s[j]) % 256]);
            }

            return cipher;
        }

        private static void Swap(byte[] s, int i, int j)
        {
            byte c = s[i];
            s[i] = s[j];
            s[j] = c;
        }
    }
}

