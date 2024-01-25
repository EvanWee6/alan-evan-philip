using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{

    public static GameAssets i;

    private void Awake()
    {
        i = this;
    }

    public Sprite wormHead;
    public Sprite wormBody;
	public Sprite redApple;
	public Sprite goldenApple;
    public Sprite BwormHead;
    public Sprite BwormBody;
    public Sprite GwormHead;
    public Sprite GwormBody;
    public Sprite WwormHead;
    public Sprite WwormBody;

}
