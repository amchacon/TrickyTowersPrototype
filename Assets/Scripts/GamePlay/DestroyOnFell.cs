using UnityEngine;

//This component must be attached to an object with some Renderer
public class DestroyOnFell : MonoBehaviour
{
    public bool milestoneReached;
    private void OnBecameInvisible()
    {
        if(!milestoneReached)
            Destroy(transform.root.gameObject);
    }
}