using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineEffect : MonoBehaviour
{
    public GameObject Target1;
    public GameObject Target2;

    private void Update()
    {
        Vector3 pos1 = Target1.transform.position;
        Vector3 pos2 = Target2.transform.position;
        
        pos1.y += 1.5f;
        pos2.y += 1.5f;

        // ?꾩튂瑜????寃잛쓽 以묎컙 吏?먯쑝濡??ㅼ젙
        transform.position = Vector3.Lerp(pos1, pos2, 0.5f);

        // ???寃??ъ씠??踰≫꽣瑜?怨꾩궛
        Vector3 direction = pos2 - pos1;

        // ?ㅻ툕?앺듃瑜??뚯쟾?섏뿬 ???寃잛쓣 ?ν븯?꾨줉 ?ㅼ젙
        transform.rotation = Quaternion.FromToRotation(Vector3.right, direction);

        // ???寃??ъ씠??嫄곕━濡??ㅼ??쇱쓣 ?ㅼ젙
        Vector3 scale = transform.localScale;
        scale.x = direction.magnitude;
        transform.localScale = scale;
    }
}
