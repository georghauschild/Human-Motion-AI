using UnityEngine;

public class FootGroundCheck : MonoBehaviour
{
    private bool isTouchingGround = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ground"))
        {
            isTouchingGround = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ground"))
        {
            isTouchingGround = false;
        }
    }

    public bool IsTouchingGround()
    {
        return isTouchingGround;
    }
}
