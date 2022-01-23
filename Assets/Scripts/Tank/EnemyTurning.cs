using UnityEngine;
using UnityEngine.UI;

public class EnemyTurning : MonoBehaviour {
    public Transform tankTransform;


    private void Update() {
        transform.LookAt(tankTransform);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
}