using BBUnity.Conditions;
using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using UnityEngine;

[Condition("Game/Perception/IsTargetVisible")]
public class IsTargetVisible : GOCondition
{
    [InParam("TargetTag")]
    private const string targetTag = "Player";  // Tag do alvo
    private GameObject target;

    public override bool Check()
    {
        if (string.IsNullOrEmpty(targetTag))
        {
            Debug.LogError("Target tag is null or empty.");
            return false;
        }

        target = GameObject.FindGameObjectWithTag(targetTag);
        if (target == null)
        {
            Debug.LogError($"No GameObject found with tag {targetTag}");
            return false;
        }

        float distance = Vector2.Distance(gameObject.transform.position, target.transform.position);
        // Debug.Log($"Target found at distance: {distance}");

        if (distance < 5)
        {
            Debug.Log("Target is within visible distance.");
            return true;
        }
        else
        {
            //Debug.Log("Target is not within visible distance.");
            return false;
        }
    }

    public GameObject GetTarget()
    {
        return target;
    }
}
