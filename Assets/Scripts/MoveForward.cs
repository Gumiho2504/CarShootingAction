using NUnit.Framework;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [SerializeField] public float speed = 40.0f;
    [SerializeField] private float topBound;
    [SerializeField] private float bottomBound;

    public GameObject effect;
    public GameObject diamond;
    public bool isDie;
    public bool isFollowPlayer = false;
    [SerializeField] private Transform playerTransform;
    private void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
    }
    void Update()
    {

        if (isDie) return;
        if (playerTransform == null) return;
        if (playerTransform.GetComponent<PlayerController>().isDie) return;
       
        if (isFollowPlayer)
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
            if (Vector3.Distance(transform.position, playerTransform.position) < 0.4f)
            {
                Destroy(gameObject);
            }
        }
        else
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
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerEnter(Collider other)
    {
        print("OnTriggerEnter");
        if (other.CompareTag("Target"))
        {

            Destroy(other.gameObject);
            if (isDie) return;
            transform.position = new Vector3(transform.position.x - Random.Range(-0.2f, 0.2f), transform.position.y, transform.position.z - Random.Range(-0.5f, 1f));
            speed -= 5;
            if (speed < 2)
                isDie = true;
            if (isDie)
            {
                transform.position = new Vector3(transform.position.x, GetComponent<BoxCollider>().bounds.size.x / 2, transform.position.z + 1f);
                gameObject.transform.Rotate(new Vector3(0, 0, Random.Range(-159, -270)));
                Destroy(gameObject, 0.3f);

                Instantiate(diamond, transform.position, diamond.transform.rotation);
            }

        }
    }


    private void OnDestroy()
    {
        if (!isDie) return;
        var effectInstance = Instantiate(effect, transform.position, effect.transform.rotation);

        Destroy(effectInstance, 0.1f);

    }


}
