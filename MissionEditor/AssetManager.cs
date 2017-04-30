using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace MissionEditor
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public class AssetManager
    {
        public string ConfigFolderPath { get; set; } = Settings.Default.ConfigFolderPath;//D:\ProjectResource\autoconfig\
        public string ImageFolderPath { get; set; } = Settings.Default.ImageFolderPath; //D:\ProjectResource\imagesets\
        public string ItemAttrFileName { get; set; } = Settings.Default.ItemAttrName;//knight.gsp.item.citemattr.xml
        public string NpcConfigFileName { get; set; } = Settings.Default.NpcConfigFileName;//knight.gsp.npc.cnpcconfig.xml
        public string NpcShapeFileName { get; set; } = Settings.Default.NpcShapeFileName;//knight.gsp.npc.cnpcshape.xml
        public string MapConfigFileName { get; set; } = Settings.Default.MapConfigFileName;//knight.gsp.map.cmapconfig.xml
        public string HeadImageset { get; set; } = Settings.Default.HeadImageset;//roleandmonster

        public string MissionFilePath { get; set; } = @"D:\ProjectResource\z主线任务.xlsx";
        public DataTable MissionDatatable { get; private set; }

        public AssetManager()
        {
            InitializeConfig();
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

        /// <summary>
        ///按照路径在imageset中查找图片坐标，在tga文件中裁切图片
        /// 例：set:roleandmonster0 image:9000
        /// </summary>
        /// <param name="imagesetName">例：roleandmonster0</param>
        /// <param name="imageName">例：9000</param>
        /// <returns>可绑定图片资源</returns>
        public BitmapSource GetImage(string imagesetName, string imageName)
        {
            //获取全部image名字
            IEnumerable<string> imageNameList = GetFileNameList(ImageFolderPath);

            //查找图片
            int findImageNumer = imageNameList.Count(element => element.Contains(imagesetName));
            if (findImageNumer == 0) return null;

            //图片完整路径
            string imageFullPath = ImageFolderPath + imagesetName + ".tga";

            //在imageset中查找图片坐标和尺寸
            Rectangle rect = GetRectangle(imagesetName, imageName);
            if (rect.Height == 0 || rect.Width == 0)
            {
                MessageBox.Show($"{imagesetName}.imageset中找不到对应{imageName}！");
                return null;
            }

            //按照坐标和尺寸裁切
            Bitmap image = CutImage(Paloma.TargaImage.LoadTargaImage(imageFullPath), rect);

            BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(image.GetHbitmap(),
                IntPtr.Zero, Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
            return bitmapSource;
        }

        /// <summary>
        /// 根据npcid查找knight.gsp.npc.cnpcconfig.xml中的name、modelID、mapid字段
        /// </summary>
        /// <param name="npcId"></param>
        /// <param name="npcName"></param>
        /// <param name="headId"></param>
        /// <param name="mapName"></param>
        public void GetNpcInfo(int npcId, out string npcName, out int headId, out string mapName)
        {
            string npcConfigPath = ConfigFolderPath + NpcConfigFileName;

            XElement rootNode = XElement.Load(npcConfigPath);

            IEnumerable<XElement> targetNodes = from target in rootNode.Descendants("record")
                                                select target;
            foreach (XElement node in targetNodes)
            {
                if (node.Attribute("id").Value != npcId.ToString()) continue;

                if (node.Attribute("name") == null || node.Attribute("modelID") == null || node.Attribute("mapid") == null) continue;

                if (int.TryParse(node.Attribute("modelID").Value, out int shapeId) &&
                    int.TryParse(node.Attribute("mapid").Value, out int mapId))
                {
                    npcName = node.Attribute("name").Value;
                    headId = GetHeadId(shapeId);
                    mapName = GetMapName(mapId);
                    return;
                }
            }
            npcName = null;
            headId = 0;
            mapName = null;
        }

        /// <summary>
        ///查找imageset中的XPos、YPos、Width、Height字段
        /// 例：set:roleandmonster0 image:9000
        /// </summary>
        /// <param name="imagesetName">例：roleandmonster0</param>
        /// <param name="imageName">例：9000</param>
        /// <returns>矩形(xPos, yPos, width, height)</returns>
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

        private static Bitmap CutImage(Image img, Rectangle rect)
        {
            Bitmap b = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(b);
            g.DrawImage(img, 0, 0, rect, GraphicsUnit.Pixel);
            g.Dispose();
            return b;
        }

        /// <summary>
        ///查找knight.gsp.map.cmapconfig.xml中的mapName字段
        /// </summary>
        /// <param name="mapId"></param>
        /// <returns>mapName</returns>
        private string GetMapName(int mapId)
        {
            string mapConfigPath = ConfigFolderPath + MapConfigFileName;

            XElement rootNode = XElement.Load(mapConfigPath);

            IEnumerable<XElement> targetNodes = from target in rootNode.Descendants("record")
                                                select target;
            foreach (XElement node in targetNodes)
            {
                if (node.Attribute("id").Value != mapId.ToString()) continue;

                if (node.Attribute("mapName") == null) continue;
                return node.Attribute("mapName").ToString();
            }
            return null;
        }

        /// <summary>
        /// 查找knight.gsp.npc.cnpcshape.xml中的headID字段
        /// </summary>
        /// <param name="shapeId"></param>
        /// <returns></returns>
        private int GetHeadId(int shapeId)
        {
            string npcShapePath = ConfigFolderPath + NpcShapeFileName;

            XElement rootNode = XElement.Load(npcShapePath);

            IEnumerable<XElement> targetNodes = from target in rootNode.Descendants("record")
                                                select target;
            foreach (XElement node in targetNodes)
            {
                if (node.Attribute("id").Value != shapeId.ToString()) continue;

                if (node.Attribute("headID") == null) continue;

                if (int.TryParse(node.Attribute("headID").Value, out int headID))
                {
                    return headID;
                }
            }
            return 0;
        }

        private static IEnumerable<string> GetFileNameList(string path)
        {
            string[] files = Directory.GetFiles(path);
            return files.Select(Path.GetFileName).ToList();
        }
    }
}