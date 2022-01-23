using UnityEngine;
using UnityEngine.UI;

public class EnemyShooting : MonoBehaviour {
    public int playerNumber = 1;
    public Rigidbody shell;
    public Transform fireTransform;
    public Slider aimSlider;
    public AudioSource shootingAudio;
    public AudioClip chargingClip;
    public AudioClip fireClip;
    public float minLaunchForce = 15f;
    public float maxLaunchForce = 30f;
    public float maxChargeTime = 0.75f;
    public Transform enemyTransform;


    private string fireButton;         
    private float currentLaunchForce;  
    private float chargeSpeed;         
    private bool fired;
    public float fireRate = 3.0f;


    private void OnEnable() {
        currentLaunchForce = minLaunchForce;
        aimSlider.value = minLaunchForce;
    }


    private void Start() {
        fireButton = "Fire" + playerNumber;

        chargeSpeed = (maxLaunchForce - minLaunchForce) / maxChargeTime;

        InvokeRepeating("EnemyFire", 0, fireRate);
    }


    private void Update() {
        Vector3 enemyToTarget = enemyTransform.position - transform.position;

        float distance = enemyToTarget.magnitude;
        currentLaunchForce = Mathf.Min(maxLaunchForce, distance);
    }

    private void EnemyFire() {
        if (playerNumber != 1) {
            if (gameObject.active) {
                aimSlider.value = minLaunchForce;
                Fire();
            } else {
                CancelInvoke("EnemyFire");
            }
        }
    }


    private void Fire() {
        fired = true;

        Rigidbody shellInstance = Instantiate(shell, fireTransform.position, fireTransform.rotation) as Rigidbody;

        shellInstance.velocity = currentLaunchForce * fireTransform.forward;

        shootingAudio.clip = fireClip;
        shootingAudio.Play();

        currentLaunchForce = minLaunchForce;
    }
}