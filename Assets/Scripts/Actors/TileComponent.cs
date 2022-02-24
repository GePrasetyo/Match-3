using System;
using UnityEngine;
using Majingari.Game.SceneComponent;

namespace Majingari.Game.ActorComponent {
    public class TileComponent : MonoBehaviour {
        [SerializeField] private int tileType;
        public int GetTileType => tileType;
        [HideInInspector] public int row, column;
        [SerializeField] private float speed;

        private Locomotion locomotion;
        public Action DestroyThisTile;
        private Coroutine updateRunning;

        void OnMouseDown() {
            DestroyThisTile?.Invoke();
        }

        public void SetupTile(int rowY, int columnX, GameModeMatchThree gameMode) {
            row = rowY;
            column = columnX;

            DestroyThisTile += () => {
                gameMode.TileDeleted(column, row);
                gameObject.SetActive(false);
            };

            locomotion = new Locomotion(transform, speed);
            locomotion.OnMoveComplete += () => {
                gameMode.CheckMatchThree(column, row, tileType);
            };
        }

        public void MoveDown(int yTarget) {
            row = yTarget;

            if (updateRunning != null)
                StopCoroutine(updateRunning);

            updateRunning = StartCoroutine(locomotion.UpdateLerp(yTarget));
        }
    }
}
