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

        // ?„ì¹˜ë¥????€ê²Ÿì˜ ì¤‘ê°„ ì§€?ìœ¼ë¡??¤ì •
        transform.position = Vector3.Lerp(pos1, pos2, 0.5f);

        // ???€ê²??¬ì´??ë²¡í„°ë¥?ê³„ì‚°
        Vector3 direction = pos2 - pos1;

        // ?¤ë¸Œ?íŠ¸ë¥??Œì „?˜ì—¬ ???€ê²Ÿì„ ?¥í•˜?„ë¡ ?¤ì •
        transform.rotation = Quaternion.FromToRotation(Vector3.right, direction);

        // ???€ê²??¬ì´??ê±°ë¦¬ë¡??¤ì??¼ì„ ?¤ì •
        Vector3 scale = transform.localScale;
        scale.x = direction.magnitude;
        transform.localScale = scale;
    }
}
