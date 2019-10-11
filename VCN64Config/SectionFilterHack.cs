using System.Data;

namespace VCN64Config
{
    public class SectionFilterHack : SectionTable
    {
        public SectionFilterHack()
        {
            Table = new DataTable("FilterHack");
            DataColumn column = new DataColumn
            {
                DataType = typeof(int),
                ColumnName = "Index",
                //ReadOnly = true,
                Unique = true
            };
            Table.Columns.Add(column);
            Table.Columns.Add("TextureAddress", typeof(int));
            Table.Columns.Add("SumPixel", typeof(int));
            Table.Columns.Add("Data2", typeof(int));
            Table.Columns.Add("Data3", typeof(int));
            Table.Columns.Add("AlphaTest", typeof(int));
            Table.Columns.Add("MagFilter", typeof(int));
            Table.Columns.Add("OffsetS", typeof(int));
            Table.Columns.Add("OffsetT", typeof(int));
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
                return Table.Rows.Add(index, null, null, null, null, null, null, null, null);
        }

        public void SetTextureAddress(int index, int value)
        {
            DataRow row = GetRow(index);
            row[1] = value;
        }

        public void SetSumPixel(int index, int value)
        {
            DataRow row = GetRow(index);
            row[2] = value;
        }

        public void SetData2(int index, int value)
        {
            DataRow row = GetRow(index);
            row[3] = value;
        }

        public void SetData3(int index, int value)
        {
            DataRow row = GetRow(index);
            row[4] = value;
        }

        public void SetAlphaTest(int index, int value)
        {
            DataRow row = GetRow(index);
            row[5] = value;
        }

        public void SetMagFilter(int index, int value)
        {
            DataRow row = GetRow(index);
            row[6] = value;
        }

        public void SetOffsetS(int index, int value)
        {
            DataRow row = GetRow(index);
            row[7] = value;
        }

        public void SetOffsetT(int index, int value)
        {
            DataRow row = GetRow(index);
            row[8] = value;
        }
    }
}
