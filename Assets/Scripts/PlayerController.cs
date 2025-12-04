using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject foodPrefab;
    [SerializeField] private Transform bulletPos;
    private InputAction move;
    private InputAction attack;
    private float speed = 10f;
    [SerializeField] private float xRange = 5f;
    AudioSource audioSource;
    [SerializeField] private AudioClip shootSound;
    int life = 5;

    public bool isDie = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        move = InputSystem.actions.FindAction("Move");
        attack = InputSystem.actions.FindAction("Attack");
        attack.performed += Attack;
    }

    private void OnDisable() => attack.performed -= Attack;

    private void Attack(InputAction.CallbackContext context)
    {
        if (isDie) return;
        audioSource.PlayOneShot(shootSound);
        Instantiate(foodPrefab, bulletPos.position, Quaternion.identity);
    }



    private Vector2 moveValue => move.ReadValue<Vector2>();


    // Update is called once per frame
    void Update()
    {
        if (transform.position.x >= xRange) transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        if (transform.position.x <= -xRange) transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);

        transform.Translate(new Vector3(moveValue.x * speed * Time.deltaTime, 0, 0));

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "animal")
        {
            other.GetComponent<MoveForward>().isDie = true;
            Destroy(other.gameObject, 0.1f);
            life--;
            FindFirstObjectByType<GameController>().OnLifeChange(life);
            if (life <= 0)
            {
                isDie = true;
                FindFirstObjectByType<GameController>().OnGameOver();
                Destroy(gameObject, 2f);
            }
        }
    }
}
