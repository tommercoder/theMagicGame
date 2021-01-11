using UnityEngine;

[CreateAssetMenu()]
public class FootstepData : ScriptableObject
{
    [SerializeField] [Range(0, 1)] private float volumeVariance = 0.04f;      // How much should footstep volume vary
    [SerializeField] [Range(0, 1)] private float pitchVariance = 0.08f;       // How much should footstep pitch vary

    [Space()]

    [SerializeField] private FootstepClips[] m_FootstepsClips;  // Footsteps clips

    public float VolumeVariance { get { return volumeVariance; } }
    public float PitchVariance { get { return pitchVariance; } }

    /// <summary>
    /// Return an array of audio clips that should be played for footstep
    /// </summary>
    /// <param name="textureNameOrTag">The texture name of terrain or tag of the object</param>
    /// <param name="characterVelocity">The current velocity of character rigidbody at the moment of footstep</param>
    /// <returns>Footstep audio clip array</returns>
    public AudioClip[] GetFootstepsClips(string textureNameOrTag, Vector3 characterVelocity)
    {
        Vector3 velocity = Vector3.Scale(characterVelocity,new Vector3(1, 0, 1));   // Ignore vertical velocity

        // Loop trough all footstep clip of this data
        foreach (FootstepClips fclip in m_FootstepsClips)
        {
            if (textureNameOrTag.Contains(fclip.m_GroundType))
            {
                return (velocity.magnitude > 3.5f) ? fclip.m_Running : fclip.m_Walking;
            }
        }

        if(m_FootstepsClips.Length == 0)
            return null;

        return (velocity.magnitude > 3.5f) ? m_FootstepsClips[0].m_Running : m_FootstepsClips[0].m_Walking;
    }

    /// <summary>
    /// Return an array of land audio clips
    /// </summary>
    /// <param name="textureNameOrTag">he texture name of terrain or tag of the object</param>
    /// <returns>Land audio clip array</returns>
    public AudioClip[] GetLandClips(string textureNameOrTag)
    {
        foreach (FootstepClips fclip in m_FootstepsClips)
        {
            if (textureNameOrTag.Contains(fclip.m_GroundType))
                return fclip.m_Landing;
        }
        return null;
    }

    public void PlayLandSound(Transform character)
    {
        AudioSource source = character.GetComponent<AudioSource>();
        if (source == null)
            source = character.GetComponentInChildren<AudioSource>(); // Try a second chance to find AudioSource on in any children game objects

        if (source == null) // If no audio source found, do nothing
            return;

        Vector3 Start = character.position + Vector3.up * 0.3f;
        string groundName = string.Empty;

        RaycastHit ground;
        if (Physics.Raycast(Start, Vector3.down, out ground, 1f, Physics.AllLayers))
        {
            if (ground.collider.tag == "Terrain")
                groundName = FootstepData.TerrainFindTexture(ground.point);
            else
                groundName = ground.collider.tag;
        }

        AudioClip[] clips = GetLandClips(groundName);
        if (clips == null)
            return;
        if(clips.Length == 0)
            return;

        int i = Random.Range(0, clips.Length - 1);

        source.clip = clips[i];
        source.Play();

    }

    #region Terrain Methods

    public static string TerrainFindTexture(Vector3 desiredWorldPosition)
    {
        GameObject terrainFinder = GameObject.FindGameObjectWithTag("Terrain");
        Terrain terrain = null;
        TerrainData terrainData = null;
        Vector3 terrainPos = Vector3.zero;

        if (terrainFinder != null)
        {    //IS THERE A TERRAIN IN THE SCENE?
            terrain = terrainFinder.GetComponent<Terrain>();
            terrainData = terrain.terrainData;
            terrainPos = terrain.transform.position;
        }

        if (terrain != null)
        {    //IS THERE A TERRAIN IN THE SCENE?
            int surfaceIndex = GetMainTexture(desiredWorldPosition, terrain, terrainData, terrainPos);
            //Not that it matters, but here we determine what position the Terrain Textures are in.
            //For example, If you added a grass texture, then a dirt, then a rock, you'd have grass=0, dirt=1, rock=2.
            return terrainData.terrainLayers[surfaceIndex].diffuseTexture.name;
            //Instead of messing around with numbers, we'll just check the texture's filename.
        }

        return string.Empty;
    }



    //Puts ALL TEXTURES from the Terrain into an array, represented by floats (0=first texture, 1=second texture, etc).
    private static float[] GetTextureMix(Vector3 WorldPos, Terrain terrain, TerrainData terrainData, Vector3 terrainPos)
    {
        if (terrain != null)
        {    //IS THERE A TERRAIN IN THE SCENE?
             // calculate which splat map cell the worldPos falls within
            int mapX = (int)(((WorldPos.x - terrainPos.x) / terrainData.size.x) * terrainData.alphamapWidth);
            int mapZ = (int)(((WorldPos.z - terrainPos.z) / terrainData.size.z) * terrainData.alphamapHeight);
            // get the splat data for this cell as a 1x1xN 3d array (where N = number of textures)
            float[,,] splatmapData = terrainData.GetAlphamaps(mapX, mapZ, 1, 1);
            float[] cellMix = new float[splatmapData.GetUpperBound(2) + 1]; //turn splatmap data into float array
            for (int n = 0; n < cellMix.Length; n++)
            {
                cellMix[n] = splatmapData[0, 0, n];
            }
            return cellMix;
        }
        else return null; //THERE'S NO TERRAIN IN THE SCENE! DON'T DO THE ABOVE STUFF.
    }




    //Takes the "GetTextureMix" float array from above and returns the MOST DOMINANT texture at Player's position.
    private static int GetMainTexture(Vector3 WorldPos, Terrain terrain, TerrainData terrainData, Vector3 terrainPos)
    {
        if (terrain != null)
        {    //IS THERE A TERRAIN IN THE SCENE?
            float[] mix = GetTextureMix(WorldPos, terrain, terrainData, terrainPos);
            float maxMix = 0;
            int maxIndex = 0;
            for (int n = 0; n < mix.Length; n++)
            {
                if (mix[n] > maxMix)
                {
                    maxIndex = n;
                    maxMix = mix[n];
                }
            }
            return maxIndex;
        }
        else return 0;    //THERE'S NO TERRAIN IN THE SCENE! DON'T DO THE ABOVE STUFF.
    }

    #endregion
}

[System.Serializable]
public class FootstepClips
{
    public string m_GroundType;
    public AudioClip[] m_Walking;
    public AudioClip[] m_Running;
    public AudioClip[] m_Landing;
}

