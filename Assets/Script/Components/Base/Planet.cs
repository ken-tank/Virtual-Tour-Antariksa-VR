using UnityEngine;

public class Planet : MonoBehaviour
{
    [System.Serializable]
    class Attribute {
        public Transform objectPlanet;
    }

    [System.Serializable]
    public struct Properties {
        public string name;
        [Tooltip("Radius Per Kilometer")]
        public float radius;
        [Tooltip("Rotate In Radiant per Second")]
        public float rotationSpeed;
        public Planet orbit;
        public float orbitDistance;
        public float orbitScale;
        public float orbitalSpeed;

        public float currentPlanetAngle;
        public float currentOrbitAngle;
    }

    [SerializeField] Attribute attribute;
    public Properties properties;

    Transform orbitPoint = null;
    Space space;

    void Start()
    {
        space = Space.instance;
    }

    void OnDrawGizmosSelected()
    {
        if (attribute.objectPlanet)
        {
            if (!space) space = FindObjectOfType<Space>();
            attribute.objectPlanet.localScale = Vector3.one * (properties.radius / space.spaceScale);
            attribute.objectPlanet.localEulerAngles = Vector3.up * properties.currentPlanetAngle;

            if (properties.orbit)
            {
                if (orbitPoint == null) 
                {
                    if(transform.parent == null) 
                    {
                        orbitPoint = new GameObject("Orbit Point").transform;
                        orbitPoint.localEulerAngles = properties.orbit.transform.localEulerAngles;
                    }
                    else
                    {
                        orbitPoint = transform.parent;
                    }
                    orbitPoint.position = properties.orbit.transform.position;
                    transform.parent = orbitPoint;
                    orbitPoint.parent = properties.orbit.transform;
                    transform.localPosition = Vector3.zero;
                }
                else
                {
                    transform.localPosition = Vector3.forward * ((properties.orbitDistance / properties.orbitScale) / space.spaceScale);
                    orbitPoint.localEulerAngles = new Vector3(orbitPoint.localEulerAngles.x, properties.currentOrbitAngle, orbitPoint.localEulerAngles.z);
                    Gizmos.color = new Color(1, 0, 0, 0.5f);
                    Gizmos.DrawLine(orbitPoint.position, transform.position);
                }
            }
            else
            {
                if (orbitPoint != null)
                {
                    orbitPoint.GetChild(0).parent = null;
                    DestroyImmediate(orbitPoint.gameObject);
                    orbitPoint = null;
                }
            }
        }
    }

    void Update()
    {
        if (attribute.objectPlanet)
        {
            properties.currentPlanetAngle = attribute.objectPlanet.localEulerAngles.y;
            properties.currentPlanetAngle -= properties.rotationSpeed * (Time.deltaTime * space.spaceTime);
            attribute.objectPlanet.localEulerAngles = Vector3.up * properties.currentPlanetAngle;
            attribute.objectPlanet.localScale = Vector3.one * (properties.radius / space.spaceScale);

            if (properties.orbit)
            {
                if (orbitPoint == null) 
                {
                    orbitPoint = new GameObject($"Orbit Point {properties.name}").transform;
                    orbitPoint.position = properties.orbit.transform.position;
                    orbitPoint.localEulerAngles = new Vector3(orbitPoint.localEulerAngles.x, properties.currentOrbitAngle, orbitPoint.localEulerAngles.z);
                    transform.parent = orbitPoint;
                    orbitPoint.parent = properties.orbit.transform;
                    transform.localPosition = Vector3.zero;
                }
                else
                {
                    transform.localPosition = Vector3.forward * ((properties.orbitDistance / properties.orbitScale) / space.spaceScale);
                    properties.currentOrbitAngle = orbitPoint.localEulerAngles.y;
                    properties.currentOrbitAngle -= properties.orbitalSpeed * (Time.deltaTime * space.spaceTime);
                    orbitPoint.localEulerAngles = new Vector3(orbitPoint.localEulerAngles.x, properties.currentOrbitAngle, orbitPoint.localEulerAngles.z);
                }
            }
            else
            {
                if (orbitPoint != null)
                {
                    orbitPoint.GetChild(0).parent = null;
                    Destroy(orbitPoint.gameObject);
                    orbitPoint = null;
                }
            }
        }
    }
}
