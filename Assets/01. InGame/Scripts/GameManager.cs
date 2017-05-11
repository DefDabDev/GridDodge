using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AL.ALUtil;

namespace GM
{
    public class GameManager : ALComponentSingleton<GameManager>
    {
        [Header("SCORE")]
        [SerializeField]
        UnityEngine.UI.Text scoreText;
        [SerializeField]
        double score = 0;

        [Header("GAME CONTROL")]
        public static bool pause = true;        // 게임 일시정지 여부 ( true : 일시정지, false : 진행중 )
        public static bool start = false;       // 게임 시작 여부 ( false : 미시작, true : 시작 )

        [Header("InGame SCORE UI")]
        [SerializeField]
        UnityEngine.UI.Text bestScoreText;

        [Header("End SCORE UI")]
        [SerializeField]
        UnityEngine.UI.Text e_nowScoreText;
        [SerializeField]
        UnityEngine.UI.Text e_bestScoreText;
        [SerializeField]
        GameObject resultCanvas;

        [Header("Character")]
        [SerializeField]
        CHAR.Character character;

        void Awake()
        {
            if (PlayerPrefs.HasKey("BESTSCORE"))
                PlayerPrefs.SetInt("BESTSCORE", 0);
            bestScoreText.text = "" + PlayerPrefs.GetInt("BESTSCORE");

            MapGenerator.instance.init();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();

            if (start && !pause)
            {
                score += Time.deltaTime;

                scoreText.text = (uint)score + "";
                //if (score > 5)
                //    gameEnd();
            }
        }

        #region _BT_CONTROL_
        /// <summary>
        /// 멈춤 버튼 눌렀을때 호출됨 ( 멈춰있을때 누를시 -> 이어하기 | 안멈춰있을때 -> 멈춤 )
        /// </summary>
        public void pauseBT()
        {
            pause = (pause ? false : true);
            if (pause)
                scoreText.transform.SetAsLastSibling();
            else
                scoreText.transform.SetAsFirstSibling();
        }

        /// <summary>
        /// 시작 버튼 눌렀을때 호출됨
        /// </summary>
        public void startBT()
        {
            start = (start ? false : true);
        }

        [SerializeField]
        Animator pauseAnimator;

        /// <summary>
        /// 재시작 버튼 눌렀을때 호출됨
        /// </summary>
        public void reStartBT()
        {
            if (pause)
            {
                pauseBT();
                pauseAnimator.SetTrigger("Fade");
            }
            gameStart();
        }
        #endregion

        #region _GAME_CONTROL_
        /// <summary>
        /// 게임 시작시에 호출됨
        /// </summary>
        public void gameStart()
        {
            character.init();

            score = 0;
            scoreText.text = (uint)score + "";

            pause = false;
            start = true;
        }

        /// <summary>
        /// 게임 종료시에 호출됨
        /// </summary>
        public void gameEnd()
        {
            if (start)
            {
                start = false;
                character.dead();

                resultCanvas.SetActive(true);
                resultCanvas.GetComponent<Animator>().SetTrigger("Fade");

                e_nowScoreText.text = "" + (uint)score;
                if (PlayerPrefs.GetInt("BESTSCORE") < (uint)score)
                    PlayerPrefs.SetInt("BESTSCORE", (int)score);
                e_bestScoreText.text = "" + PlayerPrefs.GetInt("BESTSCORE");
            }
        }

        /// <summary>
        /// 캐릭터 위치와 폭탄 위치가 같은지 체크
        /// </summary>
        /// <param name="p">폭탄 위치</param>
        public void bombCheck(CHAR.Point p)
        {
            if (character.charPos.posX.Equals(p.posX) && character.charPos.posY.Equals(p.posY) && !pause)
                gameEnd();
        }
        
        #endregion
    }
}