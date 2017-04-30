using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
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
        public MainWindow()
        {
            InitializeComponent();
            AssetManager assetManager = new AssetManager();
            MissionListView.ItemsSource = GetExcelData(assetManager.MissionDatatable);
            //MissionListView.ItemsSource = assetManager.MissionDatatable.DefaultView;

            DataProcess dataProcess = new DataProcess();
            dataProcess.CreateXmlFile();

            return;
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
    }
}