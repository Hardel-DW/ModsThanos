using HarmonyLib;
using Hazel;
using System;
using System.Collections.Generic;
using System.Linq;
using UnhollowerBaseLib;
using UnityEngine;

namespace ModsThanos.Patch.CustomGameOption {
    [HarmonyPatch(typeof(PHCKLDDNJNP))]
    public static class GameOptionMenuPatch {
        public static List<CustomNumberOptions> numberButtonLists = CustomGameOptions.InitNumberOptions();
        public static List<CustomStringOptions> stringButtonLists = CustomGameOptions.InitStringOptions();
        public static bool initText = false;

        [HarmonyPostfix]
        [HarmonyPatch("Start")]
        public static void Postfix1(PHCKLDDNJNP __instance) {
            if (numberButtonLists[0].Button == null || stringButtonLists[0].Button == null) {
                initText = false;
                PCGDGFIAJJI killcd = GameObject.FindObjectsOfType<PCGDGFIAJJI>().ToList().Where(x => x.TitleText.Text == "Kill Cooldown").First();

                // Number Option
                foreach (var item in numberButtonLists) {
                    item.Button = GameObject.Instantiate(killcd);
                    item.Button.gameObject.name = item.Name;
                    item.Button.TitleText.Text = item.Name;
                    item.Button.Value = item.DefaultValue;
                    item.Button.ValueText.Text = item.DefaultValue.ToString();
                }

                // String Option
                foreach (var item in stringButtonLists) {
                    item.Button = GameObject.Instantiate(killcd);
                    item.Button.gameObject.name = item.Name;
                    item.Button.TitleText.Text = item.Name;
                    item.Button.Value = 0;
                    item.Button.ValueText.Text = item.DefaultValue;
                }

                LLKOLCLGCBD[] options = new LLKOLCLGCBD[__instance.KJFHAPEDEBH.Count + numberButtonLists.Count + stringButtonLists.Count];
                __instance.KJFHAPEDEBH.ToArray().CopyTo(options, 0);

                for (int i = stringButtonLists.Count; i >= 1; i--) {
                    options[options.Length - (i + numberButtonLists.Count)] = stringButtonLists[i - 1].Button;
                }

                for (int i = numberButtonLists.Count; i >= 1; i--) {
                    options[options.Length - i] = numberButtonLists[i - 1].Button;
                }

                __instance.KJFHAPEDEBH = new Il2CppReferenceArray<LLKOLCLGCBD>(options);

                __instance.GetComponentInParent<Scroller>().YBounds.max = +0.5f + __instance.KJFHAPEDEBH.Count * 0.4F;
                //ModThanos.Logger.LogInfo(__instance.KJFHAPEDEBH.Count);
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch("Update")]
        public static void Postfix2(PHCKLDDNJNP __instance) {
            BCLDBBKFJPK showAnonymousvote = GameObject.FindObjectsOfType<BCLDBBKFJPK>().ToList().Where(x => x.TitleText.Text == "Anonymous Votes").First();
            bool isNull = false;

            foreach (var item in numberButtonLists)
                if (item.Button == null)
                    isNull = true;

            foreach (var item in stringButtonLists)
                if (item.Button == null)
                    isNull = true;

            if (!isNull) {
                for (int i = 0; i < numberButtonLists.Count; i++) {
                    numberButtonLists[i].Button.transform.position = showAnonymousvote.transform.position - new Vector3(0, 5.5f + (float) (i * 0.5f), 0);
                    if (!initText) 
                        numberButtonLists[i].Button.ValueText.Text = numberButtonLists[i].Value.ToString() + (numberButtonLists[i].ItsSecond == true ? "s" : "");
                }

                for (int i = 0; i < stringButtonLists.Count; i++) {
                    stringButtonLists[i].Button.transform.position = showAnonymousvote.transform.position - new Vector3(0, 5.5f + (float) ((i + numberButtonLists.Count) * 0.5f), 0);
                    if (!initText)
                        stringButtonLists[i].Button.ValueText.Text = stringButtonLists[i].AllValue[stringButtonLists[i].Index];
                }

                initText = true;
            }
        }
    }

    [HarmonyPatch(typeof(PCGDGFIAJJI))]
    public static class NumberOptionPatch {
        [HarmonyPrefix]
        [HarmonyPatch("Increase")]
        public static bool Prefix1(PCGDGFIAJJI __instance) {

            foreach (var item in GameOptionMenuPatch.numberButtonLists) {
                if (__instance.TitleText.Text == item.Name) {
                    item.Value = Math.Min(item.Value + (float) item.Interval, item.MaxValue);
                    FFGALNAPKCD.LocalPlayer.RpcSyncSettings(FFGALNAPKCD.GameOptions);
                    item.Button.NHLMDAOEOAE = item.Value;
                    item.Button.Value = item.Value;
                    item.Button.ValueText.Text = item.Value.ToString() + (item.ItsSecond == true ? "s" : "");
                    item.ValueChanged();

                    return false;
                }
            }

            foreach (var item in GameOptionMenuPatch.stringButtonLists) {
                if (__instance.TitleText.Text == item.Name) {

                    //ModThanos.Logger.LogInfo($"Max: {item.MaxValue}, Index: {item.Index}");
                    item.Index = Math.Min(item.Index + 1, item.MaxValue);
                    FFGALNAPKCD.LocalPlayer.RpcSyncSettings(FFGALNAPKCD.GameOptions);
                    item.Button.NHLMDAOEOAE = item.Index;
                    item.Button.Value = item.Index;
                    item.Button.ValueText.Text = item.AllValue[item.Index];
                    item.ValueChanged();

                    return false;
                }
            }

            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch("Decrease")]
        public static bool Prefix2(PCGDGFIAJJI __instance) {

            foreach (var item in GameOptionMenuPatch.numberButtonLists) {
                if (__instance.TitleText.Text == item.Name) {
                    item.Value = Math.Max(item.Value - (float) item.Interval, item.MinValue);
                    FFGALNAPKCD.LocalPlayer.RpcSyncSettings(FFGALNAPKCD.GameOptions);
                    item.Button.NHLMDAOEOAE = item.Value;
                    item.Button.Value = item.Value;
                    item.Button.ValueText.Text = item.Value.ToString() + (item.ItsSecond == true ? "s" : "");
                    item.ValueChanged();

                    return false;
                }
            }

            foreach (var item in GameOptionMenuPatch.stringButtonLists) {
                if (__instance.TitleText.Text == item.Name) {
                    //ModThanos.Logger.LogInfo($"Max: {item.MinValue}, Index: {item.Index}");

                    item.Index = Math.Max(item.Index - 1, item.MinValue);
                    FFGALNAPKCD.LocalPlayer.RpcSyncSettings(FFGALNAPKCD.GameOptions);
                    item.Button.NHLMDAOEOAE = item.Index;
                    item.Button.Value = item.Index;
                    item.Button.ValueText.Text = item.AllValue[item.Index];
                    item.ValueChanged();

                    return false;
                }
            }

            return true;
        }
    }

    public static class GameOptionsHud {
        public static int counter = 0;

        public static void UpdateGameMenu() {
            if (++counter < 30) return;
            counter = 0;

            bool isNull = false;

            foreach (var item in GameOptionMenuPatch.numberButtonLists)
                if (item.Button == null)
                    isNull = true;

            foreach (var item in GameOptionMenuPatch.stringButtonLists)
                if (item.Button == null)
                    isNull = true;

            if (!isNull) {
                if (!(GameObject.FindObjectsOfType<GameOptionsMenu>().Count != 0)) {
                    foreach (var item in GameOptionMenuPatch.stringButtonLists)
                        UnityEngine.Object.Destroy(item.Button.gameObject);

                    foreach (var item in GameOptionMenuPatch.numberButtonLists)
                        UnityEngine.Object.Destroy(item.Button.gameObject);

                    MessageWriter writer = FMLLKEACGIO.Instance.StartRpcImmediately(FFGALNAPKCD.LocalPlayer.NetId, (byte) CustomRPC.SyncCustomSettings, Hazel.SendOption.None, -1);

                    foreach (var item in CustomGameOptions.stringOptions)
                        item.ValueChanged();

                    foreach (var item in CustomGameOptions.numberOptions)
                        writer.Write(item.Value);

                    foreach (var item in CustomGameOptions.stringOptions)
                        writer.Write(item.Value);

                    FMLLKEACGIO.Instance.FinishRpcImmediately(writer);
                }
            }
        }
    }
}