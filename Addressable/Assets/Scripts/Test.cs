using Unity.VisualScripting;
using UnityEngine;

public class Test : MonoBehaviour
{

    public float _time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = _time;
    }
}
