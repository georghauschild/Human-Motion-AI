using UnityEngine;
using Unity.MLAgents;

namespace Unity.MLAgentsExamples
{
    /// <summary>
    /// This class contains logic for locomotion agents with joints which might make contact with the ground.
    /// By attaching this as a component to those joints, their contact with the ground can be used as either
    /// an observation for that agent, and/or a means of punishing the agent for making undesirable contact.
    /// </summary>
    [DisallowMultipleComponent]
    public class GroundContact : MonoBehaviour
    {
        [HideInInspector] public Agent agent;

        [Header("Ground Check")] public bool agentDoneOnGroundContact; // Whether to reset agent on ground contact.
        [Header("Wall Check")] public bool agentDoneOnWallContact; // Whether to reset agent on ground contact.
        public bool penalizeGroundContact; // Whether to penalize on contact.
        bool penalizeWallContact = true; // Whether to penalize on contact.
        public float groundContactPenalty; // Penalty amount (ex: -1).
        float wallContactPenalty = -0.5f; // Penalty amount (ex: -1).
        public bool touchingGround;
        public bool touchingWall;
        const string k_Ground = "ground"; // Tag of ground object.
        const string k_Wall = "wall"; // Tag of ground object.


        /// <summary>
        /// Check for collision with ground, and optionally penalize agent.
        /// </summary>
        void OnCollisionEnter(Collision col)
        {
            if (col.transform.CompareTag(k_Ground))
            {
                touchingGround = true;
                if (penalizeGroundContact)
                {
                    agent.SetReward(groundContactPenalty);
                }

                if (agentDoneOnGroundContact)
                {
                    agent.EndEpisode();
                }
            }

            if (col.transform.CompareTag(k_Wall))
            {
                touchingWall = true;
                if (penalizeWallContact)
                {
                    agent.SetReward(wallContactPenalty);
                }

              //  if (agentDoneOnWallContact)
              //  {
               //     agent.EndEpisode();
               // }
            }
        }

        /// <summary>
        /// Check for end of ground collision and reset flag appropriately.
        /// </summary>
        void OnCollisionExit(Collision other)
        {
            if (other.transform.CompareTag(k_Ground))
            {
                touchingGround = false;
            }

            if (other.transform.CompareTag(k_Wall))
            {
                touchingWall = false;
            }
        }
    }
}
