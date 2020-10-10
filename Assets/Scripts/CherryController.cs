using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    public GameObject cherry;
    private Tweener tweener;
    private float duration = 10f;
    public GameObject bonusCherry;
    // Start is called before the first frame update
    float CamX;
    float CamY;
    int line;
    void Start()
    {
        //StartCoroutine("DoSomething");
        CancelInvoke();
        InvokeRepeating("CheeryInstant", 30, 30);
        tweener = GetComponent<Tweener>();
        CamX = Camera.main.orthographicSize * Camera.main.aspect + 1;
        CamY = Camera.main.orthographicSize + 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (bonusCherry)
        {
            if (line == 0 && bonusCherry.transform.position.y == -CamY)
            {
                Destroy(bonusCherry);
            }
            if(line==1&& bonusCherry.transform.position.y == CamY)
            {
                Destroy(bonusCherry);
            }
            if (line == 2 && bonusCherry.transform.position.x == -0.5-CamX)
            {
                Destroy(bonusCherry);
            }
            if (line == 3 && bonusCherry.transform.position.x == -0.5+CamX)
            {
                Destroy(bonusCherry);
            }
        }
        

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
        line = Random.Range(0, 4);
        Vector2 cheeryPosition = default;
        if (line == 0)
        {
            cheeryPosition = new Vector2(Random.Range(-CamX, CamX), CamY);
        }
        if (line == 1)
        {
            cheeryPosition = new Vector2(Random.Range(-CamX, CamX), -CamY);
        }
        if (line == 2)
        {
            cheeryPosition = new Vector2(CamX, Random.Range(-CamY, CamY));
        }
        if (line == 3)
        {
            cheeryPosition = new Vector2(-CamX, Random.Range(-CamY, CamY));
        }
        bonusCherry = Instantiate(cherry, cheeryPosition, Quaternion.identity);
        bonusCherry.AddComponent<BoxCollider2D>();
        bonusCherry.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
        bonusCherry.GetComponent<BoxCollider2D>().size = new Vector2(1.21f, 1.21f);
        bonusCherry.GetComponent<BoxCollider2D>().isTrigger = true;
        //bonusCherry.transform.position = new Vector2(-20, 14);
        //tweener.AddTween(bonusCherry.transform, bonusCherry.transform.position, new Vector2(20,14), duration);
        tweener.AddTween(bonusCherry.transform, bonusCherry.transform.position, new Vector2(-0.5f-cheeryPosition.x,-cheeryPosition.y), duration);
    }

}
