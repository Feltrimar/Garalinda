using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public enum InBattleState { Start, PlayerAction, PlayerMove, PlayerFormation, ChoosingTarget, Item, EnemyMove, Busy, End}

public class BattleSystem : MonoBehaviour
{
    public event Action OnBattleOver;
    public static PlayerPrefs pp;
    public GameObject chrimg;
    public List<Image> playableImages;
    public List<Image> enemyImages;
    public List<EnemyUnit> enemies;
    public List<EnemyUnit> inBattleEnemies;
    public List<GameObject> nodes;
    public List<GameObject> ableNodes;
    public List<Text> stats;
    public int enemyNumber;
    public int enemyNumber2;
    public bool aux1;
    public InBattleState state;
    [SerializeField] BattleDialogBox dialogBox;
    public List<CharacterBase> possibleEnemies;
    public int currentAction;
    public int currentMove;
    public int currentCharacterInt;
    public int currentTarget;
    public int currentItem;
    public int currentFormation;
    public int experience;
    public int money;
    public Character currentCharacter;
    public Ability currentAbility;
    public List<Ability> abilities;
    public bool AllyTurn;
    public List<ItemBase> items;
    public List<GameObject> allyNodes;
    public List<GameObject> availableAllyNodes;
    


    private void OnEnable(){
        state=InBattleState.Start;
        currentAction=0;
        currentMove=0;
        currentCharacterInt=0;
        currentTarget=0;
        currentItem=0;
        money=0;
        dialogBox.EnableDialogText(true);
        dialogBox.EnableActionSelection(false);
        dialogBox.EnableMoveSelection(false);
        System.Random rnd = new System.Random();
        enemyNumber = rnd.Next(1,4);
        ableNodes=nodes.GetRange(11,10);
        pp=GameObject.Find("GlobalVariable").GetComponent<PlayerPrefs>();
        enemies=GameObject.Find("Enemies").GetComponentsInChildren<EnemyUnit>().ToList();
        enemies=enemies.GetRange(0,enemyNumber);
        for(int i=0; i<enemies.Count();i++){
            enemies[i]._base=possibleEnemies[rnd.Next(0,possibleEnemies.Count())];
            enemies[i].Setup();
            enemies[i].GetComponent<Character>().enemy=true;
            enemies[i].GetComponent<Character>().defeated=false;
            enemyNumber2 = rnd.Next(0,ableNodes.Count());
            enemies[i].GetComponent<Character>().node=ableNodes[enemyNumber2];
            ableNodes.RemoveAt(enemyNumber2);
            money+=enemies[i].GetComponent<Character>().level;
        }
        StartCoroutine(SetupBattle());
        stats=GameObject.Find("Stats").GetComponentsInChildren<Text>().ToList();
        ChangeStats(stats);
         }

    public IEnumerator SetupBattle(){
        chrimg.SetActive(true);
        PositionPlayableCharacters();
        PositionEnemyCharacters();
        currentCharacterInt=0;
        yield return dialogBox.TypeDialog("Se acercan enemigos... ¡preparate!");
        yield return new WaitForSeconds(1f);
        currentCharacter = pp.battleCharacters[currentCharacterInt];
        if(pp.battleCharacters[currentCharacterInt].node==nodes[10]){
            abilities=pp.battleCharacters[currentCharacterInt].SupportAbilities;
            dialogBox.SetAbilityNames(pp.battleCharacters[currentCharacterInt].SupportAbilities);}
        else{
            abilities=pp.battleCharacters[currentCharacterInt].MainAbilities;
            dialogBox.SetAbilityNames(pp.battleCharacters[currentCharacterInt].MainAbilities);}
        PlayerAction();
    }

    void PlayerAction(){
        state = InBattleState.PlayerAction;
        dialogBox.EnableDialogText(false);
        dialogBox.EnableActionSelection(true);
    }
    public void HandleUpdate()
    {
        if(state==InBattleState.PlayerAction){
            HandleActionSelection();
        }

        if(state==InBattleState.PlayerMove){
            HandleMoveSelection();
        }

        if(state==InBattleState.ChoosingTarget){
            HandleTargetSelection();
        }

         if(state==InBattleState.PlayerFormation){
            HandleFormationSelection();
        }

        if(state==InBattleState.Item){
            HandleItemSelection();
        }

        if(Input.GetKeyDown(KeyCode.X)&& state==InBattleState.End)
            OnBattleOver();
    }
    
    void HandleActionSelection(){
        if(Input.GetKeyDown(KeyCode.DownArrow)){
            if(currentAction==0)
                currentAction=2;
            if(currentAction==1)
                currentAction=3;
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            if(currentAction==1)
                currentAction=0;
            if(currentAction==3)
                currentAction=2;
        }
        if(Input.GetKeyDown(KeyCode.RightArrow)){
            if(currentAction==0)
                currentAction=1;
            if(currentAction==2)
                currentAction=3;
        }
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            if(currentAction==2)
                currentAction=0;
            if(currentAction==3)
                currentAction=1;
        }

        dialogBox.UpdateActionSelection(currentAction);

        if(Input.GetKeyDown(KeyCode.X)){
            if(currentAction==0){
                StartCoroutine(PlayerMove());
            }

            if(currentAction==1){
                StartCoroutine(FormationStage());
            }

            if(currentAction==2){
                StartCoroutine(ItemSelect());
            }

            if(currentAction==3){
                OnBattleOver();
            }
        }
    }

    void HandleMoveSelection(){
        if(Input.GetKeyDown(KeyCode.DownArrow)){
            if(currentMove==0)
                currentMove=2;
            if(currentMove==1)
                currentMove=3;
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            if(currentMove==1)
                currentMove=0;
            if(currentMove==3)
                currentMove=2;
        }
        if(Input.GetKeyDown(KeyCode.RightArrow)){
            if(currentMove==0)
                currentMove=1;
            if(currentMove==2)
                currentMove=3;
        }
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            if(currentMove==2)
                currentMove=0;
            if(currentMove==3)
                currentMove=1;
        }

        dialogBox.UpdateMoveSelection(currentMove);

        if(Input.GetKeyDown(KeyCode.X)){
            if(currentMove<abilities.Count()){
            StartCoroutine(ChooseTarget());
            }
        }
    }

    void HandleItemSelection(){
        if(Input.GetKeyDown(KeyCode.DownArrow)){
            ++currentItem;
        }
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            --currentItem;
        }

        dialogBox.UpdateItemSelection(currentItem);

        if(Input.GetKeyDown(KeyCode.X)){
            if(currentItem<pp.items.Count()){
            StartCoroutine(UseItem());
            }
        }
    }

    void HandleFormationSelection(){
        if(Input.GetKeyDown(KeyCode.DownArrow)){
            ++currentFormation;
        }
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            --currentFormation;
        }

        dialogBox.UpdateFormationSelection(currentFormation);

        if(Input.GetKeyDown(KeyCode.X)){
            if(currentFormation<availableAllyNodes.Count()){
            StartCoroutine(SwapFormation());
            }
        }
    }
    public IEnumerator SwapFormation(){
        state=InBattleState.Busy;
        dialogBox.EnableActionSelection(false);
        dialogBox.EnableFormationSelection(false);
        dialogBox.EnableDialogText(true);
        yield return dialogBox.TypeDialog(currentCharacter.name+" cambió de posición.");
        pp.battleCharacters[currentCharacterInt].node=availableAllyNodes[currentFormation];
        pp.GetOrderedCharacters();
        PositionPlayableCharacters();
        yield return new WaitForSeconds(1f);
        yield return NextTurn();
    }

     public IEnumerator UseItem(){
        state=InBattleState.Busy;
        dialogBox.EnableActionSelection(false);
        dialogBox.EnableItemSelection(false);
        dialogBox.EnableDialogText(true);
        yield return dialogBox.TypeDialog(currentCharacter.name+" usó el objeto "+ pp.items[currentItem].Name+".");
        yield return new WaitForSeconds(0.5f);
        currentCharacter.ItemHeal(pp.items[currentItem]);
        pp.items.RemoveAt(currentItem);
        ChangeStats(stats);
        yield return NextTurn();
    }

    public IEnumerator ChooseTarget(){
            if(abilities[currentMove]._base.Type==MoveType.Ofensivo){
                dialogBox.SetTargetNames(enemies);
            } else{
                dialogBox.SetTargetNames(pp.battleCharacters);
            }
            dialogBox.EnableMoveSelection(false);
            dialogBox.EnableTargetSelection(true);
            yield return new WaitForSeconds(0.2f);
            state = InBattleState.ChoosingTarget;  
    }

    void HandleTargetSelection(){
        if(Input.GetKeyDown(KeyCode.DownArrow)){
            if(currentTarget==0)
                currentTarget=2;
            if(currentTarget==1)
                currentTarget=3;
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            if(currentTarget==1)
                currentTarget=0;
            if(currentTarget==3)
                currentTarget=2;
        }
        if(Input.GetKeyDown(KeyCode.RightArrow)){
            if(currentTarget==0)
                currentTarget=1;
            if(currentTarget==2)
                currentTarget=3;
        }
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            if(currentTarget==2)
                currentTarget=0;
            if(currentTarget==3)
                currentTarget=1;
        }

        dialogBox.UpdateTargetSelection(currentTarget);

        if(Input.GetKeyDown(KeyCode.X)){
            dialogBox.EnableTargetSelection(false);
            dialogBox.EnableDialogText(true);
            StartCoroutine(PerformPlayerAbility());        
            }
        }

    IEnumerator FormationStage(){
        availableAllyNodes=getAvailableNodes();
        dialogBox.SetFormationNames(availableAllyNodes);
        dialogBox.EnableFormationSelection(true);
        yield return new WaitForSeconds(0.2f);
        state = InBattleState.PlayerFormation;
    }

    private IEnumerator PerformPlayerAbility(){
        var ability = abilities[currentMove];
        if(ability._base.Cost<=currentCharacter.Energy){
        currentCharacter.Energy-=ability._base.Cost;
        state = InBattleState.Busy; 
        yield return dialogBox.TypeDialog(currentCharacter.name+" usó su "+ ability._base.Name+".");
        yield return new WaitForSeconds(1f);
        state = InBattleState.ChoosingTarget;
        if(ability._base.Type==MoveType.Apoyo){
        pp.battleCharacters[currentTarget].ReceiveHeal(ability,currentCharacter);    
        }else{ 
        bool defeated = enemies[currentTarget].character.ReceiveAttack(ability,currentCharacter);

        if(defeated){
            enemies[currentTarget].character.defeated=true;
            enemyImages[currentTarget].sprite=null;
             }
        }
        ChangeStats(stats);}
        else{
            yield return dialogBox.TypeDialog("No tienes suficiente energía, se pasará turno");
            yield return new WaitForSeconds(1f);
            currentCharacter.Energy+=10;
            if(currentCharacter.Energy>currentCharacter.MaxEnergy)
                currentCharacter.Energy=currentCharacter.MaxEnergy;
            ChangeStats(stats);
        }
        bool win = CheckIfBeaten();
        if(win){
            yield return WinState();
        }
        else{
            yield return NextTurn();
        }

    }

    private IEnumerator WinState(){
        state=InBattleState.Busy;
        yield return dialogBox.TypeDialog("¡Has ganado!");
        for(int i=0; i<pp.battleCharacters.Count;i++){
            pp.battleCharacters[i].experience+=30*money;
            while(pp.battleCharacters[i].experience>99){
                pp.battleCharacters[i].experience-=100;
                pp.battleCharacters[i].level++;
            }
            pp.battleCharacters[i].getNewAttacks();
        }
        pp.gold=pp.gold+money;
        yield return new WaitForSeconds(1f);
        state=InBattleState.End;

    }

    private IEnumerator LoseState(){
        state=InBattleState.Busy;
        yield return dialogBox.TypeDialog("Has perdido...");
        yield return new WaitForSeconds(1f);
        yield return SceneManager.LoadSceneAsync(0);

    }

    IEnumerator EnemyAction(){
        state=InBattleState.EnemyMove;
        var ability=currentCharacter.GetRandomAbility();
        System.Random rnd = new System.Random();
        currentTarget=rnd.Next(0,pp.battleCharacters.Count);
        yield return dialogBox.TypeDialog(currentCharacter.name+" usó su "+ ability._base.Name+".");
        yield return new WaitForSeconds(1f);
        bool defeated = pp.battleCharacters[currentTarget].ReceiveAttack(ability,currentCharacter);
        
        ChangeStats(stats);
        bool defeat = CheckIfDefeat();
        if(defeat){
            yield return LoseState();
        }
        else{
            yield return EnemyNextTurn();
        }



    }

    private IEnumerator NextTurn(){
        if(currentCharacterInt<pp.battleCharacters.Count()-1){
            currentCharacterInt++;
        if(pp.battleCharacters[currentCharacterInt].defeated&&(currentCharacterInt==pp.battleCharacters.Count()-1)){
            state=InBattleState.Busy;
            currentCharacterInt=-1;
            yield return dialogBox.TypeDialog("TURNO ENEMIGO");
            yield return new WaitForSeconds(1f);
            yield return EnemyNextTurn();
        }else{
        
        if(pp.battleCharacters[currentCharacterInt].defeated)
            currentCharacterInt++;
        currentCharacter = pp.battleCharacters[currentCharacterInt];
        if(pp.battleCharacters[currentCharacterInt].node==nodes[10]){
            abilities=pp.battleCharacters[currentCharacterInt].SupportAbilities;
            dialogBox.SetAbilityNames(pp.battleCharacters[currentCharacterInt].SupportAbilities);}
        else{
            abilities=pp.battleCharacters[currentCharacterInt].MainAbilities;
            dialogBox.SetAbilityNames(pp.battleCharacters[currentCharacterInt].MainAbilities);}
        PlayerAction();
        }
        }
        else{
            state=InBattleState.Busy;
            currentCharacterInt=-1;
            yield return dialogBox.TypeDialog("TURNO ENEMIGO");
            yield return new WaitForSeconds(1f);
            yield return EnemyNextTurn();
        }
    }

    private IEnumerator EnemyNextTurn(){
        if(currentCharacterInt<enemies.Count()-1){
        currentCharacterInt++;
        if(enemies[currentCharacterInt].GetComponent<Character>().defeated&&(currentCharacterInt==enemies.Count()-1)){
            state=InBattleState.Busy;
            currentCharacterInt=-1;
            yield return dialogBox.TypeDialog("TURNO ALIADO");
            yield return new WaitForSeconds(1f);
            yield return NextTurn();
        }
        else{
        
        if(enemies[currentCharacterInt].GetComponent<Character>().defeated)
            currentCharacterInt++;
        currentCharacter = enemies[currentCharacterInt].GetComponent<Character>();
        abilities=enemies[currentCharacterInt].GetComponent<Character>().MainAbilities;
        dialogBox.SetAbilityNames(enemies[currentCharacterInt].GetComponent<Character>().MainAbilities);
        yield return EnemyAction();
            }
        }
        else{
            state=InBattleState.Busy;
            currentCharacterInt=-1;
            yield return dialogBox.TypeDialog("TURNO ALIADO");
            yield return new WaitForSeconds(1f);
            yield return NextTurn();
            }
        }
    

    private bool CheckIfBeaten(){
        bool victory = true;
        for(int i=0; i<enemies.Count;i++){
            if(enemies[i].GetComponent<Character>().defeated==false){
                victory=false;
                break;
            }
        }
        return victory;
    }

    private bool CheckIfDefeat(){
        bool victory = true;
        for(int i=0; i<pp.battleCharacters.Count;i++){
            if(pp.battleCharacters[i].defeated==false){
                victory=false;
                break;
            }
        }
        return victory;
    }

      private IEnumerator ItemSelect(){
        dialogBox.SetItemNames(pp.items);
        dialogBox.EnableItemSelection(true);
        yield return new WaitForSeconds(0.2f);
        state = InBattleState.Item;
        

    }

    private IEnumerator PlayerMove(){
        dialogBox.EnableDialogText(false);
        dialogBox.EnableActionSelection(false);
        dialogBox.EnableMoveSelection(true);
        yield return new WaitForSeconds(0.2f);
        state = InBattleState.PlayerMove;

    }

    private List<GameObject> getAvailableNodes(){
        allyNodes=ableNodes=nodes.GetRange(1,10);
        for(int i=0;i<pp.battleCharacters.Count;i++){
            for(int j=0;j<ableNodes.Count;j++){
                if(ableNodes[j]==pp.battleCharacters[i].node)
                    ableNodes.RemoveAt(j);
            }
        }
        return allyNodes;
    }
    private void PositionPlayableCharacters(){
        for(int i=0;i<playableImages.Count;i++){
        playableImages[i].transform.position=GameObject.Find("BattleCamera").GetComponent<Camera>().WorldToScreenPoint(new Vector3(10,10,10));
        }
        for(int i=0;i<pp.battleCharacters.Count;i++){
                playableImages[i].sprite=pp.battleCharacters[i]._base.Image;
                playableImages[i].transform.position=GameObject.Find("BattleCamera").GetComponent<Camera>().WorldToScreenPoint(pp.battleCharacters[i].node.transform.position);
              
        }
    }

    private void PositionEnemyCharacters(){
       for(int i=0;i<enemyImages.Count;i++){
        enemyImages[i].transform.position=GameObject.Find("BattleCamera").GetComponent<Camera>().WorldToScreenPoint(new Vector3(10,10,10));
        }
        for(int i=0;i<enemyNumber;i++){
                enemyImages[i].sprite=enemies[i].GetComponent<Character>()._base.Image;
                enemyImages[i].transform.position=GameObject.Find("BattleCamera").GetComponent<Camera>().WorldToScreenPoint(enemies[i].GetComponent<Character>().node.transform.position);
              
        }
    }
    
    void ChangeStats(List<Text> textStats){
        textStats[0].text=pp.battleCharacters[0].name+": "+pp.battleCharacters[0].HP+" - "+pp.battleCharacters[0].Energy;
        if(pp.battleCharacters.Count()<2)
            textStats[1].text="";
        else
        {
            textStats[1].text=pp.battleCharacters[1].name+": "+pp.battleCharacters[1].HP+" - "+pp.battleCharacters[1].Energy;}
        if(pp.battleCharacters.Count()<3)
            textStats[2].text="";
        else
        {
            textStats[2].text=pp.battleCharacters[2].name+": "+pp.battleCharacters[2].HP+" - "+pp.battleCharacters[2].Energy;}

        textStats[3].text=enemies[0].GetComponent<Character>()._base.Name+": "+enemies[0].GetComponent<Character>().HP+" - "+enemies[0].GetComponent<Character>().Energy;
        if(enemies.Count()<2)
            textStats[4].text="";
        else{
            textStats[4].text=enemies[1].GetComponent<Character>()._base.Name+": "+enemies[1].GetComponent<Character>().HP+" - "+enemies[1].GetComponent<Character>().Energy;}
        
        if(enemies.Count()<3)
            textStats[5].text="";
        else{
            textStats[5].text=enemies[2].GetComponent<Character>()._base.Name+": "+enemies[2].GetComponent<Character>().HP+" - "+enemies[2].GetComponent<Character>().Energy;}

        if(enemies.Count()<4)
            textStats[6].text="";
        else{
            textStats[6].text=enemies[3].GetComponent<Character>()._base.Name+": "+enemies[3].GetComponent<Character>().HP+" - "+enemies[3].GetComponent<Character>().Energy;}
    }
    
}
