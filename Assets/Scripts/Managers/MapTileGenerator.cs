using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Majingari.Game.Model;
using Majingari.Game.ActorComponent;

using Random = UnityEngine.Random;

namespace Majingari.Game.SceneComponent {
    public class MapTileGenerator : MonoBehaviour {
        public static event Action<int, int> MapSizeUpdated;

        [SerializeField] private Transform parentTile;
        [SerializeField] private int columnLength;
        [SerializeField] private int rowLength;
        [SerializeField] private TileCollection tileCollections;
        private List<TileComponent> tileDumpster = new List<TileComponent>();   //In case needed for pooling object

        void Start() {
            GenerateTileMap();
        }

        public void GenerateTileMap() {
            if (columnLength <= 0 || rowLength <= 0)
                return;

            Assert.IsTrue(columnLength > 3 || rowLength > 3, "Tile size has to be more than 3");

            var tableTile = new List<TileComponent>[columnLength];
            var tilePrefabColelction = new List<TileComponent>(tileCollections.GetTileCollection);
            TileComponent dumpRepeatingTile;

            Assert.IsFalse(tilePrefabColelction.Contains(null), "There is empty prefab in Tile Collection");

            var gameMode = new GameModeMatchThree();

            var resultRandom = 0;
            var lastRandomIndex = 0;
            var sameResultTime = 0;

            for (int y = 0; y < rowLength; y++) {
                sameResultTime = 0;

                for (int x = 0; x < columnLength; x++) {
                    if (sameResultTime > 0) {   // To prevent same tile more than 2 in a row
                        dumpRepeatingTile = tilePrefabColelction[lastRandomIndex];
                        tilePrefabColelction.RemoveAt(lastRandomIndex);

                        resultRandom = Random.Range(0, tilePrefabColelction.Count);

                        sameResultTime = 0;
                        lastRandomIndex = resultRandom;
                        tilePrefabColelction.Add(dumpRepeatingTile);
                    }
                    else {
                        resultRandom = Random.Range(0, tilePrefabColelction.Count);

                        sameResultTime = lastRandomIndex == resultRandom ? sameResultTime + 1 : 0;
                        lastRandomIndex = resultRandom;
                    }

                    var column = x;
                    var row = y;
                    var generatedTile = Instantiate(tilePrefabColelction[resultRandom], new Vector3(column, row, 0), Quaternion.identity, parentTile);
                    generatedTile.SetupTile(row, column, gameMode);

                    generatedTile.DestroyThisTile += () => {
                        tileDumpster.Add(generatedTile);
                    };

                    if (y == 0) {
                        tableTile[x] = new List<TileComponent>();
                    }
                    tableTile[x].Add(generatedTile);
                }
            }

            gameMode.tileColumn = tableTile;
            ServiceLocator.Register<GameModeMatchThree>(gameMode);

            MapSizeUpdated?.Invoke(columnLength, rowLength);
        }
    }
}
