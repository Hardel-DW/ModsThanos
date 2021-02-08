using System;
using System.Collections.Generic;
using System.Text;

namespace ModsThanos.Patch.CustomGameOption {
    public class CustomNumberOptions {

        private string name;
        private float maxValue;
        private float minValue;
        private float interval;
        private PCGDGFIAJJI button;
        private Action OnClick;
        private float defaultValue;
        private float oldValue;
        private float value;
        private bool itsSecond;

        public CustomNumberOptions(string name, float defaultValue, float maxValue, float minValue, float interval, PCGDGFIAJJI button, Action onClick) {
            this.name = name;
            this.maxValue = maxValue;
            this.minValue = minValue;
            this.interval = interval;
            this.button = button;
            this.defaultValue = defaultValue;
            this.value = defaultValue;
            this.OnClick = onClick;
            this.itsSecond = true;
        }

        public CustomNumberOptions(string name, float defaultValue, float maxValue, float minValue, float interval, bool second, PCGDGFIAJJI button, Action onClick) {
            this.name = name;
            this.maxValue = maxValue;
            this.minValue = minValue;
            this.interval = interval;
            this.button = button;
            this.defaultValue = defaultValue;
            this.value = defaultValue;
            this.OnClick = onClick;
            this.itsSecond = second;
        }

        public void ValueChanged() {
            this.OnClick();
        }

        public float MinValue {
            get => minValue;
            set => minValue = value;
        }
        public string Name {
            get => name;
            set => name = value;
        }
        public float MaxValue {
            get => maxValue;
            set => maxValue = value;
        }
        public float Interval {
            get => interval;
            set => interval = value;
        }
        public PCGDGFIAJJI Button {
            get => button;
            set => button = value;
        }
        public Action OnClick1 {
            get => OnClick;
            set => OnClick = value;
        }
        public float DefaultValue {
            get => defaultValue;
            set => defaultValue = value;
        }
        public float Value {
            get => value;
            set => this.value = value;
        }
        public float OldValue {
            get => oldValue;
            set => oldValue = value;
        }
        public bool ItsSecond {
            get => itsSecond;
            set => itsSecond = value;
        }
    }
}
