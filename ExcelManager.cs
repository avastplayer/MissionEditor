using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace MissionEditor
{
    public class ExcelManager
    {
        private string FilePath { get; } //文件名
        private IWorkbook Workbook { get; set; }
        private FileStream Fs { get; set; }
        private bool Disposed { get; set; }

        public ExcelManager(string filePath)//构造函数
        {
            FilePath = filePath;
            Disposed = false;
        }

        public DataTable ExcelToDataTable(string sheetName, DataTable templetDataTable)
        {
            DataTable data = templetDataTable.Clone();

            Fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
            if (FilePath.IndexOf(".xlsx", StringComparison.Ordinal) > 0) // 2007版本
                Workbook = new XSSFWorkbook(Fs);
            else if (FilePath.IndexOf(".xls", StringComparison.Ordinal) > 0) // 2003版本
                Workbook = new HSSFWorkbook(Fs);

            ISheet sheet;
            if (sheetName != null)
            {
                sheet = Workbook.GetSheet(sheetName) ?? Workbook.GetSheetAt(0);
            }
            else
            {
                sheet = Workbook.GetSheetAt(0);
            }
            if (sheet == null) return data;
            IRow firstRow = sheet.GetRow(0);

            int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数
            int startRow = sheet.FirstRowNum + 1;
            int rowCount = sheet.LastRowNum;//最后一列的标号
            for (int i = startRow; i <= rowCount; ++i)
            {
                IRow row = sheet.GetRow(i);
                if (row == null) continue; //没有数据的行默认是null　　　　　　　

                DataRow dataRow = data.NewRow();
                for (int j = row.FirstCellNum; j < cellCount; ++j)
                {
                    if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                        dataRow[j] = row.GetCell(j).ToString();
                }
                data.Rows.Add(dataRow);
            }

            return data;
        }

        public void DataTableToExcel(DataTable data, string sheetName, HashSet<int> writeRows)
        {
            Fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
            if (FilePath.IndexOf(".xlsx", StringComparison.Ordinal) > 0 || FilePath.IndexOf(".xlsm", StringComparison.Ordinal) > 0) // 2007版本
                Workbook = new XSSFWorkbook(Fs);
            else if (FilePath.IndexOf(".xls", StringComparison.Ordinal) > 0) // 2003版本
                Workbook = new HSSFWorkbook(Fs);

            if (Workbook == null) return;

            ISheet sheet = Workbook.GetSheet(sheetName);

            foreach (int writeRow in writeRows)
            {
                IRow row = sheet.CreateRow(writeRow + 1);
                for (int j = 0; j < data.Columns.Count; ++j)
                {
                    if (data.Rows[writeRow][j] == null) return;
                    if (data.Columns[j].DataType == typeof(string))
                    {
                        row.CreateCell(j).SetCellValue(data.Rows[writeRow][j].ToString());
                    }
                    else
                    {
                        if (double.TryParse(data.Rows[writeRow][j].ToString(), out double value))
                            row.CreateCell(j).SetCellValue(value);
                    }
                }
            }
            using (Fs = new FileStream(FilePath, FileMode.Create, FileAccess.Write))
            {
                Workbook.Write(Fs);
            }
        }

        public void DataTableToExcel(DataTable data, string sheetName)
        {
            Fs = new FileStream(FilePath, FileMode.Create, FileAccess.Write);
            if (FilePath.IndexOf(".xlsx", StringComparison.Ordinal) > 0 || FilePath.IndexOf(".xlsm", StringComparison.Ordinal) > 0) // 2007版本
                Workbook = new XSSFWorkbook();
            else if (FilePath.IndexOf(".xls", StringComparison.Ordinal) > 0) // 2003版本
                Workbook = new HSSFWorkbook();

            if (Workbook == null) return;

            ISheet sheet = Workbook.CreateSheet(sheetName);

            IRow rowColumn = sheet.CreateRow(0);
            for (int j = 0; j < data.Columns.Count; j++)
            {
                rowColumn.CreateCell(j).SetCellValue(data.Columns[j].ColumnName);
            }

            for (int i = 0; i < data.Rows.Count; i++)
            {
                IRow row = sheet.CreateRow(i + 1);
                for (int j = 0; j < data.Columns.Count; j++)
                {
                    if (data.Rows[i][j] == null) return;
                    if (data.Columns[j].DataType == typeof(string))
                    {
                        row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                    }
                    else
                    {
                        if (double.TryParse(data.Rows[i][j].ToString(), out double value))
                            row.CreateCell(j).SetCellValue(value);
                    }
                }
            }

            Workbook.Write(Fs);
        }

        protected void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (Disposed) return;
            if (disposing)
            {
                Fs?.Close();
            }

            Fs = null;
            Disposed = true;
        }
    }
}