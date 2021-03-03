using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{

    [SerializeField] float backgroundScrollSpeed = 0.2f;
    Material myMatarial;
    Vector2 offset;

    // Start is called before the first frame update
    void Start()
    {
        myMatarial = GetComponent<Renderer>().material;
        offset = new Vector2(0f, backgroundScrollSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        myMatarial.mainTextureOffset += offset * Time.deltaTime;
    }
}
