using HarmonyLib;
using UnityEngine;

namespace ModsThanos.Patch {

    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.SetTasks))]
    class TasksPatch {
        public static void Postfix(PlayerControl __instance) {
            if (PlayerControl.LocalPlayer != null) {
                if (GlobalVariable.allThanos != null && RoleHelper.IsThanos(PlayerControl.LocalPlayer.PlayerId)) {
                    ImportantTextTask ImportantTasks = new GameObject("ThanosTasks").AddComponent<ImportantTextTask>();
                    ImportantTasks.transform.SetParent(__instance.transform, false);
                    ImportantTasks.Text = "[FFFFFFFF]Objectif: Trouver les pierres pour obtenir le snap.[]\n\n[808080FF]Snap:[] Termine la partie.\n[008516FF]Pierre du temps :[] Permet de revenir dans le temps.\n[822FA8FF]Pierre de pouvoir :[] Permet de tuer en zone.\n[C46f1AFF]Pierre de l'âme :[] les crewmate peuvent la ramasser.\n[A6A02EFF]Pierre de l'esprit :[] Permet de se transformer en quelqu'un.\n[3482BAFF]Pierre de l'espace[]: Pose des portails.\n[D43D3DFF]Pierre de Réalité[]: Permet de se rendre invisible";
                    __instance.myTasks.Insert(0, ImportantTasks);
                }
            }
        }
    }
}