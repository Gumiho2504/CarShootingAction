using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 40.0f;
    [SerializeField] private float topBound;
    [SerializeField] private float bottomBound;
    [SerializeField] private GameObject effect;

    void Update()
    {

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (transform.position.z > topBound)
        {
            Destroy(gameObject);
        }
        else if (transform.position.z < bottomBound)
        {
            Destroy(gameObject);
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("animal"))
        {
            var effectInstance = Instantiate(effect, transform.position, effect.transform.rotation);
            Destroy(effectInstance, 0.1f);
        }



    }



}
