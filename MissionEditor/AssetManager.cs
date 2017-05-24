using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using Path = MissionEditor.Properties.Path;

namespace MissionEditor
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public class AssetManager
    {
        public string ConfigFolderPath { get; set; } = Path.Default.ConfigFolderPath;//D:\ProjectResource\autoconfig\
        public string ImageFolderPath { get; set; } = Path.Default.ImageFolderPath; //D:\ProjectResource\00UI\
        public string MissionFilePath { get; set; } = Path.Default.MissionFilePath;//D:\ProjectResource\z主线任务.xlsx"

        public string ItemAttrFileName { get; set; } = Path.Default.ItemAttrFileName;//knight.gsp.item.citemattr.xml
        public string NpcConfigFileName { get; set; } = Path.Default.NpcConfigFileName;//knight.gsp.npc.cnpcconfig.xml
        public string NpcShapeFileName { get; set; } = Path.Default.NpcShapeFileName;//knight.gsp.npc.cnpcshape.xml
        public string MapConfigFileName { get; set; } = Path.Default.MapConfigFileName;//knight.gsp.map.cmapconfig.xml

        public string HeadImageFileName { get; set; } = Path.Default.HeadImageFileName;//大头像
        public string ItemIconFileName { get; set; } = Path.Default.ItemIconFileName;//Item

        public static DataTable MissionDatatable { get; private set; }
        public XElement ItemAttrNode { get; set; }
        public XElement NpcConfigNode { get; set; }
        public XElement NpcShapeNode { get; set; }
        public XElement MapConfigNode { get; set; }

        public AssetManager()
        {
            InitializeConfig();
        }

        public void SaveExcel(HashSet<int> writeRows)
        {
            ExcelHelper excelHelper = new ExcelHelper(MissionFilePath);
            excelHelper.DataTableToExcel(MissionDatatable, "sheet1", writeRows);
        }

        public void SaveExcel()
        {
            ExcelHelper excelHelper = new ExcelHelper(MissionFilePath);
            excelHelper.DataTableToExcel(MissionDatatable, "sheet1");
        }

        private void InitializeConfig()
        {
            //检查路径
            if (!CheckSetting()) return;

            ExcelHelper excelHelper = new ExcelHelper(MissionFilePath);
            DataProcess dataProcess = new DataProcess();
            MissionDatatable = excelHelper.ExcelToDataTable("Sheet1", dataProcess.TempletDataTable);

            if (MissionDatatable == null)
            {
                MessageBox.Show("请关闭excel进程，重新打开！");
            }

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

        public BitmapSource GetImage(string fileName, string imageName)
        {
            //图片完整路径
            string imageFullPath = ImageFolderPath + fileName + "\\" + imageName + ".tga";

            var tga = new TgaLib.TgaImage(new BinaryReader(
                new FileStream(imageFullPath, FileMode.Open, FileAccess.Read, FileShare.Read)));

            return tga.GetBitmap();
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

        private Rectangle GetRectangle(string imagesetName, string imageName)
        {
            string imagesetPath = ImageFolderPath + imagesetName + ".imageset";

            XElement rootNode = XElement.Load(imagesetPath);

            IEnumerable<XElement> targetNodes = from target in rootNode.Descendants("Image")
                                                select target;
            foreach (XElement node in targetNodes)
            {
                if (node.Attribute("Name").Value != imageName) continue;

                if (node.Attribute("XPos") == null || node.Attribute("YPos") == null ||
                    node.Attribute("Width") == null || node.Attribute("Height") == null) continue;

                if (int.TryParse(node.Attribute("XPos").Value, out int xPos) &&
                    int.TryParse(node.Attribute("YPos").Value, out int yPos) &&
                    int.TryParse(node.Attribute("Width").Value, out int width) &&
                    int.TryParse(node.Attribute("Height").Value, out int height))
                {
                    return new Rectangle(xPos, yPos, width, height);
                }
            }
            return new Rectangle();
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
    }
}