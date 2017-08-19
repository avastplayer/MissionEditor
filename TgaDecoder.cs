using System;
using System.Drawing;
using System.IO;

namespace MissionEditor
{
    public class TgaDecoder
    {
        protected class TgaData
        {
            private const int TgaHeaderSize = 18;
            private readonly int _colorMapType;
            private readonly int _imageType;
            private readonly int _bitPerPixel;
            private readonly int _descriptor;
            private readonly byte[] _colorData;

            public TgaData(byte[] image)
            {
                _colorMapType = image[1];
                _imageType = image[2];
                Width = image[13] << 8 | image[12];
                Height = image[15] << 8 | image[14];
                _bitPerPixel = image[16];
                _descriptor = image[17];
                _colorData = new byte[image.Length - TgaHeaderSize];
                Array.Copy(image, TgaHeaderSize, _colorData, 0, _colorData.Length);
                // Index color RLE or Full color RLE or Gray RLE
                if (_imageType == 9 || _imageType == 10 || _imageType == 11)
                    _colorData = DecodeRle();
            }

            public int Width { get; }

            public int Height { get; }

            public int GetPixel(int x, int y)
            {
                if (_colorMapType != 0) return 0;
                switch (_imageType)
                {
                    case 1:
                    case 9:
                        // not implemented
                        return 0;

                    case 2:
                    case 10:
                        int elementCount = _bitPerPixel / 8;
                        int dy = ((_descriptor & 0x20) == 0 ? (Height - 1 - y) : y) * (Width * elementCount);
                        int dx = ((_descriptor & 0x10) == 0 ? x : (Width - 1 - x)) * elementCount;
                        int index = dy + dx;

                        int b = _colorData[index + 0] & 0xFF;
                        int g = _colorData[index + 1] & 0xFF;
                        int r = _colorData[index + 2] & 0xFF;

                        if (elementCount == 4)
                        {
                            int a = _colorData[index + 3] & 0xFF;
                            return (a << 24) | (r << 16) | (g << 8) | b;
                        }
                        else if (elementCount == 3)
                        {
                            return (r << 16) | (g << 8) | b;
                        }
                        break;

                    case 3:
                    case 11:
                        // not implemented
                        return 0;
                }
                return 0;
                // not implemented
            }

            protected byte[] DecodeRle()
            {
                int elementCount = _bitPerPixel / 8;
                byte[] elements = new byte[elementCount];
                int decodeBufferLength = elementCount * Width * Height;
                byte[] decodeBuffer = new byte[decodeBufferLength];
                int decoded = 0;
                int offset = 0;
                while (decoded < decodeBufferLength)
                {
                    int packet = _colorData[offset++] & 0xFF;
                    if ((packet & 0x80) != 0)
                    {
                        for (int i = 0; i < elementCount; i++)
                        {
                            elements[i] = _colorData[offset++];
                        }
                        int count = (packet & 0x7F) + 1;
                        for (int i = 0; i < count; i++)
                        {
                            for (int j = 0; j < elementCount; j++)
                            {
                                decodeBuffer[decoded++] = elements[j];
                            }
                        }
                    }
                    else
                    {
                        int count = (packet + 1) * elementCount;
                        for (int i = 0; i < count; i++)
                        {
                            decodeBuffer[decoded++] = _colorData[offset++];
                        }
                    }
                }
                return decodeBuffer;
            }
        }

        public static Bitmap FromFile(string path)
        {
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    int length = (int)fs.Length;
                    byte[] buffer = new byte[length];
                    fs.Read(buffer, 0, length);
                    return Decode(buffer);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Bitmap FromBinary(byte[] image)
        {
            return Decode(image);
        }

        protected static Bitmap Decode(byte[] image)
        {
            TgaData tga = new TgaData(image);

            Bitmap bitmap = new Bitmap(tga.Width, tga.Height);
            for (int y = 0; y < tga.Height; ++y)
            {
                for (int x = 0; x < tga.Width; ++x)
                {
                    bitmap.SetPixel(x, y, Color.FromArgb(tga.GetPixel(x, y)));
                }
            }
            return bitmap;
        }
    }
}