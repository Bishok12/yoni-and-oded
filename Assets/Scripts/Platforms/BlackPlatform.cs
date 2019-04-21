using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackPlatform : BasePlatform {

    protected override void OnCollisionEnter(Collision other) {
        Debug.Log("entered Black platform");
        OnBlackPlatformEnter();
    }

    protected override void TurnBlack()
    {
        return;
    }

    protected override void TurnToOriginalColor()
    {
        return;
    }
}
