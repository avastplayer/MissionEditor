using System.Windows.Media.Imaging;

namespace MissionEditor
{
    /// <summary>
    /// AwardItemCell.xaml 的交互逻辑
    /// </summary>
    public partial class AwardItemCell
    {
        public AwardItemCell(string itemId, string itemNum, string itemName, BitmapSource itemIcon, string itemIsBind, double width)
        {
            InitializeComponent();
            Width = width;
            RewardItemID.Text = itemId;
            RewardItemNum.Text = itemNum;
            RewardItemName.Text = itemName;
            RewardItemIcon.Source = itemIcon;
            RewardItemIsBind.Text = itemIsBind;
        }
    }
}