using System.Collections.Generic;
using System.ComponentModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace MissionEditor
{
    [CategoryOrder("常规", 1)]
    [CategoryOrder("奖励", 2)]
    [CategoryOrder("跳转", 3)]
    public class MissionResultEditor
    {
        public enum JumpType
        {
            无,
            传送,
            副本
        };

        [Category("常规")]
        [DisplayName("后置任务")]
        [PropertyOrder(1)]
        public List<int> PostMissionList { get; set; }

        [Category("常规")]
        [DisplayName("变身造型")]
        [PropertyOrder(1)]
        public int TransformID { get; set; }

        [Category("常规")]
        [DisplayName("显示NPC")]
        [PropertyOrder(1)]
        public List<int> DisPlayNPCID { get; set; }

        [Category("常规")]
        [DisplayName("透明框提示")]
        [PropertyOrder(1)]
        public string NoteInfo { get; set; }

        [Category("奖励")]
        [DisplayName("经验奖励")]
        [PropertyOrder(1)]
        public int ExpReward { get; set; }

        [Category("奖励")]
        [DisplayName("金钱奖励")]
        [PropertyOrder(1)]
        public int MoneyReward { get; set; }

        [Category("奖励")]
        [DisplayName("宠物经验")]
        [PropertyOrder(1)]
        public int PetExpReward { get; set; }

        [Category("奖励")]
        [DisplayName("储备金奖励")]
        [PropertyOrder(1)]
        public int SMoney { get; set; }

        [Category("奖励")]
        [DisplayName("历练声望")]
        [PropertyOrder(1)]
        public int ShengWang { get; set; }

        [Category("跳转")]
        [DisplayName("跳转方式")]
        [PropertyOrder(1)]
        public JumpType RewardMapJumpType { get; set; }

        [Category("跳转")]
        [DisplayName("进度条时间")]
        [PropertyOrder(1)]
        public int ProcessBarTime { get; set; }

        [Category("跳转")]
        [DisplayName("地图")]
        [PropertyOrder(1)]
        public int RewardMapID { get; set; }

        [Category("跳转")]
        [DisplayName("进度条文字")]
        [PropertyOrder(1)]
        public int ProcessBarText { get; set; }

        [Category("跳转")]
        [DisplayName("X坐标")]
        [PropertyOrder(1)]
        public int RewardMapXPos { get; set; }

        [Category("跳转")]
        [DisplayName("Y坐标")]
        [PropertyOrder(1)]
        public int ProcessBarColor { get; set; }

        [Category("跳转")]
        [DisplayName("进度条颜色")]
        [PropertyOrder(1)]
        public int RewardMapYPos { get; set; }
    }
}