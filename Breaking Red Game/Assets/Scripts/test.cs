using UnityEngine;
using UnityEngine.Rendering;

public class test : MonoBehaviour
{
        [SerializeField] float SeedHeight;
        [SerializeField] float FinalHeight;
        [SerializeField] float Speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Scales();
    }
    void Scales()
    {
        float newScale = Mathf.Lerp(SeedHeight, FinalHeight, Time.time * Speed);
        transform.localScale = new Vector3(newScale, newScale, newScale);
    }
}
