using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GameObjectSingleton<GameManager>
{
    public enum GameState
    {
        TITLE,
        SYNOPSIS,
        GUIDE,
        READY,
        RUNNING,
        END
    }

    public GameState State { get; private set; }

    [SerializeField] private UIManager ui;
    [SerializeField] private InputManager input;
    [SerializeField] private FollowCamera cam;
    [SerializeField] private FieldMover field;

    [SerializeField] private Runner runnerAya;
    [SerializeField] private Runner runnerHina;
    [SerializeField] private Transform trGoal;

    [SerializeField] private AudioClip bgm_title;
    [SerializeField] private AudioClip bgm_game;

    private bool isSound = true;

    private bool isHinaGoal = false;
    private bool isWin = false;
    private float winnerTime = 0f;
    private float loserTime = 0f;

    protected override void Awake()
    {
        Application.targetFrameRate = 60;
        base.Awake();
        Init();
        ChangeState(GameState.TITLE);
    }

    private void Init()
    {
        isHinaGoal = false;
        isWin = false;
        ui.InitUI();
        cam.Init();
        ui.onClickSkip = () => ChangeState(GameState.GUIDE);
        ui.onClickOkGuide = () => ChangeState(GameState.READY);
        input.callbackAnyKeyDown = CallbackAnyKeyDown;
        input.callbackInputA = CallbackInputA;
        input.callbackInputB = CallbackInputB;
        input.callbackSound = CallbackSound;
        field.ResetField();
    }

    private void PlayBGMTitle()
    {
        var au = cam.GetComponent<AudioSource>();
        au.clip = bgm_title;
        au.loop = true;
        au.Play();
    }

    private void PlayBGMGame()
    {
        var au = cam.GetComponent<AudioSource>();
        au.clip = bgm_game;
        au.loop = true;
        au.Play();
    }

    private void CallbackAnyKeyDown()
    {
        if (State == GameState.TITLE && ui.title.isPlaying == false)
        {
            ChangeState(GameState.SYNOPSIS);
        }
    }

    private void CallbackInputA()
    {
        if (State == GameState.RUNNING)
        {
            runnerAya.SpeedUp_A();
        }
    }

    private void CallbackInputB()
    {
        if (State == GameState.RUNNING)
        {
            runnerAya.SpeedUp_B();
        }
    }

    private void CallbackSound()
    {
        var au = cam.GetComponent<AudioSource>();
        isSound = !isSound;
        if (isSound)
        {
            au.volume = 1f;
        }
        else
        {
            au.volume = 0f;
        }
    }

    public void ChangeState(GameState _state)
    {
        State = _state;

        switch (_state)
        {
        case GameState.TITLE:
            runnerAya.GetReady();
            runnerHina.GetReady();
            PlayBGMTitle();
            ui.ShowTitle(true);
            break;
        case GameState.SYNOPSIS:
            ui.ShowTitle(false);
            ui.ShowSynopsis(true);
            break;
        case GameState.GUIDE:
            ui.ShowSynopsis(false);
            ui.ShowGuide(true);
            break;
        case GameState.READY:
            ui.ShowGuide(false);
            PlayBGMGame();
            ui.ShowReady(() =>
            {
                ChangeState(GameState.RUNNING);
                cam.isFollow = true;
            });
            break;
        case GameState.RUNNING:
            ui.ShowGameUI(true);
            runnerAya.StartRun();
            runnerHina.StartRun();
            runnerHina.GetComponent<BotRunnerController>().StartBot();
            break;
        case GameState.END:
            ui.objGagebar.SetActive(false);
            ui.countDown.gameObject.SetActive(false);
            StartCoroutine(CorGameEnd());
            break;
        }
    }

    private void Update()
    {
        if (State == GameState.RUNNING)
        {
            if (runnerAya.transform.position.x > trGoal.position.x && runnerAya.State != Runner.RunnerState.GOAL)
            {
                runnerAya.Goal();
                if (isHinaGoal)
                {
                    isWin = false;
                    loserTime = ui.countDown.GetCountDown();
                    ui.countDown.StopCountDown();
                }
                else
                {
                    isWin = true;
                    winnerTime = ui.countDown.GetCountDown();
                }
                ui.objRemainDist.SetActive(false);
            }

            if (runnerHina.transform.position.x > trGoal.position.x && runnerHina.State != Runner.RunnerState.GOAL)
            {
                isHinaGoal = true;
                runnerHina.Goal();
                if (isWin)
                {
                    loserTime = ui.countDown.GetCountDown();
                    ui.countDown.StopCountDown();
                }
                else
                {
                    winnerTime = ui.countDown.GetCountDown();
                }
            }

            if (runnerAya.State == Runner.RunnerState.GOAL && runnerHina.State == Runner.RunnerState.GOAL)
            {
                ChangeState(GameState.END);
            }
        }

    }
    private IEnumerator CorGameEnd()
    {
        yield return new WaitForSeconds(1f);
        ui.end.Open(isWin, winnerTime, loserTime);
        ui.end.onClickRestart = ResetGame;
    }

    private void ResetGame()
    {
        Init();
        ChangeState(GameState.TITLE);
    }
}
