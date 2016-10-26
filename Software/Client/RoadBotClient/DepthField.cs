using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadBotClient
{
    class DepthField
    {
        Random Rnd = new Random();
        int minrnddepth = 0;
        int maxrnddepth = 100;

        private int[,] _depthmass;
        public int[,] depthmass { get { return _depthmass; } set { _depthmass = value; } }

        private Point _size;
        public Point size { get { return _size; } set { _size = value; } }
        
        int mindepth;
        int maxdepth;

        public DepthField(int l, int h)
        {
            depthmass = new int[l, h];
            size = new Point(l,h);
        }

        public Bitmap test(bool r)
        {
            if (r) CreateRandomField();
            else CreateUsualField();
            return FillField();
        }

        private Color DepthIntoColor(double ratio)
        {
            int RGBconvertion = Convert.ToInt32(255 * ratio);
            Color result = Color.FromArgb(255 - RGBconvertion, 255 - RGBconvertion, 255 - RGBconvertion);
            return result;
        }

        private Bitmap FillField()
        {
            Bitmap result = new Bitmap(size.X,size.Y);
            int buff1;
            int buff2;
            double buff3;
            for (int i = 0; i < size.Y; i++) for (int o = 0; o < size.X; o++)
                {
                    buff1 = depthmass[o, i] - mindepth;
                    buff2 = maxdepth - mindepth;
                    if (buff2 == 0) buff2 = 1;
                    buff3 = Convert.ToDouble(buff1) / Convert.ToDouble(buff2);
                    result.SetPixel(o,i,DepthIntoColor(buff3));
                }
            return result;
        }

        private void CreateRandomField()
        {
            maxdepth = 1;
            mindepth = -1;
            for (int i = 0; i < size.Y; i++) for (int o = 0; o < size.X; o++)
                {
                    depthmass[o, i] = Rnd.Next(minrnddepth,maxrnddepth+1);
                    if (depthmass[o, i] > maxdepth) maxdepth = depthmass[o, i];
                    if (depthmass[o, i] < mindepth || mindepth == -1) mindepth = depthmass[o, i];
                }
        }

        private void CreateUsualField()
        {
            depthmass = new int[,] 
            {
                {5,0,3,0,1,1,0, },
                {0,2,0,32,11,3,1 },
                {1,0,25,55,32,12,0, },
                {0,12,55,70,55,32,7, },
                {5,30,56,80,80,40,0, },
                {10,19,56,80,65,32,0, },
                {0,9,40,70,57,25,0, },
                {9,0,15,32,56,5,0, },
                {0,3,8,20,15,0,0, },
                {0,0,0,5,2,0,2, }
            };
            maxdepth = 80;
            mindepth = 0;
        }
    }
}
