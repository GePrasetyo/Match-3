using Majingari.Game.ActorComponent;
using System.Collections.Generic;

namespace Majingari.Game.SceneComponent {
    public class GameModeMatchThree {
        public List<TileComponent>[] tileColumn;

        public void TileDeleted(int column, int row) {

            tileColumn[column].RemoveAt(row);
            var tileLeft = tileColumn[column].Count;
            if (tileLeft < 1) {
                return;
            }

            for (int i = row; i < tileLeft; i++) {
                tileColumn[column][i].MoveDown(i);
            }
        }

        public void CheckMatchThree(int column, int row, int tileType) {
            var maxColumn = tileColumn.Length;
            var sameTile = new List<TileComponent>();
            var indexToLeft = column;
            var indexToRight = column;
            var leftChecking = true;
            var rightChecking = true;

            while (rightChecking || leftChecking) {            //Skip the adjacent checking process if there's no tile or the neightbor isn't the same tile
                if (leftChecking) {
                    leftChecking = --indexToLeft >= 0;

                    if (!leftChecking) {
                        goto LeftcheckingSkip;  //Using goto to skip the adjacent check process 
                    }

                    leftChecking = tileColumn[indexToLeft].Count > row;

                    if (!leftChecking) {
                        goto LeftcheckingSkip;
                    }

                    var tile = tileColumn[indexToLeft][row];

                    if (tile.GetTileType != tileType) {
                        leftChecking = false;
                        goto LeftcheckingSkip;
                    }
                    sameTile.Add(tile);
                }

                LeftcheckingSkip:

                if (rightChecking) {
                    rightChecking = ++indexToRight < maxColumn;
                    if (!rightChecking) {
                        goto RightcheckingSkip;
                    }

                    rightChecking = tileColumn[indexToRight].Count > row;
                    if (!rightChecking) {
                        goto RightcheckingSkip;
                    }

                    var tile = tileColumn[indexToRight][row];

                    if (tile.GetTileType != tileType) {
                        rightChecking = false;
                        goto RightcheckingSkip;
                    }
                    sameTile.Add(tile);
                }

                RightcheckingSkip:
                continue;
            }

            if (sameTile.Count > 1) {
                sameTile.Add(tileColumn[column][row]);

                for (int i = 0; i < sameTile.Count; i++) {
                    sameTile[i].DestroyThisTile?.Invoke();
                }
            }
        }
    }
}
