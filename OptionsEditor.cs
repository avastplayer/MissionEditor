using System.ComponentModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace MissionEditor
{
    public class OptionsEditor
    {
        [DisplayName("配置文件夹")]
        [PropertyOrder(1)]
        [Description(@"例：D:\ProjectResource\autoconfig\")]
        public string ConfigFolderPath { get; set; }

        [DisplayName("图片文件夹")]
        [PropertyOrder(2)]
        [Description(@"例：D:\ProjectResource\00UI\")]
        public string ImageFolderPath { get; set; }

        [DisplayName("任务excel")]
        [PropertyOrder(3)]
        [Description(@"例：D:\ProjectResource\z主线任务.xlsx")]
        public string MissionFilePath { get; set; }

        [DisplayName("道具配置xml")]
        [PropertyOrder(4)]
        [Description(@"例：knight.gsp.item.citemattr.xml")]
        public string ItemAttrFileName { get; set; }

        [DisplayName("NPC配置xml")]
        [PropertyOrder(5)]
        [Description(@"例：knight.gsp.npc.cnpcconfig.xml")]
        public string NpcConfigFileName { get; set; }

        [DisplayName("NPC造型xml")]
        [PropertyOrder(6)]
        [Description(@"例：knight.gsp.npc.cnpcshape.xml")]
        public string NpcShapeFileName { get; set; }

        [DisplayName("地图配置xml")]
        [PropertyOrder(7)]
        [Description(@"例：knight.gsp.map.cmapconfig.xml")]
        public string MapConfigFileName { get; set; }

        [DisplayName("大头像文件夹名")]
        [PropertyOrder(8)]
        [Description(@"例：大头像")]
        public string HeadImageFileName { get; set; }

        [DisplayName("道具图标文件夹名")]
        [PropertyOrder(9)]
        [Description(@"例：Item")]
        public string ItemIconFileName { get; set; }
    }
}