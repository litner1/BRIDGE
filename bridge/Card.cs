using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bridge
{
    public class Card
    {
        public System.Windows.Media.Imaging.BitmapImage image;
        public System.Windows.Media.Imaging.BitmapImage reverse_image;

        public int value;
        public int HP;
        public int color;
        public bool trump;

        public void Set_HP()
        {
            if (value == 12)
                HP = 4;
            else if (value == 11)
                HP = 3;
            else if (value == 10)
                HP = 2;
            else if (value == 9)
                HP = 1;
            else
                HP = 0;
        }

        public void Set_Color(int i)
        {
            if (i < 13)
                color = 3;
            else if (i < 26)
                color = 0;
            else if (i < 39)
                color = 1;
            else
                color = 2;
        }

        public static int Compare_Cards(Card l, Card r)
        {
            if (l.color < r.color)
            {
                return -1;
            }
            else if (l.color > r.color)
                return 1;

            if (l.color == r.color)
            {
                if (l.value < r.value)
                    return -1;
                else
                    return 1;
            }

            return 0;
        }
    }
}
