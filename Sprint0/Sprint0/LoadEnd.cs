using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Cuphead
{
    internal class LoadEnd
    {
        public LoadEnd() 
        {
            GameObject idk = EndingFactory.CreateElement("WinScreenBackground", new Microsoft.Xna.Framework.Vector2(0, 0));
            GOManager.Instance.allGOs.Add(idk);

        }

    }
}
