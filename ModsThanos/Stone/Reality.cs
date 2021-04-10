using UnityEngine;

namespace ModsThanos.Stone {
    class Reality : StoneManager {
        Reality() : base("Reality", new Color(1f, 1f, 1f, 1f), new Vector2(1f, 0f), false) { }

        public override void OnClick() {
            Stone.System.Reality.OnRealityPressed(true);
        }

        public override void OnEffectEnd() {
            Stone.System.Reality.OnRealityPressed(false);
        }

        public override void OnUpdate() {
            if (!GlobalVariable.UsableButton)
                Button.SetCanUse(false);
            else
                Button.SetCanUse(HasStone(PlayerControl.LocalPlayer.PlayerId));
        }
    }
}
