using UnityEngine;

namespace ModsThanos.Stone {
    class Space : StoneManager {
        Space() : base("Space", new Color(1f, 1f, 1f, 1f), new Vector2(0f, 1f), false) { }

        public override void OnClick() {
            System.Space.OnSpacePressed();
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
