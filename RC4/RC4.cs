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

			var t = new byte[256];

			for (int i = 0; i < 256; i++)
			{
				s[i] = (byte)i;
				t[i] = key[i % key.Length];
			}

			for (int i = 0, j = 0; i < 256; i++)
			{
				j = (j + s[i] + t[i]) % 256;
				Swap(s, i, j);
			}

			return s;
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

				var t = (s[i] + s[j]) % 256;

				var k = s[t];

				cipher[l] = (byte)(data[l] ^ k);
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

