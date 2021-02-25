using HarmonyLib;
using System.Linq;
using UnityEngine;
using Hazel;
using ModsThanos.Utility;
using System.Collections.Generic;

namespace ModsThanos.Stone.System {

    public static class Space {
        private static List<Vent> listVent = new List<Vent>();
        public static Vent lastVent;

        public static void OnSpacePressed() {
            var pos = PlayerControl.LocalPlayer.transform.position;
            int ventId = GetAvailableVentId();
            int ventLeft = int.MaxValue;
            int ventCrnter = int.MaxValue;
            int ventRight = int.MaxValue;

            if (lastVent != null) {
                ventLeft = lastVent.Id;
            }

            RpcSpawnVent(ventId, pos, ventLeft, ventCrnter, ventRight);
        }

        static int GetAvailableVentId() {
            int id = 0;

            while (true) {
                if (!ShipStatus.Instance.AllVents.Any(v => v.Id == id)) {
                    return id;
                }
                id++;
            }
        }

        static void SpawnVent(int id, Vector2 postion, int leftVent, int centerVent, int rightVent) {
            var ventPref = GameObject.FindObjectOfType<Vent>();
            var vent = GameObject.Instantiate<Vent>(ventPref, ventPref.transform.parent);

            GameObject goRender = new GameObject();
            SpriteRenderer renderer = goRender.AddComponent<SpriteRenderer>();
            goRender.gameObject.transform.position = new Vector3(postion.x, postion.y, 1f);
            goRender.gameObject.transform.parent = ventPref.transform.parent;
            renderer.sprite = HelperSprite.LoadSpriteFromEmbeddedResources("ModsThanos.Resources.portal.png", 300f);
            vent.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);

            vent.Id = id;
            vent.transform.position = postion;
            vent.Left = leftVent == int.MaxValue ? null : ShipStatus.Instance.AllVents.FirstOrDefault(v => v.Id == leftVent);
            vent.Center = centerVent == int.MaxValue ? null : ShipStatus.Instance.AllVents.FirstOrDefault(v => v.Id == centerVent);
            vent.Right = rightVent == int.MaxValue ? null : ShipStatus.Instance.AllVents.FirstOrDefault(v => v.Id == rightVent);

            var allVents = ShipStatus.Instance.AllVents.ToList();
            allVents.Add(vent);
            ShipStatus.Instance.AllVents = allVents.ToArray();

            if (lastVent != null) {
                lastVent.Right = ShipStatus.Instance.AllVents.FirstOrDefault(v => v.Id == id);
            }

            lastVent = vent;

            HelperSprite.ShowAnimation(1, 30, true, "ModsThanos.Resources.anim-space.png", 64, 1, new Vector3(goRender.gameObject.transform.position.x, goRender.gameObject.transform.position.y + 0.3f, 1f), 5);;
        }

        static void RpcSpawnVent(int id, Vector2 postion, int leftVent, int centerVent, int rightVent) {

            SpawnVent(id, postion, leftVent, centerVent, rightVent);

            var w = AmongUsClient.Instance.StartRpc(ShipStatus.Instance.NetId, (byte) CustomRPC.SpawnPortal, SendOption.Reliable);

            w.WritePacked(id);
            w.WriteVector2(postion);
            w.WritePacked(leftVent);
            w.WritePacked(centerVent);
            w.WritePacked(rightVent);
            w.EndMessage();

        }

        [HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.HandleRpc))]
        static class ShipstatusHandleRpcPatch {
            static bool Prefix(ShipStatus __instance, byte HKHMBLJFLMC, MessageReader ALMCIJKELCP) {
                if (HKHMBLJFLMC == (byte) CustomRPC.SpawnPortal) {
                    var reader = ALMCIJKELCP;
                    var id = reader.ReadPackedInt32();
                    var postion = reader.ReadVector2();
                    var leftVent = reader.ReadPackedInt32();
                    var centerVent = reader.ReadPackedInt32();
                    var rightVent = reader.ReadPackedInt32();

                    SpawnVent(
                        id: id,
                        postion: postion,
                        leftVent: leftVent,
                        centerVent: centerVent,
                        rightVent: rightVent
                    );
                    return false;
                }
                return true;
            }
        }
    }
}