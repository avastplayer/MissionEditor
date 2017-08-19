using System.ComponentModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace MissionEditor
{
    public class MissionTaskBarEditor
    {
        [DisplayName("����")]
        [PropertyOrder(1)]
        public string TaskInfoDescriptionListA { get; set; }

        [DisplayName("Ŀ��")]
        [PropertyOrder(2)]
        public string TaskInfoPurposeListA { get; set; }

        [DisplayName("׷��")]
        [PropertyOrder(3)]
        public string TaskInfoTraceListA { get; set; }
    }
}