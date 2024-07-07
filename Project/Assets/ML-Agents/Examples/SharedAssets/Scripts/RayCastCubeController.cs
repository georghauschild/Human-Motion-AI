using UnityEngine;

public class RayCastCubeController : MonoBehaviour
{
    private Transform parentTransform;

    void Start()
    {
        // Den Parent des Cubes speichern
        parentTransform = transform.parent;
    }

    void LateUpdate()
    {
        if (parentTransform != null)
        {
            // Position des Cubes auf die Position des Parents setzen
           // transform.position = parentTransform.position;

            // Nur die Y-Achsen-Drehung des Parents übernehmen
            Vector3 newRotation = new Vector3(0, parentTransform.eulerAngles.y, 0);
            transform.rotation = Quaternion.Euler(newRotation);
        }
    }
}
