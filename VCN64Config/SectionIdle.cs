using System.Data;

namespace VCN64Config
{
    public class SectionIdle : SectionTable
    {
        public SectionIdle()
        {
            Table = new DataTable("Idle");
            DataColumn column = new DataColumn
            {
                DataType = typeof(int),
                ColumnName = "Index",
                //ReadOnly = true,
                Unique = true
            };
            Table.Columns.Add(column);
            Table.Columns.Add("Address", typeof(int));
            Table.Columns.Add("Inst", typeof(int));
            Table.Columns.Add("Type", typeof(int));
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
                return Table.Rows.Add(index, null, null, null);
        }

        public void SetAddress(int index, int value)
        {
            DataRow row = GetRow(index);
            row[1] = value;            
        }

        public void SetInst(int index, int value)
        {
            DataRow row = GetRow(index);
            row[2] = value;
        }

        public void SetType(int index, int value)
        {
            DataRow row = GetRow(index);
            row[3] = value;
        }
    }
}
