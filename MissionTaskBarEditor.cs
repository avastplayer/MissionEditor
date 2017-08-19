using System.ComponentModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace MissionEditor
{
    public class MissionTaskBarEditor
    {
        [DisplayName("ÃèÊö")]
        [PropertyOrder(1)]
        public string TaskInfoDescriptionListA { get; set; }

        [DisplayName("Ä¿µÄ")]
        [PropertyOrder(2)]
        public string TaskInfoPurposeListA { get; set; }

        [DisplayName("×·×Ù")]
        [PropertyOrder(3)]
        public string TaskInfoTraceListA { get; set; }
    }
}