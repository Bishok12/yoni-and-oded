using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPlatform : BasePlatform {

    private void Start()
    {
        m_Material = GetComponent<Renderer>().material;
        //print("Green platform started");
    }

    protected override void OnCollisionEnter(Collision other) {
        Debug.Log("entered Green platform");
        float jumpMult = _config.greenPlatformJumpMult;
        OnPlatformEnter(jumpMult);
    }

    protected override void TurnBlack()
    {
       //print("Green -> " + _config.blackColor);
       m_Material.color = _config.GetColor(ColorType.Black);
    }

    protected override void TurnToOriginalColor()
    {
        //print(_config.blackColor + " -> Gree");
        m_Material.color = _config.GetColor(ColorType.Green);
    }
}
