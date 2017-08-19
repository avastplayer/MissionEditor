using System.Collections.Generic;
using System.ComponentModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace MissionEditor
{
    public class MissionExerciseEditor
    {
        public enum MapType
        {
            明雷区,
            暗雷区
        }

        [DisplayName("练功区分类")]
        [PropertyOrder(1)]
        public MapType BattleInfoBattleMapType { get; set; }

        [DisplayName("场景区域")]
        [PropertyOrder(2)]
        public int BattleInfoBattleZoneID { get; set; }

        [DisplayName("掉落几率")]
        [PropertyOrder(3)]
        public int BattleInfoDrop { get; set; }

        [DisplayName("战斗次数")]
        [PropertyOrder(4)]
        public int BattleInfoBattleTimes { get; set; }

        [DisplayName("怪物数量")]
        [PropertyOrder(5)]
        public int BattleInfoMonsterNum { get; set; }

        [DisplayName("掉落物品ID")]
        [PropertyOrder(6)]
        public int BattleInfoDropItemID { get; set; }

        [DisplayName("掉落物品数量")]
        [PropertyOrder(7)]
        public int BattleInfoDropItemNum { get; set; }

        [DisplayName("怪物种类")]
        [PropertyOrder(8)]
        public List<int> BattleInfoMonsterList { get; set; }
    }
}