using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossGlowyState
{
    Idle, Teleport, ShotLaser, Spray 
}


public class BossGlowyController : StateController<BossGlowyState>
{
    /*
    1. 텔레포트
    패턴이 끝났을 때, 플레이어가 가까이 있으면 랜덤한 위치로 텔레포트 한다.

    2. 중앙 레이저 패턴
    플레이어 방향으로 거대한 레이저를 3방 발사한다.
    
    3. 미니건 탄막 패턴
    탄막을 중심에서 플레이어 방향으로 흩뿌린다.
     */

}
