using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShurikenCount : MonoBehaviour
{
    public Text count;

    public void SetCount(int shurikenCount)
    {
        count.text = shurikenCount.ToString();
    }
}
