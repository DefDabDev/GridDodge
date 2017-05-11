using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextRandom : MonoBehaviour
{
    [SerializeField]
    UnityEngine.UI.Text creditText;

    void Awake()
    {
        int[] arr = new int[6];

        for (int i = 0; i < 6; i++)
            arr[i] = i;

        for (int i = 0; i< 6; i++)
        {
            int n = Random.Range(0, 6 - i);
            switch (arr[n])
            {
                case 0:
                    creditText.text += "그래픽 : 박준철\n";
                    break;
                case 1:
                    creditText.text += "프로그래밍 : 김덕원\n";
                    break;
                case 2:
                    creditText.text += "기획 : 양도훈\n";
                    break;
                case 3:
                    creditText.text += "기획 : 강의택\n";
                    break;
                case 4:
                    creditText.text += "프로그래밍 : 강예찬\n";
                    break;
                case 5:
                    creditText.text += "사운드 : 김혁수\n";
                    break;
            }
            int temp = arr[n];
            arr[n] = arr[5-i];
            arr[5-i] = temp;
        }

        creditText.text += "크레딧의 순서는 랜덤입니다.";
    }
}
