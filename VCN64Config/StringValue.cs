
namespace VCN64Config
{
    public class StringValue : Token
    {
        public readonly string Value;

        public StringValue(string value, int label)
            : base(label)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
