using MissionEditor.Properties;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using static System.Windows.Interop.Imaging;
using MessageBox = Xceed.Wpf.Toolkit.MessageBox;

namespace MissionEditor
{
    public class ConfigManager
    {
        private static volatile ConfigManager _instance;
        private static readonly object SyncRoot = new object();

        private ConfigManager()
        {
            InitializeConfig();
        }

        public static ConfigManager Instance
        {
            get
            {
                if (_instance != null) return _instance;
                lock (SyncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new ConfigManager();
                    }
                }
                return _instance;
            }
        }

        public string ConfigFolderPath { get; set; } = Settings.Default.ConfigFolderPath;//D:\ProjectResource\autoconfig\
        public string ImageFolderPath { get; set; } = Settings.Default.ImageFolderPath; //D:\ProjectResource\00UI\
        public string MissionFilePath { get; set; } = Settings.Default.MissionFilePath;//D:\ProjectResource\z主线任务.xlsx"

        public string ItemAttrFileName { get; set; } = Settings.Default.ItemAttrFileName;//knight.gsp.item.citemattr.xml
        public string NpcConfigFileName { get; set; } = Settings.Default.NpcConfigFileName;//knight.gsp.npc.cnpcconfig.xml
        public string NpcShapeFileName { get; set; } = Settings.Default.NpcShapeFileName;//knight.gsp.npc.cnpcshape.xml
        public string MapConfigFileName { get; set; } = Settings.Default.MapConfigFileName;//knight.gsp.map.cmapconfig.xml

        public string HeadImageFileName { get; set; } = Settings.Default.HeadImageFileName;//大头像
        public string ItemIconFileName { get; set; } = Settings.Default.ItemIconFileName;//Item

        public XElement ItemAttrNode { get; set; }
        public XElement NpcConfigNode { get; set; }
        public XElement NpcShapeNode { get; set; }
        public XElement MapConfigNode { get; set; }

        private void InitializeConfig()
        {
            //检查路径
            if (!CheckSetting()) return;

            ItemAttrNode = XElement.Load(ConfigFolderPath + ItemAttrFileName);
            NpcConfigNode = XElement.Load(ConfigFolderPath + NpcConfigFileName);
            NpcShapeNode = XElement.Load(ConfigFolderPath + NpcShapeFileName);
            MapConfigNode = XElement.Load(ConfigFolderPath + MapConfigFileName);
        }

        private bool CheckSetting()
        {
            if (!Directory.Exists(ConfigFolderPath))
            {
                MessageBox.Show($"\"{ConfigFolderPath}\"未找到，请重新设置！");
                return false;
            }

            if (!File.Exists(ConfigFolderPath + ItemAttrFileName))
            {
                MessageBox.Show($"\"{ConfigFolderPath} + {ItemAttrFileName}\"未找到！");
                return false;
            }

            if (!File.Exists(ConfigFolderPath + NpcConfigFileName))
            {
                MessageBox.Show($"\"{ConfigFolderPath} + {NpcConfigFileName}\"未找到！");
                return false;
            }

            if (!Directory.Exists(ImageFolderPath))
            {
                MessageBox.Show($"\"{ImageFolderPath}\"未找到，请重新设置！");
                return false;
            }

            if (!File.Exists(MissionFilePath))
            {
                MessageBox.Show($"\"{MissionFilePath}\"未找到，请重新设置！");
                return false;
            }

            return true;
        }

        public void GetNpcInfo(int npcId, out string npcName, out BitmapSource headBitmapSource)
        {
            //主角特殊处理
            if (npcId == 1)
            {
                npcName = "主角";
                headBitmapSource = GetImage(HeadImageFileName, "9000");
                return;
            }

            var npcInfo = (from target in NpcConfigNode.Descendants("record")
                           where target.Attribute("id").Value == npcId.ToString()
                           select new
                           {
                               name = target.Attribute("name").Value,
                               modelID = target.Attribute("modelID").Value,
                           }).FirstOrDefault();

            npcName = npcInfo.name;
            int headId = GetHeadId(Convert.ToInt32(npcInfo.modelID));
            headBitmapSource = GetImage(HeadImageFileName, headId.ToString());
        }

        public void GetNpcInfo(int npcId, out string npcName, out BitmapSource headBitmapSource, out string mapName)
        {
            var npcInfo = (from target in NpcConfigNode.Descendants("record")
                           where target.Attribute("id").Value == npcId.ToString()
                           select new
                           {
                               name = target.Attribute("name").Value,
                               modelID = target.Attribute("modelID").Value,
                               mapid = target.Attribute("mapid").Value
                           }).FirstOrDefault();

            npcName = npcInfo.name;
            int headId = GetHeadId(Convert.ToInt32(npcInfo.modelID));
            headBitmapSource = GetImage(HeadImageFileName, headId.ToString());
            mapName = GetMapName(Convert.ToInt32(npcInfo.mapid));
        }

        private string GetMapName(int mapId)
        {
            return (from target in MapConfigNode.Descendants("record")
                    where target.Attribute("id").Value == mapId.ToString()
                    select target.Attribute("mapName").Value).FirstOrDefault();
        }

        public void GetItemInfo(int itemId, out string itemName, out int itemIcon)
        {
            var itemInfo = (from target in ItemAttrNode.Descendants("record")
                            where target.Attribute("id").Value == itemId.ToString()
                            select new
                            {
                                name = target.Attribute("name").Value,
                                icon = target.Attribute("icon").Value
                            }).FirstOrDefault();
            itemName = itemInfo.name;
            itemIcon = Convert.ToInt32(itemInfo.icon);
        }

        private int GetHeadId(int shapeId)
        {
            return (from target in NpcShapeNode.Descendants("record")
                    where target.Attribute("id").Value == shapeId.ToString()
                    select Convert.ToInt32(target.Attribute("headID").Value)).FirstOrDefault();
        }

        public BitmapSource GetImage(string fileName, string imageName)
        {
            //图片完整路径
            string imageFullPath = ImageFolderPath + fileName + "\\" + imageName + ".tga";
            return CreateBitmapSourceFromHBitmap(TgaDecoder.FromFile(imageFullPath).GetHbitmap(), IntPtr.Zero, Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
        }
    }
}