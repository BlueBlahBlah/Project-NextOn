using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    [SerializeField]
    private Image imageFadeIn;
    [SerializeField]
    private float startTime;
    [SerializeField]
    private float destroyTime;
    [SerializeField]
    private float lerpSpeed;
    [SerializeField]
    private bool getTriggered;
    private float alpha = 255f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("FadeInTrigger", startTime);
        Destroy(this.gameObject, startTime + destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (getTriggered)
        {
            alpha = Mathf.Lerp(alpha, 0f, Time.deltaTime * lerpSpeed);
            imageFadeIn.color = new Color(imageFadeIn.color.r, imageFadeIn.color.g, imageFadeIn.color.b, alpha / 255f);
        }
    }

    private void FadeInTrigger() { getTriggered = !getTriggered; }
}
