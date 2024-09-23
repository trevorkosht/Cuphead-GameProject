using Cuphead.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Cuphead.Controllers
{
    internal class ItemController
    {
        private KeyboardController keyboardController = new KeyboardController();
        private Items.Items items;
        private int potionNum;
        

        public ItemController()
        {
            potionNum = 2;
        }

        public void Update(Items.Items items)
        {
            keyboardController.Update();
            if (keyboardController.OnKeyDown(Keys.U))
            {
                potionNum--;
                if (potionNum <= 0)
                {
                    potionNum = 6;
                }

            }
            else if (keyboardController.OnKeyDown(Keys.I))
            {
                potionNum++;
                if (potionNum > 6)
                {
                    potionNum = 1;
                }
            }
            items.Sourcecorrection(potionNum);
        }


    }
}
