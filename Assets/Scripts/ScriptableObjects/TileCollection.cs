using UnityEngine;
using Majingari.Game.ActorComponent;

namespace Majingari.Game.Model {
    [CreateAssetMenu(fileName = "Tile Collection", menuName = "Model/Tile Collection")]
    public class TileCollection : ScriptableObject {
        [SerializeField] private TileComponent[] tilePrefab;

        public TileComponent[] GetTileCollection => tilePrefab;
    }

}
