using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MissionEditor
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        public AssetManager AssetManager = new AssetManager();

        public MainWindow()
        {
            InitializeComponent();
            MissionDataGrid.ItemsSource = GetExcelData(AssetManager.MissionDatatable);
            //MissionListView.ItemsSource = assetManager.MissionDatatable.DefaultView;

            DataProcess dataProcess = new DataProcess();
            dataProcess.CreateXmlFile();

            //assetManager.GetNpcInfo(14039, out string npcName, out int headId, out string mapName);
            //Console.WriteLine(npcName);
            //Console.WriteLine(mapName);

            //int imagesetName = (headId - 9000) / 4;
            //Head.Source = assetManager.GetImage(assetManager.HeadImageset + imagesetName, headId.ToString());
        }

        private DataView GetExcelData(DataTable missionDatatable)
        {
            string[] strColumns = { "MissionID", "MissionName", "MissionTypeString" };
            return missionDatatable.DefaultView.ToTable(missionDatatable.TableName, true, strColumns).DefaultView;
        }

        private void MissionDataGrid_click(object sender, MouseButtonEventArgs e)
        {
            int selectDataRow = GetSelectedRow();
            ActiveInfoNpcIDTextBox.Text = AssetManager.MissionDatatable.Rows[selectDataRow]["ActiveInfoNpcID"]
                .ToString();
            //Binding binding = new Binding()
            //{
            //    Source = AssetManager.MissionDatatable,
            //    Path = new PropertyPath("[0][ActiveInfoNpcID]")
            //};
            //ActiveInfoNpcIDTextBox.SetBinding(TextBox.TextProperty, binding);
        }

        private int GetSelectedRow()
        {
            if (MissionDataGrid != null && MissionDataGrid.SelectedCells.Count != 0)
            {
                return MissionDataGrid.SelectedIndex;
            }

            return -1;
        }

        //private void BindTextBoxValue(TextBox textBox, string value)
        //{
        //    AssetManager assetManager = new AssetManager();
        //    int selectDataRow = GetSelectedRow();
        //    ;
        //    Binding binding = new Binding()
        //    {
        //        Source = assetManager.MissionDatatable,
        //        Path = new PropertyPath("[selectDataRow][ActiveInfoNpcID]")
        //    };
        //    textBox.SetBinding(TextBox.TextProperty, binding);
        //}
    }
}