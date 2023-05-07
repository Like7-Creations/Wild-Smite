using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFiller : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(FillLevel());
    }

    IEnumerator FillLevel()
    {
        yield return new WaitForSeconds(.4f);

        int rooms = transform.childCount;

        for (int i = 0; i < rooms; i++)
        {
            if (transform.GetChild(i).GetComponent<SectionAdjacents>() != null)
                transform.GetChild(i).GetComponent<SectionAdjacents>().controls.RunForwardChecks();
        }
        //yield return new WaitForSeconds(5);
        for (int i = 0; i < rooms; i++)
        {
            if (transform.GetChild(i).GetComponent<SectionAdjacents>() != null)
                transform.GetChild(i).GetComponent<SectionAdjacents>().controls.RunLeftChecks();
        }
        //yield return new WaitForSeconds(5);
        for (int i = 0; i < rooms; i++)
        {
            if (transform.GetChild(i).GetComponent<SectionAdjacents>() != null)
                transform.GetChild(i).GetComponent<SectionAdjacents>().controls.RunRightChecks();
        }
        //yield return new WaitForSeconds(5);
        for (int i = 0; i < rooms; i++)
        {
            if (transform.GetChild(i).GetComponent<SectionAdjacents>() != null)
                transform.GetChild(i).GetComponent<SectionAdjacents>().controls.CheckToHide();
            //yield return new WaitForSeconds(5);
        }
    }
}
