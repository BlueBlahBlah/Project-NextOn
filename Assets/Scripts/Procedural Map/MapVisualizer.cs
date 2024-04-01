/*
    Made by Sunny Valle Studio
    (https://svstudio.itch.io)
*/
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SVC.ChessMaze
{
    public class MapVisualizer : MonoBehaviour
    {
        private Transform parent;
        public Color startColor, exitColor;

        private Dictionary<Vector3, GameObject> dictionaryOfObstacles = new Dictionary<Vector3, GameObject>();
        public GameObject roadStraight, roadTileCorner, tileEmpty, startTile, exitTile;
        public GameObject[] environmentTiles;

        private void Awake()
        {
            parent = this.transform;
        }

        public void VisualizeMap(MapGrid grid, MapData data, bool visualizeUsingPrefabs)
        {
            if (visualizeUsingPrefabs)
            {
                VisualizeUsingPrefabs(grid, data);
            }
            else
            {
                VisualizeUsingPrimitives(grid, data);
            }
        }

        private void VisualizeUsingPrefabs(MapGrid grid, MapData data)
        {
            for (int i = 0; i < data.path.Count; i++)
            {
                var position = data.path[i];
                if (position != data.exitPosition)
                {
                    grid.SetCell(position.x,position.z,CellObjectType.Road);
                }
            }

            for (int col = 0; col < grid.Width; col++)
            {
                for (int row = 0; row < grid.Width; row++)
                {
                    var cell = grid.GetCell(col, row);
                    var position = new Vector3(cell.X, 0, cell.Z);

                    var index = grid.CalculateIndexFromCoordinates(position.x, position.z);
                    if (data.obstacleArray[index] && cell.IsTaken == false)
                    {
                        cell.ObjectType = CellObjectType.Obstacle;
                    }

                    switch (cell.ObjectType)
                    {
                        case CellObjectType.Empty:
                            CreateIndicator(position,tileEmpty);
                            break;
                        case CellObjectType.Road:
                            CreateIndicator(position,roadStraight);
                            break;
                        case CellObjectType.Obstacle:
                            int randomIndex = Random.Range(0, environmentTiles.Length);
                            CreateIndicator(position, environmentTiles[randomIndex]);
                            break;
                        case CellObjectType.Start:
                            CreateIndicator(position,startTile);
                            break;
                        case CellObjectType.Exit:
                            CreateIndicator(position,exitTile);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void CreateIndicator(Vector3 position, GameObject prefab, Quaternion rotation = new Quaternion())
        {
            var placementPosition = position + new Vector3(.5f, .5f, .5f);
            var element = Instantiate(prefab, placementPosition, rotation);
            element.transform.parent = parent;
            dictionaryOfObstacles.Add(position,element);
        }


        private void VisualizeUsingPrimitives(MapGrid grid, MapData data)
        {
            PlaceStartAndExitPoints(data);
            for (int i = 0; i < data.obstacleArray.Length; i++)
            {
                if (data.obstacleArray[i])
                {
                    var positionOnGrid = grid.CalculateCoordinatesFromIndex(i);
                    if(positionOnGrid == data.startPosition || positionOnGrid == data.exitPosition)
                    {
                        continue;
                    }
                    grid.SetCell(positionOnGrid.x, positionOnGrid.z, CellObjectType.Obstacle);
                    if (PlaceKnightObstacle(data, positionOnGrid))
                    {
                        continue;
                    }

                    if (dictionaryOfObstacles.ContainsKey(positionOnGrid) == false)
                    {
                        CreateIndicator(positionOnGrid,Color.white,PrimitiveType.Cube);
                    }
                    
                }
            }
        }

        private bool PlaceKnightObstacle(MapData data, Vector3 positionOnGrid)
        {
            foreach (var knight in data.knightPiecesList)
            {
                if (knight.Position == positionOnGrid)
                {
                    CreateIndicator(positionOnGrid, Color.red, PrimitiveType.Cube);
                    return true;
                }
            }
            return false;
        }

        private void PlaceStartAndExitPoints(MapData data)
        {
            CreateIndicator(data.startPosition, startColor, PrimitiveType.Sphere);
            CreateIndicator(data.exitPosition, exitColor, PrimitiveType.Sphere);
        }

        private void CreateIndicator(Vector3 position, Color color, PrimitiveType sphere)
        {
            var element = GameObject.CreatePrimitive(sphere);
            dictionaryOfObstacles.Add(position,element);
            element.transform.position = position + new Vector3(.5f,.5f,.5f);
            element.transform.parent = parent;
            var renderer = element.GetComponent<Renderer>();
            //renderer.material.SetColor("_Color", color);
            renderer.material.color = color;
        }

        public void ClearMap()
        {
            foreach (var obstacle in dictionaryOfObstacles.Values)
            {
                Destroy(obstacle);
            }
            dictionaryOfObstacles.Clear();
        }
    }
}