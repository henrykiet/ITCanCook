using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ITCanCook_BusinessObject.Encryption
{
	public class SHAEncryption
	{
		public static string Encrypt(string Value)
		{
			string encodedString = string.Empty;

			byte[] valueBytes = Encoding.UTF8.GetBytes(Value);
			using (SHA512 sha512 = SHA512.Create())
			{
				byte[] hashbytes = sha512.ComputeHash(valueBytes);

				string hashString = BitConverter.ToString(hashbytes).Replace("-", "").ToLower();

				encodedString = hashString;
			}

			return encodedString;
		}
	}
}
