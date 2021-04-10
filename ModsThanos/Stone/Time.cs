using UnityEngine;

namespace ModsThanos.Stone {
    class Time : StoneManager {
        Time() : base("Time", new Color(1f, 1f, 1f, 1f), new Vector2(0f, 2f), false) { }

        public override void OnClick() {
            System.Space.OnSpacePressed();
        }

        public override void OnEffectEnd() {
            System.Time.StopRewind();
        }

        public override void OnUpdate() {
            if (!GlobalVariable.UsableButton)
                Button.SetCanUse(false);
            else
                Button.SetCanUse(HasStone(PlayerControl.LocalPlayer.PlayerId));

            if (System.Time.isRewinding)
                for (int i = 0; i < 2; i++)System.Time.Rewind();
            else System.Time.Record();
        }
    }
}
