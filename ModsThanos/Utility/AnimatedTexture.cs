using Reactor;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModsThanos {

	[RegisterInIl2Cpp]
	class AnimatedTexture : MonoBehaviour {
		public AnimatedTexture(IntPtr ptr) : base(ptr) { }

		public int TileX = 1;
		public int TileY = 28;
		public int framerate = 1;
		public string image = "ModsThanos.Resources.anim.png";
		public int PixelPerUnit = 64;
		public bool simpleAnimation = false;

		private List<Sprite> sprites = new List<Sprite>();
		private SpriteRenderer renderer;
		private int turn = 0;

		void Start() {
			renderer = this.gameObject.AddComponent<SpriteRenderer>();
			sprites = Utility.HelperSprite.LoadTileTextureEmbed(image, PixelPerUnit, TileX, TileY);
		}

		void Update() {
            turn++;
			if (turn > (TileX * TileY) - 1) {
				if (simpleAnimation) {
					Destroy(this.gameObject);
				} 
                turn = 0;
			}

            renderer.sprite = sprites[turn];
        }
	}
}
