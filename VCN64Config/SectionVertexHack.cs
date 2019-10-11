using System.Data;
using System.Text;

namespace VCN64Config
{
    public class SectionVertexHack : SectionTable
    {
        public SectionVertexHack()
        {
            Table = new DataTable("VertexHack");
            DataColumn column = new DataColumn
            {
                DataType = typeof(int),
                ColumnName = "Index",
                //ReadOnly = true,
                Unique = true
            };
            Table.Columns.Add(column);
            Table.Columns.Add("VertexCount", typeof(int));
            Table.Columns.Add("VertexAddress", typeof(int));
            Table.Columns.Add("TextureAddress", typeof(int));
            Table.Columns.Add("FirstVertex", typeof(string));
            Table.Columns.Add("Value", typeof(string));
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
                return Table.Rows.Add(index, null, null, null, null, null);
        }

        public void SetVertexCount(int index, int value)
        {
            DataRow row = GetRow(index);
            row[1] = value;
        }

        public void SetVertexAddress(int index, int value)
        {
            DataRow row = GetRow(index);
            row[2] = value;
        }

        public void SetTextureAddress(int index, int value)
        {
            DataRow row = GetRow(index);
            row[3] = value;
        }

        public void SetFirstVertex(int index, string value)
        {
            DataRow row = GetRow(index);
            row[4] = value;
        }

        public void SetValue(int index, string value)
        {
            DataRow row = GetRow(index);
            row[5] = value;
        }
    }
}
