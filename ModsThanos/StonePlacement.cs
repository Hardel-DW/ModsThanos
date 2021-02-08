using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using ModsThanos.Utility.Enumerations;

namespace ModsThanos.Map {
    public class StonePlacement {
        internal static Vector2 GetRandomLocation(string composent) {
            byte MapID = (byte) ShipStatus.Instance.Type;

            Dictionary<MapType, Vector2[]> mapLocations = new Dictionary<MapType, Vector2[]>();
            Vector2 currentPositon;

            mapLocations.Add(MapType.Skeld, new Vector2[] {
                new Vector2(-12.594f, -4.179f),
                new Vector2(-22.746f, -7.206f),
                new Vector2(-19.269f, -9.544f),
                new Vector2(-7.772f, -11.655f),
                new Vector2(-9.825f, -9.023f),
                new Vector2(-2.874f, -16.936f),
                new Vector2(0.336f, -9.152f),
                new Vector2(5.731f, -9.607f),
                new Vector2(-5.044f, -2.642f),
                new Vector2(-1.013f, 5.975f),
                new Vector2(10.772f, 1.998f),
                new Vector2(6.409f, -4.710f),
                new Vector2(17.997f, -5.689f),
                new Vector2(11.165f, -10.334f),
                new Vector2(7.522f, -14.285f),
                new Vector2(1.707f, -14.937f),
                new Vector2(-3.685f, -11.657f),
                new Vector2(-14.163f, -6.832f),
                new Vector2(-18.551f, 2.625f),
                new Vector2(-7.556f, -2.124f)
            });

            mapLocations.Add(MapType.Polus, new Vector2[] {
                new Vector2(4.458f, -3.387f),
                new Vector2(3.801F, -7.584F),
                new Vector2(7.193f, -13.089f),
                new Vector2(4.042f, -11.233f),
                new Vector2(0.660f, -15.868f),
                new Vector2(1.516f, -18.669f),
                new Vector2(2.331f, -24.491f),
                new Vector2(9.231f, -25.351f),
                new Vector2(12.681f, -24.541f),
                new Vector2(12.489f, -17.160f),
                new Vector2(17.958f, -25.710f),
                new Vector2(22.225f, -25.008f),
                new Vector2(23.933f, -20.589f),
                new Vector2(31.295f, -11.321f),
                new Vector2(18.055f, -13.020f),
                new Vector2(12.892f, -17.317f),
                new Vector2(6.582f -17,113f),
                new Vector2(23.639f, -2.799f),
                new Vector2(24.928f, -6.877f),
                new Vector2(32.344f, -10.047f),
                new Vector2(34.852f, -5.208f),
                new Vector2(40.516f, -8.102f),
                new Vector2(36.291f, -22.012f)
            });

            mapLocations.Add(MapType.MiraHQ, new Vector2[] {
                new Vector2(18.266f, -3.223f),
                new Vector2(28.257f, -2.250f),
                new Vector2(18.293f, 5.045f),
                new Vector2(28.276f, 2.735f),
                new Vector2(17.843f, 11.516f),
                new Vector2(13.750f, 17.214f),
                new Vector2(22.387f, 19.160f),
                new Vector2(13.862f, 23.878f),
                new Vector2(19.330f, 25.309f),
                new Vector2(16.177f, 3.085f),
                new Vector2(16.752f, -1.455f),
                new Vector2(11.755f, 10.300f),
                new Vector2(11.112f, 14.068f),
                new Vector2(2.444f, 13.352f),
                new Vector2(0.414f, 10.087f),
                new Vector2(-5.780f, -2.037f),
                new Vector2(16.752f, -1.455f),
                new Vector2(10.161f, 5.162f)
            });

            bool RerollPosition;
            do {
                RerollPosition = false;
                var random = new System.Random();
                Vector2[] vectors = mapLocations[(MapType) MapID];
                currentPositon = vectors[random.Next(vectors.Count())];

                if (GlobalVariable.stonePositon == null)
                    break;

                foreach (KeyValuePair<string, Vector2> element in GlobalVariable.stonePositon) {
                    float positionBeetween = Vector2.Distance(element.Value, currentPositon);
                    if (positionBeetween == 0f)
                        RerollPosition = true;
                }
            } while (RerollPosition);

            if (!GlobalVariable.stonePositon.ContainsKey(composent))
                GlobalVariable.stonePositon.Add(composent, currentPositon);

            return currentPositon;
        }

        internal static Dictionary<string, Vector2> SetAllStonePositions() {
            foreach (var stone in GlobalVariable.stonesNames) {
                Vector2 position = GetRandomLocation(stone);

                if (!GlobalVariable.stonePositon.ContainsKey(stone))
                    GlobalVariable.stonePositon.Add(stone, position);
            }

            return GlobalVariable.stonePositon;
        }

        internal static void PlaceAllStone() {
            Stone.Map.Mind.Place(GlobalVariable.stonePositon["Mind"]);
            Stone.Map.Power.Place(GlobalVariable.stonePositon["Power"]);
            Stone.Map.Soul.Place(GlobalVariable.stonePositon["Soul"]);
            Stone.Map.Time.Place(GlobalVariable.stonePositon["Time"]);
            Stone.Map.Space.Place(GlobalVariable.stonePositon["Space"]);
            Stone.Map.Reality.Place(GlobalVariable.stonePositon["Reality"]);
        }
    }
}
