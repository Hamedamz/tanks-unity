using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour {
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
        if (playerNumber == 1)
        {
            aimSlider.value = minLaunchForce;

            if (currentLaunchForce >= maxLaunchForce && !fired)
            {
                currentLaunchForce = maxLaunchForce;
                Fire();
            }
            else if (Input.GetButtonDown(fireButton))
            {
                fired = false;
                currentLaunchForce = minLaunchForce;

                shootingAudio.clip = chargingClip;
                shootingAudio.Play();
            }
            else if (Input.GetButton(fireButton) && !fired)
            {
                currentLaunchForce += chargeSpeed * Time.deltaTime;

                aimSlider.value = currentLaunchForce;
            }
            else if (Input.GetButtonUp(fireButton) && !fired)
            {
                Fire();
            }
        }
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