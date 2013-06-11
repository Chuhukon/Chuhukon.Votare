using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Chuhukon.Votare.Common
{
	public static class ExtensionMethods
	{
		/// <summary>
		/// Convert a input string to a byte array and compute the hash.
		/// </summary>
		/// <param name="value">Input string.</param>
		/// <returns>Hexadecimal string.</returns>
		public static string ToMd5Hash(this string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return value;
			}

			using (MD5 md5 = new MD5CryptoServiceProvider())
			{
				byte[] originalBytes = ASCIIEncoding.Default.GetBytes(value);
				byte[] encodedBytes = md5.ComputeHash(originalBytes);
				return BitConverter.ToString(encodedBytes).Replace("-", string.Empty);
			}
		}
	}
}