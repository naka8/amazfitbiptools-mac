using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;

namespace WatchFace.Parser.Models
{
	public class Header
	{
		public const int SignatureSize = 8;
		public const int HeaderSize = 87 - SignatureSize;
		public const int unknownPos = 18 - SignatureSize;
//		public const int parametersSizePos = 22 - SignatureSize;
		public const int parametersSizePos = 83 - SignatureSize;

		private static readonly byte[] DialSignature = { (byte)'U', (byte)'I', (byte)'H', (byte)'H', (byte)'\x01', (byte)'\x00', (byte)'\xff' };

		public uint deviceId { get; private set; }
		public byte[] Signature { get; private set; } = new byte[7];
		public uint Unknown { get; set; }
		public byte[] UnknownWrite { get; set; }
		public uint ParametersSize { get; set; } = parametersSizePos;

		public bool IsValid => DialSignature.SequenceEqual(Signature);

		public void WriteTo(Stream stream)
		{
			var buffer = new byte[HeaderSize];
			for (var i = 0; i < buffer.Length; i++) buffer[i] = 0xff;

//			Encoding.ASCII.GetBytes(Signature).CopyTo(buffer, 0);
			byte[] myB1 = new byte[1];
			myB1[0] = 6;
			myB1.CopyTo(buffer, 11);
			UnknownWrite.CopyTo(buffer, 16);
			//BitConverter.GetBytes(0x06).CopyTo(buffer, 11);
			BitConverter.GetBytes(Unknown).CopyTo(buffer, 52);
			BitConverter.GetBytes(ParametersSize).CopyTo(buffer, 56);
			BitConverter.GetBytes(HeaderSize).CopyTo(buffer, 60);
			stream.Write(buffer, 0, HeaderSize);
		}

		public static Header ReadFrom(Stream stream)
		{
			var sig_buffer = new byte[SignatureSize];
			stream.Read(sig_buffer, 0, 8);

			var buffer = new byte[HeaderSize];
			stream.Read(buffer, 0, HeaderSize);


			var header = new Header
				{
					Unknown = (BitConverter.ToUInt32(buffer, unknownPos)),
					//ParametersSize = (BitConverter.ToUInt32(buffer, parametersSizePos))- HeaderSize,
					ParametersSize = (uint)(BitConverter.ToUInt32(buffer, parametersSizePos)),
					deviceId = buffer[0],
				};

			Array.Copy(sig_buffer, header.Signature, header.Signature.Length);
			return header;
		}

		// 伝統的な32ビット入れ替え処理
		public static uint Reverse(uint value)
		{
			return (value & 0xFF) << 24 |
					((value >> 8) & 0xFF) << 16 |
					((value >> 16) & 0xFF) << 8 |
					((value >> 24) & 0xFF);
		}
	}
}