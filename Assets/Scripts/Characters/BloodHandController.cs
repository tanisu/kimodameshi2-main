using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodHandController : MonoBehaviour
{
    [SerializeField]
    GameObject blood;
    float vecY;
    float vecX;
    private void Start()
    {

        StartCoroutine(CreateBlood(3f));
 
        //foreach(GameObject blood in blood)
        //{
        //    Debug.Log(blood.name);
        //    vecY = Random.Range(-0.1f, 0.1f);
        //    vecX = Random.Range(-0.36f, 0.36f);
        //    GameObject b = Instantiate(blood,transform);
        //    b.transform.localScale = new Vector3(0.1f, 0.5f, 0f);
        //    b.transform.localPosition = new Vector3(vecX, vecY, 0f);
        //}
    }

    private IEnumerator CreateBlood(float s)
    {
        for (int i = 0; i < 5; i++)
        {
            vecY = Random.Range(-0.1f, 0.1f);
            vecX = Random.Range(-0.36f, 0.36f);

            yield return new WaitForSeconds(s);
            GameObject b = Instantiate(blood, transform);
            b.transform.localScale = new Vector3(0.1f, 0.5f, 0f);
            b.transform.localPosition = new Vector3(vecX, vecY, -1f);
        }
    }
}
