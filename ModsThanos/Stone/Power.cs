using UnityEngine;

namespace ModsThanos.Stone {
    class Power : StoneManager {
        Power() : base("Power", new Color(1f, 1f, 1f, 1f), new Vector2(1f, 1f), false) { }

        public override void OnClick() {
            Stone.System.Power.OnPowerPressed();
        }

        public override void OnEffectEnd() { }

        public override void OnUpdate() {
            if (!GlobalVariable.UsableButton)
                Button.SetCanUse(false);
            else
                Button.SetCanUse(HasStone(PlayerControl.LocalPlayer.PlayerId));
        }
    }
}
