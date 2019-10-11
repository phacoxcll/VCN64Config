using System.Data;
using System.Text;

namespace VCN64Config
{
    public class SectionRomHack : SectionTable
    {
        public SectionRomHack()
        {
            Table = new DataTable("RomHack");
            DataColumn column = new DataColumn
            {
                DataType = typeof(int),
                ColumnName = "Index",
                //ReadOnly = true,
                Unique = true
            };
            Table.Columns.Add(column);
            Table.Columns.Add("Address", typeof(int));
            Table.Columns.Add("Type", typeof(int));
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
                return Table.Rows.Add(index, null, null, null);
        }

        public void SetAddress(int index, int value)
        {
            DataRow row = GetRow(index);
            row[1] = value;
        }

        public void SetType(int index, int value)
        {
            DataRow row = GetRow(index);
            row[2] = value;
        }

        public void SetValue(int index, string value)
        {
            DataRow row = GetRow(index);
            row[3] = value;
        }

        /*private string ByteArrayToString(byte[] byteArray)
        {
            const string hexChars = "0123456789ABCDEF";
            if (byteArray == null)
                return VCN64Config.NullString;
            else
            {
                int length = (byteArray.Length > 0x1000) ? 0x100 : byteArray.Length;
                StringBuilder strBuilder = new StringBuilder(length * 2);
                for (int i = 0; i < length; i++)
                {
                    strBuilder.Append(hexChars[(int)byteArray[i] >> 4]);
                    strBuilder.Append(hexChars[(int)byteArray[i] % 0x10]);
                }
                return strBuilder.ToString();
            }
        }*/
    }
}
