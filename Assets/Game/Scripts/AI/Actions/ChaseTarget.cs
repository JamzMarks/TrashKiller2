using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using Platformer2D.Character;
using System.Collections;
using UnityEngine;

[Action("Game/ChaseTarget")]
public class ChaseTarget : BasePrimitiveAction
{
    [InParam("AiController")]
    private EnemyAiController aiController;

    [InParam("TargetTag")]
    private string targetTag = "Player";
    private GameObject target;

    public override void OnStart()
    {
        base.OnStart();
        target = GameObject.FindGameObjectWithTag(targetTag);
        if (target == null)
        {
            Debug.LogError($"No GameObject found with tag {targetTag}");
        }

    }
    public override TaskStatus OnUpdate()
    {

        if (target != null)
        {
            Debug.Log("Caçada comecou");
            Vector2 toTarget = target.transform.position - aiController.transform.position;
            aiController.movementInput.x = Mathf.Sign(toTarget.x);
            return TaskStatus.RUNNING;
        }

        return TaskStatus.FAILED;
    }

}

