
namespace VCN64Config
{
    public class IntValue : Token
    {
        public readonly int Value;

        public IntValue(int value, int label)
            : base(label)
        {
            Value = value;
        }

        public override string ToString()
        {
            if (Label == KeyLabel.IntHEX)
                return Value.ToString("X");
            else
                return Value.ToString();
        }
    }
}
