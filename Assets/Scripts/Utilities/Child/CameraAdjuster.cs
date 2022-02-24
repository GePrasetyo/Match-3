using UnityEngine;

namespace Majingari.Game.Utilities {
    public class CameraAdjuster : TransformAdjuster {
        public override void AdjustCenterPosition(int tileSizeX, int tileSizeY) {
            var zPost = (tileSizeX > tileSizeY ? tileSizeX : tileSizeY) * -1;
            transform.localPosition = new Vector3((tileSizeX - 1) / 2f, (tileSizeY - 1) / 2f, zPost);
        }
    }
}
