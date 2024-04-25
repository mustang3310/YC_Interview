namespace OfficialWebsite.Core
{
    using Org.BouncyCastle.Crypto.Digests;
    using Org.BouncyCastle.Crypto.Parameters;
    using Org.BouncyCastle.Security;
    using System;
    using System.Text;

    public static class Crypto
    {
        public class Aes
        {
            private static readonly string _key = "UTSI@2023";

            //REF: https://kashifsoofi.github.io/cryptography/aes-in-csharp-using-bouncycastle/
            public static string Encrypt(string rawString)
            {
                KeyIV keyIv = new(_key);
                // Default - AES/GCM/NoPadding、System.Security.AES - AES/CBC/PKCS7
                Org.BouncyCastle.Crypto.IBufferedCipher cipher = CipherUtilities.GetCipher("AES/CBC/PKCS7");
                cipher.Init(true, new ParametersWithIV(new KeyParameter(keyIv.Key), keyIv.IV));
                byte[] rawData = Encoding.UTF8.GetBytes(rawString);
                return Convert.ToBase64String(cipher.DoFinal(rawData));
            }

            public static string Decrypt(byte[] encryptedBytes)
            {
                KeyIV keyIv = new(_key);
                // Default - AES/GCM/NoPadding、System.Security.AES - AES/CBC/PKCS7
                Org.BouncyCastle.Crypto.IBufferedCipher cipher = CipherUtilities.GetCipher("AES/CBC/PKCS7");
                cipher.Init(false, new ParametersWithIV(new KeyParameter(keyIv.Key), keyIv.IV));
                return Encoding.UTF8.GetString(cipher.DoFinal(encryptedBytes));
            }
            public static string ConvertToHexadecimal(string encString)
            {

                byte[] bytes = Convert.FromBase64String(encString);

                string hexString = BitConverter.ToString(bytes).Replace("-", "");

                return hexString;

            }

            public static byte[] ConvertToByte(string hexString)
            {
                int numberChars = hexString.Length;
                byte[] bytes = new byte[numberChars / 2];
                for (int i = 0; i < numberChars; i += 2)
                {
                    bytes[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
                }
                return bytes;
            }

            private sealed class KeyIV
            {
                public readonly byte[] IV = new byte[16];
                public readonly byte[] Key = new byte[16];

                public KeyIV(string strKey)
                {
                    Sha256Digest sha = new();
                    byte[] hash = new byte[sha.GetDigestSize()];
                    byte[] data = Encoding.ASCII.GetBytes(strKey);
                    sha.BlockUpdate(data, 0, data.Length);
                    _ = sha.DoFinal(hash, 0);
                    Array.Copy(hash, 0, Key, 0, 16);
                    Array.Copy(hash, 16, IV, 0, 16);
                }
            }
        }
    }

}
