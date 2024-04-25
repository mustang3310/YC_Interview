namespace OfficialWebsite.Core
{
    public static class ConvertType
    {
        public static class Aes
        {
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
        }
    }
}
