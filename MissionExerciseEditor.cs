using System.Collections.Generic;
using System.ComponentModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace MissionEditor
{
    public class MissionExerciseEditor
    {
        public enum MapType
        {
            ������,
            ������
        }

        [DisplayName("����������")]
        [PropertyOrder(1)]
        public MapType BattleInfoBattleMapType { get; set; }

        [DisplayName("��������")]
        [PropertyOrder(2)]
        public int BattleInfoBattleZoneID { get; set; }

        [DisplayName("���伸��")]
        [PropertyOrder(3)]
        public int BattleInfoDrop { get; set; }

        [DisplayName("ս������")]
        [PropertyOrder(4)]
        public int BattleInfoBattleTimes { get; set; }

        [DisplayName("��������")]
        [PropertyOrder(5)]
        public int BattleInfoMonsterNum { get; set; }

        [DisplayName("������ƷID")]
        [PropertyOrder(6)]
        public int BattleInfoDropItemID { get; set; }

        [DisplayName("������Ʒ����")]
        [PropertyOrder(7)]
        public int BattleInfoDropItemNum { get; set; }

        [DisplayName("��������")]
        [PropertyOrder(8)]
        public List<int> BattleInfoMonsterList { get; set; }
    }
}