//	Copyright (c) KimPuppy.
//	http://bakak112.tistory.com/

using UnityEngine;

public class FlashSprite : MonoBehaviour
{
    public float FlashTime = 0.05f;

    private SpriteRenderer objSpr;

    private Material objMaterial;
    private Material flashMaterial;

    private Timer flashTimer;

    private void Start()
    {
        objSpr = GetComponent<SpriteRenderer>();

        objMaterial = Resources.Load<Material>("Material/Sprites-Diffuse");
        flashMaterial = Resources.Load<Material>("Material/Sprites-Solid");

        flashTimer = new Timer(FlashTime, false);
    }

    private void Update()
    {
        if (flashTimer.IsDone)
            objSpr.material = objMaterial;
    }

    public void Flash()
    {
        objSpr.material = flashMaterial;
        flashTimer.IsEnable = true;
    }
}