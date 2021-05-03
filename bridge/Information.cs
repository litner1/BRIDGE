using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bridge
{
    public class Information
    {
        public int HP_min = 0;
        public int HP_max = 40;
        public int C_min = 0;
        public int C_max = 13;
        public int D_min = 0;
        public int D_max = 13;
        public int H_min = 0;
        public int H_max = 13;
        public int S_min = 0;
        public int S_max = 13;

        public void Clear()
        {
            HP_min = 0;
            HP_max = 40;
            C_min = 0;
            C_max = 13;
            D_min = 0;
            D_max = 13;
            H_min = 0;
            H_max = 13;
            S_min = 0;
            S_max = 13;
        }
    }
}
