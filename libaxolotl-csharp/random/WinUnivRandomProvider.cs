using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.Security.Cryptography;

namespace libaxolotl_csharp.random
{
	public class WinUnivRandomProvider : IRandomProvider
	{
		public byte[] nextBytes(uint length)
		{
			IBuffer buffer = CryptographicBuffer.GenerateRandom(length);
			byte[] bytes = new byte[length];
			using (DataReader reader = DataReader.FromBuffer(buffer))
			{
				reader.ReadBytes(bytes);
			}
			return bytes;
        }

		public int nextInt(int maxValue)
		{
			throw new NotImplementedException();
		}
	}
}
