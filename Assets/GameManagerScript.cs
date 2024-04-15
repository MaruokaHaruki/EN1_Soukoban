using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {
    ///配列の宣言
    int[] map;

    ///メソッドの宣言
    //配列の出力
    void PrintArray() {
        //一行にまとめる
        string debugText = "";

        //要素数を1つずつ出力
        for (int i = 0; i < map.Length; i++) {
            debugText += map[i].ToString() + ",";
        }

        //要素数出力
        Debug.Log(debugText);
    }

    //プレイヤーの配列取得
    int GetPlayerIndex() {
        for (int i = 0; i < map.Length; i++) {
            if (map[i] == 1) {
                return i;
            }
        }
        return -1;
    }

    //プレイヤーの移動不可
    bool MoveNumber(int number, int moveFrom, int moveTo) {
        if (moveTo < 0 || moveTo >= map.Length) {
            return false;
        }
        map[moveTo] = number;
        map[moveFrom] = 0;
        return true;
    }


    // Start is called before the first frame update
    void Start() {
        //配列の実体の作成と初期化
        map = new int[] { 0, 0, 0, 1, 0, 0, 0, 0, 0 };

        PrintArray();

    }

    // Update is called once per frame
    void Update() {

        //右矢印キー入力
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            //見つからなかった場合に-1で初期化
            int playerIndex = GetPlayerIndex();

            //プレイヤーの移動
            MoveNumber(1, playerIndex, playerIndex + 1);

            //ログの出力
            PrintArray();

        }

        //左矢印キー入力
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            //見つからなかった場合に-1で初期化
            int playerIndex = GetPlayerIndex();

            //プレイヤーの移動
            MoveNumber(1, playerIndex, playerIndex - 1);

            //ログの出力
            PrintArray();

        }

    }
}