using UnityEngine;

public class SimpleRotator : MonoBehaviour
{
    public float RotationTime = 2.5f;

    // Use this for initialization
    void Start()
    {
        LeanTween.rotateAround(gameObject, Vector3.up, 360, RotationTime).setLoopClamp();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
