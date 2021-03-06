﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ImageUtil{
    public static Texture2D LoadPNG(string filePath)
    {
        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }

	public static Sprite LoadSprite(string filePath) {
		return Resources.Load<Sprite> ("Sprites/Cards/" + filePath.Substring(0, filePath.IndexOf('.')));
	}
}
