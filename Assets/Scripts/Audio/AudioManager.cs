using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioMovePlayer;

    public AudioClip[] clipWalkGrass;
    public AudioClip[] clipWalkRock;
    public AudioClip[] clipWalkWood;
    public AudioClip[] clipWalkSand;
    public bool Rock;
    public bool Wood;
    public bool Grass;
    public bool Sand;
    public bool Change;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Check();
        //if (Input.GetKey(KeyCode.LeftShift))
        //{
        //    if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        //    {
        //        if (!audioMovePlayer.isPlaying)
        //        {

        //            Change = !Change;
        //            audioMovePlayer.pitch = 1.85f;
        //            if (Grass)
        //            {
        //                if (Change)
        //                {
        //                    int i = Random.Range(0, clipWalkGrass.Length);
        //                    audioMovePlayer.clip = clipWalkGrass[i];
        //                    audioMovePlayer.panStereo = 0.1f;
        //                }
        //                else
        //                {
        //                    int i = Random.Range(0, clipWalkGrass.Length);
        //                    audioMovePlayer.clip = clipWalkGrass[i];
        //                    audioMovePlayer.panStereo = -0.1f;
        //                }
        //            }
        //            else if (Rock)
        //            {
        //                if (Change)
        //                {
        //                    int i = Random.Range(0, clipWalkRock.Length);
        //                    audioMovePlayer.clip = clipWalkRock[i];
        //                    audioMovePlayer.panStereo = 0.1f;
        //                }
        //                else
        //                {
        //                    int i = Random.Range(0, clipWalkRock.Length);
        //                    audioMovePlayer.clip = clipWalkRock[i];
        //                    audioMovePlayer.panStereo = -0.1f;
        //                }
        //            }
        //            else if (Wood)
        //            {
        //                if (Change)
        //                {
        //                    int i = Random.Range(0, clipWalkWood.Length);
        //                    audioMovePlayer.clip = clipWalkWood[i];
        //                    audioMovePlayer.panStereo = 0.1f;
        //                }
        //                else
        //                {
        //                    int i = Random.Range(0, clipWalkWood.Length);
        //                    audioMovePlayer.clip = clipWalkWood[i];
        //                    audioMovePlayer.panStereo = -0.1f;
        //                }
        //            }
        //            else if (Sand)
        //            {
        //                if (Change)
        //                {
        //                    int i = Random.Range(0, clipWalkSand.Length);
        //                    audioMovePlayer.clip = clipWalkSand[i];
        //                    audioMovePlayer.panStereo = 0.1f;
        //                }
        //                else
        //                {
        //                    int i = Random.Range(0, clipWalkSand.Length);
        //                    audioMovePlayer.clip = clipWalkSand[i];
        //                    audioMovePlayer.panStereo = -0.1f;
        //                }
        //            }
        //            audioMovePlayer.Play();
        //        }
        //    }
        //}
        //else
        //{
        //    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        //    {
        //        if (!audioMovePlayer.isPlaying)
        //        {
        //            Change = !Change;
        //            audioMovePlayer.pitch = 1;
        //            if (Grass)
        //            {
        //                if (Change)
        //                {
        //                    int i = Random.Range(0, clipWalkGrass.Length);
        //                    audioMovePlayer.clip = clipWalkGrass[i];
        //                    audioMovePlayer.panStereo = 0.1f;
        //                }
        //                else
        //                {
        //                    int i = Random.Range(0, clipWalkGrass.Length);
        //                    audioMovePlayer.clip = clipWalkGrass[i];
        //                    audioMovePlayer.panStereo = -0.1f;
        //                }
        //            }
        //            else if (Rock)
        //            {
        //                if (Change)
        //                {
        //                    int i = Random.Range(0, clipWalkRock.Length);
        //                    audioMovePlayer.clip = clipWalkRock[i];
        //                    audioMovePlayer.panStereo = 0.1f;
        //                }
        //                else
        //                {
        //                    int i = Random.Range(0, clipWalkRock.Length);
        //                    audioMovePlayer.clip = clipWalkRock[i];
        //                    audioMovePlayer.panStereo = -0.1f;
        //                }
        //            }
        //            else if (Wood)
        //            {
        //                if (Change)
        //                {
        //                    int i = Random.Range(0, clipWalkWood.Length);
        //                    audioMovePlayer.clip = clipWalkWood[i];
        //                    audioMovePlayer.panStereo = 0.1f;
        //                }
        //                else
        //                {
        //                    int i = Random.Range(0, clipWalkWood.Length);
        //                    audioMovePlayer.clip = clipWalkWood[i];
        //                    audioMovePlayer.panStereo = -0.1f;
        //                }
        //            }
        //            else if (Sand)
        //            {
        //                if (Change)
        //                {
        //                    int i = Random.Range(0, clipWalkSand.Length);
        //                    audioMovePlayer.clip = clipWalkSand[i];
        //                    audioMovePlayer.panStereo = 0.1f;
        //                }
        //                else
        //                {
        //                    int i = Random.Range(0, clipWalkSand.Length);
        //                    audioMovePlayer.clip = clipWalkSand[i];
        //                    audioMovePlayer.panStereo = -0.1f;
        //                }
        //            }
        //            audioMovePlayer.Play();
        //        }
        //    }
        //}
    }

    public void Check()
    {
        if(!Sand && !Rock && !Wood && Grass || !Sand && !Rock && !Wood)
        {
            Grass = true;
        }
    }
    public void ClearAll()
    {
        Grass = false;
        Wood = false;
        Rock = false;
        Sand = false;
    }
}
