using ModsThanos.Utility.Enumerations;
using UnityEngine;

namespace ModsThanos {
    class ComponentMap {
        public Vector3 position;
        string Ressource;
        string GameObjectName = "New";
        bool visibility = true;
        Visibility visible = Visibility.Everyone;
        GameObject component;

        public ComponentMap(Vector3 position, string ImageEmbededResourcePath, string name, Visibility visible) {
            this.position = position;
            this.Ressource = ImageEmbededResourcePath;
            this.visible = visible;
            this.GameObjectName = name;
            Start();

            if (!GlobalVariable.stoneObjects.ContainsKey(name)) {
                GlobalVariable.stoneObjects.Add(name, this);
            }
        }

        private void Start() {
            CanSee();
            component = new GameObject(this.GameObjectName);
            if (this.visibility) {
                SpriteRenderer renderer = component.AddComponent<SpriteRenderer>();
                renderer.sprite = Utility.HelperSprite.LoadSpriteFromEmbeddedResources(Ressource, CustomGameOptions.StoneSize.GetValue());
            }

            GemBehaviour gemBehaviour = component.AddComponent<GemBehaviour>();
            BoxCollider2D collider = component.AddComponent<BoxCollider2D>();
            collider.size = new Vector2(1f, 1f);
            collider.isTrigger = true;
            component.transform.position = new Vector3(position.x, position.y, -0.5f);
            component.transform.localPosition = new Vector3(position.x, position.y, -0.5f);
            component.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);

            component.SetActive(true);
        }

        public void CanSee() {
            if (PlayerControl.LocalPlayer == null) return;
            if (PlayerControl.LocalPlayer.Data == null) return;

            bool isImpostor = PlayerControl.LocalPlayer.Data.IsImpostor;
            switch (this.visible) {
                case Visibility.Everyone: {
                    this.visibility = true;
                    break;
                }
                case Visibility.OnlyCrewmate: {
                    this.visibility = !isImpostor;
                    break;
                }
                case Visibility.OnlyImpostor: {
                    this.visibility = isImpostor;
                    break;
                }
            }
        }

        internal void DestroyThisObject() {
            Object.Destroy(component);

            if (GlobalVariable.stoneObjects.ContainsKey(this.GameObjectName)) {
                GlobalVariable.stoneObjects.Remove(this.GameObjectName);
            }

            if (GlobalVariable.stonePositon.ContainsKey(this.GameObjectName)) {
                GlobalVariable.stonePositon.Remove(this.GameObjectName);
            }
        }

        internal void ModifyPosition(Vector3 position) {
            component.transform.position = position;
            GlobalVariable.stonePositon[this.GameObjectName] = new Vector2(position.x, position.y);

            if (this.GameObjectName == "Soul") {
                GlobalVariable.arrow.GetComponent<DGPHMLNNPDN>().target = new Vector2(position.x, position.y);
            }
        }
    }
}
