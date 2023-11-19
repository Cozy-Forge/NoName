using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossGlowySubState
{
    Waiting, Laser, PopBullet 
}

public class BossGlowySubController : StateController<BossGlowySubState>
{
    /*
    1. 랜덤한 위치로 가서 레이저를 십자로 발사한다.
    이후 레이저가 천천히 1바퀴 돈다.
    
    2. 랜덤한 위치로 가서 서브를 중심으로 탄막을 퍼트린다.
    
    3. 패턴이 끝나면 중심으로 가서 2초 기다리고 다음 패턴을 시작한다.
    */
}
