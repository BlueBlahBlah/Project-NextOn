using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace SVC
{
    public class GridVisualizer : MonoBehaviour
    {
        public GameObject groundPrefab;

        public void VisualizeGrid(int width, int length)
        {
            Vector3 posision = new Vector3(width / 2f, 0, length / 2f);
            Quaternion rotation = Quaternion.Euler(0,0,0);
            var board = Instantiate(groundPrefab, posision, rotation);
            board.transform.localScale = new Vector3(width, 1, length);
        }
    }
}

