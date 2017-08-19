using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace MissionEditor
{
    /// <summary>
    /// OptionsWindow.xaml 的交互逻辑
    /// </summary>
    public partial class OptionsWindow : Window
    {
        private readonly OptionsEditor _optionsEditor = new OptionsEditor();

        public OptionsWindow()
        {
            InitializeComponent();
            OptionsPropertyGrid.SelectedObject = _optionsEditor;
            SetOptionsPropertyGrid();
        }

        private void SetOptionsPropertyGrid()
        {
            _optionsEditor.ConfigFolderPath = ConfigManager.Instance.ConfigFolderPath;
            _optionsEditor.ImageFolderPath = ConfigManager.Instance.ImageFolderPath;
            _optionsEditor.MissionFilePath = ConfigManager.Instance.MissionFilePath;
            _optionsEditor.ItemAttrFileName = ConfigManager.Instance.ItemAttrFileName;
            _optionsEditor.NpcConfigFileName = ConfigManager.Instance.NpcConfigFileName;
            _optionsEditor.NpcShapeFileName = ConfigManager.Instance.NpcShapeFileName;
            _optionsEditor.MapConfigFileName = ConfigManager.Instance.MapConfigFileName;
            _optionsEditor.HeadImageFileName = ConfigManager.Instance.HeadImageFileName;
            _optionsEditor.ItemIconFileName = ConfigManager.Instance.ItemIconFileName;

            OptionsPropertyGrid.Update();
        }
    }
}