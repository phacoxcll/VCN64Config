using System.Data;

namespace VCN64Config
{
    public abstract class SectionTable : Node
    {
        public DataTable Table { protected set; get; }
        public int Count;

        public SectionTable()
        {
            Count = 0;
        }

        protected abstract DataRow GetRow(int index);

        public override string ToString()
        {
            return Table.TableName;
        }
    }
}
