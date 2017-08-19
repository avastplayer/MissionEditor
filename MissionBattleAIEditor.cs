using System.ComponentModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace MissionEditor
{
    public class MissionBattleAIEditor
    {
        public enum TeamSteate
        {
            Ҫ��ʤ��,
            ����
        }

        public enum DeathPunish
        {
            ������,
            ����
        }

        [DisplayName("ս��AI")]
        [PropertyOrder(1)]
        public int AIInfoAIID { get; set; }

        [DisplayName("���Ҫ��")]
        [PropertyOrder(2)]
        public TeamSteate AIInfoTeamSteate { get; set; }

        [DisplayName("�Ƿ������ͷ�")]
        [PropertyOrder(3)]
        public DeathPunish AIInfoDeathPunish { get; set; }

        [DisplayName("ս���Ѷȵȼ�")]
        [PropertyOrder(4)]
        public string AIInfoBattleLevel { get; set; }
    }
}