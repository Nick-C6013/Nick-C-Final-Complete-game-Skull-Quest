using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Skull_Shield : MonoBehaviour
{
    public float lifetime = 10f;

    
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

   
    void Update()
    {
        
    }

    

}

