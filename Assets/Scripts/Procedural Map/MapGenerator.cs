using System.Collections;
using System.Collections.Generic;
using SVC.ChessMaze;
using UnityEngine;


namespace SVC.ChessMaze
{
    public class MapGenerator : MonoBehaviour
    {
        public GridVisualizer gridVisualizer;
        public MapVisualizer mapVisualizer;
        public Direction startPositionEdge, exitPositionEdge;
        public bool randomPlacement;
        public Vector3 startPosition, exitPosition;
        [Range(1,10)]
        public int numberOfPieces;

        public bool visualizeUsingPrefabs = false;
        public bool autoRepair = true;
        private CandidateMap map;
        
        [Range(3, 20)] public int width, length = 11;
        private MapGrid grid;
        private void Start()
        {
            
            gridVisualizer.VisualizeGrid(width,length);
            GenerateNewMap();

        }

        public void GenerateNewMap()
        {
            mapVisualizer.ClearMap();            
            
            grid = new MapGrid(width,length);
            MapHelper.RandomlyChooseAndSetStartAndExit(grid,ref startPosition, ref exitPosition,randomPlacement,startPositionEdge, exitPositionEdge);
            map = new CandidateMap(grid, numberOfPieces);
            map.CreateMap(startPosition,exitPosition,autoRepair);
            mapVisualizer.VisualizeMap(grid,map.ReturnMapData(),visualizeUsingPrefabs);
        }

        public void TryRepair()
        {
            if (map != null)
            {
                var listOfObstaclesToRemove = map.Repair();
                if (listOfObstaclesToRemove.Count > 0)
                {
                    mapVisualizer.ClearMap();
                    mapVisualizer.VisualizeMap(grid,map.ReturnMapData(),visualizeUsingPrefabs);
                }
            }
        }
    }
}

