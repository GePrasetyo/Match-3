using UnityEngine;
using Majingari.Game.SceneComponent;

namespace Majingari.Game.Utilities {
    public class TransformAdjuster : MonoBehaviour {
        void OnEnable() {
            MapTileGenerator.MapSizeUpdated += AdjustCenterPosition;
        }

        void OnDisable() {
            MapTileGenerator.MapSizeUpdated -= AdjustCenterPosition;
        }

        public virtual void AdjustCenterPosition(int tileSizeX, int tileSizeY) {
            transform.localPosition = new Vector3((tileSizeX - 1) / 2f, (tileSizeY - 1) / 2f, transform.localPosition.z);
        }
    }
}
