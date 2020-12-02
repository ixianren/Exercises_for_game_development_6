using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMonster0 : MonoBehaviour
{
    public Transform kPlayer;
    public Camera mycamera;
    public ParticleSystem particle;

    public float fDistance = 5f;

    private Animator animator;
    private CameraShake shake;

    private bool hitwall = false;
    private float delay = 1f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        shake = mycamera.GetComponent<CameraShake>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }
        //float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");
        //float distance = Mathf.Sqrt(h * h + v * v);
        //float speed = distance / Time.deltaTime;
        //animator.SetFloat("MoveSpeed", speed);

        bool fire = Input.GetButtonDown("Fire1");
        if (fire)
        {
            animator.SetTrigger("TriggerAttack");
            if (hitwall)
            {
                StartCoroutine(Hit(delay));
            }
        }
    }

    private void FixedUpdate()
    {
        Vector3 kNewPos = Camera.main.transform.position + fDistance * Camera.main.transform.forward;
        kNewPos.y = 0;

        //kPlayer.position = kNewPos;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Wall"))
            hitwall = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Wall"))
            hitwall = false; 
    }
    IEnumerator Hit(float delay)
    {
        yield return new WaitForSeconds(delay);
        particle.Play();
        StartCoroutine(shake.Shake());
    }
}
