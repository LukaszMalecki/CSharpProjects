using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeMaster : MonoBehaviour
{
    private Transform masterPos;
    public float velocity = 1f;
    public float waitingY;
    public float prepareY;
    public float attackY;

    public AoeMasterState state;

    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        masterPos = GetComponent<Transform>();

        state = AoeMasterState.Waiting;
        masterPos.position = new Vector3(masterPos.position.x, waitingY, 0);

    }

    // Update is called once per frame
    void Update()
    {
        distance = velocity * Time.deltaTime;

        if(state == AoeMasterState.Waiting)
        {
            if(masterPos.position.y > waitingY)
            {
                ChangeY(-distance);
            }
        }
        else if(state == AoeMasterState.Preparing)
        {
            if (masterPos.position.y < prepareY)
            {
                ChangeY(distance);
            }
        }
        else
        {
            if (masterPos.position.y < attackY)
            {
                ChangeY(distance);
            }
        }
    }

    public void SetState(AoeMasterState aoeState)
    {
        state = aoeState;
    }

    private void ChangeY(float distanceY)
    {
        //masterPos.position = new Vector2(masterPos.position.x, masterPos.position.x + distanceY);
        masterPos.position += new Vector3(0, distanceY, 0);
    }
}

public enum AoeMasterState
{
    Waiting, Preparing, Attacking
}
