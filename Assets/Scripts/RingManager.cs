﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RingManager : MonoBehaviour {
	
	public Transform[] targetRingSmall;
    public Transform[] targetRingMid;
	public Transform[] targetRingBig;
    public GameObject hole;

    static RingManager instance;
    public static RingManager Instance{
        get { return instance; }
    }
    private void Awake()
    {
        instance = this;
    }

	//将环一个接一个从上至下的生成
    IEnumerator MoveRing(float time,List<Transform> trans)
    {
        int length = trans.Count;
        while (length > 0)
        {           
			if (trans [length - 1]) {
				trans [length - 1].DOLocalMoveY (trans [length - 1].localPosition.y - 20, time, false);
				length--;
			}
            yield return new WaitForSeconds(time - 0.05f);
        }
    }

	//在给定位置生成给定尺寸的环
	public Transform GenerateRings(Vector3 pos,int size)
    {
		
		Transform[] targetTrans = targetRingSmall;

		if (size == 1) {
			targetTrans = targetRingBig;
		} else if (size == 2) {
			targetTrans = targetRingBig;
		} else {
			targetTrans = targetRingBig;
		}

		List<Transform> rings = new List<Transform>();
		foreach (Transform trans in targetTrans)
        {
            GameObject go = Instantiate(trans.gameObject, transform);
            go.transform.position = pos + new Vector3(0, 20, 0);
            rings.Add(go.transform);
        }

        StartCoroutine(MoveRing(0.4f, rings));

		return rings [2];
    }

	//在指定位置生成黑洞
	public Transform GenerateHole(Vector3 pos)
    {		
		return Instantiate(hole.transform, pos, Quaternion.identity);
    }
    
}
