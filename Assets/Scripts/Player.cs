using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Initial player position coordinates
    private static int PLAYER_INITIAL_POS_X = 0;
    private static int PLAYER_INITIAL_POS_Y = 0;
    private static int PLAYER_INITIAL_POS_Z = 0;


    // Start is called before the first frame update
    void Start() {
        // Set initial position
        transform.position = new Vector3(PLAYER_INITIAL_POS_X, PLAYER_INITIAL_POS_Y, PLAYER_INITIAL_POS_Z);
    }

    // Update is called once per frame
    void Update() {
    }
}
