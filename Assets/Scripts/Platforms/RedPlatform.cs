using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPlatform : BasePlatform {

    private void Start()
    {
        m_Material = GetComponent<Renderer>().material;
        //print("Red platform started");
    }

    protected override void OnCollisionEnter(Collision other) {
        Debug.Log("entered Red platform");
        float jumpMult = _config.redPlatformJumpMult;
        OnPlatformEnter((jumpMult == 0) ? jumpMult : (1 / jumpMult));
    }

    protected override void TurnBlack()
    {
        //print("Red -> " + _config.blackColor);
        m_Material.color = _config.GetColor(ColorType.Black);
    }

    protected override void TurnToOriginalColor()
    {
        //print(_config.blackColor + " -> Red");
        m_Material.color = _config.GetColor(ColorType.Red);
    }

}
