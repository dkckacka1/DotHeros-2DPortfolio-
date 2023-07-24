도트 히어로즈
===

<hr/>

게임 장르 : 2D 턴제 RPG
---
<hr/>

개발 목적 : 즐겨 했던 턴제 RPG 게임 기능 직접 구현
---
<hr/>

사용 엔진 : UNITY 2021.3.25f1
---

<hr/>

개발 기간 : 10주 (2023.05.12 ~ 2023.07.23)
---

<hr/>

포트폴리오 영상
---
[유튜브 영상 링크](https://www.youtube.com/watch?v=ZACqozWcwWE&ab_channel=%EB%85%B8%EC%96%B4%EC%9D%B4)

<hr/>

빌드 파일
---
[구들 드라이브 다운로드 링크](https://drive.google.com/file/d/1AINMf5aS2t6UDCPbDsIh2ihyqnkEpRmg/view?usp=sharing)

<hr/>

사용 서드파티
---
* ExcelDataReader
* NewtonJSON
* google Firebase
* Dotween

<hr/>

주요 활용 기술
---
* #01)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/main/Portfolio_2D/Assets/02.%20Script/Battle/Unit/SkillSystem/BattleTargetSetExtensions.cs)) [확장 메서드, 메서드 체이닝을 이용해서 전투 대상을 찾는 함수 구현](https://copractice.tistory.com/47)
* #02)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/main/Portfolio_2D/Assets/02.%20Script/Battle/Unit/SkillSystem/Skill.cs)) [상속을 이용하여 만든 스킬 시스템 구현](https://copractice.tistory.com/60)
* #03)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/main/Portfolio_2D/Assets/02.%20Script/Core/Editor/TableToJson.cs)) [ExcelDataReader 와 Newtonsoft.Json 을 활용해서 만든 데이터 테이블 JSON 변환 시스템 구현](https://copractice.tistory.com/61)
* #04)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/b66275919473052e85dca84890841800e42a5f15/Portfolio_2D/Assets/02.%20Script/Core/GameManager/ResourcesLoader.cs#L63C1-L63C1)) [JSON을 Data 클래스로 변환 한 후 Dictionary 저장해서 데이터 관리](https://copractice.tistory.com/62)
* #05)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/b66275919473052e85dca84890841800e42a5f15/Portfolio_2D/Assets/02.%20Script/Core/GameManager/GameManager.cs#L385)) [C# 리플렉션을 이용해서 스킬, 상태이상 클래스 런타임 생성 후 데이터 관리](https://copractice.tistory.com/63)
* #06)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/b66275919473052e85dca84890841800e42a5f15/Portfolio_2D/Assets/02.%20Script/Battle/Unit/SkillSystem/Skill.cs#L87)) [string.Format을 활용한 스킬 설명 구현](https://copractice.tistory.com/64)
* #07)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/main/Portfolio_2D/Assets/02.%20Script/Core/GameManager/ItemGenerator.cs)) [ScriptableObject로 만든 랜덤 데이터 생성기를 통한 랜덤 아이템 데이터 생성 구현](https://copractice.tistory.com/65)
* #08)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/b66275919473052e85dca84890841800e42a5f15/Portfolio_2D/Assets/02.%20Script/Lobby/UI/InventoryPanel/InventoryPanel.cs#L66)) [UGUI 에서 피봇과 포지션 좌표를 통한 툴팁 생성](https://copractice.tistory.com/66)
* #09)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/b66275919473052e85dca84890841800e42a5f15/Portfolio_2D/Assets/02.%20Script/Lobby/UI/InventoryPanel/EquipmentInventory.cs#L77)) [다중 선택 토글에 의한 장비아이템 일반 선택 혹은 다중 선택 시스템 구현](https://copractice.tistory.com/67)
* #10)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/main/Portfolio_2D/Assets/02.%20Script/Battle/UI/BattleLogUI.cs)) [큐를 이용한 로그 시스템](https://copractice.tistory.com/68)
* #11)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/main/Portfolio_2D/Assets/02.%20Script/Lobby/UI/HeroPanel/CompositionPanel/UnitCompositionPanelUI.cs)) [스택을 이용한 영웅 합성 시스템](https://copractice.tistory.com/69)
* #12)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/main/Portfolio_2D/Assets/02.%20Script/WorldMap/UI/UnitSlotSelector_FormationPopup.cs)) [드래그 앤 드랍으로 만든 유닛 포지션 배정 시스템](https://copractice.tistory.com/70)
* #13)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/b66275919473052e85dca84890841800e42a5f15/Portfolio_2D/Assets/02.%20Script/Core/GameManager/UIManager.cs#L81)) [자주 사용되는 UI 팝업창을 재활용](https://copractice.tistory.com/71)
* #14)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/b66275919473052e85dca84890841800e42a5f15/Portfolio_2D/Assets/02.%20Script/Lobby/Core/LobbyUIManager.cs#L99)) [커맨드 패턴을 이용한 Undo 시스템](https://copractice.tistory.com/72)
* #15)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/b66275919473052e85dca84890841800e42a5f15/Portfolio_2D/Assets/02.%20Script/Battle/Core/BattleManager.cs#L446)) [이벤트 버스를 이용해서 만든 전투 상태에 따른 이벤트 호출 시스템](https://copractice.tistory.com/73)
* #16)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/b66275919473052e85dca84890841800e42a5f15/Portfolio_2D/Assets/02.%20Script/Battle/Core/BattleManager.cs#L67)) [싱글톤 매니저 클래스 생성](https://copractice.tistory.com/74)
* #17)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/b66275919473052e85dca84890841800e42a5f15/Portfolio_2D/Assets/02.%20Script/Start/Core/StartManager.cs#L39)) [비동기 씬 호출을 통한 로딩 구현](https://copractice.tistory.com/75)
* #18)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/main/Portfolio_2D/Assets/02.%20Script/Battle/Core/ObjectPool.cs)) [오브젝트 풀링 사용 예](https://copractice.tistory.com/76)
* #19)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/main/Portfolio_2D/Assets/02.%20Script/Battle/Core/TurnBaseSystem.cs)) [게임 턴제 배틀 시스템 구현](https://copractice.tistory.com/77)
* #20)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/main/Portfolio_2D/Assets/02.%20Script/Battle/Unit/AISystem/AISystem.cs)) [자동 전투 시스템 구현](https://copractice.tistory.com/78)
* #21)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/main/Portfolio_2D/Assets/02.%20Script/Battle/Core/ActionSystem.cs)) [선택한 스킬에 따라 유닛을 선택하는 액션시스템 구현](https://copractice.tistory.com/79)
* #22)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/main/Portfolio_2D/Assets/02.%20Script/Core/Map/LootItemTable.cs)) [루팅 테이블을 만드는 스크립테이블 오브젝트 (커스텀 인스펙터)](https://copractice.tistory.com/80)
* #23)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/b66275919473052e85dca84890841800e42a5f15/Portfolio_2D/Assets/02.%20Script/WorldMap/Editor/MapNodeDraw.cs#L23)) [트리 구조로 만든 맵 노드 트리를 DFS 재귀를 이용해서 만든 맵노드 끼리 노드 연결 자동화](https://copractice.tistory.com/81)
* #24)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/f98487a65e925662b1aa78bcfee3bd133d675ace/Portfolio_2D/Assets/02.%20Script/Battle/Unit/SkillSystem/SkillList/ActiveSkill/Skill_GWEN_BaseAttack.cs#L29)) [Dotween을 활용한 투사체 던지기 스킬 구현](https://copractice.tistory.com/85)
* #25)([스크립트](https://github.com/dkckacka1/DotHeros-2DPortfolio-/blob/main/Portfolio_2D/Assets/02.%20Script/Core/GameManager/NetworkManager.cs)) [파이어베이스 연동 구현](https://copractice.tistory.com/86)
