using System;
using System.Text;

namespace CoreFaces.Identity.Helper
{
    public class Encripty
    {
        private static string keyString = "E546C8DF278CD5931069B522E695D4F2";

        public static string EncryptString(string item)
        {
            string EcriptedData = string.Empty;
            byte[] txt_encode = new byte[item.Length];
            txt_encode = Encoding.UTF8.GetBytes(item);
            EcriptedData = Convert.ToBase64String(txt_encode);
            return EcriptedData;
        }

        public static string DecryptString(string item)
        {
            UTF8Encoding encode_pwd = new UTF8Encoding();
            string DecryptedData = string.Empty;
            Decoder Decode = encode_pwd.GetDecoder();
            byte[] todecodeByte = Convert.FromBase64String(item);
            int charCount = Decode.GetCharCount(
                todecodeByte,
                0,
                todecodeByte.Length
            );
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecodeByte, 0, todecodeByte.Length, decoded_char, 0);
            DecryptedData = new String(decoded_char);
            return DecryptedData;
        }
    }
}
