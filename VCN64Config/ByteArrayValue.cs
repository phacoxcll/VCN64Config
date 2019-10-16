using System.Text;

namespace VCN64Config
{
    public class ByteArrayValue : Token
    {
        public readonly string Value;

        public ByteArrayValue(string value, int label)
            : base(label)
        {
            Value = value.ToUpper();
        }

        public override string ToString()
        {
            return "a" + (Value.Length / 2).ToString() + ":" + Value;
        }

        public static bool IsHex(char c)
        {
            return (char.IsDigit(c) ||
                c == 'A' ||
                c == 'B' ||
                c == 'C' ||
                c == 'D' ||
                c == 'E' ||
                c == 'F' ||
                c == 'a' ||
                c == 'b' ||
                c == 'c' ||
                c == 'd' ||
                c == 'e' ||
                c == 'f');
        }

        public static bool IsValid(string byteArray)
        {
            if (byteArray.Length % 2 != 0)
                return false;

            for (int i = 0; i < byteArray.Length; i++)
                if (!IsHex(byteArray[i]))
                    return false;

            return true;
        }

        public static string Formatting(string byteArray)
        {
            int length = byteArray.Length / 2;
            StringBuilder strBuilder = new StringBuilder();

            if (length == 1)
            {
                strBuilder.Append("0x");
                strBuilder.Append(byteArray);
            }
            else
            {
                strBuilder.Append("a" + length.ToString() + ":");
                for (int i = 0; i < length; i++)
                {
                    if (i % 16 == 0)
                        strBuilder.Append("\n");
                    else
                        strBuilder.Append(" ");
                    strBuilder.Append(byteArray[i * 2]);
                    strBuilder.Append(byteArray[i * 2 + 1]);
                }
            }

            return strBuilder.ToString();
        }
    }
}
