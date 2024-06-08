using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public Transform aim;
    public GameObject arrowPrefab; // Renamed to arrowPrefab for clarity
    public Transform player; // Tambahkan referensi ke karakter pemain
    public float delay = 1.5f;
    public int damage; // Define damage here
    float timer;

    void Start()
    {
        timer = delay;
    }

    void Update()
    {
        if (!PauseMenu.isPaused)
        {
            timer += Time.deltaTime;
            if (timer > delay)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    Shoot();
                    timer = 0;
                }
            }
        }

        /*       if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }*/
    }

    void Shoot()
    {
        // Tentukan arah panah berdasarkan rotasi karakter
        Vector3 direction = (aim.position - player.position).normalized;
        Quaternion arrowRotation = Quaternion.LookRotation(Vector3.forward, direction);
        GameObject newArrow = Instantiate(arrowPrefab, aim.position, arrowRotation);

        // Set the damage value for the arrow
        newArrow.GetComponent<arrow>().SetDamage(damage);
        AudioManager.Instance.PlaySFX("attack");
    }
}
