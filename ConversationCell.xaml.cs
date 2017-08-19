using System.Windows.Media.Imaging;

namespace MissionEditor
{
    /// <summary>
    /// ConversationCell.xaml 的交互逻辑
    /// </summary>
    public partial class ConversationCell
    {
        public ConversationCell(string npcName, BitmapSource headBitmapSource, string conversation, double width)
        {
            InitializeComponent();
            NPCHeadImage.Source = headBitmapSource;
            NPCNameTextBox.Text = npcName;
            Conversation.Text = conversation;
            Width = width;
        }
    }
}