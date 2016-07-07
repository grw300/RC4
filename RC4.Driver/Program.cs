using System;
using RC4;
using System.Collections.Generic;
using System.Text;

namespace RC4.Driver
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			byte[] key = { 1, 2, 3, 4, 5 };

			var dataText = "Hello Greg!";
			var data = Encoding.Unicode.GetBytes(dataText);

			var cipher = RC4.Encrypt(key, data);
			var cipherText = Encoding.Unicode.GetString(cipher);

			var retrieved = RC4.Decrypt(key, cipher);
			var retrievedText = Encoding.Unicode.GetString(retrieved);

			Console.WriteLine($"Data text     : {dataText}");
			Console.WriteLine($"Cipher text   : {cipherText}");
			Console.WriteLine($"Retrieved text: {retrievedText}");

			byte[] s = RC4.KeyInitialization(key);

			int index = Array.IndexOf(s, s[0], Array.IndexOf(s, s[0]) + 1);

			Console.WriteLine($"Period: {index}");
		}
	}
}
