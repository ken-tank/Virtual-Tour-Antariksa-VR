using UnityEngine;

public class FollowOrbit : MonoBehaviour
{
    [SerializeField] Planet planet;

    void LateUpdate()
    {
        if (planet.properties.orbit && transform.parent != planet.transform.parent)
        {
            transform.parent = planet.transform.parent;
        }
    }
}
