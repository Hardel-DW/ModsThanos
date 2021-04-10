using UnityEngine;

namespace ModsThanos.Stone {
    class Mind : StoneManager {
        Mind() : base("Mind", new Color(1f, 1f, 1f, 1f), new Vector2(1f, 2f), true) { }

        public override void OnClick() {
            System.Mind.CoreMind.OnMindPressed();
        }

        public override void OnEffectEnd() {
            System.Mind.CoreMind.OnMindEnded();
        }

        public override void OnUpdate() {
            if (!GlobalVariable.UsableButton)
                Button.SetCanUse(false);
            else
                Button.SetCanUse(HasStone(PlayerControl.LocalPlayer.PlayerId));
        }
    }
}
