using System.Collections.Generic;
using System.Data;
using Xceed.Wpf.Toolkit;

namespace MissionEditor
{
    public class DatatableManager
    {
        private static volatile DatatableManager _instance;
        private static readonly object SyncRoot = new object();

        private DatatableManager()
        {
            InitializeDatatable();
        }

        public static DatatableManager Instance
        {
            get
            {
                if (_instance != null) return _instance;
                lock (SyncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new DatatableManager();
                    }
                }
                return _instance;
            }
        }

        public DataTable Datatable { get; set; }

        public void SaveExcel(HashSet<int> writeRows)
        {
            ExcelManager excelProcessor = new ExcelManager(ConfigManager.Instance.MissionFilePath);
            excelProcessor.DataTableToExcel(Datatable, "sheet1", writeRows);
        }

        public void SaveExcel()
        {
            ExcelManager excelProcessor = new ExcelManager(ConfigManager.Instance.MissionFilePath);
            excelProcessor.DataTableToExcel(Datatable, "sheet1");
        }

        private void InitializeDatatable()
        {
            ExcelManager excelProcessor = new ExcelManager(ConfigManager.Instance.MissionFilePath);
            Datatable = excelProcessor.ExcelToDataTable("Sheet1", TempletManager.Instance.CreateTempletDataTable());

            if (Datatable == null)
            {
                MessageBox.Show("请关闭excel进程，重新打开！");
            }
        }
    }
}