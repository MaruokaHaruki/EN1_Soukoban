using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {
    ///---------------------------------
    ///変数宣言
    ///---------------------------------
    //ゲームオブジェクトの宣言
    public GameObject playerPrefab;
    public GameObject boxPrefab;

    ///2次元配列の宣言
    int[,] map;

    GameObject[,] field;


    ///---------------------------------
    //2次元配列処理
    ///---------------------------------
    void Start() {

        //mapの初期化
        map = new int[,] {
            {1,0,0,0,0,},
            {0,0,2,0,0,},
            {0,0,2,0,0,},
            {0,0,2,0,0,},
            {0,0,0,0,0,},
            };

        //ゲーム管理用配列
        field = new GameObject[map.GetLength(0), map.GetLength(1)];

            
        //デバック用テキスト
        string debugText = "";

        //２次元配列の情報を出力
        for (int y = 0; y < map.GetLength(0); y++) {
            for (int x = 0; x < map.GetLength(1); x++) {
                debugText += map[y, x].ToString() + ",";
                if (map[y, x] == 1) {
                    field[y, x] = Instantiate(playerPrefab, new Vector3(x - map.GetLength(1) / 2 + 0.5f, -y + map.GetLength(0) / 2 - 0.5f, 0), Quaternion.identity);
                }
                if (map[y, x] == 2) {
                    field[y, x] = Instantiate(boxPrefab, new Vector3(x - map.GetLength(1) / 2 + 0.5f, -y + map.GetLength(0) / 2 - 0.5f, 0), Quaternion.identity);
                }

            }
            //改行
            debugText += "\n";
        }
        Debug.Log(debugText);
    }


    //---------------------------------
    //プレイヤーの配列取得
    //---------------------------------
    Vector2Int GetPlayerIndex() {
        for (int y = 0; y < map.GetLength(0); y++) {
            for (int x = 0; x < map.GetLength(1); x++) {
                if (field[y, x] != null && field[y, x].tag == "Player") {
                    return new Vector2Int(x, y);
                }
            }
        }
        return new Vector2Int(-1, -1);
    }


    //---------------------------------
    //プレイヤーの移動可能かどうかを確認
    //---------------------------------
    bool MovePlayer(Vector2Int moveFrom, Vector2Int moveTo) {
        //マップの範囲外かどうかをチェック
        if (moveTo.x < 0 || moveTo.x >= map.GetLength(1) || moveTo.y < 0 || moveTo.y >= map.GetLength(0)) { return false; }


        //配列街参照防止
        //Boxタグを持っていたら再起処理
        if (field[moveTo.y,moveTo.x] != null && field[moveTo.y, moveTo.x].tag == "Box") {
            Vector2Int velocity = moveTo - moveFrom;
            bool success = MovePlayer(moveTo, moveTo + velocity);
            if (!success) { return false; }
        }


        //ゲームオブジェクトの座標を変更
        field[moveFrom.y, moveFrom.x].transform.position = new Vector3(moveTo.x - map.GetLength(1) / 2 + 0.5f, -moveTo.y + map.GetLength(0) / 2 - 0.5f, 0);
        //移動処理
        field[moveTo.y, moveTo.x] = field[moveFrom.y, moveFrom.x];
        field[moveFrom.y, moveFrom.x] = null;
        return true;
    }


    ///---------------------------------
    //更新処理
    //---------------------------------
    // Update is called once per frame
    void Update() {

        //---------------------------------
        //右矢印キー入力
        //---------------------------------
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            Vector2Int playerIndex = GetPlayerIndex();
            if (playerIndex != new Vector2Int(-1, -1)) {
                MovePlayer(playerIndex, new Vector2Int(playerIndex.x + 1, playerIndex.y));
            }
        }
        //---------------------------------
        //左矢印キー入力
        //---------------------------------
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            Vector2Int playerIndex = GetPlayerIndex();
            if (playerIndex != new Vector2Int(-1, -1)) {
                MovePlayer(playerIndex, new Vector2Int(playerIndex.x - 1, playerIndex.y));
            }
        }

        //---------------------------------
        //上矢印キー入力
        //---------------------------------
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            Vector2Int playerIndex = GetPlayerIndex();
            if (playerIndex != new Vector2Int(-1, -1)) {
                MovePlayer(playerIndex, new Vector2Int(playerIndex.x, playerIndex.y - 1));
            }
        }

        //---------------------------------
        //下矢印キー入力
        //---------------------------------
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            Vector2Int playerIndex = GetPlayerIndex();
            if (playerIndex != new Vector2Int(-1, -1)) {
                MovePlayer(playerIndex, new Vector2Int(playerIndex.x, playerIndex.y + 1));
            }
        }



    }


    //end
}
