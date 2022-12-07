using System.Data;
using OfficeOpenXml;
namespace Nhom10.Models.Process
{
    public class ExcelProcess
    {
        public DataTable ExcelToDataTable(string strPath)
        {
            FileInfo fi = new FileInfo(strPath);
            ExcelPackage excelPackage = new ExcelPackage(fi);
            DataTable dt= new DataTable();
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[0];
            //check if the workiputsheet is completely emty
            if (worksheet.Dimension==null)
            {
                return dt;
            }
            //creat a list to hold the column names
            List<string> clumnNames = new List<string>();
            int currentColumn = 1;
            foreach (var cell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
            {
                string columnName = cell.Text.Trim();
                if (cell.Start.Column != currentColumn)
                {
                    clumnNames.Add("Header_" + currentColumn);
                    dt.Columns.Add("Header_" + currentColumn);
                    currentColumn++;
                }
                clumnNames.Add(columnName);
                int occurrences = columnName.Count(x =>x.Equals(columnName));
                if (occurrences > 1)
                {
                    columnName = columnName + "_" + occurrences;
                }
                dt.Columns.Add(columnName);
                currentColumn++;
            }
            //Start adding the contents of the...
            for (int i= 2; i <= worksheet.Dimension.End.Row; i++)
            {
                var row = worksheet.Cells[i, 1, i, worksheet.Dimension.End.Column];
                DataRow newRow = dt.NewRow();
                //loop all cells in the row
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text;
                }
                dt.Rows.Add(newRow);
            }
            return dt;
        }
    }
}