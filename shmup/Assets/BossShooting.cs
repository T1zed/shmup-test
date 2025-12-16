using UnityEngine;
using System.Collections;

public class BossShooting : MonoBehaviour
{
    public GameObject lazerPrefab;      
    public GameObject lazerAlertPrefab;  
    public Transform[] firePoints;       
    public float alertTime = 1f;         
    public float lazerDuration = 2f;    
    public float shootInterval = 3f;     

    void Start()
    {
        StartCoroutine(ShootingRoutine());
    }

    IEnumerator ShootingRoutine()
    {
        while (true)
        {
            foreach (Transform firePoint in firePoints)
            {
                GameObject alert = Instantiate(lazerAlertPrefab,firePoint.position,firePoint.rotation);
                alert.transform.right = firePoint.right;
                yield return new WaitForSeconds(alertTime);

                GameObject lazer = Instantiate(lazerPrefab,firePoint.position, firePoint.rotation);
                lazer.transform.right = firePoint.right;

                Lazer lb = lazer.GetComponent<Lazer>();
                lb.SetFirePoint(firePoint);

                Destroy(lazer, lazerDuration);
                Destroy(alert);
            }

            yield return new WaitForSeconds(shootInterval);
        }
    }


}
