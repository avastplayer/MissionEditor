using System.ComponentModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace MissionEditor
{
    [CategoryOrder("常规", 1)]
    [CategoryOrder("场景", 2)]
    [CategoryOrder("回收", 3)]
    [CategoryOrder("限定", 4)]
    [CategoryOrder("其他", 5)]
    public class MissionTriggeredEditor
    {
        public enum Type//todo:改枚举在combobox中的显示
        {
            点击NPC = 10,
            给予金钱 = 11,
            给予宠物 = 13,
            答题 = 17,
            使用物品 = 22,
            练功区掉落物品 = 32,
            练功区战斗计场次 = 34,
            练功区战斗计数量 = 35,
            无条件 = 50,
            步数触发 = 54,
            区域触发护送 = 56,
            NPC战斗 = 40,
            等级限制任务 = 58,
            特殊类型 = 59
        };

        public enum TeamState
        {
            Yes,
            No,
            Both
        };

        [Category("常规")]
        [DisplayName("触发类型")]
        [PropertyOrder(1)]
        public Type MissionType { get; set; }

        [Category("常规")]
        [DisplayName("NpcID")]
        [PropertyOrder(2)]
        public int ActiveInfoNpcID { get; set; }

        [Category("常规")]
        [DisplayName("场景")]
        [PropertyOrder(3)]
        public int ActiveInfoMapID { get; set; }

        [Category("常规")]
        [DisplayName("可否组队完成")]
        [PropertyOrder(4)]
        public TeamState ActiveInfoTeamState { get; set; }

        [Category("场景")]
        [DisplayName("左")]
        [PropertyOrder(5)]
        public int ActiveInfoLeftPos { get; set; }

        [Category("场景")]
        [DisplayName("上")]
        [PropertyOrder(6)]
        public int ActiveInfoTopPos { get; set; }

        [Category("场景")]
        [DisplayName("右")]
        [PropertyOrder(7)]
        public int ActiveInfoRightPos { get; set; }

        [Category("场景")]
        [DisplayName("下")]
        [PropertyOrder(8)]
        public int ActiveInfoBottomPos { get; set; }

        [Category("回收")]
        [DisplayName("回收物品数量")]
        [PropertyOrder(9)]
        public int ActiveInfoTargetID { get; set; }

        [Category("回收")]
        [DisplayName("回收物品")]
        [PropertyOrder(10)]
        public int ActiveInfoTargetNum { get; set; }

        [Category("限定")]
        [DisplayName("最少结束步骤")]
        [PropertyOrder(11)]
        public int ActiveInfoMiniStep { get; set; }

        [Category("限定")]
        [DisplayName("单步结束概率")]
        [PropertyOrder(12)]
        public int ActiveInfoStepProbability { get; set; }

        [Category("限定")]
        [DisplayName("最大结束步骤")]
        [PropertyOrder(13)]
        public int ActiveInfoMaxStep { get; set; }

        [Category("限定")]
        [DisplayName("限时时间（分钟）")]
        [PropertyOrder(14)]
        public int ActiveInfoTimeLimit { get; set; }

        [Category("限定")]
        [DisplayName("是否重新计时")]
        [PropertyOrder(15)]
        public int ActiveInfoIsRestartTimer { get; set; }

        [Category("回收")]
        [DisplayName("回收金钱数量")]
        [PropertyOrder(16)]
        public int ActiveInfoGiveBackMoney { get; set; }

        [Category("回收")]
        [DisplayName("回收宠物ID")]
        [PropertyOrder(17)]
        public int ActiveInfoGiveBackPetID { get; set; }

        [Category("回收")]
        [DisplayName("使用物品ID")]
        [PropertyOrder(18)]
        public int ActiveInfoUseItemID { get; set; }

        [Category("其他")]
        [DisplayName("其他方式")]
        [PropertyOrder(19)]
        public int ActiveInfoOtherType { get; set; }
    }
}