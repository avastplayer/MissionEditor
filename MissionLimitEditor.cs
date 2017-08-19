using System.Collections.Generic;
using System.ComponentModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace MissionEditor
{
    [CategoryOrder("等级限制", 1)]
    [CategoryOrder("转生等级限制", 2)]
    [CategoryOrder("任务需求", 3)]
    [CategoryOrder("角色需求", 4)]
    public class MissionLimitEditor
    {
        [Category("等级限制")]
        [DisplayName("等级限制下限")]
        [PropertyOrder(1)]
        public int MinLevel { get; set; }

        [Category("等级限制")]
        [DisplayName("等级限制上限")]
        [PropertyOrder(2)]
        public int MaxLevel { get; set; }

        [Category("转生等级限制")]
        [DisplayName("转生等级限制下限")]
        [PropertyOrder(3)]
        public int TransMinLevel { get; set; }

        [Category("转生等级限制")]
        [DisplayName("转生等级限制上限")]
        [PropertyOrder(4)]
        public int TransMaxLevel { get; set; }

        [Category("任务需求")]
        [DisplayName("任务需求列表")]
        [PropertyOrder(5)]
        //        [Editor(typeof(ListBoxEditor), typeof(ListBoxEditor))]
        //todo:ListboxEditor
        public List<int> RequestMissionList { get; set; }

        [Category("角色需求")]
        [DisplayName("角色需求列表")]
        [PropertyOrder(6)]
        //        [Editor(typeof(ListBoxEditor), typeof(ListBoxEditor))]
        public List<int> RequestRoleIDList { get; set; }
    }
}