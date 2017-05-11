using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CHAR
{
    struct Tile
    {
        public const uint TILE_WIDTH = 80;
        public const uint TILE_HEIGHT = 80;
    }

    [System.Serializable]
    public struct Point
    {
        public uint posX;
        public uint posY;

        public const uint MIN_POS_X = 0;
        public const uint MIN_POS_Y = 0;
        public const uint MAX_POS_X = 7;
        public const uint MAX_POS_Y = 9;
    }

    public class Character : MonoBehaviour
    {
        public Point charPos;

        [Header("SPAWN POINT")]
        [SerializeField]
        Transform[] spawnPoints;

        [SerializeField]
        GameObject[] childs;

        void Start()
        {
            init();
        }

        void Update()
        {
            childs[0].transform.localPosition = Vector3.Lerp(childs[0].transform.localPosition, transform.localPosition, 10 * Time.deltaTime);
            childs[1].transform.localPosition = Vector3.Lerp(childs[1].transform.localPosition, transform.localPosition, 20 * Time.deltaTime);
            childs[2].transform.localPosition = Vector3.Lerp(childs[2].transform.localPosition, transform.localPosition, 30 * Time.deltaTime);
            childs[3].transform.localPosition = Vector3.Lerp(childs[2].transform.localPosition, transform.localPosition, 40 * Time.deltaTime);
        }

        /// <summary>
        /// 초기화 ( 캐릭터의 시작 포지션 위치가 이곳에서 제어됨 )
        /// </summary>
        public void init()
        {
            switch (Random.Range(0, 4))
            {
                case 0:
                    charPos.posX = Point.MIN_POS_X;
                    charPos.posY = Point.MIN_POS_Y;
                    transform.position = spawnPoints[0].position;
                    break;
                case 1:
                    charPos.posX = Point.MAX_POS_X;
                    charPos.posY = Point.MIN_POS_Y;
                    transform.position = spawnPoints[1].position;
                    break;
                case 2:
                    charPos.posX = Point.MIN_POS_X;
                    charPos.posY = Point.MAX_POS_Y;
                    transform.position = spawnPoints[2].position;
                    break;
                case 3:
                    charPos.posX = Point.MAX_POS_X;
                    charPos.posY = Point.MAX_POS_Y;
                    transform.position = spawnPoints[3].position;
                    break;
                default:
                    Debug.LogError("Random Range ERROR !");
                    break;
            }
        }


        /// <summary>
        /// Move to position
        /// </summary>
        #region _MOVE_TO_
        public void moveToUp()
        {
            if (charPos.posY < Point.MAX_POS_Y && !GM.GameManager.pause)
            {
                charPos.posY++;
                transform.localPosition += new Vector3(0, Tile.TILE_HEIGHT);
            }
        }
        public void moveToRight()
        {
            if (charPos.posX < Point.MAX_POS_X && !GM.GameManager.pause)
            {
                charPos.posX++;
                transform.localPosition += new Vector3(Tile.TILE_WIDTH, 0);
            }
        }
        public void moveToLeft()
        {
            if (charPos.posX > Point.MIN_POS_Y && !GM.GameManager.pause)
            {
                charPos.posX--;
                transform.localPosition -= new Vector3(Tile.TILE_WIDTH, 0);
            }
        }
        public void moveToDown()
        {
            if (charPos.posY > Point.MIN_POS_Y && !GM.GameManager.pause)
            {
                charPos.posY--;
                transform.localPosition -= new Vector3(0, Tile.TILE_HEIGHT);
            }
        }
        #endregion
    }
}