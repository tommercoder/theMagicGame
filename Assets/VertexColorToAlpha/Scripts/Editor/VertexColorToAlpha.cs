// Author: Andreas Suter
//
// Copyright (C) 2012 by Edelweiss Interactive (http://www.edelweissinteractive.com)
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class VertexColorToAlpha : AssetPostprocessor {

	private void OnPostprocessModel (GameObject a_GameObject) {
		if (a_GameObject.name.ToLower ().Contains ("vcta")) {
			float l_Factor = 1.0f / 2.0f;
			foreach (MeshRenderer l_MeshRenderer in a_GameObject.GetComponentsInChildren <MeshRenderer> ()) {
				MeshFilter l_MeshFilter = l_MeshRenderer.GetComponent <MeshFilter> ();
				if (l_MeshFilter != null && l_MeshFilter.sharedMesh != null) {
					Mesh l_Mesh = l_MeshFilter.sharedMesh;
					if (l_Mesh.colors != null) {
						Color[] l_Colors = l_Mesh.colors;
						for (int i = 0; i < l_Colors.Length; i = i + 1) {
							Color l_Color = l_Colors [i];
							l_Color.a = l_Factor * (l_Color.r + l_Color.g + l_Color.b);
							l_Color.r = 1.0f;
							l_Color.g = 1.0f;
							l_Color.b = 1.0f;
							l_Colors [i] = l_Color;
						}
						l_Mesh.colors = l_Colors;
					}
				}
			}
		}
	}
}
