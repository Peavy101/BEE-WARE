using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneyQuakeParticles : MonoBehaviour
{

    [SerializeField] ParticleSystem quakeParticles;
    [SerializeField] ParticleSystem quakeParticlesTwo;

    void Start()
    {
        quakeParticles.Play();
        quakeParticlesTwo.Play();
    }

    public void NoMoreQuake()
    {
        quakeParticles.Stop();
        quakeParticlesTwo.Stop();
    }

}
