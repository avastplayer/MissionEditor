using System.ComponentModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace MissionEditor
{
    public class MissionBattleAIEditor
    {
        public enum TeamSteate
        {
            要求胜利,
            均可
        }

        public enum DeathPunish
        {
            不接受,
            接受
        }

        [DisplayName("战斗AI")]
        [PropertyOrder(1)]
        public int AIInfoAIID { get; set; }

        [DisplayName("结果要求")]
        [PropertyOrder(2)]
        public TeamSteate AIInfoTeamSteate { get; set; }

        [DisplayName("是否死亡惩罚")]
        [PropertyOrder(3)]
        public DeathPunish AIInfoDeathPunish { get; set; }

        [DisplayName("战斗难度等级")]
        [PropertyOrder(4)]
        public string AIInfoBattleLevel { get; set; }
    }
}