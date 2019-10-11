
namespace VCN64Config
{
    public class Word : Token
    {
        public readonly string Lexeme;

        public Word(string lexeme, int label)
            : base(label)
        {
            Lexeme = lexeme;
        }

        public override string ToString()
        {
            return Lexeme;
        }
    }
}
