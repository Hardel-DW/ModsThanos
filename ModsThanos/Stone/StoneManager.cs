using HarmonyLib;
using ModsThanos.Utility;
using ModsThanos.Utility.Enumerations;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ModsThanos.Stone {

    abstract class StoneManager {
        public static List<StoneManager> Stones;

        public string Name { get; set; }
        public Color Color { get; set; }
        public bool IsPicked { get; set; }
        public PlayerControl Player { get; set; }
        public CooldownButton Button { get; set; }
        public GameObject GameObject { get; set; }
        public Visibility Visiblity { get; set; }
        public Vector2 PositionHud { get; set; }
        public bool CanSeeStone { get; set; }
        public bool HasEffectDuration { get; set; }

        protected StoneManager(string name, Color color, Vector2 positionHud, bool hasEffectDuration) {
            Name = name;
            Color = color;
            PositionHud = positionHud;
            HasEffectDuration = hasEffectDuration;
            Stones.Add(this);
        }

        public bool HasStone(byte PlayerId) {
            return PlayerControl.AllPlayerControls.ToArray().ToList().FirstOrDefault(s => s.PlayerId == PlayerId);
        }

        public void DestroyThisObject() {
            Object.Destroy(GameObject);
            if (Stones.Contains(this)) Stones.Remove(this);
        }

        public void ModifyPosition(Vector3 position) {
            GameObject.transform.position = position;
        }

        public void CanSee() {
            if (PlayerControl.LocalPlayer == null)
                return;

            bool IsImpostor = PlayerControl.LocalPlayer.Data.IsImpostor;
            switch (Visiblity) {
                case Visibility.Everyone: {
                    CanSeeStone = true;
                    break;
                }
                case Visibility.OnlyCrewmate: {
                    CanSeeStone = !IsImpostor;
                    break;
                }
                case Visibility.OnlyImpostor: {
                    CanSeeStone = IsImpostor;
                    break;
                }
            }
        }

        public void CreateGameObject(Vector2 Position) {
            CanSee();
            GameObject = new GameObject(Name);
            SpriteRenderer renderer = GameObject.AddComponent<SpriteRenderer>();
            GemBehaviour gemBehaviour = GameObject.AddComponent<GemBehaviour>();
            BoxCollider2D collider = GameObject.AddComponent<BoxCollider2D>();

            collider.size = new Vector2(1f, 1f);
            collider.isTrigger = true;
            GameObject.transform.position = new Vector3(Position.x, Position.y, -0.5f);
            GameObject.transform.localPosition = new Vector3(Position.x, Position.y, -0.5f);
            GameObject.transform.localScale = new Vector3(1f, 1f, 1f);

            if (CanSeeStone) renderer.sprite = HelperSprite.LoadSpriteFromEmbeddedResources($"ModsThanos.Resources.{Name}.png", 300f);
            GameObject.SetActive(true);
        }

        public void ReplaceStone() {
            DestroyThisObject();
            IsPicked = false;
            Player = null;
        }

        public static void PlaceAllStone() {
            foreach (var Stone in Stones) {
                Stone.CreateGameObject(RandomPosition.GetRandomPositionUnique(Stones.Select(s => (Vector2) s.GameObject.transform.position).ToList(), 1f));
            }
        }

        public abstract void ChangeVisibility();

        public abstract void OnEffectEnd();

        public abstract void OnUpdate();

        public abstract void OnClick();
    }

    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Start))]
    public static class StonesHud {
        public static void Postfix(HudManager __instance) {
            foreach (var Stone in StoneManager.Stones) {
                if (Stone.HasEffectDuration) {
                    Stone.Button = new CooldownButton
                        (() => Stone.OnClick(),
                        CustomGameOptions.CooldownMindStone.GetValue(),
                        $"ModsThanos.Resources.{Stone.Name}.png",
                        300f,
                        Stone.PositionHud,
                        Visibility.OnlyImpostor,
                        __instance,
                        CustomGameOptions.CooldownMindStone.GetValue(),
                        () => Stone.OnEffectEnd(),
                        () => Stone.OnUpdate()
                    );
                } else {
                    Stone.Button = new CooldownButton
                        (() => Stone.OnClick(),
                        CustomGameOptions.CooldownMindStone.GetValue(),
                        $"ModsThanos.Resources.{Stone.Name}.png",
                        300f,
                        Stone.PositionHud,
                        Visibility.OnlyImpostor,    
                        __instance,
                        () => Stone.OnUpdate()
                    );
                }
            }
        }
    }
}