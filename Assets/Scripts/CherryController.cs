using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    private GameObject bonusCherry = default;
    private Tweener tweener;
    private float duration = 3f;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine("DoSomething");
        bonusCherry = GameObject.FindWithTag("cherry");
        CancelInvoke();
        InvokeRepeating("CheeryInstant", 5, 5);
        tweener = GetComponent<Tweener>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //IEnumerator DoSomething()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(5);
    //    }
    //}
    void CheeryInstant()
    {
        Instantiate(bonusCherry, new Vector2(Camera.main.transform.position.x + Random.Range(-Camera.main.orthographicSize, Camera.main.orthographicSize), -14), Quaternion.identity);
        tweener.AddTween(bonusCherry.transform, bonusCherry.transform.position, new Vector2(-0.5f,14), duration);
    }
}
