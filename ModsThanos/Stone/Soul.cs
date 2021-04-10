using UnityEngine;

namespace ModsThanos.Stone {
    class Soul : StoneManager {
        Soul() : base("Soul", new Color(1f, 1f, 1f, 1f), new Vector2(0f, 0f), false) { }

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
