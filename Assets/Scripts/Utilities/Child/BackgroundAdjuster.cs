using UnityEngine;

namespace Majingari.Game.Utilities {
    public class BackgroundAdjuster : TransformAdjuster {
        public override void AdjustCenterPosition(int tileSizeX, int tileSizeY) {
            base.AdjustCenterPosition(tileSizeX, tileSizeY);

            AdjustingScale(tileSizeX, tileSizeY);
        }

        private void AdjustingScale(int tileSizeX, int tileSizeY) {
            transform.localScale = new Vector3(tileSizeX, tileSizeY, transform.localScale.z);
        }
    }
}
