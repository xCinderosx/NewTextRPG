using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.AI;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {

        public AudioSource audioSource;
        public NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target;                                    // target to aim for
        private bool playingAudio = false;

        private void Start()
        {

            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();
            
	        agent.updateRotation = false;
	        agent.updatePosition = true;
        }


        private void Update()
        {
            if (target != null)
            {
                agent.SetDestination(target.position);
                if (audioSource != null && !playingAudio)
                {
                    playingAudio = true;
                    audioSource.Play(0);
                }
                
            }

            if (agent.remainingDistance < 15)
            {
                if (agent.remainingDistance > agent.stoppingDistance)
                    character.Move(agent.desiredVelocity, false, false);
                else
                    Attack();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                SetTarget(other.transform);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                audioSource.Stop();
                playingAudio = false;
                target = null;
            }
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }
        
        private void Attack()
        {
            character.Move(Vector3.zero, false, false);
            character.transform.LookAt(target);
        }
    }
}
