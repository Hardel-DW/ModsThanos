using System;
using System.Collections.Generic;
using System.Text;

namespace ModsThanos.Patch.CustomGameOption {
    public class CustomStringOptions {

        private string name;
        private string[] allValue;
        private PCGDGFIAJJI button;
        private Action OnClick;
        private string defaultValue;
        private string oldValue;
        private string value;
        private int oldIndex;
        private int maxValue;
        private int index = 0;
        private int minValue = 0;

        public CustomStringOptions(string name, string defaultValue, string[] allValue, PCGDGFIAJJI button, Action onClick) {
            this.name = name;
            this.allValue = allValue;
            this.button = button;
            OnClick = onClick;
            this.defaultValue = defaultValue;
            this.value = defaultValue;
            this.maxValue = allValue.Length - 1;
        }

        public string Name {
            get => name;
            set => name = value;
        }
        public string[] AllValue {
            get => allValue;
            set => allValue = value;
        }
        public PCGDGFIAJJI Button {
            get => button;
            set => button = value;
        }
        public Action OnClick1 {
            get => OnClick;
            set => OnClick = value;
        }
        public string DefaultValue {
            get => defaultValue;
            set => defaultValue = value;
        }
        public string OldValue {
            get => oldValue;
            set => oldValue = value;
        }
        public string Value {
            get => value;
            set => this.value = value;
        }
        public int Index {
            get => index;
            set => index = value;
        }
        public int OldIndex {
            get => oldIndex;
            set => oldIndex = value;
        }
        public int MinValue {
            get => minValue;
            set => minValue = value;
        }
        public int MaxValue {
            get => maxValue;
            set => maxValue = value;
        }

        public void ValueChanged() {
            value = AllValue[index];

            this.OnClick();
        }

        public void ValueChanged(string newValue) {
            value = newValue;
            this.OnClick();
        }
    }
}
