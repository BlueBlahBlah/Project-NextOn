using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    [SerializeField]
    private Image imageFadeIn;
    private bool isDone;
    [SerializeField]
    private float alpha = 255f;

    // Start is called before the first frame update
    void Start()
    {
        if (isDone) { isDone = false; }
    }

    // Update is called once per frame
    void Update()
    {
        alpha = Mathf.Lerp(alpha, 0f, Time.deltaTime * 1.5f);
        imageFadeIn.color = new Color(imageFadeIn.color.r, imageFadeIn.color.g, imageFadeIn.color.b, alpha / 255f);

        if (alpha <= 0f)
        {
            Destroy(this.gameObject);
        }
    }
}
