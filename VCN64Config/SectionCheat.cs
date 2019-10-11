using System.Data;

namespace VCN64Config
{
    public class SectionCheat : SectionTable
    {
        public SectionCheat()
        {
            Table = new DataTable("Cheat");
            DataColumn column = new DataColumn
            {
                DataType = typeof(int),
                ColumnName = "Index",
                //ReadOnly = true,
                Unique = true
            };
            Table.Columns.Add(column);
            Table.Columns.Add("N", typeof(int));
            Table.Columns.Add("Addr", typeof(int));
            Table.Columns.Add("Value", typeof(int));
            Table.Columns.Add("Bytes", typeof(int));
            DataColumn[] primaryKeyColumns = new DataColumn[1];
            primaryKeyColumns[0] = Table.Columns["Index"];
            Table.PrimaryKey = primaryKeyColumns;
        }

        protected override DataRow GetRow(int index)
        {
            DataRow row = Table.Rows.Find(index);
            if (row != null)
                return row;
            else
                return Table.Rows.Add(index, null, null, null, null);
        }

        public void SetN(int index, int value)
        {
            DataRow row = GetRow(index);
            row[1] = value;
        }

        public void SetAddr(int index, int value)
        {
            DataRow row = GetRow(index);
            row[2] = value;
        }

        public void SetValue(int index, int value)
        {
            DataRow row = GetRow(index);
            row[3] = value;
        }

        public void SetBytes(int index, int value)
        {
            DataRow row = GetRow(index);
            row[4] = value;
        }
    }
}
