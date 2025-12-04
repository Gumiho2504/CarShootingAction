using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Diemond : MonoBehaviour
{
    private Transform playerTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
        StartCoroutine(MoveToPlayer());
    }

    float localScale = 1f;
    float maxScale = 4f;
    IEnumerator MoveToPlayer()
    {
        yield return new WaitForSeconds(0.5f);

        while (localScale < maxScale)
        {
            localScale += 0.1f;
            transform.localScale = Vector3.one * localScale;
            yield return new WaitForSeconds(0.01f);
        }
        while (Vector3.Distance(transform.position, playerTransform.position) > 0f)
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.Translate(direction * 50 * Time.deltaTime, Space.World);
            yield return null;
        }


        StopAllCoroutines();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            StopAllCoroutines();
            FindFirstObjectByType<GameController>().IncreaseScore(10);
            Destroy(gameObject);
        }
    }
}
