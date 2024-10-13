using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Cuphead.Animation
{
    internal class Dust
    {
        public GameObject GameObject {  get; set; }
        private VisualEffectFactory VFactory { get; set; }

        private int x;
        private int y;

        public Dust(Rectangle rect)
        {
            x = rect.X;
            y = rect.Y;
        }
    }
}
