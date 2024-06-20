using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SoccerStateSyncMono_PushBallGoalsField : MonoBehaviour
{

    public DroneSoccerBallGoals m_ballGoals;
    public UnityEvent<DroneSoccerBallGoals> m_onBallGoals;
    public MSoccerMono_SquareBallGoalsRedBlueSetup m_goalsSetup;

   

    public IEnumerator Start()
    {
        m_ballGoals.m_goalDepthMeter = m_goalsSetup.m_goalDepthMeter;
        m_ballGoals.m_goalDistanceOfCenterMeter = m_goalsSetup.m_goalDistanceOfCenterMeter;
        m_ballGoals.m_goalCenterHeightMeter = m_goalsSetup.m_goalCenterHeightMeter;
        m_ballGoals.m_goalWidthRadiusMeter = m_goalsSetup.m_goalWidthRadiusMeter;
       

        m_onBallGoals.Invoke(m_ballGoals);
        yield return new WaitForSeconds(3f);
        m_onBallGoals.Invoke(m_ballGoals);

        

    }


}
