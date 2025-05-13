using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]

public class BGScroll : MonoBehaviour
{

    public float speed;
    public Vector2 direction;

    Image img;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        img.material.mainTextureOffset += -direction.normalized * Time.deltaTime * speed;
    }
}
