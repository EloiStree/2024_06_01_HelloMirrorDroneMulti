using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SoccerStateSyncMono_PushBallGoalsField : MonoBehaviour, MSoccerMono_RefreshablePush
{

    public S_DroneSoccerBallGoals m_ballGoals;
    public UnityEvent<S_DroneSoccerBallGoals> m_onBallGoals;
    public MSoccerMono_SquareBallGoalsRedBlueSetup m_goalsSetup;

    public void RefreshAndPush()
    {
        m_ballGoals.m_goalDepthMeter = m_goalsSetup.m_goalDepthMeter;
        m_ballGoals.m_goalDistanceOfCenterMeter = m_goalsSetup.m_goalDistanceOfCenterMeter;
        m_ballGoals.m_goalGroundHeightMeter = m_goalsSetup.m_goalGroundHeightMeter;
        m_ballGoals.m_goalWidthRadiusMeter = m_goalsSetup.m_goalWidthRadiusMeter;


        m_onBallGoals.Invoke(m_ballGoals);
    }

    public IEnumerator Start()
    {

        RefreshAndPush();
        yield return new WaitForSeconds(3f);
        RefreshAndPush();
        

    }


}
