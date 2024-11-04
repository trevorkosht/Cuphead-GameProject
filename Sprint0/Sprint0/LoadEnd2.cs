using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Cuphead
{
    internal class LoadEnd2
    {
        public LoadEnd2()
        {
            List<GameObject> all = new List<GameObject>();
            Dictionary<String, Vector2> location = new Dictionary<String, Vector2>()
            {
                {"WinScreenBackground", new Vector2(-500, -500) },
                {"WinScreenBoard", new Vector2(-500, -500) },
                {"WinScreenResultsText", new Vector2(-500, -500) },
                {"WinScreenCuphead", new Vector2(-500, -500) },
                {"WinScreenUnearnedStar", new Vector2(-500, -500) },
                {"WinScreenStarAppearAnimation", new Vector2(-500, -500) },
                {"WinScreenLine", new Vector2(-500, -500) },
                {"WinScreenStar", new Vector2(-500, -500) },
                {"WinScreenCircle", new Vector2(-500, -500) },
                {"WinScreenResultsText", new Vector2(-500, -500) },
                {"WinScreenResultsText", new Vector2(-500, -500) },
                {"WinScreenResultsText", new Vector2(-500, -500) },





            };

            foreach (var element in location)
            {
                GameObject endElement = EndingFactory.CreateElement(element.Key, element.Value);
                GOManager.Instance.allGOs.Add(endElement);
            }

        }

    }
}